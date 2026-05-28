using UnityEngine;
using TMPro;
using System.Collections;

public class GunSystem : MonoBehaviour
{
    [Header("Weapon Info")]
    public string weaponName = "REVOLVER";

    // AXE
    public bool isMeleeWeapon = false;

    // Rifle automatic fire
    public bool automaticWeapon = false;

    [Header("Ammo")]
    public int magazineAmmo = 6;

    public int reserveAmmo = 24;

    public int maxMagazineAmmo = 6;

    [Header("Fire Settings")]
    public float fireRate = 0.12f;

    private bool canShoot = true;

    [Header("Reload")]
    public float reloadTime = 2f;

    private bool isReloading = false;

    [Header("UI")]
    public TMP_Text ammoText;

    public TMP_Text weaponNameText;

    private WeaponHandler weaponHandler;

    void Start()
    {
        weaponHandler =
            GetComponent<WeaponHandler>();

        UpdateUI();
    }

    void OnEnable()
    {
        // update UI instantly when weapon switches
        UpdateUI();
    }

    void Update()
    {
        if (isMeleeWeapon)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (weaponHandler != null)
                {
                    weaponHandler.ShootAnimation();
                }
            }

            return;
        }

        
        if (isReloading)
            return;

       
        if (automaticWeapon)
        {
            // HOLD LEFT CLICK
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        else
        {
            // SINGLE CLICK
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        
        if (!canShoot)
            return;

        
        if (magazineAmmo <= 0)
        {
            Debug.Log("NO AMMO");

            return;
        }

        // stop rapid fire
        StartCoroutine(FireCooldown());

        // reduce ammo
        magazineAmmo--;

        // update UI
        UpdateUI();

        // animation
        if (weaponHandler != null)
        {
            weaponHandler.ShootAnimation();
        }

        Debug.Log(weaponName + " Fired");

        
        RaycastHit hit;

        if (Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hit,
            100f))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // DAMAGE
            HealthScript enemy =
                hit.transform.GetComponentInParent<HealthScript>();

            if (enemy != null)
            {
                enemy.ApplyDamage(20f);
            }
        }
    }

    IEnumerator FireCooldown()
    {
        canShoot = false;

        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }

    IEnumerator Reload()
    {
        // already full
        if (magazineAmmo >= maxMagazineAmmo)
            yield break;

        // no reserve ammo
        if (reserveAmmo <= 0)
            yield break;

        isReloading = true;

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        int neededAmmo =
            maxMagazineAmmo - magazineAmmo;

        if (reserveAmmo >= neededAmmo)
        {
            magazineAmmo += neededAmmo;

            reserveAmmo -= neededAmmo;
        }
        else
        {
            magazineAmmo += reserveAmmo;

            reserveAmmo = 0;
        }

        UpdateUI();

        isReloading = false;
    }

    void UpdateUI()
    {
       
        if (weaponNameText != null)
        {
            weaponNameText.text = weaponName;
        }

        if (isMeleeWeapon)
        {
            if (ammoText != null)
            {
                ammoText.text = "";
            }
        }
        else
        {
      
            if (ammoText != null)
            {
                ammoText.text =
                    magazineAmmo + " / " + reserveAmmo;
            }
        }

     
        WeaponUIManager uiManager =
            FindObjectOfType<WeaponUIManager>();

        if (uiManager != null)
        {
            uiManager.UpdateWeaponIcon(weaponName);
        }
    }
}