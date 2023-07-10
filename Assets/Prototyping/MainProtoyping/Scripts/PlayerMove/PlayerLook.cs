using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    
    /**
     * The main player object. This should be the parent of all player sub-components
     */
    public GameObject player;
    
    /**
     * The head (Camera) of the player
     */
    public Camera playerCamera;
    
    public float mouseSensitivity = 100f;
    public float minimumVerticalLook = -90f;
    public float maximumVerticalLook = 90f;

    private float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        player.transform.Rotate(Vector3.up * mouseX);

        // Looking up and down based on vertical mouse movement
        // Returns a rotation around the x-axis (i.e., the horizontal axis)
        // playerComponent[0] specifies the camera object (don't want to tilt every object, just the "head")
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
    }
}
