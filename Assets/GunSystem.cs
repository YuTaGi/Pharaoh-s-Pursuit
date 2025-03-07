using UnityEngine;
using System.Collections;
using TMPro;

public class GunSystem : MonoBehaviour
{
    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 2f;
    public float damage = 10f;
    public float range = 100f;

    private Camera fpsCamera; // ไม่ต้องตั้งค่าใน Inspector
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadText;

    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        fpsCamera = FPSController.instance.playerCamera; // ดึงกล้องจาก FPSController อัตโนมัติ
        UpdateAmmoUI();
    }

    void Update()
    {
        if (isReloading) return;

        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        currentAmmo--;

        if (muzzleFlash != null)
            muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (impactEffect != null)
            {
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
        }

        UpdateAmmoUI();
    }

    IEnumerator Reload()
    {
        isReloading = true;
        reloadText.text = "Reloading...";
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        reloadText.text = "";

        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        ammoText.text = currentAmmo + " / " + maxAmmo;
    }
}
