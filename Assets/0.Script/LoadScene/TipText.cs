using TMPro;
using UnityEngine;

public class TipText : MonoBehaviour
{
    [SerializeField] string[] tips = { "������ �������� 'O'Ű�� ���� ����� �� �ֽ��ϴ�.", 
        "�޴��� 'Tab'Ű�� �� �� �ֽ��ϴ�.", 
        "����Ʈ�� ������ Ȯ���Ϸ��� 'T'�� ��������." };

    float timer;
    float delay = 3f;
    int index;

    void Start()
    {
        index = RandomIndex();
        GetComponent<TMP_Text>().text = $"Tip. {tips[index]}";
    }

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

    int RandomIndex()
    {
        return Random.Range(0, tips.Length);
    }
}
