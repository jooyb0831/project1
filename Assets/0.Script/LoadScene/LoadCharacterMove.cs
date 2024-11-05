using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCharacterMove : MonoBehaviour
{
    [SerializeField] List<Sprite> characterImgs;
    private int index = 0;
    float timer;
    float delay = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer>=delay)
        {
            timer = 0f;
            index++;
            if(index>=characterImgs.Count)
            {
                index = 0;
            }
            GetComponent<Image>().sprite = characterImgs[index];
        }
    }
}
