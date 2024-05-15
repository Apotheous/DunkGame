using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCamPosController : MonoBehaviour
{
    public Transform RayCamTransfom;

    // Update is called once per frame
    void Update()
    {
        RayCamTransfom.position = transform.position; 
        
    }
}
