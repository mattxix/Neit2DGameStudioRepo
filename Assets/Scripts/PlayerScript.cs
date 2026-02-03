using Mono.Cecil;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float h, v;
    Vector2 dir;
    public float moveSpeed = 5f;
    public Rigidbody2D rb2d;
    public ShotGunScript weapon;
    bool facingLeft = false;
    SpriteRenderer spr;
    Animator anim;
    bool isAttacking;

    Vector2 moveDirection;
    Vector2 mousePosition;

    [Header("Dash")]
    public float dashDistance = 3f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 0.5f;
    private Vector2 lastMoveDir = Vector2.right;
    private bool isDashing;
    private bool canDash = true;

    [Header("Animation")]
    float MoveX = 0f;
    float MoveY = 0f;
    float LastMoveX = 0f;
    float LastMoveY = 0f;
    

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        FlipPlayer();
     //   anim.SetFloat("Moving", dir.magnitude);

      //  if (Input.GetMouseButton(0))
        //{
           // weapon.Fire();
       // }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Rotate weapon toward mouse
        if (weapon != null)
        {
            Vector2 weaponPos = weapon.transform.position;
            Vector2 aimDir = mousePosition - weaponPos;
            float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
            weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        rb2d.MovePosition(rb2d.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    public void MovePlayer(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        h = input.x;
        v = input.y;

        dir = input.normalized;

        if (dir != Vector2.zero)
        {
            lastMoveDir = dir;

            anim.SetFloat("LastMoveX", dir.x);
            anim.SetFloat("LastMoveY", dir.y);
        }

        anim.SetFloat("MoveX", dir.x);
        anim.SetFloat("MoveY", dir.y);
        anim.SetFloat("Speed", dir.sqrMagnitude);
    }


    public void Dash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canDash && !isDashing)
        {
            StartCoroutine(DashCoroutine());
        }
    }


    IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;

        Vector2 startPos = rb2d.position;
        Vector2 targetPos = startPos + lastMoveDir * dashDistance;

        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            rb2d.MovePosition(Vector2.Lerp(startPos, targetPos, elapsed / dashDuration));
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb2d.MovePosition(targetPos);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }



  /*public void PlayerAttack(InputAction.CallbackContext ctx)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
            StartCoroutine(AttackCooldown());
        }
    }*/

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    void FlipPlayer()
    {
        if (h > 0 && facingLeft || h < 0 && !facingLeft)
        {
            facingLeft = !facingLeft;
            spr.flipX = facingLeft;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}

