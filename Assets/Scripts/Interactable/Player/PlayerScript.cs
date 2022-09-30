using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    #region Essentials
    public GlobalTime gt;
    public Playercontrols playerControls;
    protected float Timer;
    #endregion

    #region MovementProperties
    public Rigidbody rb;
    public static float walk_velocity = 5f;
    public float spri_velocity = walk_velocity * 2;

    private float velocity;
    private bool isRunning = false;
    #endregion

    #region PlayerInputAction
    Vector3 direction = Vector2.zero;
    private InputAction move;
    private InputAction look;
    private InputAction act;
    private InputAction sprint;
    #endregion

    #region PlayerStats

    //float x = 0;

    [SerializeField]
    float stamina = 100;

    [SerializeField]
    float hunger = 100;

    [SerializeField]
    float thirst = 100;
    #endregion

    void Awake()
    {
        playerControls = new Playercontrols();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        act = playerControls.Player.Fire;
        act.Enable();
        act.performed += Fire;

        sprint = playerControls.Player.Sprint;
        sprint.Enable();
        sprint.performed += Sprint;
        sprint.canceled += Sprint;
    }
    private void OnDisable()
    {
        move.Disable();
        act.Disable();
        sprint.Disable();
    }
    void Start()
    {
        velocity = walk_velocity;
    }
    void Update()
    {
        direction = move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(direction.x * velocity, 0, direction.y * velocity);
        isRunning = (playerControls.Player.Sprint.activeControl == null) ? true : false;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if(isRunning)
            velocity = spri_velocity;
        else
            velocity = walk_velocity;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }

    // public float fallrate(float x)
    // {
    //     double value = -(4f*Math.Pow(x, 4f)) + (7f*Math.Pow(x, 3f)) - (4*Math.Pow(x, 2f)) + 1f;
    //     float fallrate = (float)value;
    //     return fallrate;
    // }

    // public void hunger()
    // {
    //     Timer += (Time.deltaTime * gt.multiplier);

    //     if(Timer >= gt.DelayAmount)
    //     {
    //         Timer = 0;
    //         x += 0.01f;

    //         hunger *= fallrate(x);

    //         Debug.Log($"x: {x}, h: {hunger}");

    //         if(hunger == 0)
    //         {
    //             hunger = 0;
    //             x=0;
    //         }
    //         if(hunger < 0)
    //         {
    //             Debug.Log("You died");
    //             x=0;
    //         }
    //     }
    // }
}
