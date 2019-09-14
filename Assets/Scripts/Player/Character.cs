using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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
        // The owner of the view takes care of growing and the other clients get the updated scale of the player via the photon view serialization
        photonView.RPC("RPC_ChangeSize", Player, sizeFactor);
    }

    [PunRPC]
    private void RPC_ChangeSize(float sizeFactor)
    {
        // Make sure only the owner can make these changes
        if (photonView.IsMine)
        {
            transform.DOScale(transform.localScale * sizeFactor, 1).SetEase(Ease.OutBack, sizeFactor > 1 ? 5 : 3);
            FirstPersonController fpsController = GetComponent<FirstPersonController>();
            fpsController.m_JumpSpeed *= sizeFactor;
            fpsController.m_RunSpeed *= sizeFactor;
            fpsController.m_WalkSpeed *= sizeFactor;
            fpsController.m_StickToGroundForce *= sizeFactor;
            fpsController.m_GravityMultiplier *= sizeFactor;
        }
    }
}
