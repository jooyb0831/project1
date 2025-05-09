using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public bool isStart = false;
    public string QuestTitle;
    public int QuestNumber;
    public bool isDone;
    public bool isEnd;
    public int maxCount;
    public int curCount;
    public int exp;
    public int gold;
    public QuestType qType;
    public int objIndex;
    public string QuestExplain;
    public string QuestRewardTxt;

    public string[] basicDialogue;
    public string[] yesDialogue;
    public string[] noDialogue;
}
public enum QuestType
{
    Kill,
    Gether
}
public abstract class Quest : MonoBehaviour
{
    public QuestData data = new QuestData();
    public QuestUI questUI;
    public QuestMenuUIPreset qMenuUI;
    public QuestUI thisQuestUI;
    public QuestMenuUIPreset thisQuestMenuUI;
    protected GameUI gi;
    protected MenuUI menuUI;
    protected PlayerData pData;


    public string qTitle;
    public int curCnt;
    public int maxCnt;

    public virtual void Init()
    {
        if(gi == null)
        {
            gi = GameManager.Instance.GameUI;
        }
        if(menuUI == null)
        {
            menuUI = GameManager.Instance.MUI;
        }
        data.curCount = 0;
    }

    /// <summary>
    /// QuestUI 추가
    /// </summary>
    public virtual void QuestAdd()
    {
        if(gi == null)
        {
            gi = GameManager.Instance.GameUI;
        }
        if(menuUI == null)
        {
            menuUI = GameManager.Instance.MUI;
        }
    }

    /// <summary>
    /// Quest 보상
    /// </summary>
    public virtual void QuestReward()
    {
        if (pData == null)
        {
            pData = GameManager.Instance.PlayerData;
        }
    }

    /// <summary>
    /// Quest제거
    /// </summary>
    public virtual void QuesUIRemove()
    {

    }
}
