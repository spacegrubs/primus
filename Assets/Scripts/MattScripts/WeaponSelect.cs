using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    public int currentWeapon;
    public Transform[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            changeWeapon(2);
        }
    }

    public void changeWeapon(int num)
    {
        currentWeapon = num;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == num)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }
}
