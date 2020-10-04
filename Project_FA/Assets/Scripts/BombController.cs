using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BombController : MonoBehaviour
{
    public Rigidbody2D rigid2D;
    public CircleCollider2D circel;
    public CircleCollider2D explosion;
    public float lanchDistance;
    public float explosionTimmer;
    public float explosionLast;

    public float triggerDelay = 0.5f;

    // Start is called before the first frame update

    private void Awake()
    {
        rigid2D.AddForce(transform.right * lanchDistance, ForceMode2D.Impulse);
    }

    void Update()
    {
        triggerDelay -= Time.deltaTime;
        explosionTimmer -= Time.deltaTime;

        if (triggerDelay <= 0 && circel.isTrigger == true)
            circel.isTrigger = false;

        if(explosionTimmer <= 0)
        {
            explosion.enabled = true;
            explosionLast -= Time.deltaTime;
        }

        if (explosionLast <= 0)
            Destroy(gameObject);
    }
}
