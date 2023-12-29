using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{

    //public PUN2_GameLobby gamelobby;

    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), -15.07465f, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        //gamelobby = FindAnyObjectByType<PUN2_GameLobby>();
        //if(gamelobby.joiningRoom)
        //{
        //    PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
