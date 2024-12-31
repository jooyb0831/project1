using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class FullInvenObj : MonoBehaviour
{
    string[] notice = { "가방이 가득 찼습니다", "체력이 가득 찼습니다", "장착된 스킬이 없습니다", "장착된 아이템이 없습니다.",
        "금액이 부족합니다","레벨이 부족합니다","재료가 부족합니다","수량이 너무 많습니다", "더 이상 강화할 수 없습니다."};

    [SerializeField] TMP_Text explainTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Act(int idx)
    {
        SetString(idx);
        GetComponent<Image>().DOFade(0.5f, 1f)
            .SetUpdate(true)
            .OnComplete(
            () =>
            {
                GetComponent<Image>().DOFade(0, 1f)
                .SetUpdate(true)
                .OnComplete(() => gameObject.SetActive(false));
            });
    }

    void SetString(int idx)
    {
        explainTxt.text = notice[idx];
    }

}
