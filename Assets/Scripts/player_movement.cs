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
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(horizontal, vertical, 0);
        transform.position += move * Time.deltaTime * speed;
    }
}
