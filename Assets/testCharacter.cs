using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class testCharacter : LivingEntity {
    
    [SerializeField]
    private Text stats;
    [SerializeField]
    private Text stats2;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text manaText;
    [SerializeField]
    private Text xpText;
    [SerializeField]
    private Transform xpBar;
    [SerializeField]
    private Transform manaBar;
    [SerializeField]
    private Transform hpBar;
    private magicMissile magicMiss;
    private magicProjectile magicProj;
    private giantsSpell giant;
    private Rigidbody rigid;          
    private Vector3 destinationPosition;        
    private float destinationDistance;          
    private float moveSpeed;
    private float velocity;
    private bool isMoving = false;

    [SerializeField]
    private LayerMask mask;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        rigid = GetComponent<Rigidbody>();
        magicProj = GetComponent<magicProjectile>();
        magicMiss = GetComponent<magicMissile>();
        giant = GetComponent<giantsSpell>();
        destinationPosition = transform.position;
        velocity = 1000;
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
            "\n" + "ArmorPen: " + armorPen.ToString() +
            "\n" + "MagicPen: " + magicPen.ToString() +
            "\n" + "MPregen: " + manaRegen.ToString() +
            "\n" + "tenacity: " + tenacity.ToString() +
            "\n" + "attackSpeed" + attackspeed.ToString();
        healthText.text = "health " + health.ToString() + "/" + maxHealth.ToString();
        manaText.text = "mana " + mana.ToString() + "/" + maxMana.ToString();
        xpText.text = "LVL " + Level.ToString() +"         xp " + xp.ToString(); 

    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
        changeBars();
    }
    void FixedUpdate()
    {
        destinationDistance = Vector3.Distance(destinationPosition, transform.position);
        if (destinationDistance < 0.5f/* && isMoving == true*/)
        {
            moveSpeed = 0;
            rigid.velocity =    new Vector3(0,0,0);
        }
        else if (destinationDistance > 0.5f)
        {
            moveSpeed = moveMentspeed / 10;
        }
        if (Input.GetMouseButtonDown(0))
        {
            rigid.velocity = new Vector3(0, 0, 0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            transform.lookAtMouse(999);
            if (Physics.Raycast(ray, out hit, mask))
            {
                destinationPosition = transform.mousePos();
            }
            //destinationPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            transform.lookAtMouse(999);
            if (Physics.Raycast(ray, out hit, mask))
            {
                destinationPosition = transform.mousePos();
                //print(destinationPosition);
            }
            //destinationPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //transform.lookAtMouse(moveSpeed);
        rigid.AddForce(((destinationPosition - transform.position).normalized*moveSpeed), ForceMode.Impulse);// = Vector3.Lerp(transform.position, destinationPosition, (moveMentspeed) * Time.fixedDeltaTime);
        rigid.limitVelocityHard3D(moveMentspeed/10);
    }

    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            magicProj.shoot(magicPen, cooldownReduction);
            mana -= 110;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            magicMiss.shoot(magicPen, cooldownReduction);
            mana -= 110;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            giant.shoot(magicPen, cooldownReduction);
            mana -= 180;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            magicProj.shoot(magicPen, cooldownReduction);
            mana -= 110;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            magicProj.shoot(magicPen, cooldownReduction);
            mana -= 110;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            magicProj.shoot(magicPen, cooldownReduction);
            mana -= 110;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            magicProj.shoot(magicPen, cooldownReduction);
            mana -= 110;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TakeDamgOverTime(10, 10, magicPen, 25,false);
            magicProj.shoot(magicPen, cooldownReduction);
            mana -= 110;
        }
    }
    void changeBars ()
    {
        float newXHp = (health / maxHealth);
        hpBar.localScale = new Vector3(newXHp, 1, 1);
        float newXMp = (mana / maxMana);
        manaBar.localScale = new Vector3(newXMp, 1, 1);
        /*float newXXp = (health / maxHealth);
        xpBar.localScale = new Vector3(newXXp, transform.localScale.y, transform.localScale.z);*/
    }
}
