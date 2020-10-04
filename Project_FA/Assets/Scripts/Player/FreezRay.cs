using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class FreezRay : MonoBehaviour
{
    public GameObject gunBarrel;
    public GameObject frizeRay;
    private GameObject _frizeRay;
    public float granadeLanchDistanse;
    public float dealy = 7f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dealy -= Time.deltaTime;
         
        if (Input.GetMouseButtonDown(3) && dealy <= 0)
        {
            _frizeRay = Instantiate(frizeRay, gunBarrel.transform.position, gunBarrel.transform.rotation);
            dealy = 7f;
        }
    }
}
