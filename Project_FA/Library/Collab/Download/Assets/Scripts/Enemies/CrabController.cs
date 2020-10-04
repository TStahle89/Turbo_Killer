using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CrabController : MonoBehaviour
{
    public float moveSpeed;
    public Transform leftWaypoint, rightWaypoint;
    private bool _movingRight;
    public int hp;

    public GameObject enemyDeathAnim;

    private Rigidbody2D _myRigidbody;
    private Animator _myAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myAnimation = GetComponent<Animator>();

        leftWaypoint.parent = null;
        rightWaypoint.parent = null;

        _movingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_movingRight)
        {
            _myRigidbody.velocity = new Vector2(moveSpeed, _myRigidbody.velocity.y);
            transform.localScale = new Vector2(-1.5f, 1.5f);

            if (transform.position.x > rightWaypoint.position.x)
            {
                _movingRight = false;
            }
        }
        else
        {
            _myRigidbody.velocity = new Vector2(-moveSpeed, _myRigidbody.velocity.y);
            transform.localScale = new Vector2(1.5f, 1.5f);

            if (transform.position.x < leftWaypoint.position.x)
            {
                _movingRight = true;
            }
        }
        
        _myAnimation.SetFloat("Speed", Mathf.Abs(_myRigidbody.velocity.x));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            hp--;
            Instantiate(enemyDeathAnim, transform.position, transform.rotation);
            if (hp<= 0)
            {
                Destroy(gameObject);

                Instantiate(enemyDeathAnim, transform.position, transform.rotation);
            }
            
        }
    }
}
