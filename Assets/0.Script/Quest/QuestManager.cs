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

    //����Ʈ ���൵�� pd�ʿ� �ִ°� ����.


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
    /// ���� ���� ������ ųī��Ʈ�� üũ�ϴ� �Լ�
    /// </summary>
    /// <param name="enemy">���� �ε��� ��ȣ</param>
    public void Check(int eIdx)
    {
        int idx = -1;
        QuestUI qUI = null;
        QuestMenuUIPreset qMUI = null;
        if(onGoingQList.Count == 0)
        {
            return;
        }
        //���� ������ �� �ִ� ����Ʈ����� ���鼭
        for (int i = 0; i< onGoingQList.Count; i++)
        {
            //���� �ε��� ��ȣ�� ����Ʈ�� �� �ε��� ��ȣ�� ������
            if (eIdx == onGoingQList[i].data.objIndex)
            {
                idx = onGoingQList[i].data.objIndex;
                //����Ʈ�� UI�� ã�Ƽ� ����
                qUI = Find(eIdx);
                qMUI = FindUI(eIdx);
                break;
            }
        }
        //���� ��ġ�ϴ� �׸��� ���ų� UI�� ���ٸ� ����
        if(idx == -1 || qUI == null || qMUI == null)
        {
            return;
        }
        //�� ���� ���
        else
        {
            //�ش��ϴ� �ε����� �� ų ī��Ʈ ����
            enemyKillCnt[idx]++;
            //���� �������� ����Ʈ �������� ī��Ʈ�� ����ȭ
            onGoingQList[0].data.curCount = enemyKillCnt[eIdx];
            //UI�� �ݿ�
            qUI.curCnt = enemyKillCnt[eIdx];
            qMUI.curCnt = enemyKillCnt[eIdx];
        }
    }

    /// <summary>
    /// QuestUI ã�� �Լ�
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    QuestUI Find (int idx)
    {
        QuestUI qUI = null;
        //����Ʈ�� UI ����Ʈ��ŭ ���鼭
        for(int i = 0; i < qUIList.Count; i++)
        {
            //��ǥ�� ������Ʈ �ε����� �����ϴٸ�
            if(qUIList[i].objIndex == idx)
            {
                //UI�� ����
                qUI = qUIList[i];
                break;
            }
        }
        return qUI;
    }

    /// <summary>
    /// Quest QuickUIã�� �Լ�
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
    /// Quest�߰�
    /// </summary>
    /// <param name="quest"></param>
    public void AddQuest(Quest quest)
    {
        //PlayerData�ʿ� ����Ʈ �߰�
        pd.OnGoingQList.Add(quest);
        //���� �������� ����Ʈ ��Ͽ� �߰�
        onGoingQList.Add(quest);
        //�ش� ����Ʈ ���� ó��
        quest.data.isStart = true;
        //����Ʈ �߰�ó��
        quest.QuestAdd();
    }
}
