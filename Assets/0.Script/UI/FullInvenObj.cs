using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FullInvenObj : MonoBehaviour
{
    string[] notice = { "가방이 가득 찼습니다", "체력이 가득 찼습니다", "장착된 스킬이 없습니다", "장착된 아이템이 없습니다.",
        "금액이 부족합니다","레벨이 부족합니다","재료가 부족합니다","수량이 너무 많습니다", "더 이상 강화할 수 없습니다."};

    [SerializeField] TMP_Text explainTxt;


    public void Act(int idx)
    {
        SetString(idx);
        explainTxt.DOFade(1f, 1f).SetUpdate(true);
        GetComponent<Image>().DOFade(0.7f, 1f)
            .SetUpdate(true)
            .OnComplete(
            () =>
            {
                explainTxt.DOFade(0, 1f);
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
