using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookAt : MonoBehaviour
{
    public Transform targetObject; // Hedef obje
    public float distanceFromTarget; // Hedef objeden sabit mesafe
    public float fixedHeight ; // Kameranýn sabit yüksekliði

    void LateUpdate()
    {
        // Kameranýn yeni pozisyonunu belirle
        Vector3 newPosition = targetObject.position - targetObject.forward * distanceFromTarget;
        newPosition.y = fixedHeight; // Kameranýn yüksekliðini sabit tut
        transform.position = newPosition;

        // Kamerayý hedef objeye bakacak þekilde ayarla (sadece y ekseninde)
        Vector3 lookAtPosition = targetObject.position;
        lookAtPosition.y = transform.position.y; // Kameranýn y eksenini sabit tut
        transform.LookAt(lookAtPosition);
    }


}
