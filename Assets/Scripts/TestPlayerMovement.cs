using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    //CharacterController ch;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        moveSpeed = 6;
        //ch = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontalMovement, 0f, verticalMovement);
            transform.Translate(direction * (moveSpeed * Time.deltaTime));
            //ch.Move(new Vector3(horizontalMovement, 0f, verticalMovement));
        }
    }
}
