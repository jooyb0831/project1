using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int maxHP;
    public int MAXHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
            if(GameUI.Instance!=null)
            {
                GameUI.Instance.MAXHP = maxHP;
            }
            if(StatUI.Instance!=null)
            {
                StatUI.Instance.HP = maxHP;
            }

            
        }
    }
    private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if(GameUI.Instance!=null)
            {
                GameUI.Instance.HP = hp;
            }

        }
    }

    private int maxExp =25;
    public int MAXEXP
    {
        get { return maxExp; }
        set
        {
            maxExp = value;
            if (GameUI.Instance != null)
            {
                GameUI.Instance.MAXEXP = maxExp;
            }
            

        }
    }

    private int exp;
    public int EXP
    {
        get { return exp; }
        set
        {
            exp = value;
            if (GameUI.Instance != null)
            {
                GameUI.Instance.EXP = exp;
            }
            
        }
    }

    private int level = 1;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            if(GameUI.Instance!=null)
            {
                GameUI.Instance.Lv = level;
            }
            if(StatUI.Instance!=null)
            {
                StatUI.Instance.Level = level;
            }

        }
    }

    private int coin = 0;
    public int Coin
    {
        get { return coin; }
        set
        {
            coin = value;
            if(GameUI.Instance!=null)
            {
                GameUI.Instance.Coin = coin;
            }
            
            if(StatUI.Instance!=null)
            {
                StatUI.Instance.Gold = coin;
            }
            
        }
    }

    private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
            if(StatUI.Instance!=null)
            {
                StatUI.Instance.Speed = (int)speed;
            }
            
        }
    }

    public int AttackDamage { get; set; } = 2;
       

    public float RunSpeed { get; set; }

    public List<Quest> OnGoingQList;

    public bool[] StageCleared = { false, false, false };
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        MAXHP = 30;
        HP = MAXHP;
        EnchantSystem.Instance.HP = MAXHP;
        MAXEXP = 25;
        EXP = 0;
        Speed = 4f;
        RunSpeed = 6f;
        
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

        if(Input.GetKeyDown(KeyCode.F9))
        {
            Coin += 10;
        }
    }
}
