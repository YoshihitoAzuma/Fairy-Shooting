using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stage4BossEnemyManager : MonoBehaviour
{
    public GameObject bossEnemy;
   
    private float MoveTimer;// どっちに移動するか判断する変数
    public float RoundTripSpan = 0f;// 往復運動を実行する期間 

    void Start()
    {
        bossEnemy.SetActive(false);
    }

    
    void FixedUpdate()
    {
        if(bossEnemy.activeInHierarchy)
        {
            MoveTimer += Time.deltaTime;// MoveTimerを時間の経過分増加させる

            //右に移動する処理
            if (MoveTimer < RoundTripSpan / 2)// もし、MoveTimerが実行期間の半分より小さいなら、
            {
                //Vector2 force = new Vector2(10,0);
                bossEnemy.transform.Translate(0.08f,0f,0f);
                
                // Lerp(最初の値,変更後の値,変更するスピード)で最初の値から変更後の値まで変更するスピードで値を変更する
                //bossEnemy.transform.localPosition = Vector3.Lerp(bossEnemy.transform.localPosition, new Vector3(5f, 5f, 0), 1f);
            }
            //左に移動する処理
            if (MoveTimer >= RoundTripSpan / 2 && MoveTimer < RoundTripSpan)// もし、MoveTimerが実行期間の半分以上、かつ、実行期間より小さいなら、
            {
                //Vector2 force = new Vector2(-1,0);
                bossEnemy.transform.Translate(-0.08f,0f,0f);
                //bossEnemy.transform.localPosition = Vector3.Lerp(bossEnemy.transform.localPosition, new Vector3(-5, 5f, 0), 1f);
            }
            if (MoveTimer >= RoundTripSpan)// もし、MoveTimerが実行期間以上になったら、
            {
                MoveTimer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        // ボスにプレイヤーが激突した場合
        if (collision.tag == "Player")
        {
            Player.instance.GetDamage(10);
            //Debug.Log("プレイヤーがボスに激突");
        }

    }
}
