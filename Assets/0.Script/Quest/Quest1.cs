using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1 :Quest
{
  
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(data.curCount >= data.maxCount)
        {
            data.isDone = true;
        }
        if (data.isDone == true)
        {
            questUI.gameObject.SetActive(false);
        }
    }

    public override void Init()
    {
        if (gi == null)
        {
            gi = GameManager.Instance.gameUI;
        }
        data.QuestTitle = "눈알 몬스터 처치";
        data.QuestNumber = 0;
        data.isDone = false;
        data.maxCount = 1;
        data.exp = 15;
        data.qType = QuestType.Kill;
        data.objIndex = 0;
        questUI.quest = this;
        questUI.index = data.QuestNumber;
        questUI.questTxt.text = $"{data.QuestTitle} ({data.curCount}/{data.maxCount})";

        Instantiate(questUI, gi.questArea);
        base.Init();
    }


}
