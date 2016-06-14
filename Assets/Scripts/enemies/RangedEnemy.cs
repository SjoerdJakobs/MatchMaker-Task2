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
/*
public class RangedEnemy : LivingEntity
{
    public enum State { Idle, Chasing, Attacking };
    State currentState;

    private NavMeshAgent agent;

    private magicProjectile magicPro;

    private Rigidbody rigid;

    [SerializeField]
    private GameObject target;


    [SerializeField]
    private float range = 10;
    [SerializeField]
    private float refreshRate;

    private WaitForSeconds waitForSec;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        rigid = gameObject.GetComponent<Rigidbody>();
        waitForSec = new WaitForSeconds(refreshRate);
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(moveTo());
        magicPro = gameObject.GetComponent<magicProjectile>();
        currentState = State.Chasing;
    }
    void FixedUpdate()
    {
        rigid.limitVelocitySoft3D(2.5f, 15);
        
    }
    IEnumerator moveTo()
    {
        while (target != null)
        {
            float velocity = Vector3.Magnitude(rigid.velocity);
            if (velocity < 2.5f)
            {
                rigid.velocity = Vector3.zero;
            }
            print(currentState + "1");
            if (currentState == State.Chasing)
            {
                print(moveMentspeed + "2");
                agent.SetDestination(target.transform.position);
                agent.speed = moveMentspeed/10;
            }
            else if(currentState == State.Attacking)
            {
                print(currentState + "3");
                agent.Stop();
            }
            yield return waitForSec;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.isDistanceSmallerThan(target.transform.position, range))
        {
            magicPro.shoot(magicPen, cooldownReduction, mana, magicDamage);
            print("hello");
            currentState = State.Attacking;
        }
        else
        {
            print("hello2");
            currentState = State.Chasing;
        }
    }
}*/
