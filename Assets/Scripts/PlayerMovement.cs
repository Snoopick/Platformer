using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : CharacterMovement
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private Transform graphics;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private float runSpeed = 2f;

    private bool isRunning;
    private Rigidbody2D rig;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsInAir = Animator.StringToHash("IsInAir");
    private static readonly int JumpTrigger = Animator.StringToHash("Jump");
    private static readonly int IsRunningTrigger = Animator.StringToHash("IsRunning");

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        InputController.JumpAction += OnJumpAction;
        InputController.SwitchRun += OnSwitchRunAction;
    }

    private void OnDestroy()
    {
        InputController.JumpAction -= OnJumpAction;
        InputController.SwitchRun -= OnSwitchRunAction;
    }

    private void FixedUpdate()
    {
        if (IsFreezing)
        {
            Vector2 velocity = rig.velocity;
            velocity.x = 0f;
            rig.velocity = velocity;
            return;
        }

        var direction = new Vector2(InputController.HorizontalAxis, 0f);
        if (!IsGrounded())
        {
            direction *= 0.5f;
        }

        Move(direction);
        if (gameObject.transform.position.y < -10f)
        {
            SceneManager.LoadScene("Platformer");
        }
    }

    private void Update()
    {
        if (IsGrounded())
        {
            animator.SetFloat(Speed, Mathf.Abs(rig.velocity.x));
            animator.SetBool(IsInAir, false);
        }
        else
        {
            animator.SetFloat(Speed, Mathf.Abs(0f));
            animator.SetBool(IsInAir, true);
        }

        if (Mathf.Abs(rig.velocity.x) < 0.01f)
        {
            return;
        }

        float angle = rig.velocity.x > 0f ? 0f : 180f;
        graphics.localEulerAngles = new Vector3(0f, angle, 0f);
    }

    public override void Move(Vector2 direction)
    {
        Vector2 velocity = rig.velocity;
        velocity.x = direction.x * maxSpeed;
        rig.velocity = velocity;
    }

    public override void Stop(float timer)
    {
        throw new System.NotImplementedException();
    }

    public override void Jump(float force)
    {
        rig.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
    }

    private void OnJumpAction(float force)
    {
        if (IsGrounded() && !IsFreezing)
        {
            Jump(jumpForce * force);
            animator.SetTrigger(JumpTrigger);
        }
    }

    private void OnSwitchRunAction()
    {
        if (isRunning)
        {
            isRunning = false;
            maxSpeed /= runSpeed;
        }
        else
        {
            isRunning = true;
            maxSpeed *= runSpeed;
        }

        animator.SetBool(IsRunningTrigger, isRunning);
    }

    private bool IsGrounded()
    {
        Vector2 point = transform.position;
        point.y -= 0.1f; //чтобы избежать пересечения с самим собой
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.down, 0.2f);
        return hit.collider != null;
    }
}
