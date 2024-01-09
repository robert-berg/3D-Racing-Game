using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SpawnPlayers : MonoBehaviour
{

    //public PUN2_GameLobby gamelobby;

    public GameObject playerPrefab;
    //public GameObject playerPrefab2;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public Vector3 playerSpawnPosition1 = new Vector3(-3, 1, 5);
    public Vector3 playerSpawnPosition2 = new Vector3(3, 1, 5);

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), -15.07465f, Random.Range(minZ, maxZ));
        //PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        //PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPosition1, Quaternion.identity);
        //PhotonNetwork.Instantiate(playerPrefab2.name, playerSpawnPosition2, Quaternion.identity);

        //gamelobby = FindAnyObjectByType<PUN2_GameLobby>();
        //if(gamelobby.joiningRoom)
        //{
        //    PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        //}

        if (PhotonNetwork.IsConnected)
        {
            // Überprüfe, ob der lokale Spieler der MasterClient ist
            //if (PhotonNetwork.IsMasterClient)
            //{
            //    // Rufe eine Methode auf, um Spieler zu instanziieren, nur wenn der MasterClient ist
            //    StartCoroutine(InstantiatePlayersAfterSceneLoaded());
            //}
            StartCoroutine(InstantiatePlayersAfterSceneLoaded());

        }
    }

    IEnumerator InstantiatePlayersAfterSceneLoaded()
    {
        // Lade die Szene asynchron
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level_01", LoadSceneMode.Additive);

        // Warte, bis die Szene vollständig geladen ist
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Instanziere Spieler, nachdem die Szene vollständig geladen ist
        //PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 1f, 0f), Quaternion.identity);
        PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPosition1, Quaternion.identity);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    //void OnGui()
    //{
    //    if (GUILayout.Button("Instantiate Players", GUILayout.Width(155)))
    //    {
    //        PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPosition1, Quaternion.identity);
    //    }
    //}
}
