using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatEnchantUI : MonoBehaviour
{
    [SerializeField] PlayerData pd;
    [SerializeField] EnchantSystem enchtSystem;
    [SerializeField] TMP_Text curHPTxt;
    [SerializeField] TMP_Text upHPTxt;
    [SerializeField] TMP_Text reqLvTxt;
    [SerializeField] TMP_Text coinTxt;
    [SerializeField] TMP_Text itemTxt;

    
    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
        enchtSystem = GameManager.Instance.EnchtSystem;
        SetData();

    }

    void SetData()
    {
        curHPTxt.text = $"{pd.MAXHP}";
        upHPTxt.text = $"{enchtSystem.data.HPdata.NextHP}";
        reqLvTxt.text = $"필요 레벨 : {enchtSystem.data.HPdata.NeedLv}";
        coinTxt.text = $"{enchtSystem.data.HPdata.Gold}/{pd.Coin}";
        int itemCnt = Inventory.Instance.ItemCheck(0);
        itemTxt.text = $"{enchtSystem.data.HPdata.CyrstalNum}/{itemCnt}";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8)) 
        {
            Debug.Log($"{pd.MAXHP}");
        }
        
    }

    public void OnCilcked()
    {
        if(enchtSystem.data.HPdata.Gold<=pd.Coin && enchtSystem.data.HPdata.NeedLv<=pd.Level && Inventory.Instance.ItemCheck(0)>=enchtSystem.data.HPdata.CyrstalNum)
        {
            pd.MAXHP = enchtSystem.data.HPdata.NextHP; // 체력 증가 처리
            pd.Coin -= enchtSystem.data.HPdata.Gold; // 골드 사용 처리
            Inventory.Instance.Enchant(0, enchtSystem.data.HPdata.CyrstalNum); // 인벤에서 아이템 사용처리 코드
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

            else if (Inventory.Instance.ItemCheck(0) < enchtSystem.data.HPdata.CyrstalNum)
            {
                Debug.Log("강화 재료가 부족합니다.");
                return;
            }
        }

    }
}
