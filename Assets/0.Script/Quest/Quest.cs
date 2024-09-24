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
    public QuestType qType;
    public int objIndex;
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
    protected GameUI gi;
    protected PlayerData pData;


    public string qTitle;
    public int curCnt;
    public int maxCnt;
    public virtual void Init()
    {
        if(gi == null)
        {
            gi = GameManager.Instance.gameUI;
        }
        data.curCount = 0;
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
