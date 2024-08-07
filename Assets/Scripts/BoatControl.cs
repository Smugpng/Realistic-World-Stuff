using System.Collections;
using System.Collections.Generic;
using Ditzelgames;
using UnityEngine;
using UnityEngine.InputSystem;


public class BoatControl : MonoBehaviour
{
    public Transform motor;
    public float steerPower = 500f;
    public float power = 5f;
    public float maxSpeed = 10f;
    public float drag = 0.1f;

    protected Rigidbody rb;
    protected Quaternion startRot;

    private float moveVector;
    private float forwardVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startRot = motor.localRotation;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var forceDirection = transform.forward;
        var steer = 0 + -moveVector;
        AddRotForce(steer);

        var forward = Vector3.Scale(new Vector3(1,0,1), transform.forward);
        var placeholder = 0 + forwardVector;
        AddForwardForce(forward, placeholder);

        Debug.Log(steer);
        Debug.Log(placeholder);
    }
    private void AddRotForce(float steer)
    {
        rb.AddForceAtPosition(steer * transform.right * steerPower / 100f, motor.position);

    }
    private void AddForwardForce(Vector3 forward, float placeholder)
    {
        PhysicsHelper.ApplyForceToReachVelocity(rb, forward * (maxSpeed * placeholder), power);
    }
    public void Move(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>().x;
    }
    public void Forward(InputAction.CallbackContext context)
    {
        forwardVector = context.ReadValue<Vector2>().y;
    }

}
