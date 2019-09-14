using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(PhotonTransformView))]
[RequireComponent(typeof(BoxCollider))]
public class Objects : MonoBehaviourPun
{
   
    public RpcTarget RPCObject {get; private set;}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      

    }

    public void ChangeSize(float sizeFactor)
    {
        // The owner of the view takes care of growing and the other clients get the updated scale of the player via the photon view serialization
        photonView.RPC("RPC_ChangeSize", RPCObject, sizeFactor);
    }

    [PunRPC]
    private void RPC_ChangeSize(float sizeFactor)
    {
        transform.DOScale(transform.localScale * sizeFactor, 1).SetEase(Ease.OutBack, sizeFactor > 1 ? 5 : 3);   
    }
}
