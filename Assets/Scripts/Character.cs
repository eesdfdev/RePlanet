using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Vector2 moveInput;
    public Transform playerTransform;
    public Rigidbody rb;
    public float MoveSpeed = 5f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rb.velocity = Vector3.zero;
            playerTransform.position = new Vector3(0, 2.7f, 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void FixedUpdate()
    {
        moveInput.x = Input.GetAxis("Horizontal") * MoveSpeed;
        moveInput.y = Input.GetAxis("Vertical") * MoveSpeed;

        rb.velocity = new Vector3(moveInput.x, rb.velocity.y, moveInput.y);
        rb.AddForce(new Vector3(0, -10, 0), ForceMode.Acceleration);
    }
}
