using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject body;
    private Rigidbody rb;
    //private Animator animator;
    private Animator[] animators;
    private SpriteRenderer spriteRenderer;

    public Vector3 moveVec;
    public float MoveSpeed = 5f;

    public int jumpCnt = 0;
    public bool isJumping = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        animators = GetComponentsInChildren<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Physics.gravity = new Vector3(0f, -24f, 0f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rb.velocity = Vector3.zero;
            gameObject.transform.position = new Vector3(0, 2.7f, 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        int LRRotated = animators[0].GetInteger("LRRotated");
        bool BackRotated = animators[0].GetBool("BackRotated");
        bool Walking = false;
        bool LRSetted = false;
        {
            if (horizontal != 0)
            {
                Walking = true;
                LRSetted = true;
                BackRotated = false; //뒤에 돌아있는 상태에서 양엎으로 움직이면 왼오로 변환됨.
                if (horizontal > 0)
                    LRRotated = 1;
                else
                    LRRotated = -1;
            }
            else
                Walking = false;
            if (vertical != 0)
            {
                Walking = true;
                if (vertical < 0)
                    BackRotated = false;
                else
                    BackRotated = true;
                if (!LRSetted)
                    LRRotated = 0;
            }               
        }
        
        body.transform.eulerAngles = new Vector3(LRRotated == 1 ? -40f : 40f, LRRotated == 1 ? -180 : 0f, body.transform.eulerAngles.z);
        foreach (var animator in animators)
        {
            animator.SetBool("Walking", Walking);
            animator.SetBool("Jumping", isJumping);
            animator.SetBool("BackRotated", BackRotated);
            animator.SetInteger("LRRotated", LRRotated);
        }
        moveVec = new Vector3(horizontal, 0, vertical);
        moveVec.Normalize(); //이동 벡터 정규화
    }

    private void Jump()
    {
        if (jumpCnt >= 2) return;
        
        //무브 벡터 방향으로 점프
        rb.velocity = new Vector3(moveVec.x * MoveSpeed, 12f, moveVec.z * MoveSpeed);
        jumpCnt++;
        isJumping = true;
    }

    void FixedUpdate()
    {
        if (jumpCnt == 0) //지면 이동
        {
            rb.velocity = new Vector3(moveVec.x * MoveSpeed, rb.velocity.y, moveVec.z * MoveSpeed);
        }

        if (rb.velocity.y == 0)
        {
            jumpCnt = 0;
            isJumping = false;
        }
    }
}
