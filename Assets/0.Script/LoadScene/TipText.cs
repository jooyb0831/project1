using TMPro;
using UnityEngine;

public class TipText : MonoBehaviour
{
    [SerializeField] string[] tips = { "장착한 아이템은 'O'키를 눌러 사용할 수 있습니다.", 
        "메뉴는 'Tab'키로 열 수 있습니다.", 
        "퀘스트를 빠르게 확인하려면 'T'를 누르세요." };

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
