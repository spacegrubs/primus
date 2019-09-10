using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformViewSmooth : MonoBehaviourPun, IPunObservable
{
    private Vector3 _truePosition;
    private Quaternion _trueRotation;
    private CharacterController _characterController;
    float _lastUpdateTime;

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
            transform.position = Vector3.Lerp(transform.position, _truePosition, Time.deltaTime * 15) + _characterController.velocity * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, _trueRotation, Time.deltaTime * 15);
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }

        else
        {
            _truePosition = (Vector3) stream.ReceiveNext();
            _trueRotation = (Quaternion) stream.ReceiveNext();
        }
    }
}
