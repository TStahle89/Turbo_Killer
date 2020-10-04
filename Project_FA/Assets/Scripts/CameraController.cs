using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float cameraSpeed;
    public float minHeight, maxHeight;
    public float maxLeft, maxRight;
    
    public Transform backGround, middleGround;
    
    private Vector2 _lastPos;
  

    // Start is called before the first frame update
    void Start()
    {
        _lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y), cameraSpeed * Time.deltaTime);

        float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        float clampedX = Mathf.Clamp(transform.position.x, maxLeft, maxRight);
        transform.position = new Vector2(clampedX, clampedY);
        
        Vector2 amountToMove = new Vector2(transform.position.x - _lastPos.x, transform.position.y - _lastPos.y);

        backGround.position = backGround.position + new Vector3(amountToMove.x,amountToMove.y, 0f);
        middleGround.position += new Vector3(amountToMove.x, amountToMove.y,0f) * .5f;
        
        _lastPos = transform.position;
    }
}
