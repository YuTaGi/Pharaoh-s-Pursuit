using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class GunSystem : MonoBehaviour
{
    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 2f;
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadText;
    public Image reloadImage;

    public AudioSource audioSource;   
    public AudioClip shootSound;      
    public AudioClip reloadSound;

    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
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

        if (audioSource != null && shootSound != null)
            audioSource.PlayOneShot(shootSound);

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
        ShowEffect(reloadImage);

        if (audioSource != null && reloadSound != null)
            audioSource.PlayOneShot(reloadSound);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        reloadText.text = "";
        UpdateAmmoUI();
    }

    void ShowEffect(Image effectImage)
    {
        StartCoroutine(ShowAndHideEffect(effectImage, reloadTime));
    }

    IEnumerator ShowAndHideEffect(Image effectImage, float duration)
    {
        effectImage.color = new Color(effectImage.color.r, effectImage.color.g, effectImage.color.b, 1);
        yield return new WaitForSeconds(duration);
        effectImage.color = new Color(effectImage.color.r, effectImage.color.g, effectImage.color.b, 0);
    }

    void UpdateAmmoUI()
    {
        ammoText.text = currentAmmo + " / " + maxAmmo;
    }
}