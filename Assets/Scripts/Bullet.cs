using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 500f;
    [SerializeField] private float maxLifetime = 10f;
    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
       _rigidbody.AddForce(direction * speed); 
       
       Destroy(gameObject, maxLifetime);
    }

    private void FixedUpdate()
    {
        transform.localScale *= 0.98f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
