﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float m_Speed = 5f;

    public Animator m_Anim;

    private bool m_IsMoving = false;

    Rigidbody2D m_Body;
    
    void Start()
    {
        m_Body = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (UIManager.Instance.IsGamePaused())
            return;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        this.transform.position += input * Time.deltaTime * m_Speed;

        if (Input.GetAxis("Horizontal") < 0)
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        else if (Input.GetAxis("Horizontal") > 0)
            GetComponentInChildren<SpriteRenderer>().flipX = false;

        if (Input.GetAxis("Vertical") > 0)
        {
            m_Body.velocity = Vector2.Scale(m_Body.velocity, Vector2.right);
            m_Body.gravityScale = 0;
        }
        else
            m_Body.gravityScale = 2;

        // Animation if you've just started or stopped moving
        if(input.x != 0 && !m_IsMoving)
        {
            m_IsMoving = true;
            //m_Anim.SetTrigger("StartMoving");
            m_Anim.SetBool("IsMoving", true);
        }
        else if(input.x == 0 && m_IsMoving)
        {
            m_IsMoving = false;
            //m_Anim.SetTrigger("StopMoving");
            m_Anim.SetBool("IsMoving", false);
        }
    }

    void FixedUpdate()
    {
        if (m_Body.velocity.y < -12)
        {
            Vector2 vel = m_Body.velocity;
            vel.y = -12;
            m_Body.velocity = vel;
        }
    }
}
