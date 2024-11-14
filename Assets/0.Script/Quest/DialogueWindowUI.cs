using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueWindowUI : MonoBehaviour
{
    public TMP_Text stringArea;
    int idx = 0;
    public List<string> dialogue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNextBtn()
    {
        idx++;
        stringArea.text = dialogue[idx];

        if(idx>dialogue.Count)
        {
            gameObject.SetActive(false);
        }
    }
}
