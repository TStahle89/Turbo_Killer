using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theher : MonoBehaviour
{
    private BulletController bullCon;
    public SpriteRenderer sprite;
    public GameObject player;
    public GameObject hook;
    public GameObject _hook;
    public GameObject gunBarrel;
    private float decay = 0.005f;
    private bool fistShootchek = false;
    private bool spawnd;
    
    public float force = 10f;
    // Start is called before the first frame update
    private void Start()
    {
        sprite.enabled = false;
    }
    // Update is called once per frame
    private void Update()
    {
        Reset();

        if (Input.GetMouseButtonDown(1))
        {
            decay = 0.005f;
            fistShootchek = true;
            spawnd = true;
        }

        if (spawnd)
        {
            SpriteOnOrOf();
        }
            
    }

    private void SpriteOnOrOf()
    {
        if (bullCon.destroyed == true)
        {
            sprite.enabled = false;
        }

        else if (bullCon.destroyed == false)
        {
            sprite.enabled = true;
        }
    }

    private void Reset()
    {
        if (fistShootchek && decay > 0)
        {
            decay -= Time.deltaTime;
        }
        if (decay < 0)
        {
            decay = 0;
            fistShootchek = false;
            _hook = GameObject.FindWithTag("Hook");
            bullCon = _hook.GetComponent<BulletController>();
        }
    }

    private void PlaceInMittel()
    {
        transform.position = player.transform.position + (_hook.transform.position - player.transform.position)/2;
    }
    private void ChangeSize()
    {
        float distToHook = Vector2.Distance(player.transform.position, _hook.transform.position);
        sprite.size = new Vector2(distToHook, 1f);
    }
    void FixedUpdate()
    {

        if (spawnd)
        {
            PlaceInMittel();
            ChangeSize();
            Turn();
        }
        
    }

    private void Turn()
    {
        Vector3 vectortToPlayer = _hook.transform.position - transform.position;
        float angel = Mathf.Atan2(vectortToPlayer.y, vectortToPlayer.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angel, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * force);
    }
}
    
