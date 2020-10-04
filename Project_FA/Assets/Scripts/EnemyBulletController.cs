using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public GameObject target;
    public float bulletSpeed;
    private Rigidbody2D _myRigidbody;
    public GameObject bulletImpactEffect;

    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * bulletSpeed;
        _myRigidbody.velocity = new Vector2(moveDir.x, moveDir.y);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x > Screen.width || screenPosition.x < 0 || screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(bulletImpactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
