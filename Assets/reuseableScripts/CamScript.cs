using UnityEngine;
using System.Collections;

public class CamScript : MonoBehaviour {    
       
    //transforms
    [SerializeField]
    private Transform target;

    //bools
    [SerializeField]
    private bool lockCursor = true;
    private bool lockedCursor = true;

    //floats
    [SerializeField]
    private float sensitivityY = 2f;
    [SerializeField]
    private float targetHeight = 0.6f; 
    [SerializeField]
    private float sensitivityX = 2f;
    [SerializeField]
    private float xRotation;
    private float yRotation;

    private Quaternion camLockRot;
    private Quaternion camRotation;
    private Quaternion targetLockRot;
    private Quaternion targetRotation;

    // Use this for initialization
    void Start()
    {
        targetRotation = target.rotation;
        camRotation = transform.rotation;
        targetRotation = Quaternion.identity;
        camRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FPcam();
        if (lockCursor)
        {
            lockUpdate();
        }
    }

    // hier zit alles voor de firstperson camera
    void FPcam()
    {
        xRotation = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
        yRotation += Input.GetAxis("Mouse Y") * sensitivityY;
        yRotation = Mathf.Clamp(yRotation, -80, 80);
        //print("test1");
        transform.localEulerAngles = new Vector3(-yRotation, xRotation, 0);
        transform.position = new Vector3(target.position.x, (target.position.y + targetHeight), target.position.z);
        target.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    private void lockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            lockedCursor = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lockedCursor = true;
        }
        if (lockedCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!lockedCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
