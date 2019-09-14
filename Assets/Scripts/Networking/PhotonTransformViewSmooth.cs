using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonTransformViewSmooth : MonoBehaviourPun, IPunObservable
{
    private Vector3 _truePosition;
    private Quaternion _trueRotation;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, _truePosition, Time.deltaTime * 15);
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
