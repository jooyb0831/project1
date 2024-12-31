using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SceneType 사용자 변수
/// </summary>
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
    public SceneType sceneType = SceneType.GameStart;

    private PlayerData pd;
    void Start()
    {
        DontDestroyOnLoad(this);
        pd = GameManager.Instance.PlayerData;
    }

    // 각 씬별로 이동할 때 참조할 수 있도록 함수 생성
    public void Title()
    {
        // Loading SceneManager 스크립트의 LoadScene을 통해 Loadeing Scene 호출
        LoadingSceneManager.LoadScene("GameStart");
        sceneType = SceneType.GameStart;
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
        ResetHP();
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
