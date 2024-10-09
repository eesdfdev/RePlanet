using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    private Animator animator;
    private Animator[] animators;
    private SpriteRenderer spriteRenderer;
  
    public float MoveSpeed = 5f;
    private void Start()
    {
        player = GetComponent<GameObject>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vectical = Input.GetAxis("Vertical");

        bool WalkingX = false;
        bool WalkS = false;
        bool WalkN = false;
        bool Flip = spriteRenderer.flipX;
        bool FlipZ = animator.GetBool("FlipZ");
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
            if (vectical != 0)
            {
                if (vectical < 0)
                {
                    WalkS = true;
                    WalkN = false;
                    FlipZ = false;
                }
                if (vectical > 0)
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
        spriteRenderer.flipX = Flip;
        animator.SetBool("WalkingX", WalkingX);
        animator.SetBool("WalkS", WalkS);
        animator.SetBool("WalkN", WalkN);
        animator.SetBool("Flip", Flip);
        animator.SetBool("FlipZ", FlipZ);
        foreach (Animator itemAnimator in animators)
        {
            itemAnimator.SetBool("WalkingX", WalkingX);
            itemAnimator.SetBool("WalkS", WalkS);
            itemAnimator.SetBool("WalkN", WalkN);
            itemAnimator.SetBool("Flip", Flip);
        }


        rb.velocity = new Vector3(horizontal * MoveSpeed, rb.velocity.y, vectical * MoveSpeed);
    }
}
