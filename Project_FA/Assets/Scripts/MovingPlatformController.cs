using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public GameObject movingPlatform;

    public Transform downPoint;
    public Transform upPoint;
    public float moveSpeed;
    private Vector2 _currentWaypoint;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentWaypoint = downPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        movingPlatform.transform.position = Vector3.MoveTowards
            (movingPlatform.transform.position, _currentWaypoint, moveSpeed * Time.deltaTime);

        if (movingPlatform.transform.position == downPoint.position)
        {
            _currentWaypoint = upPoint.position;
        }

        if (movingPlatform.transform.position == upPoint.position)
        {
            _currentWaypoint = downPoint.position;
        }
    }
}
