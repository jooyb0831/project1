using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class FullInvenObj : MonoBehaviour
{
    string[] notice = { "������ ���� á���ϴ�", "ü���� ���� á���ϴ�", "������ ��ų�� �����ϴ�", "������ �������� �����ϴ�.",
        "�ݾ��� �����մϴ�","������ �����մϴ�","��ᰡ �����մϴ�","������ �ʹ� �����ϴ�", "�� �̻� ��ȭ�� �� �����ϴ�."};

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
