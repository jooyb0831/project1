using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public List<Quest> qList;
    public List<QuestUI> qUIList;
    private PlayerData pd;

    public List<int> enemyKillCnt;

    public int e0;
    public int e1;

    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.playerData;;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            pd.OnGoingQList.Add(qList[0]);
            Instantiate(qList[0], transform);
        }
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

    public void Check(Enemy enemy)
    {
        for(int i = 0; i<qList.Count; i++)
        {
            if (qList[i].data.objIndex == enemy.data.index)
            {
                enemyKillCnt[enemy.data.index]++;
                QuestUI qUI = Find(enemy.data.index);
                qUI.curCnt = enemyKillCnt[enemy.data.index];
                break;
            }
        }

    }

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
}
