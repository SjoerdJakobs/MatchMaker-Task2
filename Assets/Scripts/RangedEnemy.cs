using UnityEngine;
using System.Collections;

public class RangedEnemy : LivingEntity
{
    private NavMeshAgent agent;

    private magicProjectile magicPro;

    private Rigidbody rigid;

    [SerializeField]
    private GameObject target;


    [SerializeField]
    private float range = 10;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float refreshRate;
    private float originalSpeed;

    private WaitForSeconds waitForSec;

    // Use this for initialization
    void Start()
    {
        base.Start();
        rigid = gameObject.GetComponent<Rigidbody>();
        waitForSec = new WaitForSeconds(refreshRate);
        agent = GetComponent<NavMeshAgent>();
        originalSpeed = speed;
        StartCoroutine(moveTo());
        magicPro = gameObject.GetComponent<magicProjectile>();
    }
    void FixedUpdate()
    {
        rigid.limitVelocitySoft3D(2.5f, 15);
    }
    IEnumerator moveTo()
    {
        while (true)
        {
            float velocity = Vector3.Magnitude(rigid.velocity);
            if (velocity < 2.5f)
            {
                rigid.velocity = Vector3.zero;
            }
            agent.SetDestination(target.transform.position);
            agent.speed = speed;
            yield return waitForSec;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.isDistanceSmallerThan(target.transform.position, range))
        {
            magicPro.shoot(magicPen, cooldownReduction, mana, magicDamage);
        }
    }
}
