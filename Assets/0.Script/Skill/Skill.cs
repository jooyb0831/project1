using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public string SkillTitle { get; set; }
        public string SkillExplain { get; set; }
        public int Index { get; set; }
        public float CoolTime { get; set; }
        public int Damage { get; set; }
        public int NeedLevel { get; set; }
        public int SkillLevel { get; set; }

        public Sprite SkillIcon;

    }
    protected Player p;
    protected PlayerData pd;
    public Data data = new Data();
    public bool isSet = false;
    public int slotIdx;
    public Transform slot;
    public SkillUISample ui;
    public bool isStart = false;
    public bool isWorking = false;
    // Start is called before the first frame update
    void Start()
    {

    }



    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        isSet = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log($"{data.CoolTime}");
        }

    }
}
