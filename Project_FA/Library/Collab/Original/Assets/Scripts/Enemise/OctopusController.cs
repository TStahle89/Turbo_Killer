using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OctopusController : MonoBehaviour
{
    public float moveSpeed;
    public float lineOfSight;
    public float shootRange;
    public float fireRate;
    private float randomMovement;
    public int hp;
    public int randomShootAmount;
    private float _nextFireTime;
    public GameObject enemyBulletPrefab;
    public GameObject enemyBulletSpawn;
    private Transform _player;
    private Rigidbody2D rigid2D;
    public GameObject enemyDeathAnim;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        rigid2D = GetComponent<Rigidbody2D>();
        randomMovement = UnityEngine.Random.Range(1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(_player.position, transform.position);

        if (distanceFromPlayer > shootRange) // randomShootAmount <= 0
        {
            /* transform.position = Vector2.MoveTowards
                 (this.transform.position, _player.position, moveSpeed * Time.deltaTime);
            */
            rigid2D.velocity = Vector2.Lerp(rigid2D.velocity, (_player.position * randomMovement - transform.position).normalized * moveSpeed, Time.deltaTime);


            //randomShootAmount = UnityEngine.Random.Range(1,3);
        }

        if (transform.position.y < _player.position.y)
        {
            

            rigid2D.AddForce(Vector2.up * moveSpeed * Time.deltaTime);
        }

        else if (distanceFromPlayer <= shootRange && _nextFireTime < Time.time)
        {
            Instantiate(enemyBulletPrefab, enemyBulletSpawn.transform.position, enemyBulletSpawn.transform.rotation);
            _nextFireTime = Time.time + fireRate;
            //randomShootAmount--;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            hp--;
            if(hp<= 0)
            {
                Destroy(gameObject);

                Instantiate(enemyDeathAnim, transform.position, transform.rotation);
            }
           
        }
    }
}
