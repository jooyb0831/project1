using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int MAXHP { get; set; } = 30;
    private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            GameUI.Instance.HP = hp;
        }
    }

    private int maxExp =25;
    public int MAXEXP
    {
        get { return maxExp; }
        set
        {
            maxExp = value;
            GameUI.Instance.MAXEXP = maxExp;
        }
    }

    private int exp;
    public int EXP
    {
        get { return exp; }
        set
        {
            exp = value;
            GameUI.Instance.EXP = exp;
        }
    }

    private int level = 1;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            GameUI.Instance.Lv = level;
        }
    }

    private int coin = 0;
    public int Coin
    {
        get { return coin; }
        set
        {
            coin = value;
            GameUI.Instance.Coin = coin;
        }
    }

    public float Speed { get; set; }

    public float RunSpeed { get; set; }

    public List<Quest> OnGoingQList;



    private int e0;
    public int Enemy0
    {
        get { return e0; }
        set
        {
            e0 = value;
            if(QuestManager.Instance.qUIList.Count !=0)
            {
                QuestManager.Instance.qUIList[0].curCnt = e0;
            }
            
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        MAXHP = 30;
        HP = MAXHP;
        MAXEXP = 25;
        EXP = 0;
        Speed = 4f;
        RunSpeed = 6f;
        Enemy0 = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EXP >= MAXEXP)
        {
            Level += 1;
            EXP = 0;
            MAXEXP += 10;
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            EXP += 5;
        }
    }
}
