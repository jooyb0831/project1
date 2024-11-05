using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TipText : MonoBehaviour
{
    [SerializeField] string[] tips = { "������ �������� 'O'Ű�� ���� ����� �� �ֽ��ϴ�.", 
        "�޴��� 'Tab'Ű�� �� �� �ֽ��ϴ�.", 
        "����Ʈ�� ������ Ȯ���Ϸ��� 'T'�� ��������." };

    float timer;
    float delay = 3f;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_Text>().text = $"Tip. {tips[index]}";
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=delay)
        {
            timer = 0;
            index++;
            if (index >= tips.Length)
            {
                index = 0;
            }
            GetComponent<TMP_Text>().text = $"Tip. {tips[index]}";
        }
    }
}
