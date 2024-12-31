using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnchant : StatEnchantUI
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SetData();
    }

    public void SetData()
    {
        curTxt.text = $"{pd.AttackDamage}";
        upTxt.text = $"{enchtSystem.data.ATKdata.NextATK}";
        reqLvTxt.text = $"�ʿ� ���� : {enchtSystem.data.ATKdata.NeedLv}";
        coinTxt.text = $"{enchtSystem.data.ATKdata.Gold}/{pd.Coin}";
        int crystalCnt = Inventory.Instance.ItemCheck(enchtSystem.data.ATKdata.CrystalIdx);
        crystalTxt.text = $"{enchtSystem.data.ATKdata.CrystalNum}/{crystalCnt}";
    }

    public void OnCilcked()
    {
        if (enchtSystem.data.ATKdata.Gold <= pd.Coin && enchtSystem.data.ATKdata.NeedLv <= pd.Level 
            && Inventory.Instance.ItemCheck(enchtSystem.data.ATKdata.CrystalIdx) >= enchtSystem.data.ATKdata.CrystalNum)
        {
            //ü�� ���� ó��
            pd.AttackDamage = enchtSystem.data.ATKdata.NextATK;

            //��ȭ �� ������ ��� ó��
            pd.Coin -= enchtSystem.data.ATKdata.Gold;
            Inventory.Instance.Enchant(enchtSystem.data.ATKdata.CrystalIdx, enchtSystem.data.ATKdata.CrystalNum);
            
            
            //���� �ܰ� ����
            enchtSystem.ATKEnchant(); 
            SetData();
        }
        else
        {
            if (enchtSystem.data.ATKdata.Gold > pd.Coin)
            {
                GameUI.Instance.fullInvenObj.SetActive(true);
                GameUI.Instance.fullInvenObj.GetComponent<FullInvenObj>().Act(4);
                return;
            }

            else if (enchtSystem.data.ATKdata.NeedLv > pd.Level)
            {
                GameUI.Instance.fullInvenObj.SetActive(true);
                GameUI.Instance.fullInvenObj.GetComponent<FullInvenObj>().Act(5);
                return;
            }

            else if (Inventory.Instance.ItemCheck(enchtSystem.data.ATKdata.CrystalIdx) < enchtSystem.data.ATKdata.CrystalNum)
            {
                GameUI.Instance.fullInvenObj.SetActive(true);
                GameUI.Instance.fullInvenObj.GetComponent<FullInvenObj>().Act(6);
                return;
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (EnchantUI.Instance.window.activeSelf)
        {
            SetData();
        }
    }
}
