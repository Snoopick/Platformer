using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage { get; set; }
    public float Range { get; set; }
    public float ProjectileSpeed { get; set; }
    private float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        var currentX = transform.position.x;
        if (currentX > startX + Range || currentX < startX - Range)
        {
            Destroy(gameObject);
        }

        transform.Translate(Time.deltaTime * ProjectileSpeed * Vector3.right);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.GetComponent<IHitBox>().Hit(Damage);
        Destroy(gameObject);
    }
}