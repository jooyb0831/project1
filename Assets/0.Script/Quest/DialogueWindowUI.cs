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

    /// <summary>
    /// UI 다이얼로그 세팅
    /// </summary>
    /// <param name="dialogue"></param>
    public void SetCurDialogue(List<string> dialogue)
    {
        currentDialogue.Clear();
        for(int i = 0; i<dialogue.Count; i++)
        {
            currentDialogue.Add(dialogue[i]);
        }
        stringArea.text = currentDialogue[0];
    }

    /// <summary>
    /// Next버튼
    /// </summary>
    public void OnClickNextBtn()
    {
        idx++;

        if (idx<currentDialogue.Count)
        {
            stringArea.text = currentDialogue[idx];
        }    

        else if (idx >= currentDialogue.Count)
        {
            //퀘스트 조건 달성 시
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
            
            //퀘스트 수락 중 조건 미달성 시
            else if(curQuest.data.isStart && !curQuest.data.isDone)
            {
                Time.timeScale = 1;
                gameObject.SetActive(false);
               
            }
            
            //모든 퀘스트 종료했을 경우
            else if (QuestNPCUI.Instance.allDone)
            {
                Time.timeScale = 1;
                gameObject.SetActive(false);
            }

            //퀘스트 미수락 상태
            else if(!curQuest.data.isStart)
            {
                //기본 다이얼로그였다면
                if(isBasicDialogue)
                {
                    //수락 창 활성화
                    answerWindow.SetActive(true);
                    nextBtn.gameObject.SetActive(false);
                }

                //기본 다이얼로그가 아닌 상태 : 대화 종료
                else
                {
                    Time.timeScale = 1;
                    gameObject.SetActive(false);
                    isBasicDialogue = true;
                }
            }
        }
    }

    /// <summary>
    /// 다이얼로그 재생
    /// </summary>
    /// <param name="strs"></param>
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

    /// <summary>
    /// Yes버튼
    /// </summary>
    public void OnYesClicked()
    {
        SetCurDialogue(yesDialogue);
        isBasicDialogue = false;
        idx = 0;
        QuestNPCUI.Instance.onGoingQuest = curQuest;
        QuestManager.Instance.AddQuest(curQuest);
        answerWindow.SetActive(false);
        nextBtn.SetActive(true);
    }

    /// <summary>
    /// No버튼
    /// </summary>
    public void OnNoClicked()
    {
        SetCurDialogue(noDialogue);
        isBasicDialogue = false;
        idx = 0;
        answerWindow.SetActive(false);
        nextBtn.SetActive(true);
    }

    
}
