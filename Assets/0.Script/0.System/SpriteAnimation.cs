using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (sprites.Count == 0)
        {
            return;
        }

        if (isStop == false)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                timer = 0f;
                index++;

                if (index >= sprites.Count)
                {
                    index = 0;
                    if (loop == false)
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
        else if (isStop == true)
        {
            return;
        }



    }

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
