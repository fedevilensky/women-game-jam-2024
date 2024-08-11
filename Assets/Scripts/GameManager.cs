using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TimeManager timeManager;
    private Rigidbody2D playerRb;

    [SerializeField]
    private Vector3 playerResetPosition = new Vector3(0, 0, 0);

    public ScreenFade screenFade;

    private Animator animator;

    void Start()
    {
        timeManager = GetComponent<TimeManager>();
        timeManager.SetCallback(ResetLoop);
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void ResetLoop()
    {
        BlackScreen();
    }

    // TODO Call animation manager to fade to black screen and while on the black screen, call ResetPlayerPosition
    void BlackScreen()
    {
        GameObject.FindWithTag("Player").GetComponent<player_movement>().enabled = false;
        screenFade.FadeToBlackAndBack(ResetPlayerPosition);
        // screenFade.FadeToBlack();
        // ResetPlayerPosition();
        // screenFade.FadeToClear();
        // Code to make the screen black
    }


    void ResetPlayerPosition()
    {
        if (playerRb == null)
        {
            playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        }
        playerRb.MovePosition(playerResetPosition);
        GameObject.FindWithTag("Player").GetComponent<player_movement>().enabled = true;
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
