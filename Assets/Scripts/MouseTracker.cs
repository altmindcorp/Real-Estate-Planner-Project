using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseTracker : MonoBehaviour
{
    private TMP_Text textTracker;

    private void Start()
    {
        textTracker = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textTracker.text = "Mouse position, m: \n" + new Vector2(UIController.GetUnscaledObjectPosition(0).x, UIController.GetUnscaledObjectPosition(0).y).ToString("F3");
    }
}
