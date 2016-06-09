using UnityEngine;
using System.Collections;

public class magicMissile : MonoBehaviour, ISkill
{
    [SerializeField]
    private GameObject magicCube;
    [SerializeField]
    private float cooldown = 2f;
    private bool hasShot;

    public void shoot(float magicPen, float cooldownReduction, float mana)
    {
        StartCoroutine(useCooldown(magicPen, cooldownReduction));
    }
    IEnumerator useCooldown(float magicPen, float cooldownReduction)
    {
        if (!hasShot)
        {
            IDamageable damageable = gameObject.hitByMouse().collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                hasShot = true;
                yield return new WaitForSeconds(cooldown - (cooldown / 100 * cooldownReduction));
                hasShot = false;
            }
        }
    }
}
