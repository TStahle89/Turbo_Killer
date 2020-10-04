using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private PlayerController _playerController;
    public GrapelingHook hook;


    // Start is called before the first frame update
    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerController.horizontalInput = Input.GetAxisRaw("Horizontal");
        _playerController.verticalInput = Input.GetAxisRaw("Vertical");
        _playerController.jump = Input.GetButtonDown("Jump");
        _playerController.fireWeapon = Input.GetButton("Fire1");
        _playerController.bombButton = Input.GetButton("Fire3");
        hook.grapelingbutton = Input.GetButtonDown("GrapelHookButon");
        hook.jumpout = Input.GetButtonDown("Jump");
    }
}
