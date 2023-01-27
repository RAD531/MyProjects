using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject popupWindow;

    [SerializeField]
    private GameObject popUpWindowText;

    public void Start()
    {
        popupWindow.SetActive(true);
        popupWindow.transform.localScale = Vector2.zero;
    }

    public void ButtonOfflineStart()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

    public void OpenPopupWindow()
    {
        popUpWindowText.GetComponent<TextMeshProUGUI>().text =  "This is a demo version of Power Champions. Therefore, this feature is not yet available.";
        popupWindow.transform.LeanScale(Vector2.one, 0.8f);
    }

    public void OpenLatestGameUpdateWindowWindow()
    {
        popUpWindowText.GetComponent<TextMeshProUGUI>().text = "Latest Update 20/06/2021:" +
        Environment.NewLine +
        "Welcome to the Power Champions Demo. The demo has one simple game mode where the player must score 5 baskets before the enemy does to win the game.";
        popupWindow.transform.LeanScale(Vector2.one, 0.8f);
    }

    public void OpenControlsWindow()
    {
        popUpWindowText.GetComponent<TextMeshProUGUI>().text = "Controls:" +
        Environment.NewLine +
        "Up = up arrow, Down = down arrow, Right = right arrow, Left = left arrow, Space = pick up ball and throw (hold), Shift = turn sharply";
        popupWindow.transform.LeanScale(Vector2.one, 0.8f);
    }

    public void ClosePopupWindow()
    {
        popupWindow.transform.LeanScale(Vector2.zero, 1f).setEaseInBack();
    }
}