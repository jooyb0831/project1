using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUI : MonoBehaviour
{
    bool isOn = false;
    [SerializeField] GameObject menuUI;
    [SerializeField] Toggle[] menuTabs;
    [SerializeField] GameObject[] menuUIs;

    [SerializeField] int onIdx = -1;

    public Transform questArea;

    [SerializeField] TMP_Text noQTxt;
    [SerializeField] Transform questObj;
    [SerializeField] Color32 selectedColor;
    [SerializeField] Color32 normalColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isOn)
            {
                Time.timeScale = 0;
                menuUI.SetActive(true);
                isOn = true;
                if(onIdx == -1)
                {
                    menuTabs[0].GetComponent<Image>().color = selectedColor;
                }
                else if(onIdx!=-1)
                {
                    menuTabs[onIdx].GetComponent<Image>().color = selectedColor;
                }
            }
            else if (isOn)
            {
                Time.timeScale = 1;
                menuUI.SetActive(false);
                isOn = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isOn)
        {
            Time.timeScale = 1;
            menuUI.SetActive(false);
            isOn = false;
        }

        if(questObj.childCount !=0)
        {
            noQTxt.gameObject.SetActive(false);
        }
        else
        {
            noQTxt.gameObject.SetActive(true);
        }
    }

    public void OnExitBtn()
    {
        Time.timeScale = 1;
        menuUI.SetActive(false);
        isOn = false;
    }

    public void OnToggleChanged(int idx)
    {
        /*
        foreach(var item in menuTabs)
        {
            item.isOn = false;
        }

        foreach (var item in menuUIs)
        {
            item.SetActive(false);
        }
        */
        if (menuTabs[idx].isOn == true)
        {
            menuUIs[idx].SetActive(true);
            onIdx = idx;
            
        }
        else if (menuTabs[idx].isOn == false)
        {
            menuUIs[idx].SetActive(false);
            menuTabs[idx].GetComponent<Image>().color = normalColor;

        }
    }

}
