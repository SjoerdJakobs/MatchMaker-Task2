using UnityEngine;
using System.Collections;

public static class livingEnitityStaticAttacks
{
    public static void skillShotProjectile(this GameObject T, GameObject g, Vector3 target, float damage, float pen, bool physical, float scaling, float apOrAd, float range = 10, float speed = 10)
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
    public static void fallingObject(this GameObject T, GameObject g, Vector3 target, float damage, float pen, bool physical, float scaling, float apOrAd, float force, float range = 30, float speed = 30)
    {
        g.transform.rotation = Quaternion.LookRotation(target);
        FallingProjectile proj = g.GetComponent<FallingProjectile>();
        proj._caster = T;
        proj._force = force;
        proj._AdOrAp = apOrAd;
        proj._scaling = scaling;
        proj._damage = damage;
        proj._projectileSpeed = speed;
        proj._maxRange = range;
        proj._physical = physical;
        proj._pen = pen;
        proj._target = target;
    }
    public static void followingProjectile(this GameObject T, GameObject g, GameObject target, float damage, float pen, bool physical, float scaling, float apOrAd, float speed)
    {
        g.transform.rotation = Quaternion.LookRotation(target.transform.position);
        FollowingProjectile proj = g.GetComponent<FollowingProjectile>();
        proj._caster = T;
        proj._AdOrAp = apOrAd;
        proj._scaling = scaling;
        proj._damage = damage;
        proj._physical = physical;
        proj._pen = pen;
        proj._speed = speed;
        proj._target = target;
    }
    public static void selfExplodingObject(this GameObject T, float damage, float pen, bool physical, float scaling, float apOrAd, float force, float radius)
    {
        foreach (RaycastHit i in T.transform.getWithinSphere(radius))
        {
            IDamageable damageableObject = i.collider.GetComponent<IDamageable>();//check for component idamagable on the hit object
            if (damageableObject != null)//"if object has idamagable"
            {
                bool shouldHit = true;
                damageableObject.returnCaster(T);
                if (T.tag == i.collider.gameObject.tag)
                {
                    shouldHit = false;
                    //print("ello?");
                }
                if (shouldHit)
                {
                    damageableObject.TakeDamg(damage, pen, scaling, physical, apOrAd);//_damage it
                }
            }
        }
        T.explosion(force, radius, true, true);
    }
}
