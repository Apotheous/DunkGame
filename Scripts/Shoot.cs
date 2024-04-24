using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform ballObj, ballObjChild;
    public Transform sunset;
    public Transform potaPosY;
    public float TouchY;
    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 1.0f;

    // The time at which the animation started.
    private float startTime;
    public bool shooting= false;


    void Start()
    {
        // Note the time at the start of the animation.
        startTime = Time.time;  
    }
    private void OnMouseDown()
    {
        shooting = true;   
    }
    void Update()
    {

        //transform.position = ballObjChild.position;
        if (Input.touchCount > 0) // Ekrana dokunma varsa
        {
            Touch touch = Input.GetTouch(0); // �lk parmak dokunu�unu al�r

            //if (touch.phase == TouchPhase.Moved) // Dokunu� devam ediyorsa
            //{
                // Dokunman�n hareket mesafesi ve y�n�
                float magnitude = touch.deltaPosition.magnitude;
               Vector2 direction = touch.deltaPosition.normalized;
            TouchY = touch.deltaPosition.magnitude;
            if (magnitude > 50f && direction.y > 0.5f) // Uzun ve yukar� do�ru bir hareket
            //    //{
                //if (Mathf.Approximately(direction.y, 1f)) // Uzun ve yukar� do�ru bir hareket  direction.y > 0.9f
                {
                TouchY = direction.y;
                    Debug.Log("Parma��n yukar� do�ru h�zl� ve uzun bir hareketi alg�land�!");
                    // Buraya yukar� do�ru h�zl� ve uzun harekette yap�lacak i�lemler eklenebilir
            
                    // Her bir alt objenin ad�n� konsola yazd�r�r�z
                    Debug.Log("Alt obje ad�: " + ballObjChild.name);
                      
                    ballObjChild.GetComponent<CharacterController>().enabled = false;
                    ballObjChild.GetComponent<GroundCheckRayCast>().enabled = false;
                    ballObjChild.GetComponent<UPDown>().enabled = false;
                    ballObjChild.GetComponent<BallController>().enabled = false;
                    
                    //sunrise.GetComponent<CharacterController>().enabled = false;
                    shooting = true;
                    
                }
            //}
        }

        if (shooting == true)
        {
            
            // The center of the arc// Yay�n merkezi
            Vector3 center = (ballObj.position + sunset.position) * 0.5F;


            // move the center a bit downwards to make the arc vertical// yay� dikey yapmak i�in merkezi biraz a�a�� do�ru hareket ettirin
            center -= new Vector3(0, 20, 0);

            // Interpolate over the arc relative to center// Yay �zerinde merkeze g�re enterpolasyon yap�n
            Vector3 riseRelCenter = ballObj.position - center;
            Vector3 setRelCenter = sunset.position - center;

            // The fraction of the animation that has happened so far is// Animasyonun �u ana kadar ger�ekle�en k�sm�
            // equal to the elapsed time divided by the desired time for// ge�en s�renin istenen s�reye b�l�nmesine e�ittir
            // the total journey.                                       // toplam yolculuk.    
            float fracComplete = (Time.time - startTime) / journeyTime;

            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += center;

        }
        
    }
}
