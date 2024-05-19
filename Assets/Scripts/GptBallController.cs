using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GptBallController : MonoBehaviour
{

    public Transform ballMainObj;
    public Rigidbody ballRigidbody;
    public float shootForce = 10f; // Atýþ kuvveti
    public float speed; // Karakterin hareket hýzý
    public float speedMoving; // Karakterin hareket hýzý
    public float speedJump = 2f; // Karakterin hareket hýzý
    public float speedUpDown;

    public Transform ballPos;
    [SerializeField] private float groundDisRayLong, RayDis, BallGroundContactFloat;
    [SerializeField] private LayerMask checkLayers;
    public bool up, down, moving;

    private Vector2 previousTouchPos;

    void Start()
    {
        speedUpDown = -50f;
        up = false;
        down = true;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Ýlk dokunma

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
            {
                // Parmaðýn pozisyonunu sakla
                previousTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 touchEndPos = touch.position;
                Vector2 touchDelta = touchEndPos - previousTouchPos;

                if (Mathf.Abs(touchDelta.x) < Mathf.Abs(touchDelta.y) && touchDelta.y > 0)
                {
                    ShootBall(touchDelta);
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = touch.position - previousTouchPos;
                MoveCharacter(touchDeltaPosition);
            }
        }
    }

    void MoveCharacter(Vector2 touchDeltaPosition)
    {
        Vector3 moveDirection = new Vector3(touchDeltaPosition.x * speedMoving, speedUpDown, touchDeltaPosition.y * speedMoving);
        moveDirection = transform.TransformDirection(moveDirection); // Yönü düzelt

        moveDirection *= speed;

        // Karakterin yerçekimini uygula
        moveDirection.y -= 9.81f * Time.deltaTime;

        // Karakteri hareket ettir
        GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime);
    }

    void ShootBall(Vector2 touchDelta)
    {
        // Topa kuvvet uygula
        Vector3 shootDirection = new Vector3(touchDelta.x, touchDelta.y, touchDelta.y).normalized;
        ballRigidbody.AddForce(shootDirection * shootForce, ForceMode.Impulse);
    }
}
