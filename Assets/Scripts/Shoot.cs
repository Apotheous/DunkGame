using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Shooot : MonoBehaviour
{
    
    Rigidbody ball;
    //
    public float h = 25;
    public float gravity = -18;
    public bool debugPath;

    //
    public bool  canLaunch;
    public float launchForce; // F�rlatma kuvveti
    public float launchAngle; // F�rlatma a��s�
    public float shootForce;


    //
    [HideInInspector]
    public Transform ballObj, sunset,LookAtObj,FailShootTargetObj;

    [HideInInspector]
    public float potaPosYDist, sunsetPosDist, pCntrFloat, pCntrFloat2, pCntrFloat1, pCntrFloat3;
    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 1.0f;

    // The time at which the animation started.
    private float startTime;
    public bool BasketShoot= false, FailShoot=false, lookAtLock = true;

    void Start()
    {
        
        lookAtLock = false;
        ball = GetComponent<Rigidbody>();
        BodyTurnOff();
        // Note the time at the start of the animation.
        startTime = Time.time;

        // Ba�lang��ta CenterObject'� iki objenin tam ortas�na yerle�tir
        UpdateCenterPosition();


    }
    void Update()
    {
        // Her g�ncelleme ad�m�nda CenterObject'�n pozisyonunu g�ncelle
        UpdateCenterPosition();
        
        if (LookAtObj && sunset)// 
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

            //BallComptOff();
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

                    Launch(sunset);
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
                    //BodyTurnON();
                    Launch(FailShootTargetObj);
                    //ShootBall(LongShootPosY,45);

                }
            }
        }
    }
    #region newShootCode
    public void LaunchFonc(Transform target)
    {
        Launch(target);
    }

    void Launch(Transform target)
    {
        BallComptOff();
        ball.isKinematic = false;
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchData(target).initialVelocity;
    }

    LaunchData CalculateLaunchData(Transform target)
    {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath(Transform target)
    {
        LaunchData launchData = CalculateLaunchData(target);
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
    #endregion

    #region oldShootCode
    //launch for rigidbody with addforce
    void ShootBall(Transform target)
    {
        // Hedef pozisyonu ile topun pozisyonu aras�ndaki fark� hesapla
        Vector3 direction = target.position - transform.position;

        // Y�kseklik fark�n� hesapla
        float heightDifference = direction.y;
        direction.y = 0;
        float horizontalDistance = direction.magnitude;
        direction.y = horizontalDistance * Mathf.Tan(launchAngle * Mathf.Deg2Rad);

        // Toplam mesafeyi hesapla
        float distance = direction.magnitude;

        // F�rlatma h�z�n� hesapla
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * launchAngle * Mathf.Deg2Rad));

        // Kuvvet vekt�r�n� hesapla
        Vector3 force = velocity * direction.normalized;

        // Mevcut h�z� s�f�rla
        ball.velocity = Vector3.zero;

        // Kuvvet uygula
        ball.AddForce(force, ForceMode.VelocityChange);
    }
    #endregion

    #region withSlerpShootCode
    //Slerp ile f�rlatma kodu
    private void ShootingFonc(Transform ball, Transform target, float centerFlt, float centerFlt2, float centerFlt1, float centerFlt3)
    {
        Vector3 center = (ball.position + target.position) * centerFlt;

        // move the center a bit downwards to make the arc vertical// yay� dikey yapmak i�in merkezi biraz a�a�� do�ru hareket ettirin
        center -= new Vector3(centerFlt1, centerFlt2, centerFlt3);

        // Interpolate over the arc relative to center// Yay �zerinde merkeze g�re enterpolasyon yap�n
        Vector3 riseRelCenter = ball.position - center;
        Vector3 setRelCenter = target.position - center;

        float fracComplete = (Time.time - startTime) / journeyTime;

        ball.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        ball.position += center;
    }
    #endregion
    private void BodyTurnOff()
    {
        if (ball != null)
        {
            ball.isKinematic = true; // Rigidbody'nin dinamik fizik etkilerini devre d��� b�rak

        }
        else
        {
            Debug.Log("RigidBody bile�eni mevcut de�il.");
        }
    }  
    private void BodyTurnON()
    {
        if (ball != null)
        {
            ball.isKinematic = false; // Rigidbody'nin dinamik fizik etkilerini devre d��� b�rak

        }
        else
        {
            Debug.Log("RigidBody bile�eni mevcut de�il.");
        }
    }
    void UpdateCenterPosition()
    {
        if (BasketShoot==false && FailShoot == false)
        {
        //// Pota ve Ball objelerinin pozisyonlar�n� al
        Vector3 potaPos = sunset.transform.position;
        Vector3 ballPos = ballObj.transform.position;

        // CenterObject'� iki objenin tam ortas�na yerle�tir
        FailShootTargetObj.transform.position = (potaPos + ballPos) / 2f;
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
        // X ekseni �zerinde takip
        transform.LookAt(lookAtLock ? new Vector3(LookAtObj.position.x, transform.position.y, LookAtObj.position.z) : transform.position);

        // Z ekseni �zerinde takip
        transform.LookAt(lookAtLock ? new Vector3(LookAtObj.position.x, transform.position.y, LookAtObj.position.z) : transform.position);
    }

}
