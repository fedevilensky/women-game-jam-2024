using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{

    private Image panelImage;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float timeToFade = 1.0f;

    public bool fadeIn = false;
    public bool fadeOut = false;

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
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, Mathf.Clamp(panelImage.color.a + Time.deltaTime * timeToFade, 0, 1));
        }
        else if (fadeOut)
        {
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, Mathf.Clamp(panelImage.color.a - Time.deltaTime * timeToFade, 0, 1));
        }
    }

    public void FadeToBlack()
    {
        fadeIn = true;
        fadeOut = false;
    }

    public void FadeToClear()
    {
        fadeIn = false;
        fadeOut = true;
    }
}
