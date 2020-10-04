using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public GrapelingHook hook;

    public static int hp;

    public float flashCounter;
    public float flashLength;

    public float bombRelode;

    public GameObject frizeRay;
    public GameObject gunBarrel;
    public GameObject bulletPrefab;
    public SpriteRenderer mySpriteRenderer;

    public bool isShooting;
    public float shootCooldown;
    private bool _shootIsOnCooldown;
    public GameObject playerDeathAnim;
    
    public bool isAimingUp;
    public bool isAimingDiagonal;

    public AudioSource PlayerJump;

    private Rigidbody2D _myRigidbody;
    private Animator _myAnimation;

    [NonSerialized] public float horizontalInput;
    [NonSerialized] public float verticalInput;
    [NonSerialized] public bool jump;
    [NonSerialized] public bool fireWeapon;
    [NonSerialized] public bool bombButton;


    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myAnimation = GetComponent<Animator>();
        hook = GetComponent<GrapelingHook>();

        hp = 5;
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isShooting = fireWeapon;

        //Moving Right and aiming Up = Diagonal right
        if (horizontalInput > 0f && verticalInput > 0f)
        {
            _myRigidbody.velocity = new Vector2(moveSpeed, _myRigidbody.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
            gunBarrel.transform.rotation = Quaternion.Euler(0f,0f,40f);
            isAimingDiagonal = true;
        }
        //Moving Left and aiming Up = Diagonal left
         else if (horizontalInput < 0f && verticalInput > 0f)
        {
            _myRigidbody.velocity = new Vector2(-moveSpeed, _myRigidbody.velocity.y);
            transform.localScale = new Vector2(-1f, 1f);
            gunBarrel.transform.rotation = Quaternion.Euler(0f, 0f, 140f);
            isAimingDiagonal = true;
        }
        //Moving Right + Shooting right
        else if (horizontalInput > 0f)
        {
            _myRigidbody.velocity = new Vector2(moveSpeed, _myRigidbody.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
            gunBarrel.transform.rotation = Quaternion.Euler(0f,0f,0f);
            isAimingDiagonal = false;
        }
        //Moving Left + Shooting left
        else if (horizontalInput < 0f)
        {
            _myRigidbody.velocity = new Vector2(-moveSpeed, _myRigidbody.velocity.y);
            transform.localScale = new Vector2(-1f, 1f);
            gunBarrel.transform.rotation = Quaternion.Euler(0f,0f,180f);
            isAimingDiagonal = false;
        }
        // Aiming up
        else if (verticalInput > 0f)
        {
            _myRigidbody.velocity = new Vector2(0f, _myRigidbody.velocity.y); // prevents horizontal movement while aiming up
            gunBarrel.transform.rotation = Quaternion.Euler(0f,0f,90f);
            isAimingUp = true;
            isAimingDiagonal = false;
        }
        
        else
        {
            _myRigidbody.velocity = new Vector2(0f, _myRigidbody.velocity.y);
            isAimingUp = false;
            isAimingDiagonal = false;
            TurnRightDirection();
        }

        //made it so you can jump if you are grapeling hooking
        if (jump && isGrounded || jump && hook.grapelingHooking)
        {
            _myRigidbody.velocity = new Vector2(_myRigidbody.velocity.x, jumpForce);
            hook.grapelingHooking = false;

            PlayerJump.Play();
        }
        bombRelode -= Time.deltaTime;
        if (bombButton && bombRelode <=0)
        {
            Instantiate(frizeRay, gunBarrel.transform.position, gunBarrel.transform.rotation);
            bombRelode = 7f;
        }

        if (flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;

            if (flashCounter <= 0)
            {
                mySpriteRenderer.material.SetFloat("_FlashAmount", 0);
            }
        }

        if (fireWeapon)
        {
            StartCoroutine(Shoot());
        }
        

        _myAnimation.SetFloat("Speed", Mathf.Abs(_myRigidbody.velocity.x));
        _myAnimation.SetBool("Grounded", isGrounded);
        _myAnimation.SetBool("Shoot", isShooting);
        _myAnimation.SetBool("IsAimingUp", isAimingUp);
        _myAnimation.SetBool("IsAimingDiagonal", isAimingDiagonal);
    }

    private void TurnRightDirection()
    {
        if (transform.localScale.x >= 1)
        {
            gunBarrel.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (transform.localScale.x <= -1)
        {
            gunBarrel.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
    }

    private IEnumerator Shoot()
    {
        if (_shootIsOnCooldown == false)
        {
            Instantiate(bulletPrefab, gunBarrel.transform.position, gunBarrel.transform.rotation);
            
            //Bullet Shoot Delay
            _shootIsOnCooldown = true;
            yield return new WaitForSeconds(shootCooldown);
            _shootIsOnCooldown = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyBullet")
        {
            hp--;
            Flash();
            if(hp<=0)
            gameObject.SetActive(false);

            
            Instantiate(playerDeathAnim, transform.position, transform.rotation);
        }

        if (other.tag == "Enemy")
        {
            gameObject.SetActive(false);

            Instantiate(playerDeathAnim, transform.position, transform.rotation);
        }
    }

    void Flash()
    {
        mySpriteRenderer.material.SetFloat("_FlashAmount", 1);
        flashCounter = flashLength;
    }
}
