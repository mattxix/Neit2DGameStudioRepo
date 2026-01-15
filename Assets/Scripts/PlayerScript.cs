using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    public float h, v;
    Vector2 dir;
    Rigidbody2D rb2d;
    bool facingLeft = false;
    SpriteRenderer spr;
    Animator anim;
    bool isAttacking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(dir * Time.deltaTime * 10 );
        rb2d.MovePosition(rb2d.position + dir * Time.fixedDeltaTime * 10);
        FlipPlayer();
        anim.SetFloat("Moving", dir.magnitude);
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

