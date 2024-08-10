using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TimeManager timeManager;
    private Rigidbody2D playerRb;

    [SerializeField]
    private Vector3 playerResetPosition = new Vector3(0, 0, 0);

    void Start()
    {
        timeManager = GetComponent<TimeManager>();
        timeManager.SetCallback(ResetLoop);
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void ResetLoop()
    {
        BlackScreen();
    }

    // TODO Call animation managern to fade to black screen and while on the black screen, call ResetPlayerPosition
    void BlackScreen()
    {
        // Code to make the screen black
    }


    void ResetPlayerPosition()
    {
        playerRb.MovePosition(playerResetPosition);
    }



}
