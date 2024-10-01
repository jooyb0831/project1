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
        GetComponent<Image>().DOFade(1, 3f);
    }
}
