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
    [SerializeField]
    protected GameObject lvlScreen;
    [SerializeField]
    private Animator _anim;


    private chargeBash charge;
    private magicMissile magicMiss;
    private magicProjectile magicProj;
    private giantsSpell giant;
    private Rigidbody rigid;
    private astroid astr;
    private sliceAura slice;
    private NavMeshAgent agent;
    private Vector3 target;   

    private bool isMoving = false;
    
    [SerializeField]
    private LayerMask mask;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        lvlScreen.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        magicProj = GetComponent<magicProjectile>();
        slice = GetComponent<sliceAura>();
        magicMiss = GetComponent<magicMissile>();
        astr = GetComponent<astroid>();
        giant = GetComponent<giantsSpell>();
        charge = GetComponent<chargeBash>();
        Time.timeScale = 1;
        isLVLing = true;
    }

    protected override void setAndCheckStats()
    {
        base.setAndCheckStats();
        changeBars();
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
        //print(isLVLing);
        if (isLVLing)
        {
            lvlScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            lvlScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SpendPoints(int stat)
    {
        if(stat == 0 || stat == 2) //armor or magicresist
        {
            changeStat(10, stat);
            powerPoints--;
        }
        else if (stat == 1 || stat == 3) //armor or magicpen
        {
            changeStat(5, stat);
            powerPoints--;
        }
        else if (stat == 4)//hp
        {
            changeStat(50, stat);
            changeStat(75, 13);
            powerPoints--;
        }
        else if (stat == 6)//mana
        {
            changeStat(75, stat);
            changeStat(75, 14);
            powerPoints--;
        }
        else if (stat == 5 || stat == 7)//health or mana regen
        {
            changeStat(5, stat);
            powerPoints--;
        }
        else if (stat == 9 || stat == 10)//magic or attack damage
        {
            changeStat(15, stat);
            powerPoints--;
        }
        else if (stat == 8 || stat == 15)//movementspeed or cooldownReduction
        {
            changeStat(10, stat);
            powerPoints--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
        changeBars();
        moveMent();
    }
    void FixedUpdate()
    {
        rigid.limitVelocityHard3D(moveMentspeed/10);
    }

    void moveMent()
    {
        if (Input.GetMouseButtonDown(1))
        {
            agent.ResetPath();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, mask))
            {
                target = transform.mousePos();
            }
        }
        else if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, mask))
            {
                target = transform.mousePos();
                //print(destinationPosition);
            }
        }
        float velocity = Vector3.Magnitude(rigid.velocity);
        if (velocity < 2.5f)
        {
            rigid.velocity = Vector3.zero;
        }
        agent.SetDestination(target);
        agent.speed = moveMentspeed/10;
    }

    void checkInput()
    {
        Vector3 inputmouse = transform.mousePos() + new Vector3(0, 1f, 0);
        if (Input.GetMouseButtonDown(0))
        {
            slice.shoot(inputmouse, armorPen,attackspeed,mana,attackDamage);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            agent.Stop();
            magicProj.shoot(inputmouse, magicPen, cooldownReduction, mana, magicDamage);
            transform.lookAtMouse(999);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            agent.Stop();
            magicMiss.shoot(inputmouse, magicPen, cooldownReduction, mana, magicDamage);
            transform.lookAtMouse(999);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            agent.Stop();
            astr.shoot(inputmouse, magicPen, cooldownReduction, mana, magicDamage);
            transform.lookAtMouse(999);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            giant.shoot(inputmouse, magicPen, cooldownReduction, mana);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            agent.Stop();
            magicProj.shoot(inputmouse, magicPen, cooldownReduction, mana, magicDamage);
            transform.lookAtMouse(999);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            agent.Stop();
            magicProj.shoot(inputmouse, magicPen, cooldownReduction, mana, magicDamage);
            transform.lookAtMouse(999);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            agent.Stop();
            magicProj.shoot(inputmouse, magicPen, cooldownReduction, mana, magicDamage);
            TakeDamg(200, armorPen, 80, true, attackDamage);
            transform.lookAtMouse(999);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            agent.Stop();
            TakeDamgOverTime(10, 10, magicPen, 25,false,magicDamage);
            magicProj.shoot(inputmouse, magicPen, cooldownReduction, mana, magicDamage);
            transform.lookAtMouse(999);
        }
    }
    void changeBars ()
    {
        float newXHp = (health / maxHealth);
        hpBar.localScale = new Vector3(newXHp, 1, 1);
        float newXMp = (mana / maxMana);
        manaBar.localScale = new Vector3(newXMp, 1, 1);
    }
}
