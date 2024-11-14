using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestNPCUI : Singleton<QuestNPCUI>
{
    public GameObject window;
    public List<string> chats;
    [SerializeField] Quest curQuest = null;
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
            Debug.Log($"{curQuest.data.dialogue.Length}");
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
            //window.GetComponent<DialogueWindowUI>().stringArea.text = window.GetComponent<DialogueWindowUI>().dialogue[0];
        }
    }

    public void SetString()
    {
        for (int i = 0; i < curQuest.data.dialogue.Length; i++)
        {
            Debug.Log($"{i}");
            chats.Add(curQuest.data.dialogue[i]);
            window.GetComponent<DialogueWindowUI>().dialogue.Add(curQuest.data.dialogue[i]);
        }
        window.GetComponent<DialogueWindowUI>().stringArea.text = curQuest.data.dialogue[0];
    }

}
