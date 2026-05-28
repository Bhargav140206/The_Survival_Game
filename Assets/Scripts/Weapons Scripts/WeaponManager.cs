using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_Weapon_Index;

    // unlocked weapons
    private bool[] unlockedWeapons;

    [Header("Unlock Popup UI")]
    public TMP_Text unlockPopupText;

    void Start()
    {
        unlockedWeapons = new bool[weapons.Length];

        // disable all weapons first
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }

        // Axe unlocked first
        unlockedWeapons[0] = true;

        current_Weapon_Index = 0;

        weapons[current_Weapon_Index].gameObject.SetActive(true);

        UpdateWeaponUI();

        // hide popup at start
        if (unlockPopupText != null)
        {
            unlockPopupText.gameObject.SetActive(false);
        }

        // start unlock system
        StartCoroutine(UnlockWeaponsOverTime());
    }

    IEnumerator UnlockWeaponsOverTime()
    {
        // 1 minute -> Revolver
        yield return new WaitForSeconds(60f);

        UnlockWeapon(1);

        // 1 minute -> Shotgun
        yield return new WaitForSeconds(60f);

        UnlockWeapon(2);

        // 1 minute -> Rifle
        yield return new WaitForSeconds(60f);

        UnlockWeapon(3);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TrySwitchWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TrySwitchWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TrySwitchWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TrySwitchWeapon(3);
        }
    }

    void TrySwitchWeapon(int weaponIndex)
    {
        if (!unlockedWeapons[weaponIndex])
        {
            Debug.Log("Weapon Locked!");

            return;
        }

        TurnOnSelectedWeapon(weaponIndex);
    }

    void TurnOnSelectedWeapon(int weaponIndex)
    {
        if (current_Weapon_Index == weaponIndex)
            return;

        // disable old weapon
        weapons[current_Weapon_Index]
            .gameObject.SetActive(false);

        // enable new weapon
        weapons[weaponIndex]
            .gameObject.SetActive(true);

        current_Weapon_Index = weaponIndex;

        UpdateWeaponUI();
    }

    public void UnlockWeapon(int weaponIndex)
    {
        unlockedWeapons[weaponIndex] = true;

        Debug.Log("Unlocked Weapon: " + weaponIndex);

        TurnOnSelectedWeapon(weaponIndex);

        // popup
        StartCoroutine(
            ShowUnlockPopup(
                weapons[weaponIndex].name +
                " UNLOCKED!"
            )
        );
    }

    IEnumerator ShowUnlockPopup(string message)
    {
        if (unlockPopupText != null)
        {
            unlockPopupText.gameObject.SetActive(true);

            unlockPopupText.text = message;

            yield return new WaitForSeconds(2f);

            unlockPopupText.gameObject.SetActive(false);
        }
    }

   
    void UpdateWeaponUI()
    {
        GunSystem gunSystem =
            weapons[current_Weapon_Index]
            .GetComponent<GunSystem>();

        if (gunSystem != null)
        {
            gunSystem.SendMessage("UpdateUI");
        }
    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[current_Weapon_Index];
    }
}