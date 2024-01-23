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

    //public float minX;
    //public float maxX;
    //public float minZ;
    //public float maxZ;

    //public Vector3 playerSpawnPosition1 = new Vector3(-3, 1, 5);
    //public Vector3 playerSpawnPosition2 = new Vector3(3, 1, 5);
    public float spawnDistance = 20f; // Distance between Spawn-Positionen
    void Start()
    {
        Debug.Log("start");
        Debug.Log("coroutine2: " + PhotonNetwork.LevelLoadingProgress);
        Debug.Log("current scene: " + SceneManager.GetActiveScene().name);
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("pre-coroutine");
            // Rufe die Coroutine auf, um Spieler zu instanziieren
            //StartCoroutine(InstantiatePlayersAfterSceneLoaded());
            InstantiatePlayers();
        }
    }

    Vector3 CalculateSpawnPosition(int playerIndex)
    {
        // Berechne die X-Position basierend auf dem Index
        float xPos = playerIndex * spawnDistance;

        // Setze die Y-Position auf 1, um Spieler über dem Boden zu spawnen
        float yPos = 1f;

        // Verwende die gleiche Z-Position für alle Spieler (kann nach Bedarf angepasst werden)
        float zPos = 5f;

        return new Vector3(xPos, yPos, zPos);
    }

    public void InstantiatePlayers()
    {
        Debug.Log("Playercount: " + PhotonNetwork.CurrentRoom.PlayerCount);
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        Vector3 spawnPosition = CalculateSpawnPosition(playerCount);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
    }

    IEnumerator InstantiatePlayersAfterSceneLoaded()
    {
        Debug.Log("coroutine");
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level_01", LoadSceneMode.Additive);
        //PhotonNetwork.LoadLevel("Level_01");

        // Wait for a short period before checking the loading progress
        yield return new WaitForSeconds(0.1f);

        Debug.Log("coroutine1");
        while (!PhotonNetwork.LevelLoadingProgress.Equals(1f))
        {
            Debug.Log("coroutine2: " + PhotonNetwork.LevelLoadingProgress);
            Debug.Log("current scene: " + SceneManager.GetActiveScene().name);
            yield return null;
        }
        Debug.Log("coroutine3");
        // Finde das "SpawnPlayers"-Skript in der geladenen Szene und rufe die Methode auf
        Debug.Log("Playercount: " + PhotonNetwork.CurrentRoom.PlayerCount);
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        Vector3 spawnPosition = CalculateSpawnPosition(playerCount);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    //Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), -15.07465f, Random.Range(minZ, maxZ));
    //    //PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

    //    //
    //    //PhotonNetwork.Instantiate(playerPrefab2.name, playerSpawnPosition2, Quaternion.identity);

    //    //gamelobby = FindAnyObjectByType<PUN2_GameLobby>();
    //    //if(gamelobby.joiningRoom)
    //    //{
    //    //    PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    //    //}

    //    //if (PhotonNetwork.IsConnected)
    //    //{
    //    //    // Überprüfe, ob der lokale Spieler der MasterClient ist
    //    //    //if (PhotonNetwork.IsMasterClient)
    //    //    //{
    //    //    //    // Rufe eine Methode auf, um Spieler zu instanziieren, nur wenn der MasterClient ist
    //    //    //    StartCoroutine(InstantiatePlayersAfterSceneLoaded());
    //    //    //}
    //    //    StartCoroutine(InstantiatePlayersAfterSceneLoaded());

    //    //}
    //}

    //IEnumerator InstantiatePlayersAfterSceneLoaded()
    //{
    //    // Lade die Szene asynchron
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level_01", LoadSceneMode.Additive);

    //    // Warte, bis die Szene vollständig geladen ist
    //    while (!asyncLoad.isDone)
    //    {
    //        yield return null;
    //    }

    //    // Instanziere Spieler, nachdem die Szene vollständig geladen ist
    //    //PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 1f, 0f), Quaternion.identity);
    //    PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPosition1, Quaternion.identity);
    //}

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
