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
    // Start is called before the first frame update
    void Start()
    {
        inven = GameManager.Instance.Inven;
        fadeScreen.color = new Color(0, 0, 0, 1);
        fadeScreen.DOFade(0, 0.5f)
            .OnComplete(() =>
            {
                StartCoroutine(LoadScene());
            });
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }


    IEnumerator LoadScene()
    {
        loadingScreen.SetActive(true);
        SceneChanger.Instance.sceneType = SceneType.Loading;
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;

        while (op.isDone == false)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);

                if(progressBar.fillAmount>=op.progress)
                {
                    timer = 0f;
                }
            }

            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    fadeScreen.DOFade(1, 0.5f)
                        .OnComplete(() =>
                        {
                            op.allowSceneActivation = true;

                            if (nextScene.Equals("Test"))
                            {
                                SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                                inven.gameObject.SetActive(true);
                                SceneChanger.Instance.sceneType = SceneType.Stage1;
                            }

                            if(nextScene.Equals("Ship"))
                            {
                                SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("MerchantUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("EnchantUI", LoadSceneMode.Additive);
                                inven.gameObject.SetActive(true);
                                SceneChanger.Instance.sceneType = SceneType.Ship;
                            }
                        });
                    yield break;
                }    
            }
        }
        loadingScreen.SetActive(false);
    }
}
