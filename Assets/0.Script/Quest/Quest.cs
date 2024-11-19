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
    Get
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

    public virtual void QuestAdd()
    {

    }

    public virtual void SetQuestUI()
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
