using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : FieldItem
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
    }

    public override void Using()
    {
        base.Using();
        GameObject obj = Pooling.Instance.GetPool(DicKey.bomb, p.bombPos);
        obj.transform.SetParent(null);
    }
}
