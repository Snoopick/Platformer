using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private bool isAnimated;
    [SerializeField] private float animationDistance = 5f;
    [SerializeField] bool isSurface = false;
    private int surfaceSpeed = 10;

    private float startX;
    private void Start()
    {
        startX = transform.position.x;
        if (isAnimated)
        {
            StartCoroutine(AnimationProcess());
        }
    }

    private IEnumerator AnimationProcess()
    {
        var distance = 0f;
        var direction = 1f;
        var rightMovement = true;
        var delta = 0f;

        while (true)
        {
            delta += Time.deltaTime;

            if (delta > 1f)
            {
                delta = 0;
                rightMovement = !rightMovement;
            }
            
            Vector3 position = transform.position;
            var from = rightMovement ? startX : startX + animationDistance;
            var to = rightMovement ? startX + animationDistance : startX;
            position.x = Mathf.Lerp(from, to, delta);
            transform.position = position;

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isMovementObject = other.GetComponent<CharacterMovement>();
        if (isMovementObject)
        {
            other.transform.parent = transform;
        }

        if (isSurface)
        {
            surfaceSpeed *= -1;
            SurfaceEffector2D surfaceEffector = GetComponent<SurfaceEffector2D>();
            surfaceEffector.speed = surfaceSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent == transform)
        {
            other.transform.parent = null;
        }
    }
}
