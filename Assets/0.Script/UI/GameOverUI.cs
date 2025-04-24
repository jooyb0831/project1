using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] GameObject retryBtn;
    [SerializeField] GameObject quitBtn;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Active()
    {
        GetComponent<Image>().DOFade(1, 1.5f).SetUpdate(true).OnComplete(() => Time.timeScale = 0);
    }

    public void OnClickedRetryBtn()
    {
        Time.timeScale = 1;
        SceneChanger.Instance.ReloadScene();

    }

    public void OnClickedQuitBtn()
    {
        Time.timeScale = 1;
        SceneChanger.Instance.GoShip();
    }
}
