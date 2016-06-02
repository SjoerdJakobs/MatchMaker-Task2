using UnityEngine;
using System.Collections;

public class testCharacter : LivingEntity {

    public GameObject magicCube;

    private magicMissile magicMiss;
    private magicProjectile magicProj;
    private giantsSpell giant;
    // Use this for initialization
    protected override void Start () {
        base.Start();
        TakeDamgOverTime(10, 10, magicPen, 25,false);
        magicProj = GetComponent<magicProjectile>();
        magicMiss = GetComponent<magicMissile>();
        giant = GetComponent<giantsSpell>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.lookAtMouse(10);
        checkInput();
    }
    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            magicProj.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            magicMiss.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            giant.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            magicProj.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            magicProj.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            magicProj.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            magicProj.shoot(magicPen, cooldownReduction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            magicProj.shoot(magicPen, cooldownReduction);
        }
    }
}
