using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;

    public AudioSource audioSource;
    public AudioClip enemySound;
    [Range(0.0f, 1.0f)] [SerializeField] float enemyVolume = 1f;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount)
    {
        audioSource.PlayOneShot(enemySound, enemyVolume);
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Grow()
    {
        gameObject.transform.localScale += new Vector3(.01f, .01f, .01f);
        gameObject.transform.localPosition += new Vector3(0, .01f, 0);
        health += .10f;
    }

    public void Shrink()
    {
        gameObject.transform.localScale += new Vector3(-.01f, -.01f, -.01f);
        gameObject.transform.localPosition += new Vector3(0, -.01f, 0);
        health -= .05f;
    }
    public void Transport()
    {
        gameObject.transform.localPosition = new Vector3(Random.Range(-10f, 10f), 1.4899f, Random.Range(-10f, 10f));
    }
}
