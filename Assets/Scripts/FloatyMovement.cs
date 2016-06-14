using UnityEngine;
using System.Collections;

public class FloatyMovement : MonoBehaviour {

    [SerializeField]
    private Transform target;

    private Rigidbody rigid;

    [SerializeField]
    private float speed;
	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float targetDist = Vector3.Distance(target.position, transform.position);
        rigid.AddForce(((new Vector3(target.position.x , 1, target.position.z) - transform.position).normalized * Mathf.Clamp(speed / targetDist,0,500)), ForceMode.Impulse);// = Vector3.Lerp(transform.position, destinationPosition, (moveMentspeed) * Time.fixedDeltaTime);
        rigid.limitVelocitySoft3D(speed, 20);
    }
}
