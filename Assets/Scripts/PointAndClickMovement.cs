using UnityEngine;
using System.Collections;

public class PointAndClickMovement : MonoBehaviour {

    [SerializeField]
    private LayerMask mask;

    private NavMeshAgent agent;

    private Vector3 target;
    
    private Rigidbody rigid;

    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float originalSpeed;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        rigid = gameObject.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rigid.limitVelocitySoft3D(2.5f, 15);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, mask))
            {
                target = transform.mousePos();
            }
        }
        else if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, mask))
            {
                target = transform.mousePos();
                //print(destinationPosition);
            }
        }
        float velocity = Vector3.Magnitude(rigid.velocity);
        if (velocity < 2.5f)
        {
            rigid.velocity = Vector3.zero;
        }
        agent.SetDestination(target);
        agent.speed = speed;
    }
}
