using UnityEngine;
using System.Collections;

public class giantsSpell : MonoBehaviour, ISkill {
    [SerializeField]
    private float cooldown = 20f;
    [SerializeField]
    private float manaCost = 200f;
    private bool hasShot;

    public void shoot(Vector3 target, float magicPen, float cooldownReduction, float mana, float apOrAd = 0)
    {
        StartCoroutine(useCooldown(cooldownReduction, mana));
    }
    IEnumerator useCooldown(float cooldownReduction, float mana)
    {
        if (!hasShot && mana >= manaCost)
        {
            hasShot = true;
            IDamageable casterMana = GetComponent<IDamageable>();
            casterMana.changeStat(-manaCost, 14);
            GetComponent<IDamageable>().DebufAndBuf(10, 30, 0);
            GetComponent<IDamageable>().DebufAndBuf(10, 30, 2);
            GetComponent<IDamageable>().DebufAndBuf(10, 600, 4);
            GetComponent<IDamageable>().DebufAndBuf(10, 30, 8);
            GetComponent<IDamageable>().DebufAndBuf(10, 8, 11);
            GetComponent<IDamageable>().changeStat(600, 13);
            yield return new WaitForSeconds( Mathf.Clamp(cooldown - (cooldown / 100 * cooldownReduction),10,60));
            hasShot = false;
        }
    }
}
