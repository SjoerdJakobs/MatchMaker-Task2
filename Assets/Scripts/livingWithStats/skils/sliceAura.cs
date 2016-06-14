using UnityEngine;
using System.Collections;

public class sliceAura : MonoBehaviour, ISkill
{

    [SerializeField]
    private GameObject sliceArea;
    [SerializeField]
    private float manaCost = 100;
    [SerializeField]
    private float scaling = 50;
    [SerializeField]
    private float baseDamg = 30;
    [SerializeField]
    private float range = 10;
    private bool hasShot;

    // Update is called once per frame

    public void shoot(Vector3 target, float armorPen, float attackSpeed, float mana, float apOrAd)
    {
        StartCoroutine(useCooldown(armorPen, attackSpeed, mana, apOrAd));
    }

    void Update () {
        sliceArea.transform.position = transform.position + new Vector3(0,transform.localScale.y/25, 0);
    }
    IEnumerator useCooldown(float armorpen, float attackSpeed, float mana, float apOrAd)
    {
        if (!hasShot)
        {
            hasShot = true;
            IDamageable casterMana = GetComponent<IDamageable>();
            gameObject.explosion(1, 5, true, false);
            sliceArea.transform.localScale = new Vector3(2,0.1f,2);
            foreach (RaycastHit i in transform.getWithinSphere(range))
            {
                IDamageable damageableObject = i.collider.GetComponent<IDamageable>();//check for component idamagable on the hit object
                if (damageableObject != null)//"if object has idamagable"
                {
                    bool shouldHit = true;
                    damageableObject.returnCaster(gameObject);
                    if (gameObject == i.collider.gameObject)
                    {
                        shouldHit = false;
                    }
                    if (shouldHit)
                    {
                        damageableObject.TakeDamg(baseDamg, armorpen, scaling, true, apOrAd);//_damage it              
                    }
                }
            }
            //Debug.Log(hit.collider.gameObject.name);
            yield return new WaitForSeconds(0.1f);

            sliceArea.transform.localScale = new Vector3(0.5f,0.1f,0.5f);
            yield return new WaitForSeconds(1 / attackSpeed);
            hasShot = false;
        }

    }
}
