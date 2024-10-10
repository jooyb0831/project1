using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    bool isOn = false;
    [SerializeField] GameObject menuUI;
    [SerializeField] Toggle[] menuTabs;
    [SerializeField] GameObject[] menuUIs;


    public Transform questArea;

    [SerializeField] TMP_Text noQTxt;
    [SerializeField] Transform questObj;

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
        }
        else if (menuTabs[idx].isOn == false)
        {
            menuUIs[idx].SetActive(false);
        }
    }
}
