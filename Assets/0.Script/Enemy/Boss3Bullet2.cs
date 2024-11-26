using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss3Bullet2 : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        Fire();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        transform.DOScale(new Vector3(10, 0.5f, 1f), 0.8f);
    }
}
