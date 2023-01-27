using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeUIText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private string newText;

    private string oldText;

    private void Start()
    {
        oldText = text.text;
    }

    public void TurnOn()
    {
        text.SetText(newText);
    }

    public void TurnOff()
    {
        text.SetText(oldText);
    }
}
