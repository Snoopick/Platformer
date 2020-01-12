using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;

    // Update is called once per frame
    void Update()
    {
        var position = target.position;

        position.y = transform.position.y;
        position.z = transform.position.z;

        position.x = Mathf.Lerp(transform.position.x, target.position.x, Time.deltaTime * 5f);
        position.x = Mathf.Clamp(position.x, leftBound, rightBound);
        
        transform.position = position;
    }
}
