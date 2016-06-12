using UnityEngine;
using System.Collections;

public static class livingEnitityStaticAttacks {
    public static void skillShotProjectile(this GameObject T, GameObject g, Vector3 target, float damage, float pen, bool physical, float scaling, float apOrAd, float range = 10,float speed = 10)
    {
        g.transform.rotation = Quaternion.LookRotation(target);
        Projectile proj = g.GetComponent<Projectile>();
        proj._caster = T;
        proj._AdOrAp = apOrAd;
        proj._scaling = scaling;
        proj._damage = damage;
        proj._projectileSpeed = speed;
        proj._maxRange = range;
        proj._physical = physical;
        proj._pen = pen;
        proj._target = target;      
    }
}
