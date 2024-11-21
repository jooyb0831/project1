using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectScene : MonoBehaviour
{
    private PlayerData pd;

    [SerializeField] Button stage2;
    [SerializeField] GameObject stage2LockedIcon;

    [SerializeField] Button stage3;
    [SerializeField] GameObject stage3LockedIcon;
    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
    }

    // Update is called once per frame
    void Update()
    {
        if(pd.StageCleared[0]==true)
        {
            stage2LockedIcon.SetActive(false);
            stage2.interactable = true;
        }
        
        if(pd.StageCleared[1] == true)
        {
            stage3LockedIcon.SetActive(false);
            stage3.interactable = true;
        }
    }
    public void OnClickedBackBtn()
    {
        SceneChanger.Instance.GoShip();
    }
    public void OnStage1Clicked()
    {
        SceneChanger.Instance.Stage1();
    }

    public void OnStage2Clicked()
    {
        SceneChanger.Instance.Stage2();
    }

    public void OnStage3Clicekd()
    {
        SceneChanger.Instance.Stage3();
    }
}
