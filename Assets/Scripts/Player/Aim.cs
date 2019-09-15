using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviourPun
{
    [SerializeField] private Vector3 _offset;

    //! this is never being used. 
    private Transform _target;
    private Animator _animator;
    private Transform _chest;

    public Quaternion ChestRotation { get; set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _chest = _animator.GetBoneTransform(HumanBodyBones.Chest);
    }

    private void LateUpdate()
    {
        if (photonView.IsMine && _animator.GetFloat("Speed") <= 0)
        {
            _chest.LookAt(Camera.main.transform.position + Camera.main.transform.forward);
            _chest.rotation = _chest.rotation * Quaternion.Euler(_offset);
            ChestRotation = _chest.rotation;
        }
    }
}
