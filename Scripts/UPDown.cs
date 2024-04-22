using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UPDown : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    public float speed = 2f; // Karakterin hareket hýzý
    public GameObject ballPos;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(controller.name);

        if (GroundCheckRayCast.up&&!GroundCheckRayCast.down)
        {
            CharacterUp();
        }
        if (GroundCheckRayCast.down&&!GroundCheckRayCast.up)
        {
            CharacterDown();
        }



       
        
    }
    public void CharacterDown()
    {

        // Karakterin yüksekliðini yavaþça arttýrarak yukarý doðru hareket uygula
        Vector3 verticalMovement = Vector3.down * speed * Time.deltaTime;
        controller.Move(verticalMovement);
    }    public void CharacterUp()
    {

        // Karakterin yüksekliðini yavaþça arttýrarak yukarý doðru hareket uygula
        Vector3 verticalMovement = Vector3.up * speed * Time.deltaTime;
        controller.Move(verticalMovement);
    }

}
