using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int weaponSelected = 0;


    public GameObject primary, secondary;

    void Start()
    {
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (weaponSelected != 1)
            {
                SwapWeapon(1);
                weaponSelected = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weaponSelected != 2)
            {
                SwapWeapon(2);
                weaponSelected = 2;
            }

        }
    }


    void SwapWeapon(int weaponType)
    {
        if(weaponType==1)
        {
            primary.SetActive(true);
            secondary.SetActive(false);
        }
        if(weaponType==2)
        {
            primary.SetActive(false);
            secondary.SetActive(true);
        }
    }
}
