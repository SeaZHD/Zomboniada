using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class RifleScript : MonoBehaviour
{
    public float damage = 15f;
    public float range = 75f;
    public float currentAmmo = 30f;
    public float maxAmmo = 30f;
    float fireRate = 6f;
    float lastFired;


    bool outOfAmmo;
    bool isReloading;
    

    public AudioSource shootAudioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public ParticleSystem muzzleFlash;
    public Animator anim;
    public Camera playerCamera;
    public GameObject impactEffect;
    public Player player;


    void Update()
    {

        RaycastHit hit;

        if (currentAmmo <= 0)
            outOfAmmo = true;
        else
            outOfAmmo = false;


        if (Input.GetKey(KeyCode.R))
        {
            shootAudioSource.clip = reloadSound;
            shootAudioSource.Play();
            anim.Play("Reload");
            currentAmmo = maxAmmo;
        }


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
            isReloading = true;
        else
            isReloading = false;

        if (Input.GetButton("Fire1") && !player.isRunning && !outOfAmmo && !isReloading)
        {
            if (Time.time - lastFired > 1 / fireRate)
            {
                lastFired = Time.time;
                muzzleFlash.Play();
                shootAudioSource.clip = shootSound;
                shootAudioSource.Play();
                anim.Play("Fire", 0, 0f);

                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
                {

                    HealthStatus target = hit.transform.GetComponent<HealthStatus>();
                    if (target != null && hit.transform.name != this.transform.name)
                    {
                        target.TakeDamage(damage);
                    }
                    GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGO, 3f);

                }

                currentAmmo -= 1;
            }
        }
    }

    float CalculateAmmo()
    {
        return currentAmmo / maxAmmo;
    }

}
