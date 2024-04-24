using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour
{
    public Transform balMainObj;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    public float speed = 2f; // Karakterin hareket hýzý
    public float speedUpDown; 


    void Start()
    {
        controller = GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(controller.name);

        //balMainObj.position = transform.position;

        // Dokunmatik ekran giriþlerini kullanarak karakteri hareket ettir
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Ýlk dokunma

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = touch.deltaPosition;
                moveDirection = new Vector3(touchDeltaPosition.x, speedUpDown, touchDeltaPosition.y);
                moveDirection = transform.TransformDirection(moveDirection); // Yönü düzelt

                moveDirection *= speed;

                // Karakterin yerçekimini uygula
                moveDirection.y -= 9.81f * Time.deltaTime;

                // Karakteri hareket ettir
                controller.Move(moveDirection * Time.deltaTime);
            }
        }





    }


}
