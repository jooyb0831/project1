using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2 : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (data.curCount >= data.maxCount)
        {
            data.isDone = true;
        }
    }
    public override void Init()
    {
        if (gi == null)
        {
            gi = GameManager.Instance.GameUI;
        }
        if (menuUI == null)
        {
            menuUI = GameManager.Instance.MUI;
        }
        data.QuestTitle = "용암 몬스터 처치";
        data.QuestNumber = 1;
        data.isDone = false;
        data.maxCount = 2;
        data.curCount = 0;
        data.exp = 20;
        data.gold = 25;
        data.qType = QuestType.Kill;
        data.objIndex = 1;
        data.QuestExplain = $"용암 몬스터 {data.maxCount}마리를 처치하기.";
        data.QuestRewardTxt = $"보상 : EXP {data.exp}, 골드 {data.gold}";
        data.basicDialogue = JsonData.Instance.questDialogueData.questDialogueData[1].quest1Basic;
        data.yesDialogue = JsonData.Instance.questDialogueData.questDialogueData[1].quest1Y;
        data.noDialogue = JsonData.Instance.questDialogueData.questDialogueData[1].quest1N;

        questUI.quest = this;
        qMenuUI.quest = this;

        base.Init();
    }
    public override void QuestAdd()
    {
        base.QuestAdd();
        QuestUI qUI = Instantiate(questUI, gi.questArea);
        QuestMenuUIPreset qMUI = Instantiate(qMenuUI, menuUI.questArea);
        qUI.SetData(this.data);
        qMUI.SetData(this.data);


        GameManager.Instance.QuestManager.qUIList.Add(qUI);
        GameManager.Instance.QuestManager.qMUIList.Add(qMUI);

        thisQuestUI = qUI;
        thisQuestMenuUI = qMUI;

       
    }


    public override void QuestReward()
    {
        base.QuestReward();
        pData.EXP += data.exp;
        pData.Coin += data.gold;
        data.isEnd = true;

    }

    public override void QuesUIRemove()
    {
        thisQuestUI.gameObject.SetActive(false);
        thisQuestMenuUI.gameObject.SetActive(false);
    }
}
