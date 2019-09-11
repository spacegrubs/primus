using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonPlayerView : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private GameObject _gunParentObject;

    private Vector3 _truePosition;
    private Quaternion _trueRotation;
    private CharacterController _characterController;

    private Vector3 _gunTruePosition;
    private Quaternion _gunTrueRotation;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {

        }

        else
        {
            // Player pos/rot extrapolation
            transform.position = Vector3.Lerp(transform.position, _truePosition, Time.deltaTime * 15) + _characterController.velocity * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, _trueRotation, Time.deltaTime * 15);

            // Gun pos/rot extrapolation
            //_gunParentObject.transform.position = Vector3.Lerp(transform.position, _gunTruePosition, Time.deltaTime * 15);
            _gunParentObject.transform.localRotation = Quaternion.Lerp(_gunParentObject.transform.localRotation, _gunTrueRotation, Time.deltaTime * 15);
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Player pos/rot
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            // Gun pos/rot
            //stream.SendNext(_gunParentObject.transform.position);
            stream.SendNext(_gunParentObject.transform.localRotation);
        }

        else
        {
            // Player pos/rot
            _truePosition = (Vector3) stream.ReceiveNext();
            _trueRotation = (Quaternion) stream.ReceiveNext();

            // Gun pos/rot
            //_gunTruePosition = (Vector3)stream.ReceiveNext();
            _gunTrueRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
