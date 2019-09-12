using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviourPun
{
    [SerializeField] private List<Weapon> _weapons;

    public Player Player { get; private set; }
    public Weapon CurrentWeapon { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
            Player = PhotonNetwork.LocalPlayer;

        else
            Player = photonView.Owner;

        if (_weapons.Any())
            CurrentWeapon = _weapons[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        if (CurrentWeapon.Automatic && Input.GetKey(KeyCode.Mouse0))
        {
            CurrentWeapon.Use();
        }

        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CurrentWeapon.Use();
        }
    }

    public void ChangeSize(float sizeFactor)
    {
        photonView.RPC("RPC_ChangeSize", RpcTarget.AllViaServer, sizeFactor);
    }

    [PunRPC]
    private void RPC_ChangeSize(float sizeFactor)
    {
        transform.localScale *= sizeFactor;
    }
}
