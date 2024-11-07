using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBuyWindow : MonoBehaviour
{
    public Transform slot;
    public Button plusBtn;
    public Button minusBtn;
    public TMP_InputField numInputField;
    public TMP_Text totalPriceTxt;

    private PlayerData pd;
    [SerializeField] InvenItem invenItem;
    [SerializeField] int price;
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
        price = item.data.price;
    }

    [SerializeField] int totalPrice;
    public void OnInputField()
    {
        string numStr = numInputField.text;
        int x = int.Parse(numStr);
        totalPrice = x * price;
        totalPriceTxt.text = $"{totalPrice}";
    }

    public void OnBuyBtn()
    {
        if(int.Parse(numInputField.text)>invenItem.data.count)
        {
            Debug.Log("수량이 너무 많습니다.");
            return;
        }

        if(totalPrice>pd.Coin)
        {
            Debug.Log("골드가 부족합니다.");
            return;
        }

        Inventory.Instance.ItemCount(invenItem, int.Parse(numInputField.text), false);
        //MerchantSystem.Instance.FindItem(invenItem, int.Parse(numInputField.text));
        pd.Coin -= totalPrice;
        MerchantSystem.Instance.SetInven();
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

    public void OnValueChange()
    {
        string numStr = numInputField.text;
        int x = int.Parse(numStr);
        totalPrice = x * price;
        totalPriceTxt.text = $"{totalPrice}";
    }
}
