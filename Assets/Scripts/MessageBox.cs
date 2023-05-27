using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    private TextMeshProUGUI content;
    private Image background;
    private const float ctOpacity = 1f;
    private const float bgOpacity = 0.375f;
    private const float reduceOpacitySpeed = 0.005f;

    void Start()
    {
        content = transform.Find("Content").gameObject.GetComponent<TextMeshProUGUI>();
        background = transform.Find("Background").transform.GetComponent<Image>();
    }

    void ReduceOpacity()
    {
        Color color = content.color;
        color.a -= reduceOpacitySpeed * ctOpacity;
        content.color = color;

        color = background.color;
        color.a -= reduceOpacitySpeed * bgOpacity;
        background.color = color;

        if (color.a == 0) CancelInvoke("ReduceOpacity");
    }

    void SetText(string text)
    {
        content.text = text;
    }

    public void Send(string message)
    {
        SetText(message);

        Color color = content.color;
        color.a = ctOpacity;
        content.color = color;

        color = background.color;
        color.a = bgOpacity;
        background.color = color;

        CancelInvoke("ReduceOpacity");
        InvokeRepeating("ReduceOpacity", 2, 0.01f);
    }

    public void Hide()
    {
        Color color = content.color;
        color.a = 0;
        content.color = color;

        color = background.color;
        color.a = 0;
        background.color = color;

        CancelInvoke("ReduceOpacity");
    }
}
