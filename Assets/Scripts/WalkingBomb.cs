using UnityEngine;
using System.Collections;

public class WalkingBomb : LivingEntity
{
    [SerializeField]
    private Transform target;
    protected override void Start()
    {
        base.Start();

    }

// Update is called once per frame
void Update () {
	    if(transform.isDistanceSmallerThan(target.position,2))
        {
            gameObject.selfExplodingObject(100, armorPen, true, 50, attackDamage, 10 ,4);
        }
	}
}
