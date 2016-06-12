using UnityEngine;
using System.Collections;

public class magicProjectile : MonoBehaviour, ISkill {

    [SerializeField]
    private GameObject magicCube;
    [SerializeField]
    private float cooldown = 2f;
    [SerializeField]
    private float manaCost = 100;
    [SerializeField]
    private float scaling = 50;
    [SerializeField]
    private float baseDamg = 30;
    [SerializeField]
    private float range = 10;
    [SerializeField]
    private float speed= 30;
    private bool hasShot;

    public void shoot(float magicPen, float cooldownReduction, float mana,float apOrAd)
    {
        StartCoroutine(useCooldown(magicPen, cooldownReduction, mana, apOrAd));
    }
    IEnumerator useCooldown(float magicPen, float cooldownReduction, float mana, float apOrAd)
    {
        if (!hasShot && mana >= manaCost)
        {
            IDamageable casterMana = GetComponent<IDamageable>();
            casterMana.changeStat(-manaCost, 14);
            hasShot = true;
            transform.lookAtMouse(999);
            Vector3 target = transform.mousePos() + new Vector3(0, 1f, 0);
            gameObject.skillShotProjectile(magicCube, target, baseDamg, magicPen, false, scaling, apOrAd, range, speed);
            Instantiate(magicCube, transform.position + new Vector3(0,1f,0), Quaternion.identity);
            yield return new WaitForSeconds(cooldown - (cooldown/100*cooldownReduction));
            hasShot = false;
        }
    }
}
