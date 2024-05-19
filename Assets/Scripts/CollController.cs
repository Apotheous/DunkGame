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
            Debug.Log("BoxCollider: Player ile �arp��ma tespit edildi.");
            ShoootSc.canLaunch = true;
            ShoootSc.lookAtLock = true;

            // Di�er i�lemler
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("isGorunded"))
        {
            Debug.Log("BoxCollider: Player ile �arp��ma ��inde ");
            ShoootSc.canLaunch = true;

            // Di�er i�lemler
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("isGorunded"))
        {
            Debug.Log("BoxCollider: Player ile �arp��ma Zeminden ��kt�/Z�plad� ");
            ShoootSc.canLaunch =false;
            ShoootSc.lookAtLock =false;
            
            // Di�er i�lemler
        }
    }

}
