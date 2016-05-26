using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable
{
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
    protected float sizeMod = 0;//scale * sizeMod
    [SerializeField]
    protected float attackDamage = 10;//modefier for physical attack
    [SerializeField]
    protected float magicDamage = 0;//modefier for magic attack
    protected bool dead;//to be or not to be :)

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = maxHealth;//sets health 
        mana = maxMana;
    }

    public void TakeTrueDamg(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Invoke("death",0);
        }
    }
    public void TakeDamg(float damage, float pen, bool physical)
    {
        if (physical)
        {
            damage = damage * (1 - (((armor * ((100 - pen) / 100)) * 0.80f * 0.75f) / 100));
        }
        else
        {
            damage = damage * (1 - (((magicResist * ((100 - pen) / 100)) * 0.80f * 0.75f) / 100));
        }
        health -= damage;
    }

    public void TakeDamgOverTime(float time, float damagePerTick, float pen, bool physical)
    {
        StartCoroutine(TakeDamgOverTimeCoroutine( time, damagePerTick, pen, physical));
    }

    IEnumerator TakeDamgOverTimeCoroutine(float time, float damagePerTick, float pen, bool physical)
    {
        if (physical)
        {
            damagePerTick = damagePerTick * (1 - (((armor * ((100 - pen) / 100)) * 0.80f * 0.75f) / 100));
        }
        else
        {
            damagePerTick = damagePerTick * (1 - (((magicResist * ((100 - pen) / 100)) * 0.80f * 0.75f) / 100));
        }
        while (time > 0)
        {
            time--;
            health -= damagePerTick;
            print("woop woop");
            yield return new WaitForSeconds(1);
        }
    }

    public void DebufAndBuf(float time, float ammount)
    {

    }

    public void permanentBuf(float ammount)
    {

    }
    virtual protected void death()//when would this be used >_>
    {
        dead = true;
        if (OnDeath != null)
            OnDeath();
        GameObject.Destroy(gameObject);
    }  
}