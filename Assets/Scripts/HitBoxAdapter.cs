using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxAdapter : MonoBehaviour, IHitBox
{
    [SerializeField] private GameObject hitTarget;
    private IHitBox hitBox;

    private void Reset()
    {
        var hit = GetComponentInParent<IHitBox>();
        if (hit != null)
        {
            hitTarget = (hit as MonoBehaviour)?.gameObject;
        }
    }

    private void Start()
    {
        hitBox = hitTarget.GetComponent<IHitBox>();

        if (gameObject != hitTarget)
        {
            return;
        }
        
        Destroy(this);
        Debug.Log($"Wrong place on {name} gameObject");
    }

    public int Heals => hitBox.Heals;
    public void Hit(int damage)
    {
        hitBox.Hit(damage);
    }

    public void Die()
    {
        hitBox.Die();
    }
}
