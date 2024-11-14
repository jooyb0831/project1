using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        SetString();
    }

    // Update is called once per frame
    void Update()
    {
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

    void SetString()
    {
        if(!qm.data.isQuest1Started)
        {
            curQuest = qm.qList[0];
            for(int i =0; i<curQuest.data.dialogue.Length; i++)
            {
                chats[i] = curQuest.data.dialogue[i];
            }
            for(int i = 0; i<chats.Count; i++)
            {
                window.GetComponent<DialogueWindowUI>().dialogue[i] = chats[i];
            }
            window.GetComponent<DialogueWindowUI>().stringArea.text = window.GetComponent<DialogueWindowUI>().dialogue[0];
        }
    }
}
