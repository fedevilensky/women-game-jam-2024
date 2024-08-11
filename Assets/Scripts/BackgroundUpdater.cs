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

    void Start()
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
        GameObject.Find("Table").GetComponent<SpriteRenderer>().sprite = tables[level];
        GameObject.Find("Coathanger").GetComponent<SpriteRenderer>().sprite = coathangers[level];
    }
}
