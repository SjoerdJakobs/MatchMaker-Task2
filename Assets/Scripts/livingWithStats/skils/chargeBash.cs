using UnityEngine;
using System.Collections;

public class chargeBash : MonoBehaviour, ISkill
{
    [SerializeField]
    private float cooldown = 20f;
    [SerializeField]
    private float manaCost = 200f;
    [SerializeField]
    private float force = 200f;
    private bool hasShot;
    private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void shoot(float magicPen, float cooldownReduction, float mana, float apOrAd = 0)
    {
        StartCoroutine(useCooldown(cooldownReduction, mana));
    }
    IEnumerator useCooldown(float cooldownReduction, float mana)
    {
        if (!hasShot && mana >= manaCost)
        {
            hasShot = true;
            Vector3 direct = (transform.position - transform.mousePos()).normalized;
            rigid.knockback(direct, force, true);
            yield return new WaitForSeconds(cooldown - (cooldown / 100 * cooldownReduction));
            hasShot = false;
        }
    }
}
