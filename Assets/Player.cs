using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Debug data")]
    public float verticalInput;
    public float horizontalInput;

    [Header("Movement data")]
    public float moveSpeed;
    public float rotationSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 2.5f;
        rotationSpeed = 1;
    }

    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (verticalInput < 0)
            horizontalInput = -horizontalInput;
    }

    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * moveSpeed * verticalInput;

        rb.velocity = movement;

        transform.Rotate(0, horizontalInput * rotationSpeed, 0);
    }
}
