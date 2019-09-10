using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkRay : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * rayLength, Color.blue);

        if (Input.GetButton("Fire1"))
        {
            ShootPrimary();
        }

        if (Input.GetButton("Fire2"))
        {
            ShootSecondary();
        }
    }

    void ShootPrimary()
    {
        audioSource.PlayOneShot(shootingSound, shootingVolume);

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, rayLength, layerMask))
        {
            impactRef = Instantiate(impact, hit.point, Quaternion.identity);
            Destroy(impactRef, 1f);

            if (hit.collider.gameObject.tag == "Enemy")
            {
                enemyRef = hit.transform.GetComponent<Enemy>();
                
                if(enemyRef != null)
                {
                    enemyRef.Grow();
                }
            }

            print("I hit " + hit.collider.gameObject.name);
        }
        else
        {
            print("I missed!");
        }

        //working on a way to figure out a laserbeam to appear on shoot
        //lr.SetPosition(0, firePoint.transform.position);
    }

    void ShootSecondary()
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
                    enemyRef.Shrink();
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
