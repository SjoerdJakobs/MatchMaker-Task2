using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private LayerMask collisionMask;//layer wich the projectile checks for

    public Vector3 _target;
    private Vector3 aim;

    public bool _physical;

    private float rangeTraveled;//how much the bullet has gone
    public float _maxRange;//the max range the bullet will go
    public float _pen;
    public float _projectileSpeed;//the _speed of the projectile >_>
    public float _damage;//the ammount of damg this thing does


    void Start()
    {
        rangeTraveled = 0;
        aim = (_target - transform.position).normalized;
    }
	void FixedUpdate () {
        float moveDistance = _projectileSpeed * Time.deltaTime;//this calculates the distance it moves before actualy moving
        if (_maxRange > rangeTraveled)//is the max range still bigger than range traveled?
        {
            CheckCollisions(moveDistance);
            transform.Translate(aim * moveDistance);//this moves the projectile forward
            rangeTraveled += moveDistance;//
        }
        else
        {
            GameObject.Destroy(gameObject);//destroy this object(the projectile)
        }
	}

    void CheckCollisions(float moveDistance)//checks if the projectile hits something before hitting it
    {
        Ray ray = new Ray(transform.position, (_target - transform.position).normalized);//defines a ray that gets a starting pos and a direction
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, moveDistance + .2f, collisionMask, QueryTriggerInteraction.Collide))//actual raycast, queryTriggerInteraction allows me to decide if i want it to collide with triggers to, wich is what i want in this case
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)//this wil take in information abbout the object hit
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();//check for component idamagable on the hit object
        if(damageableObject != null)//"if object has idamagable"
        {
            damageableObject.TakeDamg(_damage, _pen, _physical);//_damage it
        }
        //Debug.Log(hit.collider.gameObject.name);
        GameObject.Destroy(gameObject);//destroy this object(the projectile)
    }
}