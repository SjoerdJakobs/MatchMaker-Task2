using UnityEngine;
using System.Collections;

public class magicMissile : MonoBehaviour, ISkill
{
    [SerializeField]
    private GameObject magicCube;
    [SerializeField]
    private float cooldown = 2f;
    [SerializeField]
    private float manaCost = 200f;
    private bool hasShot;

    public void shoot(float magicPen, float cooldownReduction, float mana, float apOrAd)
    {
        StartCoroutine(useCooldown(magicPen, cooldownReduction, mana));
    }
    IEnumerator useCooldown(float magicPen, float cooldownReduction, float mana)
    {
        if (!hasShot && mana >= manaCost)
        {
            IDamageable damageable = gameObject.hitByMouse().collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                hasShot = true;
                IDamageable casterMana = GetComponent<IDamageable>();
                casterMana.changeStat(-manaCost, 14);
                yield return new WaitForSeconds(cooldown - (cooldown / 100 * cooldownReduction));
                hasShot = false;
            }
        }
    }
}
