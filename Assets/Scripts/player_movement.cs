using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float speed = 5.0f;
    private Collider2D interactionTrigger;

    // Start is called before the first frame update
    void Start()
    {
        interactionTrigger = GetComponentsInChildren<Collider2D>().FirstOrDefault(c => c.gameObject.name == "interaction");
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
        var move = new Vector3(horizontal, vertical, 0);

        MovePosition(move);
        RotateInteractionTrigger(move);
    }

    private void RotateInteractionTrigger(Vector3 move)
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

    private void MovePosition(Vector3 move)
    {
        transform.position += move * (speed * Time.deltaTime);
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        if (interactionTrigger != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(interactionTrigger.bounds.center, interactionTrigger.bounds.size);
        }
    }
}
