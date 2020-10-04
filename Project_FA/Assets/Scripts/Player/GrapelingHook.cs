using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapelingHook : MonoBehaviour
{
    public Rigidbody2D _myRigidbody;
    public SpringJoint2D springJoint;
    public GameObject hook;
    private GameObject _hook;
    private BulletController bullCon;
    public GameObject gunBarrel;
    private Animator _myAnimation;
    private bool firstShot = false;
    [Tooltip("the max range of the grapeling hook. it's also the range of how far the hook can go")]
    public float grapelingMaxRange; // Max range the hook can be fired before destroying itself
    public float hookMaxRange = 5f; // The range that the player is allowed to descend from the hook point while hanging
    public float chosenHookRange = 5f; // The range the player chooses that the range is
    public bool grapelingHooking;
    public float force;
    public float changeForce;
    private float delay;

    public bool jumpout;

    public bool grapelingbutton;

    public float changeRangeSpeed;
    

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myAnimation = GetComponent<Animator>();
        springJoint = GetComponent<SpringJoint2D>();
        
        chosenHookRange = hookMaxRange;
        springJoint.enabled = false;
    }
    private void Update()
    {
        delay -= Time.deltaTime;
        if (grapelingbutton && delay <= 0)
        {
            _hook = Instantiate(hook, gunBarrel.transform.position, gunBarrel.transform.rotation);
            bullCon = _hook.GetComponent<BulletController>();
            firstShot = true;
            springJoint.connectedBody = _hook.GetComponent<Rigidbody2D>();
            springJoint.enabled = true;
            delay = 1f;
        }

        if (firstShot == false)
            return;

        if (jumpout && bullCon.isInWall)
        {
            bullCon.destroy = true;
        }
            
        
  
    }

   /* private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W) && chosenHookRange > 0)
        {
            chosenHookRange -= changeRangeSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.S) && chosenHookRange < hookMaxRange)
        {
            chosenHookRange += changeRangeSpeed * Time.deltaTime;
        }
    } */

    private void FixedUpdate()
    {
        if (firstShot == false || bullCon.destroyed == true)
            return;
        if (bullCon.isInWall == true)
            grapelingHooking = true;
        
 
        springJoint.distance = chosenHookRange;

        float distToHook = Vector2.Distance(transform.position, _hook.transform.position);

        if(distToHook > grapelingMaxRange && bullCon.isInWall == false)
        {
            bullCon.destroyed = true;
            Destroy(_hook);
        }

    }

    
    
    private void TurnToHook()
    {
        Vector3 vectortToPlayer = _hook.transform.position - transform.position;
        float angel = Mathf.Atan2(vectortToPlayer.y, vectortToPlayer.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angel, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * force);
    }
}
