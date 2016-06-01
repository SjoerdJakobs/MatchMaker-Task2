using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

    [SerializeField]
    private float explosionForce = 50;
    [SerializeField]
    private float explosionRadius = 5;


    [SerializeField]
    private bool absoluteKnockback = true;
    [SerializeField]
    private bool removeOnExplosion = true;


	// Use this for initialization
	void Start () {
        int count = 0;
        foreach (RaycastHit i in transform.getWithinSphere(10))
        {
            count++;
            //print(i.transform.position + " object " + count);
        }
        gameObject.explosion(explosionForce, explosionRadius, absoluteKnockback,removeOnExplosion);
        Vector3 pos = gameObject.hitByMouse().point;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
