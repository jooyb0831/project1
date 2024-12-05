using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeartEnchant : StatEnchantUI
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
        curTxt.text = $"{pd.MAXHP}";
        upTxt.text = $"{enchtSystem.data.HPdata.NextHP}";
        reqLvTxt.text = $"�ʿ� ���� : {enchtSystem.data.HPdata.NeedLv}";
        coinTxt.text = $"{enchtSystem.data.HPdata.Gold}/{pd.Coin}";
        int crystalCnt = Inventory.Instance.ItemCheck(enchtSystem.data.HPdata.CrystalIdx);
        crystalTxt.text = $"{enchtSystem.data.HPdata.CrystalNum}/{crystalCnt}";
    }

    public void OnCilcked()
    {
        if (enchtSystem.data.HPdata.Gold <= pd.Coin && enchtSystem.data.HPdata.NeedLv <= pd.Level && Inventory.Instance.ItemCheck(enchtSystem.data.HPdata.CrystalIdx) >= enchtSystem.data.HPdata.CrystalNum)
        {
            pd.MAXHP = enchtSystem.data.HPdata.NextHP; // ü�� ���� ó��
            pd.Coin -= enchtSystem.data.HPdata.Gold; // ��� ��� ó��
            Inventory.Instance.Enchant(0, enchtSystem.data.HPdata.CrystalNum); // �κ����� ������ ���ó�� �ڵ�
            enchtSystem.HPEnchant(); // ���� �ܰ� ����

            SetData();
        }
        else
        {
            if (enchtSystem.data.HPdata.Gold > pd.Coin)
            {
                Debug.Log("�ݾ��� �����մϴ�.");
                return;
            }

            else if (enchtSystem.data.HPdata.NeedLv > pd.Level)
            {
                Debug.Log("������ �����մϴ�.");
                return;
            }

            else if (Inventory.Instance.ItemCheck(0) < enchtSystem.data.HPdata.CrystalNum)
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
