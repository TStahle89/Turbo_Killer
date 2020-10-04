using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlatformDropThru : MonoBehaviour
{
    private PlatformEffector2D _effector;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        _effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTime = 0.1f;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            if (waitTime <= 0)
            {
                _effector.rotationalOffset = 180f;
                waitTime = 0.1f;
            }
            else
            {
                waitTime -= Time.deltaTime;
                
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _effector.rotationalOffset = 0;
        }
    }
}
