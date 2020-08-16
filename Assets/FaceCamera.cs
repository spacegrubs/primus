using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//! 0 references
public class FaceCamera : MonoBehaviour
{
    // private Gun _gunRef;  //? unused test ref
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
