using UnityEngine;
using System.Collections;

public class sliceAura : MonoBehaviour, ISkill
{

    [SerializeField]
    private GameObject sliceArea;
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
    private bool hasShot;

    // Update is called once per frame

    public void shoot(float armorPen, float cooldownReduction, float mana, float apOrAd)
    {
        StartCoroutine(useCooldown(armorPen, cooldownReduction, mana, apOrAd));
    }

    void Update () {
        sliceArea.transform.position = transform.position;
    }
    IEnumerator useCooldown(float armorpen, float cooldownReduction, float mana, float apOrAd)
    {
        if (!hasShot)
        {
            hasShot = true;
            IDamageable casterMana = GetComponent<IDamageable>();
            gameObject.explosion(1, 5, true, false);
            sliceArea.transform.localScale = new Vector3(2,0.1f,2);
            foreach (RaycastHit i in transform.getWithinSphere(5))
            {
                IDamageable damageableObject = i.collider.GetComponent<IDamageable>();//check for component idamagable on the hit object
                if (damageableObject != null)//"if object has idamagable"
                {
                    print("ello?");
                    damageableObject.returnCaster(gameObject);
                    damageableObject.TakeDamg(baseDamg, armorpen, scaling, true, apOrAd);//_damage it              
                }
            }
            //Debug.Log(hit.collider.gameObject.name);
            yield return new WaitForSeconds(0.1f);

            sliceArea.transform.localScale = new Vector3(0.5f,0.1f,0.5f);
            yield return new WaitForSeconds(1 / cooldownReduction);
            hasShot = false;
        }

    }
}
