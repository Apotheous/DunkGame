using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSkinUpDown : MonoBehaviour
{
    private Transform characterTransform;
    private Vector3 moveDirection = Vector3.zero;
    public float speed = 2f; // Karakterin hareket h�z�
    public GameObject ballPos;

    void Start()
    {
        characterTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(characterTransform.name);

        if (GroundCheckRayCast.up && !GroundCheckRayCast.down)
        {
            CharacterUp();
        }
        if (GroundCheckRayCast.down && !GroundCheckRayCast.up)
        {
            CharacterDown();
        }
    }

    public void CharacterDown()
    {
        // Karakterin y�ksekli�ini yava��a artt�rarak yukar� do�ru hareket uygula
        Vector3 verticalMovement = Vector3.down * speed * Time.deltaTime;
        characterTransform.Translate(verticalMovement);
    }

    public void CharacterUp()
    {
        // Karakterin y�ksekli�ini yava��a artt�rarak yukar� do�ru hareket uygula
        Vector3 verticalMovement = Vector3.up * speed * Time.deltaTime;
        characterTransform.Translate(verticalMovement);
    }
}
