using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFloatingName : MonoBehaviour
{
    [SerializeField] Character _character;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().text = _character.Player.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
