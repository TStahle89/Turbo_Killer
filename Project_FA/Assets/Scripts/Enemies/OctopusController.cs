using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OctopusController : MonoBehaviour
{
    public float moveSpeed;
    public float lineOfSight;
    public float shootRange;
    public float fireRate;
    public float frizeTimer;
    private float timer;
    private bool frozen = false;
    public bool grapeld = false;
    private float randomMovement;
    public int hp;
    public int randomShootAmount;
    private float _nextFireTime;
    public GameObject enemyBulletPrefab;
    public GameObject enemyBulletSpawn;
    private GameObject hook;
    public SpriteRenderer rend;
    private Transform _player;
    private Rigidbody2D rigid2D;
    public GameObject enemyDeathAnim;
    
    //Material
    public SpriteRenderer mySpriteRenderer;
    public float flashLength;
    public float flashCounter;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        rigid2D = GetComponent<Rigidbody2D>();
        randomMovement = UnityEngine.Random.Range(1f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(_player.position, transform.position);

        if (distanceFromPlayer > shootRange && frozen == false) // randomShootAmount <= 0
        {

            rigid2D.velocity = Vector2.Lerp(rigid2D.velocity, (_player.position - transform.position).normalized * moveSpeed * randomMovement, Time.deltaTime);


            //randomShootAmount = UnityEngine.Random.Range(1,3);
        }

        if (transform.position.y < _player.position.y && frozen == false)
        {
            

            rigid2D.AddForce(Vector2.up * moveSpeed * randomMovement * Time.deltaTime);
        }

        if (distanceFromPlayer <= shootRange && _nextFireTime < Time.time && frozen == false)
        {
            Instantiate(enemyBulletPrefab, enemyBulletSpawn.transform.position, enemyBulletSpawn.transform.rotation);
            _nextFireTime = Time.time + fireRate;
            //randomShootAmount--;
        }

        if (frozen)
        {
            rigid2D.bodyType = RigidbodyType2D.Static;
            timer -= Time.deltaTime;
            hook = GameObject.FindWithTag("Hook");

            if (hook == null)
            {
                Debug.Log("nothing");
                grapeld = false;
            }
        }

        if (flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;

            if (flashCounter <= 0)
            {
                mySpriteRenderer.material.SetFloat("_FlashAmount", 0);
            }
        }

        
        

        if (timer<= 0 && frozen && grapeld == false)
        {
            frozen = false;
            rend.color = Color.white;
            gameObject.tag = "FlyingEnemy";
            rigid2D.bodyType = RigidbodyType2D.Kinematic;
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
            Flash();
            if(hp<= 0)
            {
                Flash();
                ScoreDisplay.scoreValue += 10;

                Destroy(gameObject);

                Instantiate(enemyDeathAnim, transform.position, transform.rotation);
            }
           
        }
        else if(other.tag == "Fireez")
        {
            gameObject.tag = "Frozen";
            frozen = true;
            timer = frizeTimer;
            rend.color = Color.blue;
        }

        if(other.tag == "Hook" && frozen)
        {
            grapeld = true;
        }
    }

    public void Flash()
    {
        mySpriteRenderer.material.SetFloat("_FlashAmount", 1);
        flashCounter = flashLength;
    }
    
}
