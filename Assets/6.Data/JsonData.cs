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
public class SkillJsonData
{
    public List<SkillData> sData = new List<SkillData>();
}
public class JsonData : Singleton<JsonData>
{
    [SerializeField] private TextAsset skillJson;
    public SkillJsonData skillData = new SkillJsonData();

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        skillData = JsonUtility.FromJson<SkillJsonData>(skillJson.text);
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
