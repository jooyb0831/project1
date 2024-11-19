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
    public Quest curQuest = null;
    private QuestManager qm;
    // Start is called before the first frame update
    void Start()
    {
        qm = GameManager.Instance.QuestManager;
        SetQuest();
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

    void SetQuest()
    {
        if(!qm.data.isQuest1Started)
        {
            curQuest = qm.qList[0];
            window.GetComponent<DialogueWindowUI>().curQuest = curQuest;
            //window.GetComponent<DialogueWindowUI>().stringArea.text = window.GetComponent<DialogueWindowUI>().dialogue[0];
        }
    }

    public void SetString()
    {
        for (int i = 0; i < curQuest.data.basicDialogue.Length; i++)
        {
            Debug.Log($"{i}");
            basicDialogue.Add(curQuest.data.basicDialogue[i]);
            window.GetComponent<DialogueWindowUI>().basicDialogue.Add(curQuest.data.basicDialogue[i]);
        }
        for(int i = 0; i < curQuest.data.yesDialogue.Length; i++)
        {
            yesDialogue.Add(curQuest.data.yesDialogue[i]);
            window.GetComponent<DialogueWindowUI>().yesDialogue.Add(curQuest.data.yesDialogue[i]);
        }
        for(int i = 0; i< curQuest.data.noDialogue.Length; i++)
        {
            noDialogue.Add(curQuest.data.noDialogue[i]);
            window.GetComponent<DialogueWindowUI>().noDialogue.Add(curQuest.data.noDialogue[i]);
        }
        window.GetComponent<DialogueWindowUI>().SetCurDialogue(basicDialogue);
    }

}
