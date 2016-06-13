using UnityEngine;
using System.Collections;
public class EnemyMove : MonoBehaviour
{
    // Fix a range how early u want your enemy detect the obstacle.
    [SerializeField]
    private float width;
    [SerializeField]
    private int range;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool isThereAnyThing = false;
    [SerializeField]
    private LayerMask mask;
    // Specify the target for the enemy.
    public GameObject target;
    [SerializeField]
    private float rotationSpeed;
    private RaycastHit hit;
    // Use this for initialization
    void Start()
    {
        //range = 80;
        //speed = 10f;
        //rotationSpeed = 15f;
    }
    // Update is called once per frame
    void Update()
    {
        //Look At Somthly Towards the Target if there is nothing in front.
        if (!isThereAnyThing)
        {
            Vector3 relativePos = target.transform.position + new Vector3(0,0.5f,0) - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        }
        // Enemy translate in forward direction.
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //Checking for any Obstacle in front.
        // Two rays left and right to the object to detect the obstacle.
        Transform leftRay = transform;
        Transform rightRay = transform;
        //Use Phyics.RayCast to detect the obstacle
        if (Physics.Raycast(leftRay.position + (transform.right * width), transform.forward, out hit, range, mask) || Physics.Raycast(rightRay.position - (transform.right * width), transform.forward, out hit, range, mask))
        {
            if (hit.collider.gameObject.CompareTag("Obstacles"))
            {
                isThereAnyThing = true;
                transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
            }
        }
        // Now Two More RayCast At The End of Object to detect that object has already pass the obsatacle.
        // Just making this boolean variable false it means there is nothing in front of object.
        if (Physics.Raycast(transform.position - (transform.forward * width), transform.right, out hit, mask) ||
        Physics.Raycast(transform.position - (transform.forward * width), -transform.right, out hit, mask))
        {
            if (hit.collider.gameObject.CompareTag("Obstacles"))
            {
                isThereAnyThing = false;
            }
        }
        // Use to debug the Physics.RayCast.
        Debug.DrawRay(transform.position + (transform.right * width), transform.forward * range, Color.red);
        Debug.DrawRay(transform.position - (transform.right * width), transform.forward * range, Color.red);
        Debug.DrawRay(transform.position - (transform.forward * width), -transform.right * range, Color.yellow);
        Debug.DrawRay(transform.position - (transform.forward * width), transform.right * range, Color.yellow);
    }
}