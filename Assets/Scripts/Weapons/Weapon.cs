using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]

public abstract class Weapon : MonoBehaviourPun
{
    [Header("Weapon")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _useSpeed;
    [SerializeField] protected bool _automatic;
    [SerializeField] protected bool _hasAmmunition;
    [SerializeField] protected float _reloadTime;
    [SerializeField] protected int _clipAmmunition;
    [SerializeField] protected int _maxAmmunition;

    [Header("Effects")]
    [SerializeField] protected GameObject _hitEffect;
    [SerializeField] protected GameObject _useEffect;

    [Header("Sound")]
    [SerializeField] protected AudioClip _useSound;
    [SerializeField] [Range(0, 1)] protected float _useSoundVolume = 1;

    protected int _currentAmmunition;
    protected int _currentMaxAmmunition;
    protected float _cooldown;

    protected Character _owner;
    protected AudioSource _audioSource;

    public bool HasAmmunition => _hasAmmunition;
    public bool Automatic => _automatic;

    #region Events
    public delegate void Used();
    public event Used OnUsed;
    #endregion

    protected void Start(){
        _owner = FindObjectsOfType<Character>().First(c => c.Player == PhotonNetwork.LocalPlayer);
        _audioSource = _owner?.GetComponent<AudioSource>();
        _currentAmmunition = _clipAmmunition;
        _currentMaxAmmunition = _maxAmmunition - _currentAmmunition > 0 ? _maxAmmunition - _currentAmmunition : 0;
    }

    protected void Update(){
        if(_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
        }
    }

    public void Use(){
        if (!CanUse())
            return;

        _cooldown = 1 / _useSpeed;

        if (_hasAmmunition) {
            _currentAmmunition -= 1;

            if(_currentAmmunition == 0){
                Reload();
            }
        }

        OnUsed?.Invoke();
    }

    public bool CanUse()
    {
        return _cooldown <= 0 && (!_hasAmmunition || _currentAmmunition > 0);
    }

    public void Reload()
    {
        _currentAmmunition = _clipAmmunition;
    }
}

