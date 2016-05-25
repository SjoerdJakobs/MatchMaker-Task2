using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float startingHealth;//how much health at the start
    [SerializeField]
    protected float health;//how much health
    [SerializeField]
    protected float armor;//how much armor
    [SerializeField]
    protected float armorPen;//how much armor penetration
    [SerializeField]
    protected float magicResist;//how much magicResist
    [SerializeField]
    protected float magicPen;//how much magic penetration
    [SerializeField]
    protected float moveMentspeed;//how fast you move
    [SerializeField]
    protected float tenacity;//more tenacity means less time slowed or stunned
    [SerializeField]
    protected float sizeMod;//scale * sizeMod
    [SerializeField]
    protected float attackDamage;//modefier for physical attack
    [SerializeField]
    protected float magicDamage;//modefier for magic attack
    protected bool dead;//to be or not to be :)

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;//sets health  
    }

    public void TakeTrueDamg(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Invoke("death",0);
        }
    }
    public void TakeDamg(float damage,float pen, bool physical)
    {
        if(physical)
        {
            damage = damage * (1-(((armor*((100-pen)/100)) * 0.80f *0.75f)/100));
        }
    }

    virtual protected void death()//when would this be used >_>
    {
        dead = true;
        if (OnDeath != null)
            OnDeath();
        GameObject.Destroy(gameObject);
    }

   
}