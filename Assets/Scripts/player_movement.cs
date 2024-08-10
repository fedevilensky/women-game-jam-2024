using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

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
        transform.position += move * Time.deltaTime * speed;
    }
}
