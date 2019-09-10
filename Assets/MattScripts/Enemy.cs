using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;

    public AudioSource audioSource;
    public AudioClip enemySound;
    [Range(0.0f, 1.0f)] [SerializeField] float enemyVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        if (gameObject.tag == "Big")
        {
            gameObject.transform.localScale += new Vector3(1, 1, 1);
            gameObject.transform.localPosition += new Vector3(0, 1, 0);
        }

        if(gameObject.tag == "Transport")
        {
            //gameObject.transform.localPosition += new Vector3(1, 0, 1);
            gameObject.transform.localPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        }
    }
}
