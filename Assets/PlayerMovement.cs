using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    CharacterController ch;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 10;
        ch = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        ch.Move(new Vector3(horizontalMovement, 0, verticalMovement));
    }
}
