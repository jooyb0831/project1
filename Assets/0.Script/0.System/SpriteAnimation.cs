using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//애니메이션을 사용하는 대신 Sprite를 Delay에 맞게 연속으로 재생시켜
//마치 애니메이션처럼 보이게 하는 스크립트

public class SpriteAnimation : MonoBehaviour
{
    SpriteRenderer sr;

    private List<Sprite> sprites;
    private float delay;

    private int index = 0;
    private bool loop = true;
    private float timer;

    public bool isStop = false;

    public UnityAction action;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (sprites.Count == 0 || sprites == null)
        {
            return;
        }

        if (!isStop)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                timer = 0f;
                index++;

                if (index >= sprites.Count)
                {
                    index = 0;
                    if (!loop)
                    {
                        if(action != null)
                        {
                            action.Invoke();
                            action = null;
                        }
                        //sprites.Clear();
                        return;
                    }
                }
                sr.sprite = sprites[index];
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Sprite를 받아와 세팅
    /// </summary>
    /// <param name="sprites"></param>
    /// <param name="delay"></param>
    /// <param name="loop"></param>
    public void SetSprite(List<Sprite> sprites, float delay, bool loop = true)
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }

        this.sprites = sprites;
        this.delay = delay;
        this.loop = loop;
    }

    /// <summary>
    /// Sprite를 받아와 세팅(action인수)
    /// </summary>
    /// <param name="sprites"></param>
    /// <param name="delay"></param>
    /// <param name="loop"></param>
    /// <param name="action"></param>
    public void SetSprite(List<Sprite> sprites, float delay, bool loop, UnityAction action)
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }

        this.action = action;
        this.sprites = sprites;
        this.delay = delay;
        this.loop = loop;
    }
}
