using UnityEngine;
using System.Collections;

public class WalkingBomb : LivingEntity
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private AudioSource boom;
    protected override void Start()
    {
        base.Start();

    }

// Update is called once per frame
void Update () {
	    if(transform.isDistanceSmallerThan(target.position,2))
        {
            boom.Play();
            gameObject.selfExplodingObject(100, armorPen, true, 50, attackDamage, 20 ,4);
        }
	}
}
