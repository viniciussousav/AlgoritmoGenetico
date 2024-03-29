﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentEnemy : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f,0f,Random.Range(10f,12f));
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SpawnerDestroyer") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
