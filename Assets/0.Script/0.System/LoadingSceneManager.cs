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
                //Fade�� ������ �ε� �� ȣ��
                StartCoroutine(LoadScene());
            });
    }

    /// <summary>
    /// SceneName�� �μ��� �޾Ƽ� ���� �ε��ϴ� �Լ�
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    /// <summary>
    /// SceneLoad Progress�� ����ȭ�Ǵ� ProgressBar
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
        //�ε��� ������Ʈ Ȱ��ȭ
        loadingScreen.SetActive(true);
        //�� Ÿ���� Loading���� ����
        SceneChanger.Instance.sceneType = SceneType.Loading;
        yield return null;

        //nextScene�� ���൵�� ����ȭ
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;

        //Progress�� ������ �ʾ��� ���
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            // progress�� 90%�̸��� ���
            if (op.progress < 0.9f)
            {
                //progressBar�� fillAmount ����.
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);

                //Progress���� fillamount�� Ŀ���� �ð� =0
                if(progressBar.fillAmount>=op.progress)
                {
                    timer = 0f;
                }
            }

            //Progress�� ������ ���
            else
            {
                //fillAmount�� 1��(�Ϸ�� ����)
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    //���� �� �����ֱ�(ȣ��)
                    fadeScreen.DOFade(1, 0.5f)
                        .OnComplete(() =>
                        {
                            op.allowSceneActivation = true;
                            
                            //�� Ÿ�Կ� ���� �߰��Ǿ�� �� ��ɾ� �Է�
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

        //�ε� ��ũ�� ��Ȱ��ȭ
        loadingScreen.SetActive(false);
    }
}
