using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheckRayCast : MonoBehaviour
{
   
    public static float ballDisYAnti;
    public Transform potaPosY,potaPosZero;
    [SerializeField] private Transform groundPos,ballPos;
    [SerializeField] private float groundDisRayLong,DistanceY,RayDistanceY,BallGroundContactFloat;
    [SerializeField] public static Vector3 ballPosStatic ;
    [SerializeField] private LayerMask checkLayers;
    public static bool up, down;
    public bool up2, down2;

    public static Transform groundCheckObj;
    public static float ballHeight;
    

    private void Start()
    {
        
        ballDisYAnti = RayDistanceY;
    }
    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider ballMeshColl)
    {
        
        Debug.Log("Çarpýþma algýlandý: " + ballMeshColl.gameObject.name);
    }
    //void OnCollisionEnter(Collision ballMeshColl)
    //{
    //    Debug.Log("Çarpýþma algýlandý: " + ballMeshColl.gameObject.name);
    //}


    private void Update()
    {
        


        up2 = up;
        down2 = down;   

        Vector3 startPoint = ballPos.position;
        Vector3 direction = ballPos.forward;

        


        Debug.DrawRay(startPoint, direction * groundDisRayLong, Color.red);

        if (Physics.Raycast(startPoint, direction, out RaycastHit hit, groundDisRayLong, checkLayers))
        {
            float RayDis = startPoint.y - hit.point.y;
            RayDistanceY = RayDis;
            ballDisYAnti = RayDis-2f;
            ballDisYAnti = Mathf.Abs(ballDisYAnti);
            //print("AntiY" + ballDisYAnti);
            potaPosY.gameObject.transform.position = new Vector3(potaPosY.position.x, potaPosZero.position.y + GroundCheckRayCast.ballDisYAnti, potaPosY.position.z);

            if (RayDis < BallGroundContactFloat) {
                
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
        if (hit.transform!=null) { Debug.Log(hit.transform.name); }
        
    }
}
