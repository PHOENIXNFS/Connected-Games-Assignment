using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class GameLauncher : MonoBehaviourPunCallbacks
{
    public static GameLauncher gameLauncherInstance;
    
    [SerializeField] TMP_InputField roomName;
    [SerializeField] TMP_Text errorMsg;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform RoomListContent;
    [SerializeField] Transform PlayerListContent;
    [SerializeField] GameObject RoomItemListPrefab;
    [SerializeField] GameObject PlayerItemListPrefab;
    [SerializeField] GameObject StartGameButton;


    private void Awake()
    {
        gameLauncherInstance = this;
    }

    private void Start()
    {

        Debug.Log("Connecting to Server");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        GameMenuManager.gameMenuManagerInstance.Open("title menu");
        Debug.Log("Lobby Joined Succesfully");
        PhotonNetwork.NickName = "Player" + Random.Range(0, 999).ToString("000");
    }

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(roomName.text))
        {
            return;
        }

        PhotonNetwork.CreateRoom(roomName.text);
        GameMenuManager.gameMenuManagerInstance.Open("loading");
    }

    public void JoinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
        GameMenuManager.gameMenuManagerInstance.Open("loading");

    }

    public override void OnJoinedRoom()
    {
        GameMenuManager.gameMenuManagerInstance.Open("game room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(PlayerItemListPrefab, PlayerListContent).GetComponent<PlayerListItem>().PlayerSetUp(players[i]);
        }


        StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorMsg.text = "Room Failed to Create" + message;
        GameMenuManager.gameMenuManagerInstance.Open("error");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        GameMenuManager.gameMenuManagerInstance.Open("loading");
    }

    public override void OnLeftRoom()
    {
        GameMenuManager.gameMenuManagerInstance.Open("title menu");
    }

    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform a in RoomListContent)
        {
            Destroy(a.gameObject);
        }

        for (int i= 0; i<roomList.Count; i++)
        {
            Instantiate(RoomItemListPrefab, RoomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        Instantiate(PlayerItemListPrefab, PlayerListContent).GetComponent<PlayerListItem>().PlayerSetUp(newPlayer);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
}