using UnityEngine;
using System.Collections;

public class astroid : MonoBehaviour, ISkill
{

    [SerializeField]
    private GameObject magicCube;
    [SerializeField]
    private float cooldown = 2f;
    [SerializeField]
    private float manaCost = 100;
    [SerializeField]
    private float scaling = 150;
    [SerializeField]
    private float baseDamg = 60;
    [SerializeField]
    private float range = 30;
    [SerializeField]
    private float speed = 30;
    [SerializeField]
    private float force = 10;
    private bool hasShot;

    public void shoot(Vector3 target ,float magicPen, float cooldownReduction, float mana, float apOrAd)
    {
        StartCoroutine(useCooldown(target ,magicPen, cooldownReduction, mana, apOrAd));
    }
    IEnumerator useCooldown(Vector3 target ,float magicPen, float cooldownReduction, float mana, float apOrAd)
    {
        if (!hasShot && mana >= manaCost)
        {
            IDamageable casterMana = GetComponent<IDamageable>();
            casterMana.changeStat(-manaCost, 14);
            hasShot = true;
            transform.LookAt(target);
            Vector3 point = target;
            gameObject.fallingObject(magicCube, point, baseDamg, magicPen, false, scaling, apOrAd, force,range, speed);
            Instantiate(magicCube, transform.position + new Vector3(0, 20f, 0), Quaternion.identity);
            yield return new WaitForSeconds(cooldown - (cooldown / 100 * cooldownReduction));
            hasShot = false;
        }
    }
}
