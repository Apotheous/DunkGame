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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name== "BallSkin")
        {
            Debug.Log("Succes Basket1");
            ball.GetComponent<CharacterController>().enabled = true;
            ball.GetComponent<BallController>().enabled = true;
            //ball.GetComponent<MeshCollider>().enabled = true;
            Debug.Log("Succes Basket1");

            ball.GetComponent<Shooot>().enabled = true;
            Debug.Log("Succes Basket1");
        }
        //if (other.gameObject.name== "BallSkin")
        //{
        //    Debug.Log("Succes Basket1");
        //    ball.GetComponent<CharacterController>().enabled = true;
        //    ball.GetComponent<BallController>().enabled = true;
        //    //ball.GetComponent<MeshCollider>().enabled = true;
        //    Debug.Log("Succes Basket1");

        //    ball.GetComponent<Shooot>().enabled = true;
        //    Debug.Log("Succes Basket1");
        //}

    }

}
