using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UPDown : MonoBehaviour
{
   
    [SerializeField] private Transform groundPos;
    [SerializeField] private float groundDisRayLong, ballDisYAnti, DistanceY, RayDistanceY, BallGroundContactFloat;
    
    [SerializeField] private LayerMask checkLayers;


    public Transform ballMainObj;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    public float speed ; // Karakterin hareket hýzý
    public Transform ballPos,ballPos2;
    public bool up, down;
    
    void Start()
    {
        up= false;
        down= true;
        ballDisYAnti = RayDistanceY;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(controller.name);

        if (up && !down)
        {
            CharacterUp();
        }
        if (down && !up)
        {
            CharacterDown();
        }

        Vector3 startPoint = ballPos.position;
        Vector3 direction = ballPos.forward;

        Debug.DrawRay(startPoint, direction * groundDisRayLong, Color.red);

        if (Physics.Raycast(startPoint, direction, out RaycastHit hit, groundDisRayLong, checkLayers))
        {
            float RayDis = startPoint.y - hit.point.y;
            RayDistanceY = RayDis;
            ballDisYAnti = RayDis - 2f;
            ballDisYAnti = Mathf.Abs(ballDisYAnti);

            if (RayDis < BallGroundContactFloat)
            {

                //Time.timeScale = 0f;
                Debug.Log("Up2222");
                down = false;
                up = true;
            }
            if (RayDis > 2f)
            {
                Debug.Log("Down2222");
                up = false;
                down = true;
            }
        }
        if (hit.transform != null) { Debug.Log(hit.transform.name); }
    }
    public void CharacterDown()
    {


        //transform.Translate(Vector3.down * Time.deltaTime * speed);
        // Karakterin yüksekliðini yavaþça arttýrarak yukarý doðru hareket uygula
        Vector3 verticalMovement = Vector3.down * speed * Time.deltaTime;
        controller.Move(verticalMovement);
        ballMainObj.transform.position = transform.position;

    }    
    public void CharacterUp()
    {

        //transform.Translate(Vector3.up * Time.deltaTime * speed);
        // Karakterin yüksekliðini yavaþça arttýrarak yukarý doðru hareket uygula
        Vector3 verticalMovement = Vector3.up * speed * Time.deltaTime;
        controller.Move(verticalMovement);
        ballMainObj.transform.position = transform.position;
    }

}
