using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage9BossEnemyManager : MonoBehaviour
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
            // DOTween系
            //this.transform.DORotate(endValue: new Vector3(0, 360f, 0), duration: 1.0f ,mode: RotateMode.FastBeyond360);
            //this.transform.DOPunchRotation(punch: new Vector3(0f, 10f, 0f),duration: 1.0f);
            //this.transform.DOShakePosition(duration: 1.0f, strength: 0.5f, vibrato: 10, randomness: 10,snapping:false,fadeOut:true);
            //bossEnemy.transform.DOShakeRotation(duration: 1.0f);

            MoveTimer += Time.deltaTime;// MoveTimerを時間の経過分増加させる

            //右に移動する処理
            if (MoveTimer < RoundTripSpan / 2)// もし、MoveTimerが実行期間の半分より小さいなら、
            {
                bossEnemy.transform.position += new Vector3(4f*Time.deltaTime,-3f*Time.deltaTime,0);              
            }
            //左に移動する処理
            if (MoveTimer >= RoundTripSpan / 2 && MoveTimer < RoundTripSpan)// もし、MoveTimerが実行期間の半分以上、かつ、実行期間より小さいなら、
            {
                bossEnemy.transform.position += new Vector3(-4f*Time.deltaTime,3f*Time.deltaTime,0);
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
        }

    }
}
