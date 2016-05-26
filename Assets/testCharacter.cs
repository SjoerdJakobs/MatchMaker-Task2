using UnityEngine;
using System.Collections;

public class testCharacter : LivingEntity {

    public float testPen;
    public bool testMagic;
    // Use this for initialization
    protected override void Start () {
        base.Start();
        TakeDamgOverTime(10, 10, testPen, testMagic);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
