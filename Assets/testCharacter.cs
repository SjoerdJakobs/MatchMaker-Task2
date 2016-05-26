using UnityEngine;
using System.Collections;

public class testCharacter : LivingEntity {

    // Use this for initialization
    protected override void Start () {
        base.Start();
        TakeDamgOverTime(10, 10, 1, true);


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
