using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShrinkCast : MonoBehaviour {   
    [SerializeField] private  GameObject _objSphere;
    private Vector3 _punchScale = new Vector3(6,6,6);
    private Transform _sphere;
    private bool expanding;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        _sphere = FindObjectOfType<SphereCollider>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && expanding == false) {
            _objSphere.SetActive(true);
           ExpandSphere();
        }
    }

    void OnTriggerEnter(Collider other) {
        print("hit: " + other);
        Character character = other.GetComponent<Character>();
        Objects anObject = other.GetComponent<Objects>();
        Rigidbody rbody = other.GetComponent<Rigidbody>();
        //RpcTarget rpcobject = hitObject.GetComponent<Objects>();

        if (character){
            character.ChangeSize(0.5f);
        }  
        if (anObject) {
            anObject.ChangeSize(0.5f);
            rbody.isKinematic = false;
        }
    }

    void ExpandSphere(){
        expanding = true;
        //expand the sphere
        _sphere.DOPunchScale(_punchScale, 1, 1);
        StartCoroutine("ResetSphereScale");
    }

    IEnumerator ResetSphereScale(){
        yield return new WaitForSeconds(1);
        expanding = false;
        //_objSphere.SetActive(false);
    }
}
