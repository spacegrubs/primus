using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LoginMenu : MonoBehaviourPunCallbacks
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text _connectionStatusText;
    [SerializeField] private TMP_InputField _nicknameInput;
    [SerializeField] private Button _connectButton;

    // Start is called before the first frame update
    void Start()
    {
        _connectionStatusText.gameObject.SetActive(false);
        _nicknameInput.Select();
        _connectButton.onClick.AddListener(ConnectToMasterServer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConnectToMasterServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = 
            string.IsNullOrEmpty(_nicknameInput.text) ? $"Primus{Random.Range(1000, 9999)}" : _nicknameInput.text;

        _connectionStatusText.gameObject.SetActive(true);
        _connectionStatusText.text = "Connecting...";
        _connectionStatusText.color = Color.yellow;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _connectionStatusText.text = "Connection Failed";
        _connectionStatusText.color = Color.red;
    }

    public override void OnJoinedLobby()
    {
        _connectionStatusText.text = "Joining Test Room...";
        _connectionStatusText.color = Color.yellow;

        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 8,
            IsOpen = true,
            IsVisible = false
        };

        PhotonNetwork.JoinOrCreateRoom("_TEST_", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.IsMessageQueueRunning = false; // Set to false to only received buffered calls after we finish loading the game scene
        SceneManager.LoadScene("TestRoom");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _connectionStatusText.text = message;
        _connectionStatusText.color = Color.red;

        if(PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
    }
}
