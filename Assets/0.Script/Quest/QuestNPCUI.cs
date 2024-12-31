using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestNPCUI : Singleton<QuestNPCUI>
{
    public GameObject window;
    public List<string> basicDialogue;
    public List<string> yesDialogue;
    public List<string> noDialogue;
    public List<string> questGoingDialogue;
    public List<string> questEndDialogue;
    public List<string> noQuestDialogue;
    public Quest curQuest = null;
    public Quest onGoingQuest = null;
    private QuestManager qm;
    public bool allDone = false;
    // Start is called before the first frame update
    void Start()
    {
        qm = GameManager.Instance.QuestManager;
        SetQuest();
        Init();
    }
    void Init()
    {
        questGoingDialogue.Add("임무가 끝나거든 알려주시오.");
        questEndDialogue.Add("고맙소. 여기 약속한 보상일세.");
        noQuestDialogue.Add("당분간은 부탁할 일이 없소.");

        if(qm.onGoingQList.Count!=0 && !allDone)
        {
            onGoingQuest = qm.onGoingQList[0];
        }
        
        if(curQuest == null)
        {
            allDone = true;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetQuest()
    {
        for (int i = 0; i < qm.qList.Count; i++)
        {
            if (!qm.qList[i].data.isEnd)
            {
                curQuest = qm.qList[i];
                break;
            }
        }
        window.GetComponent<DialogueWindowUI>().curQuest = curQuest;

       
    }

    public void SetString()
    {
        if(allDone)
        {
            window.GetComponent<DialogueWindowUI>().SetCurDialogue(noQuestDialogue);
            return;
        }

        if(onGoingQuest == null && !allDone)
        {
            for (int i = 0; i < curQuest.data.basicDialogue.Length; i++)
            {
                Debug.Log($"{i}");
                basicDialogue.Add(curQuest.data.basicDialogue[i]);
                window.GetComponent<DialogueWindowUI>().basicDialogue.Add(curQuest.data.basicDialogue[i]);
            }
            for (int i = 0; i < curQuest.data.yesDialogue.Length; i++)
            {
                yesDialogue.Add(curQuest.data.yesDialogue[i]);
                window.GetComponent<DialogueWindowUI>().yesDialogue.Add(curQuest.data.yesDialogue[i]);
            }
            for (int i = 0; i < curQuest.data.noDialogue.Length; i++)
            {
                noDialogue.Add(curQuest.data.noDialogue[i]);
                window.GetComponent<DialogueWindowUI>().noDialogue.Add(curQuest.data.noDialogue[i]);
            }
            window.GetComponent<DialogueWindowUI>().SetCurDialogue(basicDialogue);
        }

        else if(onGoingQuest!=null)
        {
            if(curQuest.data.isDone)
            {
                window.GetComponent<DialogueWindowUI>().SetCurDialogue(questEndDialogue);
                
            }
            else
            {
                window.GetComponent<DialogueWindowUI>().SetCurDialogue(questGoingDialogue);
            }
            
        }

        
    }

    public void QuestReset()
    {
        qm.onGoingQList.Clear();
        onGoingQuest = null;
        SetQuest();
    }

}
