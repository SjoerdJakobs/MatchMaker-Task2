using UnityEngine;
using System.Collections;

public class camScript : MonoBehaviour {
    [SerializeField]
    private Transform followObject;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void LateUpdate()
    {
        if (followObject != null)
        {
            //print(followObject.position);
            transform.position = new Vector3(followObject.position.x, transform.position.y, transform.position.z);
        }
    }
}
