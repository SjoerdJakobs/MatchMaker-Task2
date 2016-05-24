using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class testScript : MonoBehaviour {
    [SerializeField]
    private Transform otherTrans;
    [SerializeField]
    private float range;
    [SerializeField]
    private float maxVelocity = 20;
    [SerializeField]
    private float pushBackForce = 10;
    [SerializeField]
    private float dragg = 1;
    private Rigidbody rigid;
	// Use this for initialization
	void Start ()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
	    if(transform.isDistanceBiggerThan(otherTrans.position, range))
        {
            print("out of range");
        }
        else
        {
            print("in range");
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rigid.limitVelocitySoft3D(maxVelocity,pushBackForce);
        //rigid.limitVelocityHard3D(maxVelocity);
        //rigid.airDragg3D(dragg);
	}
}
