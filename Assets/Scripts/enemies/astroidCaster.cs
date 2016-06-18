using UnityEngine;
using System.Collections;


[RequireComponent(typeof(NavMeshAgent))]
public class astroidCaster : LivingEntity
{
    public enum State { Idle, Chasing, Attacking };
    State currentState;

    private astroid astr;

    private NavMeshAgent agent;
    private GameObject target;
    private Rigidbody rigid;

    [SerializeField]
    private float range = 5;
    [SerializeField]
    private float refreshRate = .25f;
    private float playerLVL;
    private float upgradeRate;

    bool hasTarget;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        astr = GetComponent<astroid>();
        rigid = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        playerLVL = target.GetComponent<LivingEntity>()._publicLVL;
        setStats();
        if (target != null)
        {
            hasTarget = true;
        }
    }
    void setStats()
    {
        Level = playerLVL;
        upgradeRate = playerLVL * 1.75f;
        xpOnDeath = xpOnDeath * upgradeRate;
        maxHealth = maxHealth * upgradeRate;
        healthRegen = healthRegen * upgradeRate; ;
        maxMana = maxMana * upgradeRate;
        manaRegen = manaRegen * upgradeRate;
        armor = armor * upgradeRate;
        armorPen = armorPen * upgradeRate;
        magicResist = magicResist * upgradeRate;
        magicPen = magicPen * upgradeRate;
        moveMentspeed = moveMentspeed * upgradeRate;
        tenacity = tenacity * upgradeRate;
        sizeMod += sizeMod * 0.05f;
        attackDamage = attackDamage * upgradeRate;
        magicDamage = magicDamage * upgradeRate;
        cooldownReduction = cooldownReduction * upgradeRate;
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
                //print(cooldownReduction);
                currentState = State.Attacking;
                agent.enabled = false;

                astr.shoot(target.transform.position, magicPen, cooldownReduction, mana, magicDamage);
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