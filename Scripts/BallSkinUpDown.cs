using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSkinUpDown : MonoBehaviour
{
    private Transform characterTransform;
    private Vector3 moveDirection = Vector3.zero;
    public float speed = 2f; // Karakterin hareket hýzý
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
        // Karakterin yüksekliðini yavaþça arttýrarak yukarý doðru hareket uygula
        Vector3 verticalMovement = Vector3.down * speed * Time.deltaTime;
        characterTransform.Translate(verticalMovement);
    }

    public void CharacterUp()
    {
        // Karakterin yüksekliðini yavaþça arttýrarak yukarý doðru hareket uygula
        Vector3 verticalMovement = Vector3.up * speed * Time.deltaTime;
        characterTransform.Translate(verticalMovement);
    }
}
