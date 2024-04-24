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
            Touch touch = Input.GetTouch(0); // Ýlk parmak dokunuþunu alýr

            //if (touch.phase == TouchPhase.Moved) // Dokunuþ devam ediyorsa
            //{
                // Dokunmanýn hareket mesafesi ve yönü
                float magnitude = touch.deltaPosition.magnitude;
               Vector2 direction = touch.deltaPosition.normalized;
            TouchY = touch.deltaPosition.magnitude;
            if (magnitude > 50f && direction.y > 0.5f) // Uzun ve yukarý doðru bir hareket
            //    //{
                //if (Mathf.Approximately(direction.y, 1f)) // Uzun ve yukarý doðru bir hareket  direction.y > 0.9f
                {
                TouchY = direction.y;
                    Debug.Log("Parmaðýn yukarý doðru hýzlý ve uzun bir hareketi algýlandý!");
                    // Buraya yukarý doðru hýzlý ve uzun harekette yapýlacak iþlemler eklenebilir
            
                    // Her bir alt objenin adýný konsola yazdýrýrýz
                    Debug.Log("Alt obje adý: " + ballObjChild.name);
                      
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
            
            // The center of the arc// Yayýn merkezi
            Vector3 center = (ballObj.position + sunset.position) * 0.5F;


            // move the center a bit downwards to make the arc vertical// yayý dikey yapmak için merkezi biraz aþaðý doðru hareket ettirin
            center -= new Vector3(0, 20, 0);

            // Interpolate over the arc relative to center// Yay üzerinde merkeze göre enterpolasyon yapýn
            Vector3 riseRelCenter = ballObj.position - center;
            Vector3 setRelCenter = sunset.position - center;

            // The fraction of the animation that has happened so far is// Animasyonun þu ana kadar gerçekleþen kýsmý
            // equal to the elapsed time divided by the desired time for// geçen sürenin istenen süreye bölünmesine eþittir
            // the total journey.                                       // toplam yolculuk.    
            float fracComplete = (Time.time - startTime) / journeyTime;

            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += center;

        }
        
    }
}
