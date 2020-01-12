using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IPlayer, IHitBox
{
    private const string MeleeWeaponButton = "Fire1";
    private const string RangeWeaponButton = "Fire2";

    private PlayerWeapon[] weapons;

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

    public int Health
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

    public int Heals { get; }

    public void Hit(int damage)
    {
        Health -= damage;
    }

    public void Die()
    {
        print("Player Died");
    }

    private void Awake()
    {
        RegisterPlayer();
        weapons = GetComponents<PlayerWeapon>();
        InputController.FireAction += Attack;
    }

    private void OnDestroy()
    {
        InputController.FireAction -= Attack;
    }

    private void Attack(string button)
    {
        foreach (var weapon in weapons)
        {
            if (button.Equals(MeleeWeaponButton))
            {
                weapon.SetDamage();
            }

            if (button.Equals(RangeWeaponButton))
            {
                weapon.ThrowProjectile();
            }
        }
    }
}