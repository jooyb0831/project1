using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public string skilltitle;
    public string skillexplain;
    public int index;
    public float cooltime;
    public int damage;
    public int needlevel;
    public int skilllevel;
}

[System.Serializable]
public class QuestDialogueData
{
    [System.Serializable]
    public class Quest1Dialogue
    {
        public string[] quest1Basic;
        public string[] quest1Y;
        public string[] quest1N;
    }
    public List<Quest1Dialogue> questDialogueData = new List<Quest1Dialogue>();
}

[System.Serializable]
public class EnemyData
{
    public int hp;
    public int index;
    public float speed;
    public int atkPower;
    public int exp;
}

[System.Serializable]
public class SkillJsonData
{
    public List<SkillData> sData = new List<SkillData>();
}

[System.Serializable]
public class EnemyJsonData
{
    public List<EnemyData> eData = new List<EnemyData>();
}

[System.Serializable]
public class BossEnemyData
{
    public int maxHP;
    public int exp;
    public int atk1Power;
    public int atk2Power;
    public float speed;
    public int index;
    public string bossName;
}

[System.Serializable]
public class BossJsonData
{
    public List<BossEnemyData> bossData = new List<BossEnemyData>();
}

/*
[System.Serializable]
public class QuestDialogueJsonData
{
    public List<QuestDialogueData> questDialogueData = new List<QuestDialogueData>();
} 
*/

public class JsonData : Singleton<JsonData>
{
    [SerializeField] private TextAsset skillJson;
    [SerializeField] private TextAsset questDialogueJson;
    [SerializeField] private TextAsset enemyJson;
    [SerializeField] private TextAsset bossJson;

    public SkillJsonData skillData = new SkillJsonData();
    public QuestDialogueData questDialogueData = new QuestDialogueData();
    public EnemyJsonData enemyData = new EnemyJsonData();
    public BossJsonData bossJsonData = new BossJsonData();
    //public QuestDialogueJsonData qDiaData = new QuestDialogueJsonData();
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        skillData = JsonUtility.FromJson<SkillJsonData>(skillJson.text);
        questDialogueData = JsonUtility.FromJson<QuestDialogueData>(questDialogueJson.text);
        enemyData = JsonUtility.FromJson<EnemyJsonData>(enemyJson.text);
        bossJsonData = JsonUtility.FromJson<BossJsonData>(bossJson.text);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            Debug.Log($"{skillData.sData[0].skillexplain}");
        }
    }
}
