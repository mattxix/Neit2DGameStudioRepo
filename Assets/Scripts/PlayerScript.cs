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
    public GunScript weapon;
    bool facingLeft = false;
    SpriteRenderer spr;
    Animator anim;
    bool isAttacking;

    Vector2 moveDirection;
    Vector2 mousePosition;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        FlipPlayer();
        anim.SetFloat("Moving", dir.magnitude);

        if (Input.GetMouseButton(0))
        {
            weapon.Fire();
        }

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
        rb2d.MovePosition(rb2d.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    public void MovePlayer(InputAction.CallbackContext ctx)
    {
        h = ctx.ReadValue<Vector2>().x;
        v = ctx.ReadValue<Vector2>().y;

        dir = new Vector2(h, v);
        Debug.Log($"h:{h}, v:{v}");
    }

    public void PlayerAttack(InputAction.CallbackContext ctx)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
            StartCoroutine(AttackCooldown());
        }
    }

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

