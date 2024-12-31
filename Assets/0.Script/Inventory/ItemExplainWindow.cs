using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemExplainWindow : MonoBehaviour
{
    [SerializeField] TMP_Text titleTxt;
    [SerializeField] TMP_Text explainTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(InvenItem item)
    {
        titleTxt.text = item.data.title;
        explainTxt.text = $"°¡°Ý : {item.data.price}°ñµå";
    }
}
