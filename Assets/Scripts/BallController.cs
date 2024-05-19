using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour
{
    public Transform ballMainObj;
    
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    public float speed ; // Karakterin hareket hýzý
    public float speedMoving ; // Karakterin hareket hýzý
    public float speedJump = 2f; // Karakterin hareket hýzý
    public float speedUpDown;

    public Transform ballPos;
    [SerializeField] private float groundDisRayLong, RayDis, BallGroundContactFloat;
    [SerializeField] private LayerMask checkLayers;
    public bool up, down;

    public Vector2 touchPosXY;
    private Vector2 previousTouchPos;

    void Start()
    {
        speedUpDown = -50f;
        up = false;
        down = true;
        controller = GetComponent<CharacterController>();
        Debug.Log("STartÇalýþtý");
    }

    // Update is called once per frame
    void Update()
    {
        //ballMainObj.transform.position = transform.position;
        StaticObjects.DebugText3.text = Input.touchCount.ToString();

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Ýlk dokunma

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
            {
                // Parmaðýn pozisyonunu sakla
                previousTouchPos = touch.position;
            }
        }

        if (up && !down)
        {
            CharacterUpDown(controller);
        }
        if (down && !up)
        {
            CharacterUpDown(controller);
        }
        Vector3 startPoint = ballPos.position;
        Vector3 direction = ballPos.forward;

        Debug.DrawRay(startPoint, direction * groundDisRayLong, Color.red);

        if (Physics.Raycast(startPoint, direction, out RaycastHit hit, groundDisRayLong, checkLayers))
        {
            RayDis = startPoint.y - hit.point.y;

            if (RayDis < BallGroundContactFloat)
            {
                //Time.timeScale = 0f;
                Debug.Log("Up2222");
                speedUpDown = 30f;
                down = false;
                up = true;


            }
            if (RayDis > 2f)
            {
                Debug.Log("Down2222");
                speedUpDown = -30f;
                up = false;
                down = true;
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Ýlk dokunma

            // Delta pozisyonu hesapla
            Vector2 touchDeltaPosition = (Input.touchCount > 0) ? (Input.GetTouch(0).position - previousTouchPos) : Vector2.zero;
            //Vector2 touchDeltaPosition = touch.deltaPosition;
            if (touch.phase == TouchPhase.Moved)
            {
                CharacterMove(touchDeltaPosition);
            }
        }
    }

    private void CharacterMove(Vector2 touchDeltaPosition)
    {
        moveDirection = new Vector3(touchDeltaPosition.x * speedMoving, speedUpDown, touchDeltaPosition.y * speedMoving);
        moveDirection = transform.TransformDirection(moveDirection); // Yönü düzelt

        moveDirection *= speed;
        //moveDirection =moveDirection * speed;

        // Karakterin yerçekimini uygula
        moveDirection.y -= 9.81f * Time.deltaTime;

        // Karakteri hareket ettir
        controller.Move(moveDirection * Time.deltaTime);
    }
    private void CharacterUpDown(CharacterController Cntrllr)
    {
        moveDirection = new Vector3(Cntrllr.transform.position.x, speedUpDown, Cntrllr.transform.position.y);
        StaticObjects.DebugText.text = (moveDirection.ToString());
        moveDirection = transform.TransformDirection(moveDirection); // Yönü düzelt

        moveDirection *= speed;

        //// Karakterin yerçekimini uygula
        moveDirection.y -= 9.81f * Time.deltaTime;

        //// Karakteri hareket ettir
        controller.Move(moveDirection * Time.deltaTime);
    }
}
