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
    [SerializeField]
    private float baseDamg = 30;
    [SerializeField]
    private float scaling = 35;
    [SerializeField]
    private float speed = 10;
    private bool hasShot;

    public void shoot(Vector3 target, float magicPen, float cooldownReduction, float mana, float apOrAd)
    {
        StartCoroutine(useCooldown(magicPen, cooldownReduction, mana, apOrAd));
    }
    IEnumerator useCooldown(float magicPen, float cooldownReduction, float mana, float apOrAd)
    {
        if (!hasShot && mana >= manaCost)
        {
            GameObject clickedObject = gameObject.hitByMouse().collider.gameObject;
            IDamageable damageable = clickedObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                hasShot = true;
                IDamageable casterMana = GetComponent<IDamageable>();
                casterMana.changeStat(-manaCost, 14);                
                gameObject.followingProjectile(magicCube, clickedObject, baseDamg, magicPen, false, scaling, apOrAd, speed);
                Instantiate(magicCube, new Vector3(transform.position.x, 1f, transform.position.z), Quaternion.identity);
                yield return new WaitForSeconds(cooldown - (cooldown / 100 * cooldownReduction));
                hasShot = false;
            }
        }
    }
}
