using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour
{
    [SerializeField] private float _timeToLive = 5.0f;
    private float timeAlive;

    // Start is called before the first frame update
    void Start()
    {
        timeAlive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;

        if(timeAlive >= _timeToLive)
        {
            DestroyImmediate(gameObject);
        }
    }
}
