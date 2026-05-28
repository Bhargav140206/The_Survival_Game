using UnityEngine;

public class WeaponUnlockZone : MonoBehaviour
{
    public int weaponIndex;

    private bool unlocked = false;

    void OnTriggerEnter(Collider other)
    {
        if (unlocked)
            return;

        if (other.CompareTag("Player"))
        {
            WeaponManager wm =
                FindObjectOfType<WeaponManager>();

            wm.UnlockWeapon(weaponIndex);

            unlocked = true;

            Debug.Log("Weapon Unlocked!");
        }
    }
}