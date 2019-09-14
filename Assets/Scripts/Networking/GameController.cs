using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class GameController : MonoBehaviourPun
{
    public Vector3 spawnPoint;
    public Transform spawnTransform;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = spawnTransform.position;
        PhotonNetwork.IsMessageQueueRunning = true;
        PhotonNetwork.Instantiate("Player", new Vector3(PhotonNetwork.CurrentRoom.PlayerCount * spawnPoint.x,spawnPoint.y,spawnPoint.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
