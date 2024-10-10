using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject body;
    private GameObject player;
    private Rigidbody rb;
    //private Animator animator;
    private Animator[] animators;
    private SpriteRenderer spriteRenderer;

    public Vector3 moveVec;
    public float MoveSpeed = 5f;
    private void Start()
    {
        player = GetComponent<GameObject>();
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        animators = GetComponentsInChildren<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rb.velocity = Vector3.zero;
            player.transform.position = new Vector3(0, 2.7f, 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool WalkingX = false;
        bool WalkS = false;
        bool WalkN = false;
        bool Flip = Math.Abs(body.transform.eulerAngles.y - 180f) < 0.1f;
        bool FlipZ = animators[0].GetBool("FlipZ");
        //캐릭터 애니메이션
        {
            if (horizontal != 0)
            {
                WalkingX = true;
                if (horizontal > 0)
                    Flip = true;
                else
                    Flip = false;
            }
            else
                WalkingX = false;
            if (vertical != 0)
            {
                if (vertical < 0)
                {
                    WalkS = true;
                    WalkN = false;
                    FlipZ = false;
                }
                if (vertical > 0)
                {
                    WalkS = false;
                    WalkN = true;
                    FlipZ = true;
                }
            }
            else
            {
                WalkN = false;
                WalkS = false;
            }
        }
        body.transform.eulerAngles = new Vector3(Flip?-30f:30f,Flip?180f:0f,body.transform.eulerAngles.z) ;
        foreach (var animator in animators)
        {
            animator.SetBool("WalkingX", WalkingX);
            animator.SetBool("WalkS", WalkS);
            animator.SetBool("WalkN", WalkN);
            animator.SetBool("Flip", Flip);
            animator.SetBool("FlipZ", FlipZ);
        }
        moveVec = new Vector3(horizontal, 0, vertical);
        moveVec.Normalize();
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveVec.x * MoveSpeed, rb.velocity.y, moveVec.z * MoveSpeed);
    }
}
