using UnityEngine;
using System.Collections;

public class magicProjectile : MonoBehaviour, ISkill {

    [SerializeField]
    private GameObject magicCube;
    [SerializeField]
    private float cooldown = 2f;
    private bool hasShot;

    public void shoot(float magicPen, float cooldownReduction)
    {
        StartCoroutine(useCooldown(magicPen, cooldownReduction));
    }
    IEnumerator useCooldown(float magicPen, float cooldownReduction)
    {
        if (!hasShot)
        {
            hasShot = true;
            Vector3 target = transform.mousePos();
            gameObject.skillShotProjectile(magicCube, target, 30, magicPen, false, 50, 10, 40);
            Instantiate(magicCube, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown - (cooldown/100*cooldownReduction));
            hasShot = false;
        }
    }
}
