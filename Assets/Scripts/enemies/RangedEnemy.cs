using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class RangedEnemy : LivingEntity
{
    public enum State { Idle, Chasing, Attacking };
    State currentState;

    private magicProjectile magicPro;

    private NavMeshAgent agent;
    [SerializeField]
    private GameObject target;
    private Rigidbody rigid;

    [SerializeField]
    private float range = 5;
    [SerializeField]
    private float refreshRate = .25f;

    bool hasTarget;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        magicPro = GetComponent<magicProjectile>();
        rigid = GetComponent<Rigidbody>();
        if (target != null)
        {
            hasTarget = true;
        }
        agent.speed = moveMentspeed;
    }

    protected override void Start()
    {
        base.Start();

        if (hasTarget)
        {
            currentState = State.Chasing;
            StartCoroutine(move());
        }
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }
    void FixedUpdate()
    {
        rigid.limitVelocitySoft3D(2.5f, 15);
        float velocity = Vector3.Magnitude(rigid.velocity);
        if (velocity < 2.5f)
        {
            rigid.velocity = Vector3.zero;
        }

    }
    void Update()
    {

        if (hasTarget)
        {
            if (transform.isDistanceSmallerThan(target.transform.position, range))
            {
                print(cooldownReduction);
                currentState = State.Attacking;
                agent.enabled = false;

                magicPro.shoot(target.transform.position,magicPen, cooldownReduction, mana, magicDamage);
            }
            else
            {
                currentState = State.Chasing;
                agent.enabled = true;
            }
        }
        else
        {
            agent.enabled = false;
        }
    }

    IEnumerator move()
    {
        while (hasTarget)
        {
            if (currentState == State.Chasing)
            {
                if (!dead)
                {
                    agent.SetDestination(target.transform.position);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
