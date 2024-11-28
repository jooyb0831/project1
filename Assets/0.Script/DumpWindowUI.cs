using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DumpWindowUI : MonoBehaviour
{
    public Transform slot;
    public Button plusBtn;
    public Button miusBtn;
    public TMP_InputField numInputField;
    public int count;

    private Player p;
    private PlayerData pd;
    [SerializeField] InvenItem invenItem;
    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(InvenItem item)
    {
        invenItem = item;
        Instantiate(item.gameObject, slot);
        count = item.data.count;
    }

    public void OnOKBtn()
    {
        if(int.Parse(numInputField.text)>invenItem.data.count)
        {
            Debug.Log("수량이 너무 많습니다.");
            return;
        }
        Inventory.Instance.ItemCount(invenItem, int.Parse(numInputField.text), true);
        Destroy(gameObject);
    }

    public void OnPlusBtn()
    {
        int x = int.Parse(numInputField.text);
        if (x >= invenItem.data.count)
        {
            return;
        }
        x += 1;
        numInputField.text = $"{x}";
    }

    public void OnMinusBtn()
    {
        int x = int.Parse(numInputField.text);
        if (x <= 0)
        {
            return;
        }
        x -= 1;
        numInputField.text = $"{x}";
    }

    public void OnExitBtn()
    {
        Destroy(gameObject);
    }
}
