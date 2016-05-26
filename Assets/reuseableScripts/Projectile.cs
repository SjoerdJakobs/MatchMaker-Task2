using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private LayerMask collisionMask;//layer wich the projectile checks for

    [SerializeField]
    private float maxRange = 100;//the max range the bullet will go
    private float rangeTraveled;//how much the bullet has gone
    [SerializeField]
    private float projectileSpeed = 10;//the _speed of the projectile >_>
    [SerializeField]
    private float damage = 1;//the ammount of damg this thing does

    //Vector3
    private Vector3 myRotation;
    //Vector3

    //GameObject
    private GameObject getPlayerRotation;
    //GameObject

    public void SetSpeed(float newSpeed)//here otherclasses can change the _speed of the projectile(incase of more guns or something like that)
    {
        projectileSpeed = newSpeed;
    }

    void Start()
    {
        rangeTraveled = 0;
        getPlayerRotation = GameObject.FindGameObjectWithTag("Player");
        myRotation = getPlayerRotation.transform.TransformDirection(Vector3.forward);
    }
	void FixedUpdate () {
        float moveDistance = projectileSpeed * Time.deltaTime;//this calculates the distance it moves before actualy moving
        if (maxRange > rangeTraveled)//is the max range still bigger than range traveled?
        {
            CheckCollisions(moveDistance);
            transform.Translate(myRotation * moveDistance);//this moves the projectile forward
            rangeTraveled += moveDistance;//
        }
        else
        {
            GameObject.Destroy(gameObject);//destroy this object(the projectile)
        }
	}

    void CheckCollisions(float moveDistance)//checks if the projectile hits something before hitting it
    {
        Ray ray = new Ray(transform.position, transform.forward);//defines a ray that gets a starting pos and a direction
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
            damageableObject.TakeTrueDamg(damage);//damage it
        }
        //Debug.Log(hit.collider.gameObject.name);
        GameObject.Destroy(gameObject);//destroy this object(the projectile)
    }
}