using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPotion : FieldItem
{
    public int Recover = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Using()
    {
        base.Using();
        if (pd.HP == pd.MAXHP)
        {
             Debug.Log("이미 체력이 가득 찼습니다");
             return;
        }
        else
        {
            if (pd.MAXHP - pd.HP >= Recover)
            {
                pd.HP += Recover;
            }
            else if (pd.MAXHP - pd.HP < Recover)
            {
                pd.HP = pd.MAXHP;
            }
        }
    }
}
