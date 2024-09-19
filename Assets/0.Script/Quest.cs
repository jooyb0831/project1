using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public string QuestTitle;
    public int QuestNumber;
    public bool isDone;
    public int maxCount;
    public int curCount;
    public int exp;
}
public enum QuestType
{
    Kill,
    Get
}
public class Quest : MonoBehaviour
{
    [SerializeField] List<QuestData> qData;
    private PlayerData pd;
    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.playerData;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            pd.OnGoingQList.Add(qData[0]);
        }
    }
}
