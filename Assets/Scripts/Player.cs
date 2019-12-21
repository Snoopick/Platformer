using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer, IHitBox
{
    public void RegisterPlayer()
    {
        GameManager manager = FindObjectOfType<GameManager>();

        if (manager.Player == null)
        {
            manager.Player = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private int health = 1;

    public int Heals
    {
        get => health;

        private set
        {
            health = value;
            if (health <= 0)
            {
                Die();
            }
        }
    }
    public void Hit(int damage)
    {
        Heals -= damage;
    }

    public void Die()
    {
        print("Player died");
    }
    
    private void Awake()
    {
        RegisterPlayer();
    }
}
