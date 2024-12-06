using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueWindowUI : MonoBehaviour
{
    public TMP_Text stringArea;
    int idx = 0;
    public List<string> basicDialogue;
    public List<string> yesDialogue;
    public List<string> noDialogue;
    [SerializeField] List<string> currentDialogue;
    [SerializeField] GameObject answerWindow;
    [SerializeField] GameObject nextBtn;
    [SerializeField] bool isBasicDialogue = true;
    public Quest curQuest = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurDialogue(List<string> dialogue)
    {
        currentDialogue.Clear();
        for(int i = 0; i<dialogue.Count; i++)
        {
            currentDialogue.Add(dialogue[i]);
        }
        stringArea.text = currentDialogue[0];
    }

    public void OnClickNextBtn()
    {
        idx++;

        if (idx<currentDialogue.Count)
        {
            stringArea.text = currentDialogue[idx];
        }    

        else if (idx >= currentDialogue.Count)
        {
            
            if(curQuest.data.isDone)
            {
                curQuest.QuestReward();
                curQuest.QuesUIRemove();
                gameObject.SetActive(false);
                curQuest = null;
                QuestNPCUI.Instance.QuestReset();
                idx = 0;
                return;
            }
            else if(curQuest.data.isStart && !curQuest.data.isDone)
            {
                gameObject.SetActive(false);
               
            }
            else if(!curQuest.data.isStart)
            {
                if(isBasicDialogue)
                {
                    answerWindow.SetActive(true);
                    nextBtn.gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                    isBasicDialogue = true;
                }
            }
        }
    }

    void DialoguePlayer(List<string> strs)
    {
        idx++;
        if(idx<strs.Count)
        {
            stringArea.text = strs[idx];
        }
        else if(idx==strs.Count)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnYesClicked()
    {
        SetCurDialogue(yesDialogue);
        isBasicDialogue = false;
        idx = 0;
        QuestNPCUI.Instance.onGoingQuest = curQuest;
        QuestManager.Instance.AddQuest(curQuest);
        answerWindow.SetActive(false);
        nextBtn.gameObject.SetActive(true);
    }

    public void OnNoClicked()
    {
        SetCurDialogue(noDialogue);
        isBasicDialogue = false;
        idx = 0;
        answerWindow.SetActive(false);
        nextBtn.gameObject.SetActive(true);
    }

    
}
