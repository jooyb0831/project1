using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagerData
{
    public bool isQuest1Started = false;
}
public class QuestManager : Singleton<QuestManager>
{
    public List<Quest> qList;
    public List<Quest> onGoingQList;
    public List<QuestUI> qUIList;
    public List<QuestMenuUIPreset> qMUIList;
    private PlayerData pd;
    public QuestManagerData data = new QuestManagerData();

    public List<int> enemyKillCnt;

    public int e0;
    public int e1;

    //퀘스트 진행도는 pd쪽에 넣는게 나음.

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        pd = GameManager.Instance.PlayerData;;
        Init();
    }


    void Init()
    {
        for (int i = 0; i < 2; i++)
        {
            enemyKillCnt.Add(0);
        }
        e0 = enemyKillCnt[0];
        e1 = enemyKillCnt[1];
    }

    /// <summary>
    /// 킬카운트체크
    /// </summary>
    /// <param name="enemy"></param>
    public void Check(Enemy enemy)
    {
        int idx = -1;
        QuestUI qUI = null;
        QuestMenuUIPreset qMUI = null;
        if(onGoingQList.Count == 0)
        {
            return;
        }
        for (int i = 0; i< onGoingQList.Count; i++)
        {
            if (enemy.data.Index == onGoingQList[i].data.objIndex)
            {
                idx = i;
                qUI = Find(enemy.data.Index);
                break;
            }
        }
        for(int i = 0; i<onGoingQList.Count; i++)
        {
            if(enemy.data.Index == onGoingQList[i].data.objIndex)
            {
                idx = i;
                qMUI = FindUI(enemy.data.Index);
                break;
            }
        }
        if(idx == -1 || qUI == null || qMUI == null)
        {
            return;
        }
        else
        {
            enemyKillCnt[idx]++;
            onGoingQList[idx].data.curCount = enemyKillCnt[enemy.data.Index];
            qUI.curCnt = enemyKillCnt[enemy.data.Index];
            qMUI.curCnt = enemyKillCnt[enemy.data.Index];
        }
    }

    /// <summary>
    /// QuestUI 찾는 함수
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    QuestUI Find (int idx)
    {
        QuestUI qUI = null;
        for(int i =0; i<qUIList.Count; i++)
        {
            if(qUIList[i].objIndex == idx)
            {
                qUI = qUIList[i];
                break;
            }
        }
        return qUI;
    }

    /// <summary>
    /// Quest QuickUI찾는 함수
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    QuestMenuUIPreset FindUI (int idx)
    {
        QuestMenuUIPreset qMUI = null;
        for(int i =0; i<qList.Count; i++)
        {
            if(qUIList[i].objIndex == idx)
            {
                qMUI = qMUIList[i];
                break;
            }
        }
        return qMUI;
    }

    /// <summary>
    /// Quest추가
    /// </summary>
    /// <param name="quest"></param>
    public void AddQuest(Quest quest)
    {
        pd.OnGoingQList.Add(quest);
        //Quest obj = Instantiate(qList[0], transform);
        onGoingQList.Add(quest);
        quest.data.isStart = true;
        quest.QuestAdd();
    }
}
