using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public int selectedWeapon;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) 
        {
            selectedWeapon = 1;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            selectedWeapon = 2;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            selectedWeapon = 3;
        }
    }
}
