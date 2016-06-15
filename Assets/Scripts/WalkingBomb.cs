using UnityEngine;
using System.Collections;

public class WalkingBomb : LivingEntity
{
    private Transform target;
    private Transform followTarget;

    [SerializeField]
    private AudioSource boom;
    protected override void Start()
    {
        base.Start();
        followTarget = GameObject.FindGameObjectWithTag("dwarf").transform;
        target = followTarget;
        
    }

// Update is called once per frame
void Update () {
	    if(transform.isDistanceSmallerThan(target.position,2))
        {
            target = followTarget;
            gameObject.selfExplodingObject(100, armorPen, true, 50, attackDamage, 20 ,4);
        }
	}
}
