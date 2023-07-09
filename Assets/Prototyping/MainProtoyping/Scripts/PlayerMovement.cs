using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    /**
     * The main player object. This should be the parent of all player sub-components
     */
    public GameObject player;

    private Transform playerBody;

    public float playerSpeed = 10f;
    public float mouseSensitivity = 100f;
    public float minimumVerticalLook = -90f;
    public float maximumVerticalLook = 90f;

    private float xRotation = 0f;
    
    private CharacterController movementController;

    private List<GameObject> playerComponents;

    // Start is called before the first frame update
    void Start()
    {
        
        // Get player movement controller
        movementController = player.GetComponent<CharacterController>();
        
        // Init playerComponents
        playerComponents = new List<GameObject>();
        
        // Get the transform object on the parent
        playerBody = player.transform;
        
        // With the parent transform, one can get any child objects
        foreach (Transform child in playerBody)
        {
            playerComponents.Add(child.gameObject);
        }
        
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        
        // Set player movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movementController.Move(move * Time.deltaTime * playerSpeed);
        
        // Mouse Input (Looking)
        // Get Horizontal mouse input (X)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        
        // Get Vertical mouse input (Y)
        // Clamp it (to prevent the player from breaking their neck)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        // Invert vertical mouse input
        xRotation -= mouseY;
        // Clamp the invert so that the player does not break their neck
        xRotation = Mathf.Clamp(xRotation,minimumVerticalLook,maximumVerticalLook);
        
        // Rotate around the Y-axis of the player based on horizontal mouse movement
        // Can transform everything, thus no need to get an index in playerComponents
        transform.Rotate(Vector3.up * mouseX);

        // Looking up and down based on vertical mouse movement
        // Returns a rotation around the x-axis (i.e., the horizontal axis)
        // playerComponent[0] specifies the camera object (don't want to tilt every object, just the "head")
        playerComponents[0].transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        
        // TODO Head bobbing

    }
}
