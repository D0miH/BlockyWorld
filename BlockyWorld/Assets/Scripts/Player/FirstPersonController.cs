using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class FirstPersonController : MonoBehaviour {

    // the sensitivity of the mouse
    [SerializeField]
    public float sensitivity = 5.0f;
    // the walking speed of the player
    [SerializeField]
    public float walkingSpeed = 15f;
    [SerializeField]
    public float jumpForce = 10f;

    // the rigidbody and capsule collider components
    Rigidbody rb;
    CapsuleCollider cc;

    // Start is called before the first frame update
    void Start() {
        // get the rb and cc component
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update() {
        // handle the input
        HandleKeyInput();
        HandleMouseInput();
    }

    void FixedUpdate() {
        MovePlayer();
    }

    /// <summary>
    /// Handles keyboard input for the player
    /// </summary>
    void HandleKeyInput() {
        // unlock the cursor when escape is pressed
        if (Input.GetKeyDown("escape")) {
            Cursor.lockState = CursorLockMode.None;
        }

        // if the space bar is pressed, move the player up
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Moves the player when horizontal or vertical input is used.
    /// </summary>
    void MovePlayer() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(horizontal, 0, vertical);
        moveVector = moveVector * walkingSpeed * Time.deltaTime;
        transform.Translate(moveVector);
    }

    /// <summary>
    /// Handles the mouse input for the player
    /// </summary>
    void HandleMouseInput() {
        // if the cursor is not locked, just return
        if (Cursor.lockState == CursorLockMode.None) {
            return;
        }

        float xMovement = Input.GetAxisRaw("Mouse X") * sensitivity;
        float yMovement = Input.GetAxisRaw("Mouse Y") * sensitivity;

        // apply the x-movement (rotation around y-axis) to the rigidbody
        transform.Rotate(Vector3.up * xMovement);
        // apply the y-movement (rotation around the x-axis) to the camera)
        transform.GetChild(0).transform.Rotate(Vector3.right * -yMovement);

        // handle any click event
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit raycastHit;
            Transform camera = transform.GetChild(0).transform;
            if (Physics.Raycast(camera.position, camera.forward, out raycastHit, 100)) {
                Vector3 blockPos = EditTerrain.GetBlockPos(raycastHit);
                EditTerrain.SetBlock(raycastHit, new AirBlock(blockPos));
            }
        }
    }

    /// <summary>
    /// Is called when the game window is focused or unfocused.
    /// </summary>
    /// <param name="hasFocus">Indicates whether the window has focus or not</param>
    void OnApplicationFocus(bool hasFocus) {
        // if game is focused, set the cursor to locked
        if (hasFocus) {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
