using UnityEngine;
using System.Collections;

public class testCharacter : LivingEntity {

    public GameObject magicCube;

    private magicProjectile magicProj;
    // Use this for initialization
    protected override void Start () {
        base.Start();
        TakeDamgOverTime(10, 10, magicPen, 25,false);

    }

    // Update is called once per frame
    void Update()
    {
        transform.lookAtMouse(10);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 target = transform.mousePos();
            gameObject.skillShotProjectile(magicCube, target, 30, armorPen, true, 50, 100);
            Instantiate(magicCube, transform.position, Quaternion.identity);
            //TakeDamgOverTime(10, 10, testPen, 25, testMagic);
        }
    }
}
/*public class SkillShot
{
    Projectile projectil;
    private float _cooldown;
    public float Cooldown
    {
        get { return _cooldown; }
    }

    bool _onCooldown;

    IEnumerator Cooldown()
    {
        float timer = _cooldown;
        _onCooldown = true;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        _onCooldown = false;
    }
}*/
