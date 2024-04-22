using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamPos : MonoBehaviour
{
    //Bu kodun optimizasyon sorunu var...
    public Transform rayCamTransform,potaPos,ballMainPos; // Hedef Transform
    public float posZ,posX,posY,newFloat;
    void Update()
    {
        newFloat = ballMainPos.transform.position.x / 2;

        Vector3 newPosition = rayCamTransform.position; 
       
        newPosition.x = (ballMainPos.localPosition.x) - (posX);
        newPosition.z = (ballMainPos.position.z) - posZ;
        newPosition.y = posY;
        rayCamTransform.position = newPosition; 
        rayCamTransform.transform.LookAt(potaPos.transform);
        //posX = rayCamTransform.position.x / 2f;
          
    }
}
