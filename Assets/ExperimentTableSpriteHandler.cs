using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentTableSpriteHandler : MonoBehaviour
{
    public Sprite[] experimentTableSprites;

    public void SetExperimentTableSprite(bool broken)
    {
        GetComponent<SpriteRenderer>().sprite = experimentTableSprites[broken ? 1 : 0];
    }
}
