using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScene : MonoBehaviour
{
    [SerializeField] GameObject helpWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartBtn()
    {
        SceneChanger.Instance.GameStart();
    }

    public void OnHelpBtn()
    {
        helpWindow.SetActive(true);
    }

    public void OnExitBtn()
    {
        Application.Quit();
    }
}
