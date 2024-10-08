using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUIBtn : MonoBehaviour
{
    [SerializeField] GameObject questUI;
    [SerializeField] GameObject questBtn;
    [SerializeField] GameObject upBtn;
    [SerializeField] bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(!isOn)
            {
                OnClickQUIBtn();
            }
            else if (isOn)
            {
                OnClickUpBtn();
            }

        }
    }

    public void OnClickQUIBtn()
    {
        questUI.SetActive(true);
        questBtn.SetActive(false);
        upBtn.SetActive(true);
        isOn = true;
    }

    public void OnClickUpBtn()
    {
        questUI.SetActive(false);
        questBtn.SetActive(true);
        upBtn.SetActive(false);
        isOn = false;
    }
}
