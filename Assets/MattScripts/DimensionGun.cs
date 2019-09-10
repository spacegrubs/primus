using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionGun : MonoBehaviour
{
    [SerializeField] private float rayLength = 10f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private GameObject impact;

    private RaycastHit hit;
    public LayerMask layerMask;

    private GameObject impactRef;
    private Enemy enemyRef;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootingSound;
    [Range(0.0f, 1.0f)] [SerializeField] float shootingVolume = .4f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.blue);

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(shootingSound, shootingVolume);

        if(Physics.Raycast(transform.position, transform.forward, out hit, rayLength, layerMask))
        {
            impactRef = Instantiate(impact, hit.point, Quaternion.identity);
            Destroy(impactRef, 1f);

            if (hit.collider.gameObject.tag == "Big")
            {
                enemyRef = hit.transform.GetComponent<Enemy>();
                
                if(enemyRef != null)
                {
                    enemyRef.TakeDamage(damage);
                }
            }

            if (hit.collider.gameObject.tag == "Transport")
            {
                enemyRef = hit.transform.GetComponent<Enemy>();

                if (enemyRef != null)
                {
                    enemyRef.TakeDamage(damage);
                }
            }

            print("I hit " + hit.collider.gameObject.name);
        }
        else
        {
            print("I missed!");
        }
    }
}
