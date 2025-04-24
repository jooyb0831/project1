using System.Collections.Generic;

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
    /// 적을 죽일 때마다 킬카운트를 체크하는 함수
    /// </summary>
    /// <param name="enemy">적의 인덱스 번호</param>
    public void Check(int eIdx)
    {
        int idx = -1;
        QuestUI qUI = null;
        QuestMenuUIPreset qMUI = null;
        if(onGoingQList.Count == 0)
        {
            return;
        }
        //현재 진행할 수 있는 퀘스트목록을 돌면서
        for (int i = 0; i< onGoingQList.Count; i++)
        {
            //적의 인덱스 번호와 퀘스트의 적 인덱스 번호가 같으면
            if (eIdx == onGoingQList[i].data.objIndex)
            {
                idx = onGoingQList[i].data.objIndex;
                //퀘스트의 UI를 찾아서 세팅
                qUI = Find(eIdx);
                qMUI = FindUI(eIdx);
                break;
            }
        }
        //만약 일치하는 항목이 없거나 UI가 없다면 리턴
        if(idx == -1 || qUI == null || qMUI == null)
        {
            return;
        }
        //그 외의 경우
        else
        {
            //해당하는 인덱스의 적 킬 카운트 증가
            enemyKillCnt[idx]++;
            //현재 진행중인 퀘스트 데이터의 카운트와 동일화
            onGoingQList[0].data.curCount = enemyKillCnt[eIdx];
            //UI도 반영
            qUI.curCnt = enemyKillCnt[eIdx];
            qMUI.curCnt = enemyKillCnt[eIdx];
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
        //퀘스트의 UI 리스트만큼 돌면서
        for(int i = 0; i < qUIList.Count; i++)
        {
            //목표물 오브젝트 인덱스와 동일하다면
            if(qUIList[i].objIndex == idx)
            {
                //UI를 연결
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
        //PlayerData쪽에 퀘스트 추가
        pd.OnGoingQList.Add(quest);
        //현재 진행중인 퀘스트 목록에 추가
        onGoingQList.Add(quest);
        //해당 퀘스트 시작 처리
        quest.data.isStart = true;
        //퀘스트 추가처리
        quest.QuestAdd();
    }
}
