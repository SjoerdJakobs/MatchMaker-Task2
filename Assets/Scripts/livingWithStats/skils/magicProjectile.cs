using UnityEngine;
using System.Collections;

public class magicProjectile : MonoBehaviour, ISkill {

    [SerializeField]
    private GameObject magicCube;
    [SerializeField]
    private float cooldown = 2f;
    [SerializeField]
    private float manaCost = 100;
    private bool hasShot;

    public void shoot(float magicPen, float cooldownReduction, float mana)
    {
        StartCoroutine(useCooldown(magicPen, cooldownReduction, mana));
    }
    IEnumerator useCooldown(float magicPen, float cooldownReduction, float mana)
    {
        if (!hasShot && mana >= manaCost)
        {
            IDamageable casterMana = GetComponent<IDamageable>();
            casterMana.changeStat(-manaCost, 14);
            hasShot = true;
            transform.lookAtMouse(999);
            Vector3 target = transform.mousePos() + new Vector3(0, 1.5f, 0);
            gameObject.skillShotProjectile(magicCube, target, 30, magicPen, false, 50, 10, 40);
            Instantiate(magicCube, transform.position + new Vector3(0,1.5f,0), Quaternion.identity);
            yield return new WaitForSeconds(cooldown - (cooldown/100*cooldownReduction));
            hasShot = false;
        }
    }
}
