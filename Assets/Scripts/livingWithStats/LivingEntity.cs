using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] protected float Level = 1;
    [SerializeField] protected float xp = 0;
    [SerializeField] protected float xpOnDeath;
    [SerializeField] protected float maxHealth = 100;//how much health at the start
    [SerializeField] protected float health;//how much health
    [SerializeField] protected float healthRegen = 1;
    [SerializeField] protected float maxMana = 100;
    [SerializeField] protected float mana;
    [SerializeField] protected float manaRegen = 8;
    [SerializeField] protected float armor = 20;//how much armor
    [SerializeField] protected float armorPen = 0;//how much armor penetration
    [SerializeField] protected float magicResist =15;//how much magicResist
    [SerializeField] protected float magicPen = 0;//how much magic penetration
    [SerializeField] protected float moveMentspeed = 10;//how fast you move
    [SerializeField] protected float tenacity = 0;//more tenacity means less time slowed or stunned
    [SerializeField] protected float sizeMod = 1;//scale * sizeMod
    [SerializeField] protected float attackDamage = 10;//modefier for physical attack
    [SerializeField] protected float magicDamage = 0;//modefier for magic attack
    [SerializeField] protected float attackspeed = 1.2f;
    [SerializeField] protected float cooldownReduction = 0;
    [SerializeField] protected int powerPointsPerLVL = 10;
    [SerializeField]
    protected bool canLVL = false;


    protected bool isLVLing = false;
    protected int powerPoints;
    protected IDamageable enemyCaster;
    protected bool dead;//to be or not to be :)

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = maxHealth;//sets health 
        mana = maxMana;
        StartCoroutine(regenTimer());
        enemyCaster = GetComponent<IDamageable>();
    }

    IEnumerator regenTimer()
    {
        while(true)
        {
            health += healthRegen;
            mana += manaRegen;
            setAndCheckStats();
            yield return new WaitForSeconds(0.5f);
        }
    }
    protected virtual void setAndCheckStats()
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
        if (xp >= (50 * Mathf.Pow(Level, 3) / 3) - (100 * Mathf.Pow(Level, 2)) + 850 * Level / 3 - 100 && canLVL)
        {
            Level++;
            isLVLing = true;
            powerPoints += powerPointsPerLVL;
        }
        //print("level xp = level " + Level + " xp = " + ((50 * Mathf.Pow(Level, 3) / 3) - (100 * Mathf.Pow(Level, 2)) + 850 * Level / 3 - 200));
    }
    public void returnCaster(GameObject caster)
    {
        enemyCaster = caster.GetComponent<IDamageable>();
    }
    public void addXp(float addedXp)
    {
        xp += addedXp;
        setAndCheckStats();
    }
    void giveXp()
    {
        enemyCaster.addXp(xpOnDeath);
    }
    public void TakeTrueDamg(float damage, float scaling, bool physical, float apOrAd)
    {
        if(physical)
        {
            damage = damage + (apOrAd * (scaling / 100));
        }
        else
        {
            damage = damage + (apOrAd * (scaling / 100));
        }
        health -= damage;
        checkDeath();
    }
    public void TakeDamg(float damage, float pen, float scaling, bool physical, float apOrAd)
    {
        if (physical)
        {
            float armorPenetrated = armor * (1 - (pen / 100));
            damage = (damage + (apOrAd * (scaling / 100))) * (1-(armorPenetrated / (armorPenetrated + 100)));
        }
        else
        {
            float magicPenetrated = magicResist * (1 - (pen / 100));
            damage = (damage + (apOrAd * (scaling / 100))) * (1 - (magicPenetrated / (magicPenetrated + 100)));
        }
        health -= damage;
        checkDeath();
    }

    public void TakeDamgOverTime(float time, float damagePerTick, float pen, float scaling, bool physical, float apOrAd)
    {
        StartCoroutine(TakeDamgOverTimeCoroutine( time, damagePerTick, pen, scaling, physical, apOrAd));
    }

    IEnumerator TakeDamgOverTimeCoroutine(float time, float damagePerTick, float pen, float scaling, bool physical, float apOrAd)
    {
        if (physical)
        {
            float armorPenetrated = armor * (1 - (pen / 100));
            damagePerTick = (damagePerTick + (apOrAd * (scaling / 100))) * (1-(armorPenetrated / (armorPenetrated + 100)));
        }
        else
        {
            float magicPenetrated = magicResist * (1 - (pen / 100));
            damagePerTick = (damagePerTick + (apOrAd * (scaling / 100))) * (1 - (magicPenetrated/ (magicPenetrated + 100)));
        }
        while (time > 0)
        {
            time--;
            health -= damagePerTick;
            setAndCheckStats();
            //print("woop woop");
            checkDeath();
            yield return new WaitForSeconds(1);
        }
    }

    public void DebufAndBuf(float time, float ammount, int stat)
    {
        StartCoroutine(bufsNshit(time , ammount, stat));
    }
    IEnumerator bufsNshit(float time, float ammount, int stat)
    {
        changeStat(ammount, stat);
        yield return new WaitForSeconds(time);
        changeStat(-ammount, stat);
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
                break;
            case 5:
                healthRegen += ammount;
                break;
            case 6:
                maxMana += ammount;
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
                tenacity += ammount;
                break;
            case 13:
                health += ammount;
                break;
            case 14:
                mana += ammount;
                break;
            case 15:
                cooldownReduction += ammount;
                break;
            default:
                print("ERROR wrong or no stat number given");
                break;
        }
        if(powerPoints <= 1)
        {
            isLVLing = false;
        }
        setAndCheckStats();
    }

    void checkDeath()
    {
        if (health <= 0 && !dead)
        {
            StopAllCoroutines();
            health = 0;
            setAndCheckStats();
            Invoke("death", 0);
        }
    }

    virtual protected void death()//when would this be used >_>
    {
        giveXp();
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        StopAllCoroutines();
        GameObject.Destroy(gameObject);
    }  
}