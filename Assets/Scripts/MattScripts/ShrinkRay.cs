using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkRay : Gun
{
    [SerializeField] private float rayLength = 20f;
    [SerializeField] private GameObject impact;

    private RaycastHit hit;
    public LayerMask layerMask;

    private GameObject impactRef;
    private Enemy enemyRef;

    public Camera cam;
    public GameObject firePoint;
    public LineRenderer lr;
    public float maximumLength;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootingSound;
    [Range(0.0f, 1.0f)] [SerializeField] float shootingVolume = .4f;

    private void Start()
    {
        // Disable if not the owner
        if (!GetComponentInParent<PhotonView>().IsMine)
            enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * rayLength, Color.blue);

        if (Input.GetButtonDown("Fire1"))
        {
            ShootPrimary();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            ShootSecondary();
        }
    }

    private void ShootPrimary()
    {
        audioSource.PlayOneShot(shootingSound, shootingVolume);

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, rayLength, layerMask))
        {
            impactRef = Instantiate(impact, hit.point, Quaternion.identity);
            Destroy(impactRef, 1f);

            if (hit.collider.gameObject.tag == "Enemy")
            {
                enemyRef = hit.transform.GetComponent<Enemy>();
                
                if(enemyRef)
                {
                    enemyRef.Grow();
                }
            }
        }

        //working on a way to figure out a laserbeam to appear on shoot
        //lr.SetPosition(0, firePoint.transform.position);
    }

    private void ShootSecondary()
    {
        audioSource.PlayOneShot(shootingSound, shootingVolume);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, rayLength, layerMask))
        {
            impactRef = Instantiate(impact, hit.point, Quaternion.identity);
            Destroy(impactRef, 1f);

            if (hit.collider.gameObject.tag == "Enemy")
            {
                enemyRef = hit.transform.GetComponent<Enemy>();

                if (enemyRef)
                {
                    enemyRef.Shrink();
                }
            }
        }
    }
}
