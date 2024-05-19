using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookAt : MonoBehaviour
{
    public Transform targetObject; // Hedef obje
    public float distanceFromTarget; // Hedef objeden sabit mesafe
    public float fixedHeight ; // Kameran�n sabit y�ksekli�i

    void LateUpdate()
    {
        // Kameran�n yeni pozisyonunu belirle
        Vector3 newPosition = targetObject.position - targetObject.forward * distanceFromTarget;
        newPosition.y = fixedHeight; // Kameran�n y�ksekli�ini sabit tut
        transform.position = newPosition;

        // Kameray� hedef objeye bakacak �ekilde ayarla (sadece y ekseninde)
        Vector3 lookAtPosition = targetObject.position;
        lookAtPosition.y = transform.position.y; // Kameran�n y eksenini sabit tut
        transform.LookAt(lookAtPosition);
    }


}
