using UnityEngine;
using System.Collections;

public class magicProjectile : MonoBehaviour, ISkill {

    LivingEntity caster;
	// Use this for initialization
	void Start () {
        caster = GetComponent<LivingEntity>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void shoot()
    {
        Vector3 target = transform.mousePos();
        gameObject.skillShotProjectile(magicCube, target, 30, armorPen, true, 50, 100);
        Instantiate(magicCube, transform.position, Quaternion.identity);
    }
}
