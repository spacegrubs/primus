using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : MonoBehaviour
{
    [SerializeField] private float rayLength = 20f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private GameObject impact;

    private RaycastHit hit;
    public LayerMask layerMask;

    private GameObject impactRef;
    private Enemy enemyRef;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootingSound;
    [Range(0.0f, 1.0f)] [SerializeField] float shootingVolume = .4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * rayLength, Color.blue);

        if (Input.GetButtonDown("Fire1"))
        {
            ShootPrimary();
        }
    }

    void ShootPrimary()
    {
        audioSource.PlayOneShot(shootingSound, shootingVolume);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, rayLength, layerMask))
        {
            impactRef = Instantiate(impact, hit.point, Quaternion.identity);
            Destroy(impactRef, 1f);

            if (hit.collider.gameObject.tag == "Enemy")
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
