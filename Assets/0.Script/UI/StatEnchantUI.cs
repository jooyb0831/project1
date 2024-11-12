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
        reqLvTxt.text = $"�ʿ� ���� : {enchtSystem.data.HPdata.NeedLv}";
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
            pd.MAXHP = enchtSystem.data.HPdata.NextHP; // ü�� ���� ó��
            pd.Coin -= enchtSystem.data.HPdata.Gold; // ��� ��� ó��
            Inventory.Instance.Enchant(0, enchtSystem.data.HPdata.CyrstalNum); // �κ����� ������ ���ó�� �ڵ�
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

            else if (Inventory.Instance.ItemCheck(0) < enchtSystem.data.HPdata.CyrstalNum)
            {
                Debug.Log("��ȭ ��ᰡ �����մϴ�.");
                return;
            }
        }

    }
}
