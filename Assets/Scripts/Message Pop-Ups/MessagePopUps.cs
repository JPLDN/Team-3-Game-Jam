using UnityEngine;
using TMPro;
using System.Collections;

public class PopupMessage : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject panel;     
    [SerializeField] private TMP_Text messageText; 

    [SerializeField] private float defaultDuration = 2.5f;

    void Awake()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void Show(string msg, float? duration = null)
    {
        if (panel == null || messageText == null) return;

        messageText.text = msg;
        panel.SetActive(true);

        float d = duration ?? defaultDuration;
        StopAllCoroutines();
        StartCoroutine(AutoHideAfter(d));
    }

    IEnumerator AutoHideAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Hide();
    }

    public void Hide()
    {
        if (panel != null)
            panel.SetActive(false);
    }
}

