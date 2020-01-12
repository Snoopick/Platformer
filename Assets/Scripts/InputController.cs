using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static float HorizontalAxis;
    public static event Action<float> JumpAction;
    public static event Action<string> FireAction;
    public static event Action SwitchRun;
    private float jumpTimer;
    private Coroutine waitForJumpCoroutine;

    private float jump = 1f;
    private float doubleJump = 1.25f;
    private float timer = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        HorizontalAxis = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalAxis = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (waitForJumpCoroutine == null)
            {
                waitForJumpCoroutine = StartCoroutine(WaitForJump());
                return;
            }

            jumpTimer = Time.time;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            FireAction?.Invoke("Fire1");
        }
        
        if (Input.GetButtonDown("Fire2"))
        {
            FireAction?.Invoke("Fire2");
        }

        if (Input.GetButtonDown("Fire3"))
        {
            SwitchRun?.Invoke();
        }
    }

    private void OnDestroy()
    {
        HorizontalAxis = 0f;
    }

    private IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(timer);
        if (JumpAction != null)
        {
            float force = Time.time - jumpTimer < timer ? doubleJump : jump; 
            JumpAction.Invoke(force);
        }
        
        waitForJumpCoroutine = null;
    }
}