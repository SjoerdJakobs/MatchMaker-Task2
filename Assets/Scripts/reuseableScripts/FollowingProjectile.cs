using UnityEngine;
using System.Collections;

public class FollowingProjectile : MonoBehaviour
{

    [SerializeField]
    private LayerMask collisionMask;//layer wich the projectile checks for
    public GameObject _target;
    public GameObject _caster;
    private Vector3 aim;

    public bool _physical;

    private float rangeTraveled;//how much the bullet has gone
    public float _scaling;
    public float _maxRange;//the max range the bullet will go
    public float _pen;
    public float _projectileSpeed;//the _speed of the projectile >_>
    public float _damage;//the ammount of damg this thing does
    public float _AdOrAp;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, 0.03f);
        if (transform.isDistanceSmallerThan(_target.transform.position, 1))
        {
            OnHitObject();
        }
    }

    void OnHitObject()//this wil take in information abbout the object hit
    {
        IDamageable damageableObject = _target.GetComponent<IDamageable>();//check for component idamagable on the hit object
        if (damageableObject != null)//"if object has idamagable"
        {
            damageableObject.returnCaster(_caster);
            damageableObject.TakeDamg(_damage, _pen, _scaling, _physical, _AdOrAp);//_damage it
        }
        //Debug.Log(hit.collider.gameObject.name);
        GameObject.Destroy(gameObject);//destroy this object(the projectile)
    }
}