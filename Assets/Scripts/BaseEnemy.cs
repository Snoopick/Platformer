using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyState
{
    Sleep,
    Wait,
    StartWalk,
    Walk,
    StartAttack,
    Attack,
    StartRunAway,
    RunAway,
}

public class BaseEnemy : MonoBehaviour, IEnemy, IHitBox
{
    // State machine
    [SerializeField] private Animator animator;
    [SerializeField] private Transform checkGroundPoint;
    [SerializeField] private Transform checkAttackPoint;
    [SerializeField] private Transform graphics;
    [SerializeField] private bool IsNoob;

    private GameManager gameManager;
    private EEnemyState currentState = EEnemyState.Sleep;
    private EEnemyState nextState;
    private float wakeUpTimer;
    private float waitTimer;
    private float RunAwayTime;
    private float attackTimer;
    private float currentDirection = 1f;
    
    public void RegisterEnemy()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.Enemies.Add(this);
    }
    
    private void Awake()
    {
        RegisterEnemy();
        wakeUpTimer = Time.time + 1f;
        gameManager = FindObjectOfType<GameManager>();
    }
    
    [SerializeField] private int health = 1;

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

        currentState = EEnemyState.StartRunAway;

    }

    public void Die()
    {
        animator.SetTrigger("Die");
        Destroy(this);
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        switch (currentState)
        {
            case EEnemyState.Sleep:
                Sleep();
                break;
            case EEnemyState.Wait:
                Wait();
                break;
            case EEnemyState.StartWalk:
                animator.SetInteger("Walking", 1);
                currentState = EEnemyState.Walk;
                break;
            case EEnemyState.Walk:
                Walk();
                break;
            case EEnemyState.StartAttack:
                animator.SetTrigger("Attack");
                ((IHitBox) gameManager.Player).Hit(1);
                currentState = EEnemyState.Attack;
                break;
            case EEnemyState.Attack:
                Attack();
                break;
            case EEnemyState.StartRunAway:
                RaycastHit2D hit = Physics2D.Raycast(checkAttackPoint.position, checkAttackPoint.right, 0.5f);
                if (hit.collider != null)
                {
                    currentDirection *= -1;
                    RunAwayTime = Time.time + 3f;
                    currentState = EEnemyState.RunAway;
                }
                else
                {
                    currentDirection *= -1;
                    transform.Translate(transform.right * Time.deltaTime * currentDirection);
                    float angle = currentDirection > 0 ? 0f : 180f;
                    graphics.localEulerAngles = new Vector3(0, angle, 0f);
                    currentState = EEnemyState.Attack;
                }

                break;
            case EEnemyState.RunAway:
                RunAway();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void StarsSleeping(float sleepTime = 1f)
    {
        wakeUpTimer = Time.time + sleepTime;
        currentState = EEnemyState.Sleep;
    }
    
    private void Sleep()
    {

        if (Time.time >= wakeUpTimer)
        {
            WakeUp();
        }
        
    }

    private void WakeUp()
    {
        var playerPosition = ((MonoBehaviour) gameManager.Player).transform.position;

        if (Vector3.Distance(transform.position, playerPosition) > 20f)
        {
            StarsSleeping();
            return;
        }

        currentState = EEnemyState.Wait;
        nextState = EEnemyState.StartWalk;
        waitTimer = Time.time + 0.1f;

    }
    
    private void Wait()
    {
        if (Time.time >= waitTimer)
        {
            currentState = nextState;
        }
    }
    
    private void Walk()
    {
        transform.Translate(transform.right * Time.deltaTime * currentDirection);
        // Проверяем возможность передвижения - край платформы
        RaycastHit2D hit = Physics2D.Raycast(checkGroundPoint.position, Vector2.down, 0.3f);

        if (hit.collider == null)
        {
            currentDirection *= -1;
            float angle = currentDirection > 0 ? 0f : 180f;
            graphics.localEulerAngles = new Vector3(0, angle, 0f);
            currentState = EEnemyState.Wait;
            waitTimer = Time.time + 0.3f;
            nextState = EEnemyState.StartWalk;
            
            animator.SetInteger("Walking", 0);
            return;
        }
        
        hit = Physics2D.Raycast(checkAttackPoint.position, checkAttackPoint.right, 0.3f);

        if (hit.collider == null)
        {
            return;
        }
        
        if (hit.collider != null)
        {
            var player = hit.collider.GetComponent<Player>();
            if (player)
            {
                currentState = EEnemyState.StartAttack;
            }
        }
        
    }
    
    private void Attack()
    {
        if (Time.time < attackTimer)
        {
            return;
        }

        currentState = EEnemyState.Wait;
        nextState = EEnemyState.StartWalk;
        waitTimer = Time.time + 0.2f;
    }

    private void RunAway()
    {
        if (Time.time > RunAwayTime)
        {
            currentState = EEnemyState.StartWalk;
            return;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(checkGroundPoint.position, Vector2.down, 0.3f);
        if (hit.collider == null)
        {
            waitTimer = Time.time + 3f;
            currentState = EEnemyState.Wait;
            nextState = EEnemyState.StartWalk;
            animator.SetInteger("Walking", 0);
            return;
        }

        transform.Translate(transform.right * Time.deltaTime * currentDirection);
        float angle = currentDirection > 0 ? 0f : 180f;
        graphics.localEulerAngles = new Vector3(0, angle, 0f);
    }
}
