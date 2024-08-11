using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TimeManager timeManager;
    private Rigidbody2D playerRb;

    [SerializeField]
    private Vector3 playerResetPosition = new Vector3(0, 0, 0);

    public ScreenFade screenFade;


    void Start()
    {
        timeManager = GetComponent<TimeManager>();
        timeManager.SetCallback(ResetLoop);
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        playerResetPosition = playerRb.position;
    }

    public void ResetLoop()
    {
        BlackScreen();
    }

    void BlackScreen()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        screenFade.FadeToBlackAndBack(ResetPlayerPosition);
    }


    void ResetPlayerPosition()
    {
        if (playerRb == null)
        {
            playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        }
        playerRb.MovePosition(playerResetPosition);
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        timeManager.StartTicking();
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            print("Resetting player position");
            BlackScreen();
        }
    }
}
