using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    // 追尾対象
    //private GameObject target;

    //private GameObject target;
    private GameObject bossEnemyManager;
    private GameObject bossEnemy;

    public float speed;

    void Start()
    {
        // 最初はBossEnemyは非表示のため、親オブジェクト経由で取得する
        bossEnemyManager = GameObject.Find("BossEnemyManager");
        bossEnemy = bossEnemyManager.transform.Find("BossEnemy").gameObject;
        
    }

    // ゲームオブジェクトを取得
    //public void GetGameObj()
    //{
    //    target = GameObject.Find("BossEnemyManager"); 
    //    Debug.Log("target:"+target);   
    //}

    // Update is called once per frame
    void Update()
    {
        // ボスに弾を近づける
        if(bossEnemy.activeInHierarchy)
        {
            float step = Time.deltaTime * speed;
            transform.position = Vector3.MoveTowards(transform.position, bossEnemy.transform.position, step);
            //Debug.Log("プレイヤーの玉を敵に近づけます。");
        }
    }
}
