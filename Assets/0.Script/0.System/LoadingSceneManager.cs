using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    private Inventory inven;
    [SerializeField] Image progressBar;
    [SerializeField] Image fadeScreen;
    [SerializeField] GameObject loadingScreen;

    void Start()
    {
        inven = GameManager.Instance.Inven;
        fadeScreen.color = new Color(0, 0, 0, 1);
        fadeScreen.DOFade(0, 0.5f)
            .OnComplete(() =>
            {
                //Fade가 끝나면 로딩 씬 호출
                StartCoroutine(LoadScene());
            });
    }

    /// <summary>
    /// SceneName을 인수로 받아서 씬을 로드하는 함수
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    /// <summary>
    /// SceneLoad Progress에 동기화되는 ProgressBar
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
        //로딩씬 오브젝트 활성화
        loadingScreen.SetActive(true);
        //씬 타입을 Loading으로 변경
        SceneChanger.Instance.sceneType = SceneType.Loading;
        yield return null;

        //nextScene의 진행도와 동기화
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;

        //Progress가 끝나지 않았을 경우
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            // progress가 90%미만일 경우
            if (op.progress < 0.9f)
            {
                //progressBar의 fillAmount 진행.
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);

                //Progress보다 fillamount가 커지면 시간 =0
                if(progressBar.fillAmount>=op.progress)
                {
                    timer = 0f;
                }
            }

            //Progress가 끝났을 경우
            else
            {
                //fillAmount를 1로(완료된 상태)
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    //다음 씬 보여주기(호출)
                    fadeScreen.DOFade(1, 0.5f)
                        .OnComplete(() =>
                        {
                            op.allowSceneActivation = true;
                            
                            //씬 타입에 따라 추가되어야 할 명령어 입력
                            if(nextScene.Equals("GameStart"))
                            {
                                inven.gameObject.SetActive(false);
                                SceneChanger.Instance.sceneType = SceneType.GameStart;
                            }
                            if (nextScene.Equals("Test"))
                            {
                                SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("BossUI", LoadSceneMode.Additive);
                                inven.gameObject.SetActive(true);
                                SceneChanger.Instance.sceneType = SceneType.Stage1;
                            }

                            if(nextScene.Equals("Stage2"))
                            {
                                SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("BossUI", LoadSceneMode.Additive);
                                inven.gameObject.SetActive(true);
                                SceneChanger.Instance.sceneType = SceneType.Stage2;
                            }

                            if (nextScene.Equals("Stage3"))
                            {
                                SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                                inven.gameObject.SetActive(true);
                                SceneChanger.Instance.sceneType = SceneType.Stage3;
                            }

                            if (nextScene.Equals("Ship"))
                            {
                                SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("MerchantUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("EnchantUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("QuestNPCUI", LoadSceneMode.Additive);
                                inven.gameObject.SetActive(true);
                                SceneChanger.Instance.sceneType = SceneType.Ship;
                            }
                        });
                    yield break;
                }    
            }
        }

        //로딩 스크린 비활성화
        loadingScreen.SetActive(false);
    }
}
