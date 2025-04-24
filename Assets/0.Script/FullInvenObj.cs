using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FullInvenObj : MonoBehaviour
{
    string[] notice = { "������ ���� á���ϴ�", "ü���� ���� á���ϴ�", "������ ��ų�� �����ϴ�", "������ �������� �����ϴ�.",
        "�ݾ��� �����մϴ�","������ �����մϴ�","��ᰡ �����մϴ�","������ �ʹ� �����ϴ�", "�� �̻� ��ȭ�� �� �����ϴ�."};

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
