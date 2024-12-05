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
        reqLvTxt.text = $"필요 레벨 : {enchtSystem.data.HPdata.NeedLv}";
        coinTxt.text = $"{enchtSystem.data.HPdata.Gold}/{pd.Coin}";
        int crystalCnt = Inventory.Instance.ItemCheck(enchtSystem.data.HPdata.CrystalIdx);
        crystalTxt.text = $"{enchtSystem.data.HPdata.CrystalNum}/{crystalCnt}";
    }

    public void OnCilcked()
    {
        if (enchtSystem.data.HPdata.Gold <= pd.Coin && enchtSystem.data.HPdata.NeedLv <= pd.Level && Inventory.Instance.ItemCheck(enchtSystem.data.HPdata.CrystalIdx) >= enchtSystem.data.HPdata.CrystalNum)
        {
            pd.MAXHP = enchtSystem.data.HPdata.NextHP; // 체력 증가 처리
            pd.Coin -= enchtSystem.data.HPdata.Gold; // 골드 사용 처리
            Inventory.Instance.Enchant(0, enchtSystem.data.HPdata.CrystalNum); // 인벤에서 아이템 사용처리 코드
            enchtSystem.HPEnchant(); // 다음 단계 셋팅

            SetData();
        }
        else
        {
            if (enchtSystem.data.HPdata.Gold > pd.Coin)
            {
                Debug.Log("금액이 부족합니다.");
                return;
            }

            else if (enchtSystem.data.HPdata.NeedLv > pd.Level)
            {
                Debug.Log("레벨이 부족합니다.");
                return;
            }

            else if (Inventory.Instance.ItemCheck(0) < enchtSystem.data.HPdata.CrystalNum)
            {
                Debug.Log("강화 재료가 부족합니다.");
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
