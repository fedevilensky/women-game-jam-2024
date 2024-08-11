using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{

    private Image panelImage;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float timeToFade = 1.0f;

    private bool fadeIn = false;
    private bool fadeOut = false;

    private bool wait = false;

    private float waitTime = 1f;

    public delegate void Callback();

    private Callback callback;

    // Start is called before the first frame update
    void Start()
    {
        panelImage = GetComponentInChildren<Image>();
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            panelImage.color = new Color(
                panelImage.color.r,
                panelImage.color.g,
                panelImage.color.b,
                Mathf.Clamp(panelImage.color.a + Time.deltaTime * timeToFade, 0, 1)
            );
            if (panelImage.color.a == 1)
            {
                fadeIn = false;
                wait = true;
                waitTime = 1f;
                callback();

            }
        }
        else if (wait)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                fadeOut = true;
                wait = false;
            }
        }
        else if (fadeOut)
        {
            panelImage.color = new Color(
                panelImage.color.r,
                panelImage.color.g,
                panelImage.color.b,
                Mathf.Clamp(panelImage.color.a - Time.deltaTime * timeToFade, 0, 1)
            );
            if (panelImage.color.a == 0)
            {
                fadeOut = false;
            }
        }
    }

    public void FadeToBlackAndBack(Callback cb)
    {
        callback = cb;
        fadeIn = true;
    }
}
