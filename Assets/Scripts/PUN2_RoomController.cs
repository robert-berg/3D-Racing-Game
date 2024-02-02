using UnityEngine;
using Photon.Pun;

public class PUN2_RoomController : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    public Camera lobbyCamera;

    public Material material_player_one;
    public Material material_player_two;
    public Material material_player_three;
    public Material material_player_four;
    public Material material_player_five;

    public float spawnDistance = 15f; // Distance between spawn positions
    [SerializeField]
    private AudioSource backgroundMusic;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
    }

    Vector3 CalculateSpawnPosition(int playerIndex)
    {
        float xPos = 114 + (playerIndex * spawnDistance);
        float yPos = 4f;
        float zPos = 289f;

        return new Vector3(xPos, yPos, zPos);
    }

    public void InstantiatePlayers()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        Vector3 spawnPosition = CalculateSpawnPosition(playerCount);
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
       
        player.GetPhotonView().RPC("ChangeMaterial", RpcTarget.AllBuffered, playerCount);
    }


    void OnGUI()
    {
        if (PhotonNetwork.CurrentRoom == null) return;

        if (GUI.Button(new Rect(5, 5, 125, 25), "Leave Room"))
        {
            PhotonNetwork.LeaveRoom();
        }

        GUI.Label(new Rect(135, 5, 200, 25), PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnLeftRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_01");
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && backgroundMusic.volume != 0)
        {
            backgroundMusic.mute = !backgroundMusic.mute;
            Debug.Log("MUTE!");
        }
    }
}
