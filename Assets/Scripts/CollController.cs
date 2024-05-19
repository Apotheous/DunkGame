using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollController : MonoBehaviour
{
    Shooot ShoootSc;
    BallController BallController;

    private void Start()
    {
        ShoootSc = GetComponent<Shooot>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("isGorunded"))
        {
            Debug.Log("BoxCollider: Player ile çarpýþma tespit edildi.");
            ShoootSc.canLaunch = true;
            ShoootSc.lookAtLock = true;

            // Diðer iþlemler
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("isGorunded"))
        {
            Debug.Log("BoxCollider: Player ile çarpýþma Ýçinde ");
            ShoootSc.canLaunch = true;

            // Diðer iþlemler
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("isGorunded"))
        {
            Debug.Log("BoxCollider: Player ile çarpýþma Zeminden çýktý/Zýpladý ");
            ShoootSc.canLaunch =false;
            ShoootSc.lookAtLock =false;
            
            // Diðer iþlemler
        }
    }

}
