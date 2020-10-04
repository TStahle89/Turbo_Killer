using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapelingHook : MonoBehaviour
{

    public GameObject hook;
    public GameObject gunBarrel;
    [Tooltip("the max range of the grapeling hook")]
    public float grapelingMaxRange;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(hook, gunBarrel.transform.position, gunBarrel.transform.rotation);
        }
        float distToHook = Vector2.Distance(transform.position, hook.transform.position);
        //Debug.Log(distToHook);
    }

}
