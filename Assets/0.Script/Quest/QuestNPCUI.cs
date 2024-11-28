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
    public Quest curQuest = null;
    public Quest onGoingQuest = null;
    private QuestManager qm;
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

        if(qm.onGoingQList.Count!=0)
        {
            onGoingQuest = qm.onGoingQList[0];
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            //Debug.Log($"{curQuest.data.dialogue.Length}");
        }
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (window.activeSelf)
            {
                window.SetActive(false);
            }
            else if (!window.activeSelf)
            {
                return;
            }
        }
        */
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
        /*
        if (!qm.data.isQuest1Started)
        {
            curQuest = qm.qList[0];
            window.GetComponent<DialogueWindowUI>().curQuest = curQuest;
            //window.GetComponent<DialogueWindowUI>().stringArea.text = window.GetComponent<DialogueWindowUI>().dialogue[0];
        }
        */
    }

    public void SetString()
    {
        if(onGoingQuest == null)
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
