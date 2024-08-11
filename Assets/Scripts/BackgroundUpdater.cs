using UnityEngine;

public class BackgroundUpdater : MonoBehaviour
{

    [SerializeField]
    private Sprite[] backgrounds;

    public void SetBackground(int level)
    {
        GetComponent<SpriteRenderer>().sprite = backgrounds[level];
    }
}
