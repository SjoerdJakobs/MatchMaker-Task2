using UnityEngine;
using System.Collections;

public class FollowingProjectile : MonoBehaviour
{

    [SerializeField]
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
    public float _speed;

    void Update()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed/100);
            if (transform.isDistanceSmallerThan(_target.transform.position, 1))
            {
                OnHitObject();
            }
        }
        else if(_target == null)
        {
            GameObject.Destroy(gameObject);//destroy this object(the projectile)
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