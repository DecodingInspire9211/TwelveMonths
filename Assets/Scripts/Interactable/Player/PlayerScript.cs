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
    public Transform Target;
    public Camera Camera;

    public Vector3 Offset;
    public float SmoothTime = 0.1f;

    private Vector3 camvelocity = Vector3.zero;
     private Vector3 targetvelocity = Vector3.zero;

    public static float walk_velocity = 5f;
    public float spri_velocity = walk_velocity * 2;
    private float velocity;
    private bool isRunning = false;
    #endregion

    #region PlayerInputAction
    Vector3 direction = Vector2.zero;
    Vector3 orbit = Vector2.zero;
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

    #region PlayerCamProperties
    public float standardfov = 45f;
    public float sprintfov = 46f;
    public float sensitivity = 1.0f;
    #endregion

    void Awake()
    {
        playerControls = new Playercontrols();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        look = playerControls.Player.Look;
        look.Enable();

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

        Offset = Camera.transform.position - Target.position;
    }
    void Update()
    {
        direction = move.ReadValue<Vector2>();
        orbit = look.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(direction.x * velocity, 0, direction.y * velocity);
        isRunning = (playerControls.Player.Sprint.activeControl == null) ? true : false;
    }

    private void LateUpdate()
    {
        FollowCamera();
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        velocity = isRunning ? spri_velocity : walk_velocity;
        if(isRunning)
            StartCoroutine(ChangeFOV(standardfov, sprintfov, 0.0625f));
        if(!isRunning)
            StartCoroutine(ChangeFOV(sprintfov, standardfov, 0.0625f));
    }
    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }
    IEnumerator ChangeFOV(float start, float end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration )
        {
            Camera.fieldOfView = Mathf.Lerp(start, end, elapsed / duration );
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.fieldOfView = end;
    }

    void FollowCamera()
    {
        Vector3 targetPosition = Target.position + Offset;
        Camera.transform.position = Vector3.SmoothDamp(Camera.transform.position, targetPosition, ref camvelocity, 0.1f);

        Camera.transform.LookAt(Target);

        
    }
}
