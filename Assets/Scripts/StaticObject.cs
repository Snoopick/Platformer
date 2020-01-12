using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObject : MonoBehaviour, IHitBox
{
    [SerializeField] private LevelObjectData objectData;
    private Rigidbody2D rig;
    private int health = 1;

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
        print("Object destroy");
    }

    [ContextMenu("Rename")]
    private void Rename()
    {
        if (objectData != null)
        {
            gameObject.name = objectData.Name;
        }
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Heals = objectData.Health;
        
        if (rig)
        {
            rig.bodyType = objectData.Static ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        }
    }

    [ContextMenu("CopyUp")]
    private void CopyUp()
    {
        var copy = Instantiate((gameObject));
        var pos = copy.transform.position;
        pos.y += 4;
        copy.transform.position = pos;
    }
}
