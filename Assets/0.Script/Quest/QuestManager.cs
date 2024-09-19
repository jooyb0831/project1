using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] List<Quest> qList;
    private PlayerData pd;
    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.playerData;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            pd.OnGoingQList.Add(qList[0]);
            Instantiate(qList[0], transform);
        }
    }
}
