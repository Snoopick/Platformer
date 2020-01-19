using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float movementDistance = 5f;
    private Coroutine moveUpCoroutine;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (moveUpCoroutine == null && other.GetComponent<Player>())
        {
            StartCoroutine(MoveUp());
        }
    }

    private IEnumerator MoveUp()
    {
        animator.SetTrigger("Rotate");
        float distance = 0f;
        while (distance < movementDistance)
        {
            var shift = movementDistance * Time.deltaTime;
            distance += shift;
            transform.Translate(Vector3.up * shift);

            yield return null;
        }
        
        Destroy(gameObject);
    }
}
