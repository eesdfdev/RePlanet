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
    private void Start()
    {
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
            gameObject.transform.position = new Vector3(0, 2.7f, 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
        /*
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
        */
        
        body.transform.eulerAngles = new Vector3(LRRotated == 1 ? -30f : 30f, LRRotated == 1 ? -180 : 0f, body.transform.eulerAngles.z);
        foreach (var animator in animators)
        {
            animator.SetBool("Walking", Walking);
            animator.SetBool("BackRotated", BackRotated);
            animator.SetInteger("LRRotated", LRRotated);
        }
        moveVec = new Vector3(horizontal, 0, vertical);
        //moveVec.Normalize(); 미끄러지는 느낌 나서 삭제
    }
 
    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveVec.x * MoveSpeed, rb.velocity.y, moveVec.z * MoveSpeed);
    }
}
