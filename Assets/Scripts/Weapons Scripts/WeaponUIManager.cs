using UnityEngine;
using UnityEngine.UI;

public class WeaponUIManager : MonoBehaviour
{
    [Header("Weapon Icon UI")]
    public Image weaponIcon;

    [Header("Weapon Sprites")]
    public Sprite axeSprite;

    public Sprite revolverSprite;

    public Sprite shotgunSprite;

    public Sprite rifleSprite;


    public void UpdateWeaponIcon(string weaponName)
    {
        switch (weaponName)
        {
            case "AXE":

                weaponIcon.sprite = axeSprite;

                break;

            case "REVOLVER":

                weaponIcon.sprite = revolverSprite;

                break;

            case "SHOTGUN":

                weaponIcon.sprite = shotgunSprite;

                break;

            case "ASSAULT RIFLE":

                weaponIcon.sprite = rifleSprite;

                break;
        }
    }
}