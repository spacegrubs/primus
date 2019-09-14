using DG.Tweening;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public abstract class Gun : Weapon
{
    [Header("Gun")]
    [SerializeField] protected Transform _firePoint;
    [SerializeField] protected bool _hasBulletTime;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _range;
    [SerializeField] protected LayerMask _layerMask;

    [Header("Debugging")]
    [SerializeField] protected bool _debugRay;

    public delegate void HitObjectEvent(GameObject hitObject, Vector3 hitPoint);
    public event HitObjectEvent OnHitObject;

    protected new void Start()
    {
        base.Start();
        if(photonView.IsMine)       //? if the object this script is attached to is mine,  
            OnUsed += Fired;        //? then turn Onused the Fired() event.
    }

    protected new void Update()
    {
        base.Update();
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * _range, Color.red);
    }

    private void Fired()
    {
        if (_useEffect)
        {
            //GameObject useEffect = PhotonNetwork.Instantiate(ResourceUtils.GetPrefabResourcePath(_useEffect), _firePoint.transform.position, _firePoint.transform.rotation);
            //useEffect.transform.parent = _firePoint.transform;

            photonView.RPC("RPC_SpawnFireEffect", RpcTarget.AllViaServer);
        }

        transform.DOPunchPosition(-Vector3.forward * 0.02f, 0.2f, 1);
        photonView.RPC("RPC_PlayFireSound", RpcTarget.AllViaServer);

        if (_hasBulletTime)
        {

        }

        else
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, _range, _layerMask))
            {
                OnHitObject?.Invoke(hit.collider.gameObject, hit.point);

                GameObject hitEffect = PhotonNetwork.Instantiate(ResourceUtils.GetPrefabResourcePath(_hitEffect), hit.point, Quaternion.identity);
            }
        }
    }

    [PunRPC]
    protected void RPC_SpawnFireEffect()
    {
        Instantiate(_useEffect, _firePoint.transform);
    }

    [PunRPC] //? this declares the following as an RPC method.
    protected void RPC_PlayFireSound()
    {
        //* this actually plays the sound */
        _audioSource.PlayOneShot(_useSound, _useSoundVolume);
    }
}

