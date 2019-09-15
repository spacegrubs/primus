using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class OfflineNetwork : MonoBehaviourPunCallbacks
{
    public Transform spawnTransform;
    private Vector3 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = spawnTransform.position;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = "test";
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 20;

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

     public override void OnJoinedLobby()
    {
        var g = Guid.NewGuid();
        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 1,
            IsOpen = false,
            IsVisible = false
        };

        PhotonNetwork.CreateRoom(g.ToString(), roomOptions, TypedLobby.Default);
        print(g);
    }

     public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Player", spawnPoint, Quaternion.identity);
    }

}
