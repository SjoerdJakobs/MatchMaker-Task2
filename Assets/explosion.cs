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
        gameObject.explosion(explosionForce, explosionRadius, absoluteKnockback,removeOnExplosion);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
