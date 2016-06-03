using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class testCharacter : LivingEntity {
    
    [SerializeField]
    private Text stats;
    [SerializeField]
    private Text stats2;
    private bool walking;
    private magicMissile magicMiss;
    private magicProjectile magicProj;
    private giantsSpell giant;
    private Rigidbody rigid;          
    private Vector3 destinationPosition;        
    private float destinationDistance;          
    private float moveSpeed;
    // Use this for initialization
    protected override void Start () {
        base.Start();
        rigid = GetComponent<Rigidbody>();
        TakeDamgOverTime(10, 10, magicPen, 25,false);
        magicProj = GetComponent<magicProjectile>();
        magicMiss = GetComponent<magicMissile>();
        giant = GetComponent<giantsSpell>();
        destinationPosition = transform.position;
    }

    protected override void setAndCheckStats()
    {
        base.setAndCheckStats();
        stats.text = "AttackDamage: " + attackDamage.ToString() +
            "\n" + "Armor: " + armor.ToString() +
            "\n" + "MagicRes: " + magicResist.ToString() +
            "\n" + "HPregen: " + healthRegen.ToString() +
            "\n" + "CDreduction: " + cooldownReduction.ToString() +
            "\n" + "MoveSpeed: " + moveMentspeed.ToString();
        stats2.text = "MagicDamage: " + magicDamage.ToString() +
            "\n" + "Armorpen: " + armorPen.ToString() +
            "\n" + "Magicpen: " + magicPen.ToString() +
            "\n" + "MPregen: " + manaRegen.ToString() +
            "\n" + "tenacity: " + tenacity.ToString() +
            "\n" + "attackSpeed" + attackspeed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        checkInput();
    }

    void movement()
    {
        destinationDistance = Vector3.Distance(destinationPosition, transform.position);

        if (destinationDistance < .5f)
        {
            moveSpeed = 0;
        }
        else if (destinationDistance > .5f)
        {
            moveSpeed = moveMentspeed/10;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;

            if (playerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                destinationPosition = ray.GetPoint(hitdist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                transform.rotation = targetRotation;
            }
        }
        else if (Input.GetMouseButton(0))
        {

            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;

            if (playerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                destinationPosition = ray.GetPoint(hitdist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                transform.rotation = targetRotation;
            }
        }
        if (destinationDistance > .5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPosition, moveSpeed * Time.deltaTime);
        }
    }

    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            magicProj.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            magicMiss.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            giant.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            magicProj.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            magicProj.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            magicProj.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            magicProj.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            magicProj.shoot(magicPen, cooldownReduction);
        }
    }
}
