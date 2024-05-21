using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollController : MonoBehaviour
{
    Shooot ShoootSc;
    Rigidbody rb;
 
    private void Start()
    {
        ShoootSc = GetComponent<Shooot>();
    }
    private void Update()
    {
        rb=GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("isGorunded"))
        {
            BallComptOn();
            Debug.Log("BoxCollider: Player ile çarpýþma tespit edildi.");
            ShoootSc.canLaunch = true;
            ShoootSc.lookAtLock = true;
            ShoootSc.FailShoot = false;
            ShoootSc.BasketShoot = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("isGorunded"))
        {
            Debug.Log("BoxCollider: Player ile çarpýþma Ýçinde ");
            ShoootSc.canLaunch = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("isGorunded"))
        {
            Debug.Log("BoxCollider: Player ile çarpýþma Zeminden çýktý/Zýpladý ");
            ShoootSc.canLaunch =false;
            ShoootSc.lookAtLock =false;
        }
        if (other.CompareTag("Pot"))
        {
            Debug.Log("BoxCollider: Player ile çarpýþma tespit edildi.");

        }
    }
    private void BallComptOn()
    {
        rb.GetComponent<CharacterController>().enabled = true;
        rb.GetComponent<BallController>().enabled = true;
        rb.GetComponent<Shooot>().enabled = true;

    }

}
