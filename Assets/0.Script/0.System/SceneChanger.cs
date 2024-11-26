using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    GameStart,
    Ship,
    MapSelect,
    Stage1,
    Stage2,
    Stage3,
    Loading
}
public class SceneChanger : Singleton<SceneChanger>
{
    public SceneType sceneType = SceneType.Ship;
    private Inventory inven;
    private PlayerData pd;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        inven = GameManager.Instance.Inven;
        pd = GameManager.Instance.PlayerData;
    }

    public void GameStart()
    {
        LoadingSceneManager.LoadScene("Ship");
        sceneType = SceneType.Ship;
    }

    public void MapSelect()
    {
        SceneManager.LoadScene("MapSelectScene");
        sceneType = SceneType.MapSelect;
    }

    public void Stage1()
    {
        LoadingSceneManager.LoadScene("Test");
    }

    public void Stage2()
    {
        LoadingSceneManager.LoadScene("Stage2");
    }

    public void Stage3()
    {
        LoadingSceneManager.LoadScene("Stage3");
    }

    public void GoShip()
    {
        LoadingSceneManager.LoadScene("Ship");
    }

    public void ReloadScene()
    {
        ResetHP();
        Scene scene = SceneManager.GetActiveScene();
        LoadingSceneManager.LoadScene(scene.name);
    }
    
    void ResetHP()
    {
        pd.HP = pd.MAXHP;
    }
}
