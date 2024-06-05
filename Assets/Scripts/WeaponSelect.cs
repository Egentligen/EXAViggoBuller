using UnityEngine;
using UnityEngine.UI;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;
    [SerializeField] Image[] weaponImages;
    
    int currentWeaponIndex = 0; 

    void Start()
    {
        //Deactivate all weapons at the start
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
            
        }

        foreach (Image weaponImages in weaponImages)
        {
            weaponImages.color = Color.black;
        }

        //Activate the first weapon
        if (weapons.Length > 0)
        {
            weapons[0].SetActive(true);
            weaponImages[0].color = Color.green;
        }
    }

    void Update()
    {
        //Check for weapon switch input
        for (int i = 0; i < weapons.Length + 1; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchWeapon(i); //Switch to the corresponding weapon
            }
        }
    }

    void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < weapons.Length)
        {
            //Deactivate the current weapon
            weapons[currentWeaponIndex].SetActive(false);
            weaponImages[currentWeaponIndex ].color = Color.black;

            //Activate the new weapon
            currentWeaponIndex = weaponIndex;
            weapons[currentWeaponIndex].SetActive(true);
            weaponImages[currentWeaponIndex].color = Color.green;
        }
    }
}
