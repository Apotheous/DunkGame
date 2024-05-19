using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Shooot : MonoBehaviour
{

    public Rigidbody ball;
 

    public float h = 25;
    public float gravity = -18;

    public bool debugPath;

    //

    public bool  canLaunch;
    public float launchForce; // Fýrlatma kuvveti
    public float launchAngle; // Fýrlatma açýsý
    public float shootForce;
    Rigidbody body;


    public Transform ballObj;
    public Transform sunset;
    public Transform LookAtObj,FailShootTargetObj;
    public float potaPosYDist, sunsetPosDist, pCntrFloat, pCntrFloat2, pCntrFloat1, pCntrFloat3;
    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 1.0f;

    // The time at which the animation started.
    private float startTime;
    public bool BasketShoot= false, FailShoot=false, lookAtLock = true;

    public GameObject CenterObjectPota;
    public GameObject CenterObjectBall;
    public GameObject CenterObject;
    public GameObject BallColl;
    void Start()
    {
        lookAtLock = false;

        // Note the time at the start of the animation.
        startTime = Time.time;

        // Baþlangýçta CenterObject'ý iki objenin tam ortasýna yerleþtir
        UpdateCenterPosition();

        body= GetComponent<Rigidbody>();
        BodyTurnOff();
        

    }
    void Update()
    {
        // Her güncelleme adýmýnda CenterObject'ýn pozisyonunu güncelle
        UpdateCenterPosition();
        
        if (sunset)//LookAtObj && 
        {
            potaPosYDist = Vector3.Distance(FailShootTargetObj.position, transform.position);
            sunsetPosDist = Vector3.Distance(sunset.position, transform.position); 
        }
        LookAtFonc();

        if (BasketShoot == true)
        {

            //BallComptOff();
            //BodyTurnOff();
            
            //ShootingFonc(ballObj, sunset, pCntrFloat, pCntrFloat2, pCntrFloat1, pCntrFloat3);

            if (sunsetPosDist <= 0.1f)
            {
                BallComptOn();
                BasketShoot = false;
                //*lookAtLock = true;
                BodyTurnOff();
                //canLaunch = true;
            }

        }

        if (FailShoot == true)
        {

            BallComptOff();
            //ShootingFonc(ballObj, LongShootPosY,pCntrFloat, pCntrFloat2, pCntrFloat1, pCntrFloat3);
            //BodyTurnON();
            //ShootBall(FailShootTargetObj);
            


            if (potaPosYDist <= 0.1f)
            {
                BallComptOn();
                FailShoot = false;
                //*lookAtLock = true;
                BodyTurnOff();

            }
        }

        if (sunsetPosDist < 1f) { BallComptOn(); }

        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            #region BallTouch
            //Ray ray = Camera.main.ScreenPointToRay(touch.position);
            //if (Physics.Raycast(ray, out RaycastHit hit))
            //{
            //    if (hit.collider != null && hit.collider.gameObject == gameObject)
            //    {
            //        // Parmak objeye dokundu
            //        OnTouchBall();
            //    }
            //}
            #endregion

            if (touch.deltaPosition.y > 50f && Mathf.Abs(touch.deltaPosition.x) < 3f&& canLaunch)
            {
                if (sunsetPosDist > 1f && sunsetPosDist < 15f)
                {
                    BasketShoot = true;
                    FailShoot = false;
                    //canLaunch = false;
                    BallComptOff();

                    Launch();
                    //ballLauncher.LaunchFonc();
                    //BodyTurnON();
                    //ShootBall(sunset , 30);

                }
                if (sunsetPosDist >= 15f) 
                {
                    BasketShoot = false; 
                    FailShoot = true;
                    //canLaunch = false;
                    BallComptOff();
                    BodyTurnON();

                    //ShootBall(LongShootPosY,45);

                }
            }
        }
              



    }
    public void LaunchFonc()
    {
        Launch();
    }
    void Launch()
    {
        BallComptOff();
        ball.isKinematic = false;
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchData().initialVelocity;
    }

    LaunchData CalculateLaunchData()
    {

        float displacementY = sunset.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(sunset.position.x - ball.position.x, 0, sunset.position.z - ball.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = ball.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ball.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }


    //launch for rigidbody with addforce
    void ShootBall(Transform target)
    {
        // Hedef pozisyonu ile topun pozisyonu arasýndaki farký hesapla
        Vector3 direction = target.position - transform.position;

        // Yükseklik farkýný hesapla
        float heightDifference = direction.y;
        direction.y = 0;
        float horizontalDistance = direction.magnitude;
        direction.y = horizontalDistance * Mathf.Tan(launchAngle * Mathf.Deg2Rad);

        // Toplam mesafeyi hesapla
        float distance = direction.magnitude;

        // Fýrlatma hýzýný hesapla
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * launchAngle * Mathf.Deg2Rad));

        // Kuvvet vektörünü hesapla
        Vector3 force = velocity * direction.normalized;

        // Mevcut hýzý sýfýrla
        body.velocity = Vector3.zero;

        // Kuvvet uygula
        body.AddForce(force, ForceMode.VelocityChange);
    }

    private void BodyTurnOff()
    {
        if (body != null)
        {
            body.isKinematic = true; // Rigidbody'nin dinamik fizik etkilerini devre dýþý býrak

        }
        else
        {
            Debug.Log("RigidBody bileþeni mevcut deðil.");
        }
    }  
    private void BodyTurnON()
    {
        if (body != null)
        {
            body.isKinematic = false; // Rigidbody'nin dinamik fizik etkilerini devre dýþý býrak

        }
        else
        {
            Debug.Log("RigidBody bileþeni mevcut deðil.");
        }
    }
    void OnTouchBall()
    {
        // Objenin dokunulduðunda çalýþacak kodu buraya yaz
        Debug.Log("Objeye dokunuldu!");
        StaticObjects.DebugText3.text = "Ball Touched";
        
        if (sunsetPosDist > 1f && sunsetPosDist < 15f) { FailShoot = false; BasketShoot = true; }
        if (sunsetPosDist >= 15f) { BasketShoot = false; FailShoot = true; }
    }

    //Slerp ile fýrlatma kodu
    private void ShootingFonc(Transform ball, Transform target ,float centerFlt ,float centerFlt2, float centerFlt1, float centerFlt3)
    {
        Vector3 center = (ball.position + target.position) * centerFlt;

        // move the center a bit downwards to make the arc vertical// yayý dikey yapmak için merkezi biraz aþaðý doðru hareket ettirin
        center -= new Vector3(centerFlt1, centerFlt2, centerFlt3);

        // Interpolate over the arc relative to center// Yay üzerinde merkeze göre enterpolasyon yapýn
        Vector3 riseRelCenter = ball.position - center;
        Vector3 setRelCenter = target.position - center;

        float fracComplete = (Time.time - startTime) / journeyTime;

        ball.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        ball.position += center;
    }




    void UpdateCenterPosition()
    {
        if (BasketShoot==false && FailShoot == false)
        {
        // Pota ve Ball objelerinin pozisyonlarýný al
        Vector3 potaPos = CenterObjectPota.transform.position;
        Vector3 ballPos = CenterObjectBall.transform.position;

        // CenterObject'ý iki objenin tam ortasýna yerleþtir
        CenterObject.transform.position = (potaPos + ballPos) / 2f;
        }
    }
    private void BallComptOn()
    {
        ballObj.GetComponent<CharacterController>().enabled = true;
        ballObj.GetComponent<BallController>().enabled = true;
        //ballObjChild.GetComponent<Shooot>().enabled = true;
    }    
    private void BallComptOff()
    {
        ballObj.GetComponent<CharacterController>().enabled = false;
        ballObj.GetComponent<BallController>().enabled = false;

    }
    private void LookAtFonc()
    {
        // X ekseni üzerinde takip
        transform.LookAt(lookAtLock ? new Vector3(LookAtObj.position.x, transform.position.y, LookAtObj.position.z) : transform.position);

        // Z ekseni üzerinde takip
        transform.LookAt(lookAtLock ? new Vector3(LookAtObj.position.x, transform.position.y, LookAtObj.position.z) : transform.position);
    }

}
