using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public bool isHook; // Cheks if shoot is hook or not
    public bool isInWall;
    public bool destroyed = false;
    public float bulletSpeed;
    private Rigidbody2D _myRigidbody;
    GameObject[] hookShoot;
    List<BulletController> hooks = new List<BulletController>();
    bool isActive = false;

    // Moved every thing from start to awake. As awake is when a object is created while start is when the game starts.
    private void Awake()
    {
        hookShoot = GameObject.FindGameObjectsWithTag("Hook");
        foreach (var hook in hookShoot)
        {
            hooks.Add(hook.GetComponent<BulletController>());
        }

        _myRigidbody = GetComponent<Rigidbody2D>();

        _myRigidbody.velocity = transform.right * bulletSpeed;

        CheckIfThereIsActiveBullets();
        isActive = true;
        isInWall = false;
    }


    void CheckIfThereIsActiveBullets() 
    {
        foreach (var hook in hooks)
        {
            if (hook.isActive && gameObject.tag == "Hook")
            {
                Destroy(hook.gameObject);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameObject.tag == "Hook" && isInWall == true)
        {
            destroyed = true;
            Destroy(gameObject);
        }
    }

    // changed Update to FixedUpdate, as FixedUpdate is better with physiks as it is alwas tiking. while Update is on frames, so frame drops have effekts on the game.
    void FixedUpdate()
    {
        transform.Translate(bulletSpeed * Time.deltaTime, 0f,0f);
        
        // Destroy bullets when exiting camera view
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x > Screen.width || screenPosition.x < 0 || screenPosition.y > Screen.height || screenPosition.y < 0 && gameObject.tag != "Hook")
        {
            destroyed = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && isHook == false)
        {
            Destroy(gameObject);
        }

        else if (other.tag == "Rock" && isHook == true)
        {
            transform.position = transform.position;
            bulletSpeed = 0f;
            _myRigidbody.bodyType = RigidbodyType2D.Static;
            isInWall = true;
        }
    }
}
