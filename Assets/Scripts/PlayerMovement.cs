using System;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float speed = 5.0f;
    private Collider2D interactionTrigger;

    private Rigidbody2D rb;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        interactionTrigger = GetComponentsInChildren<Collider2D>().FirstOrDefault(c => c.gameObject.name == "interaction");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Mathf.Round(Input.GetAxis("Vertical"));
        float horizontal = 0;
        if (vertical == 0)
        {
            horizontal = Mathf.Round(Input.GetAxis("Horizontal"));
        }

        animator.SetFloat("xDirection", horizontal);
        animator.SetFloat("yDirection", vertical);
        animator.SetBool("isMoving", horizontal != 0 || vertical != 0);
        var move = new Vector2(horizontal, vertical);

        MovePosition(move);
        RotateInteractionTrigger(move);
    }

    private void RotateInteractionTrigger(Vector2 move)
    {
        if (move.x != 0)
        {
            interactionTrigger.transform.rotation = Quaternion.Euler(0, 0, move.x > 0 ? 0 : 180);
        }
        else if (move.y != 0)
        {
            interactionTrigger.transform.rotation = Quaternion.Euler(0, 0, move.y > 0 ? 90 : 270);
        }
    }

    private void MovePosition(Vector2 move)
    {
        rb.velocity = move * speed;
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        if (interactionTrigger != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(interactionTrigger.bounds.center, interactionTrigger.bounds.size);
        }
    }
}
