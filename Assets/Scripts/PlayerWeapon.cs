using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IDamager
{
    [SerializeField] private WeaponData meleeWeaponData;
    [SerializeField] private WeaponData rangeWeaponData;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private string buttonName = "Fire1";
    [SerializeField] private Animator animator;
    private float lastAttackTime;
    private static readonly int Attack = Animator.StringToHash("Attack");

    public int Damage => meleeWeaponData.WeaponDamage;
    public void SetDamage()
    {
        if (Time.time - lastAttackTime < meleeWeaponData.FireRate)
        {
            return;
        }

        lastAttackTime = Time.time;
        animator.SetTrigger(Attack);
        
        var target = GetTarget();
        target?.Hit(Damage);
    }

    public void ThrowProjectile()
    {
        if (Time.time - lastAttackTime < rangeWeaponData.FireRate)
        {
            return;
        }

        lastAttackTime = Time.time;
        animator.SetTrigger(Attack);
        var projectile = Instantiate(rangeWeaponData.Projectile, attackPoint.position, attackPoint.rotation);
        var projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.Damage = rangeWeaponData.WeaponDamage;
        projectileScript.Range = rangeWeaponData.WeaponRange;
        projectileScript.ProjectileSpeed = rangeWeaponData.ProjectileSpeed;
    }

    private IHitBox GetTarget()
    {
        IHitBox target = null;
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, attackPoint.right, meleeWeaponData.WeaponRange);

        if (hit.collider != null)
        {
            target = hit.transform.gameObject.GetComponent<IHitBox>();
        }
        return target;
    }
}