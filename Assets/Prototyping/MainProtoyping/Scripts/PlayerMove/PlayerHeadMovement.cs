using System.Collections;
using System.Collections.Generic;
using Prototyping.MainProtoyping.Scripts;
using UnityEngine;
using EventType = Prototyping.MainProtoyping.Scripts.EventType;

public class PlayerHeadMovement : MonoBehaviour, Observer<MovementEvent>
{

    public GameObject player;
    public Camera playerCamera;
    public float bobbingSpeed;
    public float bobbingAmplitude;
    public float returnSpeed;

    private float defaultYPos;
    private Quaternion defaultRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        
        // Get PlayerMovement script from object to observe it
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        movement.addObserver(this);
        
        // Get default positions (for resetting position when stopped)
        defaultYPos = playerCamera.transform.localPosition.y;
        defaultRotation = playerCamera.transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Handle update is invoked when PlayerMovement fires a MovementEvent
    public void handleUpdate(MovementEvent eventFired)
    {
        
        // Check the event type
        if (eventFired.getEventType() == EventType.STOPPED)
        {
            // Player is not moving so reset position
            // local position means position relative to parent object (the camera (Head) is under the Player object so it has a relative position to the Player object of 0,some y,0)
            if (playerCamera.transform.localPosition.y != defaultYPos)
            {   
                // Y Axis Movement
                // Create a new Vector3 with the current local position of the camera, (keeping in mind x and z are 0 always)
                Vector3 newPosition = new Vector3(0,playerCamera.transform.localPosition.y,0);
                // Edit the y position of the Vector object, gradually move the y position of the camera back to the default position using Lerp, make it time (not frame) dependent
                newPosition.y = Mathf.Lerp(playerCamera.transform.localPosition.y, defaultYPos, Time.deltaTime*returnSpeed);
                // Then set the new position calculated from Lerp to the camera
                playerCamera.transform.localPosition = newPosition;

            }
            
            // Same idea applies for axis rotation
            if (playerCamera.transform.localRotation != defaultRotation)
            {
                // Z Axis Rotation
                Quaternion newRotation = playerCamera.transform.localRotation;
                newRotation.x = Mathf.Lerp(playerCamera.transform.localRotation.x, defaultRotation.x, Time.deltaTime*returnSpeed);
                newRotation.y = Mathf.Lerp(playerCamera.transform.localRotation.y, defaultRotation.y, Time.deltaTime*returnSpeed);
                newRotation.z = Mathf.Lerp(playerCamera.transform.localRotation.z, defaultRotation.z, Time.deltaTime*returnSpeed);
                playerCamera.transform.localRotation = newRotation;
            }
            
        } else if (eventFired.getEventType() == EventType.MOVING)
        {
            // Player is moving so move the camera' position to create the bobbing (both transformational and rotational)
            Vector3 pos = new Vector3(0,playerCamera.transform.localPosition.y,0);
            // Edit the desired axis
            pos.y += Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmplitude;
            // Apply edited position to camera
            playerCamera.transform.localPosition = pos;

            Quaternion rotation = playerCamera.transform.localRotation;
            rotation.y +=  Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmplitude;
            rotation.x +=  Mathf.Sin(Time.time * bobbingSpeed*2) * bobbingAmplitude*2;
            playerCamera.transform.localRotation = rotation;
        }
        
    }
}
