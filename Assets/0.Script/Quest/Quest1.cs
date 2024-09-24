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
            thisQuestUI.gameObject.SetActive(false);
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
        data.maxCount = 2;
        data.curCount = 0;
        data.exp = 15;
        data.qType = QuestType.Kill;
        data.objIndex = 0;
        questUI.quest = this;
        questUI.SetData(this.data);
        QuestUI qUI = Instantiate(questUI, gi.questArea);
        thisQuestUI = qUI;
        base.Init();
    }

    void QuestClear()
    {

    }

}
