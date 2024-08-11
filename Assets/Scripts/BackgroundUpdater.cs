using UnityEngine;

public class BackgroundUpdater : MonoBehaviour
{

    [SerializeField]
    private Sprite[] backgrounds;

    [SerializeField]
    private Sprite[] coathangers;

    GameObject coathanger;

    void Awake()
    {
        coathanger = GameObject.Find("Coathanger");
    }


    public void SetBackground(int level)
    {
        GetComponent<SpriteRenderer>().sprite = backgrounds[level];
        if (level == 3)
        {
            coathanger.SetActive(false);
            return;
        }
        else
        {
            coathanger.SetActive(true);
        }
        coathanger.GetComponent<SpriteRenderer>().sprite = coathangers[level];
    }
}
