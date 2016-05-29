using UnityEngine;
using System.Collections;

public class testCharacter : LivingEntity {

    public GameObject magicCube;
    public Transform target;
    public float testPen;
    public bool testMagic;
    // Use this for initialization
    protected override void Start () {
        base.Start();
        TakeDamgOverTime(10, 10, testPen, 25,testMagic);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.skillShotProjectile(magicCube, target.position, 30,armorPen, true, 50, 100);
            Instantiate(magicCube, transform.position, Quaternion.identity);
        }
    }
}
