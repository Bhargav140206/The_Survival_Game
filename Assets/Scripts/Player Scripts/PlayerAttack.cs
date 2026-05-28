using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weapon_Manager;

    private Animator zoomCameraAnim;

    private GameObject crosshair;

    private bool is_Aiming;

    void Awake()
    {
        weapon_Manager = GetComponent<WeaponManager>();

        zoomCameraAnim =
            transform.Find(Tags.LOOK_ROOT)
            .transform.Find(Tags.ZOOM_CAMERA)
            .GetComponent<Animator>();

        crosshair =
            GameObject.FindWithTag(Tags.CROSSHAIR);
    }

    void Update()
    {
        ZoomInAndOut();
    }

    // =========================
    // ORIGINAL AIM SYSTEM
    // =========================
    void ZoomInAndOut()
    {
        // GUN AIM TYPE
        if (weapon_Manager
            .GetCurrentSelectedWeapon().weapon_Aim
            == WeaponAim.AIM)
        {
            // RIGHT CLICK PRESS
            if (Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play(
                    AnimationTags.ZOOM_IN_ANIM);

                if (crosshair != null)
                {
                    crosshair.SetActive(false);
                }
            }

            // RIGHT CLICK RELEASE
            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play(
                    AnimationTags.ZOOM_OUT_ANIM);

                if (crosshair != null)
                {
                    crosshair.SetActive(true);
                }
            }
        }

        // SELF AIM WEAPONS
        if (weapon_Manager
            .GetCurrentSelectedWeapon().weapon_Aim
            == WeaponAim.SELF_AIM)
        {
            // AIM
            if (Input.GetMouseButtonDown(1))
            {
                weapon_Manager
                    .GetCurrentSelectedWeapon()
                    .Aim(true);

                is_Aiming = true;
            }

            // STOP AIM
            if (Input.GetMouseButtonUp(1))
            {
                weapon_Manager
                    .GetCurrentSelectedWeapon()
                    .Aim(false);

                is_Aiming = false;
            }
        }
    }
}