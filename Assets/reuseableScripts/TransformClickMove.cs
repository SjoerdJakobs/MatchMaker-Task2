using UnityEngine;
using System.Collections;

public class TransformClickMove : MonoBehaviour {
    
    private Vector3 destinationPosition;

    [SerializeField]
    private float moveMentspeed = 100f;
    private float destinationDistance;
    private float moveSpeed;

    // Use this for initialization
    void Start ()
    {
        destinationPosition = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        movement();
	}
    void movement()
    {
        destinationDistance = Vector3.Distance(destinationPosition, transform.position);

        if (destinationDistance < .5f)
        {
            moveSpeed = 0;
        }
        else if (destinationDistance > .5f)
        {
            moveSpeed = moveMentspeed / 10;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;

            if (playerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                destinationPosition = ray.GetPoint(hitdist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                transform.rotation = targetRotation;
            }
        }
        else if (Input.GetMouseButton(0))
        {

            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;

            if (playerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                destinationPosition = ray.GetPoint(hitdist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                transform.rotation = targetRotation;
            }
        }
        if (destinationDistance > .5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPosition, moveSpeed * Time.deltaTime);
        }
    }
}
