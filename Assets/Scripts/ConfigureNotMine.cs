using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureNotMine : MonoBehaviour
{
    [SerializeField] List<Component> _componentsToRemove;
    [SerializeField] List<GameObject> _objectsToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        PhotonView photonView = GetComponent<PhotonView>();

        if(photonView && !photonView.IsMine)
        {
            foreach (Component component in _componentsToRemove)
                DestroyImmediate(component);

            foreach (GameObject gameObject in _objectsToDestroy)
                DestroyImmediate(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
