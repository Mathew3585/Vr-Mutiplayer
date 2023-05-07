using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public int sceneIndex;
    public int maxPlayer;
}

public class NetworkMananger : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> defaultRooms;
    public GameObject RoomUi;

    // Update is called once per frame
    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect Server....");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("We Join the lobby");
        RoomUi.SetActive(true);

    }

    public void InitializeRoom(int defaultRoomIndex)
    {
        DefaultRoom roomSetting = defaultRooms[defaultRoomIndex];

        RoomOptions roomOptions = new RoomOptions();

        PhotonNetwork.LoadLevel(roomSetting.sceneIndex);

        roomOptions.MaxPlayers = (byte)roomSetting.maxPlayer;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom(roomSetting.Name, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("JoinedRoom");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new Player joined room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
