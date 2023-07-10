using System.Collections.Generic;
using Prototyping.MainProtoyping.Scripts;
using UnityEngine;
using EventType = Prototyping.MainProtoyping.Scripts.EventType;

public class PlayerMovement : MonoBehaviour, Observable<MovementEvent>
{
    
    /**
     * The main player object. This should be the parent of all player sub-components
     */
    public GameObject player;
    public float playerSpeed = 10f;

    private CharacterController movementController;
    
    // Create storage to keep track of Observers for this class
    private List<Observer<MovementEvent>> observers = new List<Observer<MovementEvent>>();
    // Create movement events to pass onto Observers
    private MovementEvent moving = new MovementEvent(EventType.MOVING);
    private MovementEvent stopped = new MovementEvent(EventType.STOPPED);


    // Start is called before the first frame update
    void Start()
    {
        
        // Get player movement controller
        movementController = player.GetComponent<CharacterController>();
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        // Set player movement
        // Vector3 has a special implementation of the "*" operator, this will multiply ALL axis by the float value
        // "+" adds corresponding axis, i.e. vector1.x + vector2.x, vector1.y + vector2.y,....
        Vector3 move = (player.transform.right * x) + (player.transform.forward * z);
        // Move the player, make it time dependent
        movementController.Move(move * Time.deltaTime * playerSpeed);
        
        // Check if the player is currently stopped or moving (i.e., is the Vector move set all axis to zero or not)
        if (move != Vector3.zero)
        {
            notifyObservers(this.moving);
        }
        else
        {
            notifyObservers(this.stopped);
        }
    }
    
    
    // Observing methods
    public bool addObserver(Observer<MovementEvent> observer)
    {
        if (!this.observers.Contains(observer))
        {
            this.observers.Add(observer);
            return true;
        }
        return false;
    }

    public bool removeObserver(Observer<MovementEvent> observer)
    {
        if (this.observers.Contains(observer))
        {
            this.observers.Remove(observer);
            return true;
        }
        return false;
    }

    public void notifyObservers(MovementEvent eventToFire)
    {
        foreach(Observer<MovementEvent> currentObserver in this.observers)
        {
            currentObserver.handleUpdate(eventToFire);
        }
        
    }
}
