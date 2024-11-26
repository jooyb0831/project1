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
        int itemCnt = Inventory.Instance.ItemCheck(enchtSystem.data.ATKdata.CrystalIdx);
        itemTxt.text = $"{enchtSystem.data.ATKdata.CrystalNum}/{itemCnt}";
    }

    public void OnCilcked()
    {
        if (enchtSystem.data.ATKdata.Gold <= pd.Coin && enchtSystem.data.ATKdata.NeedLv <= pd.Level && Inventory.Instance.ItemCheck(enchtSystem.data.ATKdata.CrystalIdx) >= enchtSystem.data.ATKdata.CrystalNum)
        {
            pd.AttackDamage = enchtSystem.data.ATKdata.NextATK; // ü�� ���� ó��
            pd.Coin -= enchtSystem.data.ATKdata.Gold; // ��� ��� ó��
            Inventory.Instance.Enchant(enchtSystem.data.ATKdata.CrystalIdx, enchtSystem.data.ATKdata.CrystalNum); // �κ����� ������ ���ó�� �ڵ�
            enchtSystem.ATKEnchant(); // ���� �ܰ� ����

            SetData();
        }
        else
        {
            if (enchtSystem.data.ATKdata.Gold > pd.Coin)
            {
                Debug.Log("�ݾ��� �����մϴ�.");
                return;
            }

            else if (enchtSystem.data.ATKdata.NeedLv > pd.Level)
            {
                Debug.Log("������ �����մϴ�.");
                return;
            }

            else if (Inventory.Instance.ItemCheck(enchtSystem.data.ATKdata.CrystalIdx) < enchtSystem.data.ATKdata.CrystalNum)
            {
                Debug.Log("��ȭ ��ᰡ �����մϴ�.");
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
