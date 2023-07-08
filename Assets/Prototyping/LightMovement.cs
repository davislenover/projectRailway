using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{

    public GameObject lightObject;
    private Vector3 startPos = new Vector3(-38.14f, 8.17f, 16.74f);
    private float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        lightObject.transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        
        timer = 0f;
        if (lightObject.transform.position.x > 70f)
        {
            lightObject.transform.position = startPos;
        }
        else
        {
            lightObject.transform.Translate(30*Time.deltaTime,0,0);
        }

    }
}
