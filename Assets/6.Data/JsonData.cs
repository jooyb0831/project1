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
    public Quest1Dialogue q1Dialogue = new Quest1Dialogue();
}

[System.Serializable]
public class SkillJsonData
{
    public List<SkillData> sData = new List<SkillData>();
}

[System.Serializable]
public class QuestDialogueJsonData
{
    public List<QuestDialogueData> questDialogueData = new List<QuestDialogueData>();
} 

public class JsonData : Singleton<JsonData>
{
    [SerializeField] private TextAsset skillJson;
    [SerializeField] private TextAsset questDialogueJson;

    public SkillJsonData skillData = new SkillJsonData();
    public QuestDialogueJsonData qDiaData = new QuestDialogueJsonData();
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        skillData = JsonUtility.FromJson<SkillJsonData>(skillJson.text);
        qDiaData = JsonUtility.FromJson<QuestDialogueJsonData>(questDialogueJson.text);
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
