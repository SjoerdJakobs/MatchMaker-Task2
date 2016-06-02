using UnityEngine;
using System.Collections;

public class giantsSpell : MonoBehaviour, ISkill {
    [SerializeField]
    private float cooldown = 2f;
    private bool hasShot;

    public void shoot(float magicPen, float cooldownReduction)
    {
        StartCoroutine(useCooldown(cooldownReduction));
    }
    IEnumerator useCooldown(float cooldownReduction)
    {
        if (!hasShot)
        {
            hasShot = true;
            GetComponent<IDamageable>().DebufAndBuf(10, 30, 0);
            GetComponent<IDamageable>().DebufAndBuf(10, 30, 2);
            GetComponent<IDamageable>().DebufAndBuf(10, 600, 4);
            GetComponent<IDamageable>().DebufAndBuf(10, 5, 8);
            GetComponent<IDamageable>().DebufAndBuf(10, 2, 11);
            GetComponent<IDamageable>().changeStat(600, 13);

            yield return new WaitForSeconds(20 - (20 / 100 * cooldownReduction));
            hasShot = false;
        }
    }
}
