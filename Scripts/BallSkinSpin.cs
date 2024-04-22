using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSkinSpin : MonoBehaviour
{
 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(this.transform.position, new Vector3(0f, 5f,5f), 90f * Time.deltaTime);
    }
}
