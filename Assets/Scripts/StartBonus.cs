using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBonus : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private float effectLifeTime;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        particleSystem.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            spriteRenderer.enabled = false;
            particleSystem.Play();
            Destroy(gameObject, effectLifeTime);
        }
    }
}
