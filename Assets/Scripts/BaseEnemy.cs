using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IEnemy, IHitBox
{
    public void RegisterEnemy()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        manager.Enemies.Add(this);
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
        print("Enemy died");
    }
    
    private void Awake()
    {
        RegisterEnemy();
    }
}
