using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooot : MonoBehaviour
{
    public Transform ballObj, ballObjChild;
    public Transform sunset;
    public Transform potaPosY2,LongShootPosY;
    public float TouchY,potaPosYDist, sunsetPosDist;
    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 1.0f;

    // The time at which the animation started.
    private float startTime;
    public bool shooting= false, shootingLong=false,lookAtLock = true;

    public GameObject CenterObjectPota;
    public GameObject CenterObjectBall;
    public GameObject CenterObject;
    void Start()
    {
        // Note the time at the start of the animation.
        startTime = Time.time;
        // Ba�lang��ta CenterObject'� iki objenin tam ortas�na yerle�tir
        UpdateCenterPosition();
    }
    void Update()
    {
        // Her g�ncelleme ad�m�nda CenterObject'�n pozisyonunu g�ncelle
        UpdateCenterPosition();

        // X ekseni �zerinde takip
        transform.LookAt(lookAtLock ? new Vector3(potaPosY2.position.x, transform.position.y, potaPosY2.position.z) : transform.position);

        // Z ekseni �zerinde takip
        transform.LookAt(lookAtLock ? new Vector3(potaPosY2.position.x, transform.position.y, potaPosY2.position.z) : transform.position);

        if (potaPosY2 && sunset)
        {
            potaPosYDist = Vector3.Distance(LongShootPosY.position, transform.position);
            sunsetPosDist = Vector3.Distance(sunset.position, transform.position);
            print("Distance to other: " + potaPosYDist);
        }
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
  
            float magnitude = touch.deltaPosition.magnitude;
            Vector2 direction = touch.deltaPosition.normalized;
            TouchY = touch.deltaPosition.magnitude;

            StaticObjects.DebugText.text = touch.deltaPosition.y.ToString();
            StaticObjects.DebugText2.text = Mathf.Abs(touch.deltaPosition.x).ToString();
           
            if (touch.deltaPosition.y > 50f && Mathf.Abs(touch.deltaPosition.x) < 3f)
            {
             if (sunsetPosDist >= 15f) { shooting = false; shootingLong = true; }
             else if (sunsetPosDist > 1f && sunsetPosDist < 15f) { shootingLong = false; shooting = true; }
            }
        }

        if (shooting == true)
        {
            lookAtLock = false;
            BallComptOff();

            ShootingFonc(ballObj, sunset);

            if (sunsetPosDist <= 0.1f)
            {
                BallComptOn();
                shooting = false;
                lookAtLock= true;
            }

        }

        if (shootingLong == true )
        {
            lookAtLock= false;
            BallComptOff();
            ShootingFonc(ballObj, LongShootPosY);

            if (potaPosYDist <= 0.1f)
            {
                BallComptOn();
                shootingLong = false;
                lookAtLock= true;
            }
        }

        if (sunsetPosDist < 1f) { BallComptOn(); }

    }

    private void ShootingFonc(Transform ball, Transform pota  )
    {
        Vector3 center = (ball.position + pota.position) * 0.2F;

        // move the center a bit downwards to make the arc vertical// yay� dikey yapmak i�in merkezi biraz a�a�� do�ru hareket ettirin
        center -= new Vector3(0, 2, 0);

        // Interpolate over the arc relative to center// Yay �zerinde merkeze g�re enterpolasyon yap�n
        Vector3 riseRelCenter = ball.position - center;
        Vector3 setRelCenter = pota.position - center;

        float fracComplete = (Time.time - startTime) / journeyTime;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
    }

    void UpdateCenterPosition()
    {
        if (shooting==false&&shootingLong == false)
        {
        // Pota ve Ball objelerinin pozisyonlar�n� al
        Vector3 potaPos = CenterObjectPota.transform.position;
        Vector3 ballPos = CenterObjectBall.transform.position;

        // CenterObject'� iki objenin tam ortas�na yerle�tir
        CenterObject.transform.position = (potaPos + ballPos) / 2f;
        }
    }
    private void BallComptOn()
    {
        ballObjChild.GetComponent<CharacterController>().enabled = true;
        ballObjChild.GetComponent<BallController>().enabled = true;
    }    
    private void BallComptOff()
    {
        ballObjChild.GetComponent<CharacterController>().enabled = false;
        ballObjChild.GetComponent<BallController>().enabled = false;
    }
}
