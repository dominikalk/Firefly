using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Rigidbody myRigid;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    [HideInInspector] public bool canMove;
    [SerializeField] Transform mainCamera;

    //[SerializeField] float sensitivityX;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            Vector3 forward = transform.rotation * Vector3.forward;
            Vector3 side = transform.rotation * Vector3.right;

            Vector3 movement = new Vector3();

            if (Input.GetKey(KeyCode.A))
            {
                movement -= side;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movement += side;
            }

            if (Input.GetKey(KeyCode.W))
            {
                movement += forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                movement -= forward;
            }

            Vector2 tempVector = new Vector2(movement.x, movement.z).normalized * speed;
            myRigid.velocity = new Vector3(tempVector.x, myRigid.velocity.y, tempVector.y);

            transform.rotation = Quaternion.Euler(0f, mainCamera.eulerAngles.y, 0f);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRigid.AddForce(new Vector3(0, jumpForce, 0));
            }
        }
    }
}
