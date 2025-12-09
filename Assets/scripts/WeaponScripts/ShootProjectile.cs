using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 100;

    public AudioClip shotSFX;

    public Image reticleImage;
    public Color reticleEnemyColor;
    public Color reticlePropColor;

    Color originalReticalColor;

    GameObject currentProjectilePrefab;

    void Start()
    {
        originalReticalColor = reticleImage.color;
        currentProjectilePrefab = projectilePrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(currentProjectilePrefab, transform.position + transform.forward,
                transform.rotation) as GameObject;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("Projectile Parent").transform);

            AudioSource.PlayClipAtPoint(shotSFX, transform.position);
        }
    }

    private void FixedUpdate()
    {
        ReticleEffect();
    }

    void ReticleEffect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                currentProjectilePrefab = projectilePrefab;

                reticleImage.color =
                    Color.Lerp(reticleImage.color, reticleEnemyColor, Time.deltaTime * 2);

            }
            else if (hit.collider.CompareTag("Prop"))
            {
                currentProjectilePrefab = projectilePrefab;

                reticleImage.color =
                    Color.Lerp(reticleImage.color, reticlePropColor, Time.deltaTime * 2);
            }
            else
            {
                currentProjectilePrefab = projectilePrefab;

                reticleImage.color =
                    Color.Lerp(reticleImage.color, originalReticalColor, Time.deltaTime * 2);
            }
        }
        else
        {
            reticleImage.color =
                    Color.Lerp(reticleImage.color, originalReticalColor, Time.deltaTime * 2);
        }
    }
}
