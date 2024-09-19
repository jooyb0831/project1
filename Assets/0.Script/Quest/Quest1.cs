using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1 :Quest
{
    public QuestData data;
    public int curCnt;
    public int maxCnt;
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

        }
    }

    public override void Init()
    {
        if (gi == null)
        {
            gi = GameManager.Instance.gameUI;
        }
        curCnt = data.curCount;
        maxCnt = data.maxCount;
        questUI.quest = this;
        questUI.questTxt.text = $"{data.QuestTitle} ({curCnt}/{maxCnt})";
        Instantiate(questUI, gi.questArea);
        base.Init();
    }


}
