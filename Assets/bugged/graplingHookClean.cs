using UnityEngine;
using System.Collections;

public class graplingHookClean : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform hook;
    [SerializeField]
    private Material mat;
    private Rigidbody rigidPlayer;
    private WaitForFixedUpdate waitForFrame;
    private PlayerMovement playerMov;
    


    [SerializeField]
    private float maxFireRange;
    [SerializeField]
    private float forceHook;
    [SerializeField]
    private float forcePull;
    [SerializeField]
    private float hookSpeed;
    private float fireRange;
    private float xScale;



    private bool hitSomething;
    private bool outRanged;
    private bool hooked;
    private bool drawTheLine;

    // Use this for initialization
    void Start ()
    {
        hooked = false;
        hitSomething = false;
        outRanged = false;
        hook.transform.parent = null;
        rigidPlayer = player.GetComponent<Rigidbody>();
        playerMov = player.GetComponent<PlayerMovement>();
        
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !outRanged)
        {
            if(!drawTheLine)
            {
                StartCoroutine(grapler());
                drawTheLine = true;
            }
            //print("er is input");
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(transform.position, transform.forward, out hit, fireRange))
            {
                //print("iets geraakt");
                fireRange = 0;
                if (!hitSomething)
                {
                    hooked = true;
                    hook.position = hit.point;
                    //print(hook.position);
                    //print(hook.localPosition);
                }
                hitSomething = true;
            }
            else
            {
                //print("er word geschoten");
                if (fireRange <= maxFireRange && !hitSomething)
                {
                    fireRange += hookSpeed;
                }
                else if (!hitSomething)
                {
                    fireRange = 0;
                    outRanged = true;
                    StopCoroutine(grapler());
                }
            }
            if (hooked == true)
            {
                playerMov._useGravity = false;
                float distance = (player.transform.position - hook.transform.position).sqrMagnitude;
                if (distance <= 1.5f)
                {
                    rigidPlayer.velocity = new Vector3(0, 0, 0);
                    if (Input.GetButton("Jump"))
                    {
                        rigidPlayer.velocity = new Vector3(0, 7, 0);
                        hooked = false;
                        playerMov._useGravity = true;
                        hitSomething = false;
                        outRanged = true;
                    }
                }
                else if( distance <= 35)
                {
                    rigidPlayer.AddForce((hook.transform.position - player.transform.position) * forcePull * Time.fixedDeltaTime, ForceMode.VelocityChange);
                }
                else
                {
                    rigidPlayer.AddForce((hook.transform.position - player.transform.position + (player.transform.forward * (forcePull * 5))) * forcePull * Time.fixedDeltaTime, ForceMode.VelocityChange);
                }
            }
       }
        else
        { 
            if(drawTheLine)
            {
                StopCoroutine(grapler());
            }
            fireRange = 0f;
            outRanged = false;
            hooked = false;
            hitSomething = false;
            drawTheLine = false;
            playerMov._useGravity = true;
        }
        if (!hitSomething)
        {
            hook.rotation = transform.rotation;
            hook.position = transform.position + transform.forward.normalized * (fireRange);
        }
    }


    IEnumerator grapler()
    {
        while (true)
        {
            xScale = Vector3.Distance(transform.position, hook.position);
            GameObject grapple = new GameObject();
            grapple.name = "Line";
            grapple.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            grapple.AddComponent<LineRenderer>();
            mat.mainTextureScale = new Vector2(xScale, 1);
            LineRenderer lineRender = grapple.GetComponent<LineRenderer>();
            lineRender.material = mat;
            lineRender.SetWidth(0.2f, 0.2f);
            lineRender.SetPosition(0, grapple.transform.position);
            lineRender.SetPosition(1, hook.position);
            yield return waitForFrame;
            GameObject.Destroy(grapple);
        }
    }
}
