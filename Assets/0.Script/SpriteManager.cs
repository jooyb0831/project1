using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    [System.Serializable]
    public class PlayerSprite
    {
        public List<Sprite> idleSprites;
        public List<Sprite> walkSprites;
        public List<Sprite> attackSprites;
        public List<Sprite> runAttackSprites;
        public List<Sprite> jumpSprites;
        public List<Sprite> slideSprites;
        public List<Sprite> sitSprites;

    }
    public PlayerSprite[] playerSprite;

    [System.Serializable]
    public class PlayerBulletSprite
    {
        public List<Sprite> pBulletSprites;
    }
    public PlayerBulletSprite[] pBulletSprite;

    [System.Serializable]
    public class EnemySprite
    {
        public List<Sprite> idleSprite;
    }
    public EnemySprite[] enemySprite;
}
