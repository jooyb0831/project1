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
            gi = GameManager.Instance.GameUI;
        }
        if(menuUI == null)
        {
            menuUI = GameManager.Instance.MUI;
        }
        data.QuestTitle = "���� ���� óġ";
        data.QuestNumber = 0;
        data.isDone = false;
        data.maxCount = 2;
        data.curCount = 0;
        data.exp = 15;
        data.gold = 20;
        data.qType = QuestType.Kill;
        data.objIndex = 0;
        data.QuestExplain = $"���� ���� {data.maxCount}������ óġ�ϱ�.";
        data.QuestRewardTxt = $"���� : EXP {data.exp}, ��� {data.gold}";
        //data.dialogue = JsonData.Instance.qDiaData.questDialogueData[0];

        
        questUI.quest = this;
        qMenuUI.quest = this;
        
        base.Init();
    }

    void QuestAdd()
    {
        QuestUI qUI = Instantiate(questUI, gi.questArea);
        QuestMenuUIPreset qMUI = Instantiate(qMenuUI, menuUI.questArea);
        qUI.SetData(this.data);
        qMUI.SetData(this.data);


        GameManager.Instance.QuestManager.qUIList.Add(qUI);
        GameManager.Instance.QuestManager.qMUIList.Add(qMUI);

        thisQuestUI = qUI;
        thisQuestMenuUI = qMUI;
    }

    void QuestClear()
    {

    }

}
