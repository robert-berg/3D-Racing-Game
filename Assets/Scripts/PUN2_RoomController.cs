using UnityEngine;
using Photon.Pun;

public class PUN2_RoomController : MonoBehaviourPunCallbacks
{

    //Player instance prefab, must be located in the Resources folder
    public GameObject playerPrefab;
    //Player spawn point
    public Transform[] spawnPoints;
    public Camera lobbyCamera;

    public float spawnDistance = 15f; // Distance between Spawn-Positionen

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    //In case we started this demo with the wrong scene being active, simply load the menu scene
    //if (PhotonNetwork.CurrentRoom == null)
    //{
    //    Debug.Log("Is not in the room, returning back to Lobby");
    //    UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
    //    return;
    //}

    //We're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
    //PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, spawnPoints[Random.Range(0, spawnPoints.Length - 1)].rotation, 0);
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    Vector3 CalculateSpawnPosition(int playerIndex)
    {
        // Berechne die X-Position basierend auf dem Index
        float xPos = 113f + (playerIndex * spawnDistance);

        // Setze die Y-Position auf 1, um Spieler über dem Boden zu spawnen
        float yPos = 4f;

        // Verwende die gleiche Z-Position für alle Spieler (kann nach Bedarf angepasst werden)
        float zPos = 289f;

        return new Vector3(xPos, yPos, zPos);
    }

    public void InstantiatePlayers()
    {
        //Debug.Log("Playercount: " + PhotonNetwork.CurrentRoom.PlayerCount);
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        Vector3 spawnPosition = CalculateSpawnPosition(playerCount);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
    }

    void OnGUI()
    {
        if (PhotonNetwork.CurrentRoom == null)
            return;

        //Leave this Room
        if (GUI.Button(new Rect(5, 5, 125, 25), "Leave Room"))
        {
            PhotonNetwork.LeaveRoom();
        }

        //Show the Room name
        GUI.Label(new Rect(135, 5, 200, 25), PhotonNetwork.CurrentRoom.Name);

        //Show the list of the players connected to this Room
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //Show if this player is a Master Client. There can only be one Master Client per Room so use this to define the authoritative logic etc.)
            string isMasterClient = (PhotonNetwork.PlayerList[i].IsMasterClient ? ": MasterClient" : "");
            GUI.Label(new Rect(5, 35 + 30 * i, 200, 25), PhotonNetwork.PlayerList[i].NickName + isMasterClient);
        }
    }

    public override void OnLeftRoom()
    {
        //We have left the Room, return back to the GameLobby
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room.");
        SwitchOffLobbyCamera();
        InstantiatePlayers();
    }

    public void SwitchOffLobbyCamera()
    {
        lobbyCamera.enabled = false;
    }
}