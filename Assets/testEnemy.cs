using UnityEngine;
using System.Collections;

public class testEnemy : LivingEntity
{

    private float playerLVL;
    private float upgradeRate;
    private GameObject target;


    protected override void Start()
    {
        base.Start();
        setStats();
        target = GameObject.FindGameObjectWithTag("Player");
        playerLVL = target.GetComponent<LivingEntity>()._publicLVL;

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
    }

    // Update is called once per frame
    void Update () {
	
	}
}
