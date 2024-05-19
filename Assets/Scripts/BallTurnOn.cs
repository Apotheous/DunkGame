using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTurnOn : MonoBehaviour
{
    public Rigidbody ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnCollisionExit()
    {
        Debug.Log("Shooterer1");
        ball.GetComponent<CharacterController>().enabled = true;
        ball.GetComponent<BallController>().enabled = true;
        //ball.GetComponent<MeshCollider>().enabled = true;
        Debug.Log("111Shooterer1");

        ball.GetComponent<Shooot>().enabled = true;
        Debug.Log("Shooterer2221");
    }

}
