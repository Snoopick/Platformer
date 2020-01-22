using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chest : MonoBehaviour, IHitBox
{
    [SerializeField] private int health = 1;
    [SerializeField] private GameObject coinPrefab;
    private int coinCount;

    private void Start()
    {
        coinCount = Random.Range(4, 8);
    }

    public int Heals { get; }
    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(DropCoin());
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Chest destroy");
    }

    private IEnumerator DropCoin()
    {
        var Coin = coinPrefab.GetComponent<Coin>();
        Coin.isFallDownCoin = true;
        Coin.movementDistance = 0.5f;
        
        for (int i = 1; i <= coinCount; i++)
        {
            GameObject obj = Instantiate(coinPrefab);
            float randomStart = Random.Range(-1f, 1f);
            
            Vector3 pos = new Vector3(transform.position.x + randomStart, transform.position.y, transform.position.z);
            obj.transform.position = pos;

            new WaitForSeconds(0.5f);
        }
        
        yield return new WaitForSeconds(0.3f);
    }
}
