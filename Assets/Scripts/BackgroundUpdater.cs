using UnityEngine;

public class BackgroundUpdater : MonoBehaviour
{

    [SerializeField]
    private Sprite[] backgrounds;

    [SerializeField]
    private Sprite[] tables;

    [SerializeField]
    private Sprite[] coathangers;

    GameObject table;
    GameObject coathanger;

    void Awake()
    {
        table = GameObject.Find("Table");
        coathanger = GameObject.Find("Coathanger");
    }


    public void SetBackground(int level)
    {
        GetComponent<SpriteRenderer>().sprite = backgrounds[level];
        if (level == 3)
        {
            table.SetActive(false);
            coathanger.SetActive(false);
            return;
        }
        else
        {
            table.SetActive(true);
            coathanger.SetActive(true);
        }
        table.GetComponent<SpriteRenderer>().sprite = tables[level];
        coathanger.GetComponent<SpriteRenderer>().sprite = coathangers[level];
    }
}
