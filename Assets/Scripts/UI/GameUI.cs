using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text _nicknameText;
    [SerializeField] private GameObject _joinMessagePrefab;
    [SerializeField] private GameObject _joinMessageHolder;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
            _nicknameText.text = PhotonNetwork.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject message = Instantiate(_joinMessagePrefab);
        message.GetComponent<TMP_Text>().text = $"{newPlayer.NickName} joined the room";
        message.transform.parent = _joinMessageHolder.transform;
    }
}
