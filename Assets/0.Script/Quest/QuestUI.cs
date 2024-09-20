using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : Singleton<QuestUI>
{
    public Quest quest;
    public TMP_Text questTxt;
    public int index;
    bool isAdd = false;

    public int curCnt
    {
        set
        {
            questTxt.text = $"{quest.qTitle} {quest.curCnt}/{quest.maxCnt}";
        }
    }
    
    public int maxCnt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(quest != null&& !isAdd)
        {
            GameManager.Instance.questManager.qUIList.Add(this);
            isAdd = true;
        }
    }
}
