using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PhotonPlayerTPCView : MonoBehaviourPun, IPunObservable
{
    private Vector3 _truePosition;
    private Quaternion _trueRotation;
    private Vector3 _trueScale;

    private Quaternion _trueChestRotation;

    private Animator _animator;
    private Aim _aim;
    private ThirdPersonUserControl _characterController;
    private Transform _chest;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<ThirdPersonUserControl>();
        _animator = GetComponent<Animator>();
        _chest = _animator.GetBoneTransform(HumanBodyBones.Chest);
        _aim = GetComponent<Aim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        { 
            // Player pos/rot extrapolation
            transform.position = Vector3.Lerp(transform.position, _truePosition, Time.deltaTime * 15) + _characterController.transform.forward * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, _trueRotation, Time.deltaTime * 15);
            transform.localScale = Vector3.Lerp(transform.localScale, _trueScale, Time.deltaTime * 15);
        }
    }
    
    void LateUpdate()
    {
        if (!photonView.IsMine && _animator.GetFloat("Forward") <= 0)
        {
            _chest.rotation = Quaternion.Lerp(_aim.ChestRotation, _trueChestRotation, Time.deltaTime * 15);
            _aim.ChestRotation = _chest.rotation;
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Player pos/rot
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.localScale);

            stream.SendNext(_aim.ChestRotation);
        }

        else
        {
            // Player pos/rot
            _truePosition = (Vector3) stream.ReceiveNext();
            _trueRotation = (Quaternion) stream.ReceiveNext();
            _trueScale = (Vector3) stream.ReceiveNext();

            _trueChestRotation = (Quaternion) stream.ReceiveNext();
        }
    }
}
