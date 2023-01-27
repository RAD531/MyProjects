using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField]
    private List<TabButton> tabButtons;

    [SerializeField]
    private Image tabLine;

    private TabButton selectedButton;

    [SerializeField]
    private List<GameObject> panelsToSwap;

    private int index;
    private int indexOld;
    private Vector3 centerPosition;
    private Vector3 offScreenRightPosition;

    private void Start()
    {
        centerPosition = new Vector3(0, 0, 0);
        offScreenRightPosition = new Vector3(Screen.width, 0, 0);
    }

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
    }
    public void OnTabExit(TabButton button)
    {
    }

    public void OnTabSelected(TabButton button)
    {
        selectedButton = button;
        tabLine.transform.position = new Vector2(button.transform.position.x, button.transform.position.y + -33.4893f);

        indexOld = index;
        index = button.transform.GetSiblingIndex();

        for (int i = 0; i < panelsToSwap.Count; i++)
        {
            if (i == indexOld)
            {
                //hide panel
                LeanTween.move(panelsToSwap[i].GetComponent<RectTransform>(), offScreenRightPosition, 1f).setEase(LeanTweenType.easeInOutExpo);
            }

            if (i == index)
            {
                //show panel
                panelsToSwap[i].SetActive(true);
                LeanTween.move(panelsToSwap[i].GetComponent<RectTransform>(), centerPosition, 1f).setEase(LeanTweenType.easeInOutExpo);
            }

            else
            {
                //panelsToSwap[i].SetActive(false);
            }
        }
    }
}
