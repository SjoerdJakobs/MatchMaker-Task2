using UnityEngine;
using System.Collections;

public class magicProjectile : MonoBehaviour, ISkill {

    [SerializeField]
    private GameObject magicCube;

    LivingEntity caster;
	// Use this for initialization
	void Start () {
        caster = GetComponent<LivingEntity>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void shoot(float magicPen)
    {
        Vector3 target = transform.mousePos();
        gameObject.skillShotProjectile(magicCube, target, 30, magicPen, false, 50, 20);
        Instantiate(magicCube, transform.position, Quaternion.identity);
    }
}
