using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float Level = 1;
    [SerializeField]
    protected float maxHealth = 100;//how much health at the start
    [SerializeField]
    protected float health;//how much health
    [SerializeField]
    protected float healthRegen = 1;
    [SerializeField]
    protected float maxMana = 100;
    [SerializeField]
    protected float mana;
    [SerializeField]
    protected float manaRegen = 8;
    [SerializeField]
    protected float armor = 20;//how much armor
    [SerializeField]
    protected float armorPen = 0;//how much armor penetration
    [SerializeField]
    protected float magicResist =15;//how much magicResist
    [SerializeField]
    protected float magicPen = 0;//how much magic penetration
    [SerializeField]
    protected float moveMentspeed = 10;//how fast you move
    [SerializeField]
    protected float tenacity = 0;//more tenacity means less time slowed or stunned
    [SerializeField]
    protected float sizeMod = 1;//scale * sizeMod
    [SerializeField]
    protected float attackDamage = 10;//modefier for physical attack
    [SerializeField]
    protected float magicDamage = 0;//modefier for magic attack
    [SerializeField]
    protected float attackspeed = 1.2f;
    [SerializeField]
    protected float cooldownReduction = 0;
    protected bool dead;//to be or not to be :)

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = maxHealth;//sets health 
        mana = maxMana;
        StartCoroutine(regenTimer());
    }

    IEnumerator regenTimer()
    {
        while(true)
        {
            health += healthRegen;
            mana += manaRegen;
            setAndCheckStats();
            yield return new WaitForSeconds(1);
        }
    }
    void setAndCheckStats()
    {
        gameObject.transform.localScale = new Vector3(1 * sizeMod, 1 * sizeMod, 1 * sizeMod);
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(mana > maxMana)
        {
            mana = maxMana;
        }
    }

    public void TakeTrueDamg(float damage, float scaling, bool physical)
    {
        if(physical)
        {
            damage = damage + (attackDamage * (scaling / 100));
        }
        else
        {
            damage = damage + (magicDamage * (scaling / 100));
        }
        health -= damage;
        checkDeath();
    }
    public void TakeDamg(float damage, float pen, float scaling, bool physical)
    {
        if (physical)
        {
            float armorPenetrated = armor * (1 - (pen / 100));
            damage = (damage + (attackDamage * (scaling / 100))) * (1-(armorPenetrated / (armorPenetrated + 100)));
        }
        else
        {
            float magicPenetrated = magicResist * (1 - (pen / 100));
            damage = (damage + (magicDamage * (scaling / 100))) * (1 - (magicPenetrated / (magicPenetrated + 100)));
        }
        health -= damage;
        checkDeath();
    }

    public void TakeDamgOverTime(float time, float damagePerTick, float pen, float scaling, bool physical)
    {
        StartCoroutine(TakeDamgOverTimeCoroutine( time, damagePerTick, pen, scaling, physical));
    }

    IEnumerator TakeDamgOverTimeCoroutine(float time, float damagePerTick, float pen, float scaling, bool physical)
    {
        if (physical)
        {
            float armorPenetrated = armor * (1 - (pen / 100));
            damagePerTick = (damagePerTick + (attackDamage * (scaling / 100))) * (1-(armorPenetrated / (armorPenetrated + 100)));
        }
        else
        {
            float magicPenetrated = magicResist * (1 - (pen / 100));
            damagePerTick = (damagePerTick + (magicDamage * (scaling / 100))) * (1 - (magicPenetrated/ (magicPenetrated + 100)));
        }
        while (time > 0)
        {
            time--;
            health -= damagePerTick;
            print("woop woop");
            checkDeath();
            yield return new WaitForSeconds(1);
        }
    }

    public void DebufAndBuf(float time, float ammount, int stat)
    {
        
    }

    public void changeStat(float ammount, int stat)
    {
        switch (stat)
        {
            case 0:
                armor += ammount;
                break;
            case 1:
                armorPen += ammount;
                break;
            case 2:
                magicResist += ammount;
                break;
            case 3:
                magicPen += ammount;
                break;
            case 4:
                maxHealth += ammount;
                health += ammount;
                break;
            case 5:
                healthRegen += ammount;
                break;
            case 6:
                maxMana += ammount;
                mana += ammount;
                break;
            case 7:
                manaRegen += ammount;
                break;
            case 8:
                moveMentspeed += ammount;
                break;
            case 9:
                magicDamage += ammount;
                break;
            case 10:
                attackDamage += ammount;
                break;
            case 11:
                sizeMod += ammount;
                break;
            case 12:
                attackDamage += ammount;
                break;
            default:
                print("ERROR wrong or no stat number given");
                break;
        }
        setAndCheckStats();
    }

    void checkDeath()
    {
        if (health <= 0 && !dead)
        {
            Invoke("death", 0);
        }
    }

    virtual protected void death()//when would this be used >_>
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        StopAllCoroutines();
        GameObject.Destroy(gameObject);
    }  
}