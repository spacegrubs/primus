using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeGun : Gun
{
    private bool growMode;

    private new void Start()
    {
        base.Start();
        OnHitObject += OnHit;
        growMode = true;
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Q))
            growMode = !growMode;
    }

    private void OnHit(GameObject hitObject, Vector3 hitPoint)
    {
        Character character = hitObject.GetComponent<Character>();
        if (character)
        {
            character.ChangeSize(growMode ? 1.5f : 0.5f);
        }      
    }
}
