using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    private QuestManager qm;
    // Start is called before the first frame update
    void Start()
    {
        qm = GameManager.Instance.QuestManager;
        SetQuestUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetQuestUI()
    {
        if (qm.onGoingQList.Count !=0)
        {
            qm.qUIList.Clear();
            qm.qMUIList.Clear();
            for (int i = 0; i<qm.onGoingQList.Count; i++)
            {
                Debug.Log("µ¹¾Æ°¨");
                qm.onGoingQList[i].SetQuestUI();
            }
        }
        else
        {
            return;
        }
    }
}
