using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2FireMissile : MonoBehaviour
{
    public GameObject missilePrefab;

    //public AudioClip fireSound;

    public float _Velocity_0, Degree, Angle_Split;

    //float _theta;
    //float PI = Mathf.PI;


    private float currentTime = 0f;
    private int timeCount = 0;

    // 弾を発射する時間間隔
    public int shotTime = 0;

    // 処理を実行するスパン
    public float span = 0f;
    public float shotSpeed = 0f;

    public float decSpeed = 0f;
    public float minSpeed = 0f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void BossShoot()
    {           
        currentTime += Time.deltaTime;
        timeCount += 1;

        // 指定した時間ごとに弾を生成
        if( currentTime > span )
        {
          if (timeCount % shotTime == 0)
          {
            for (int i = 0; i < 24; i++)
            {
                Vector2 vec = new Vector2(0.0f, 1.0f);//player.transform.position - transform.position;
                vec.Normalize();
                // nway弾分割
                vec = Quaternion.Euler( 0, 0, (Degree / Angle_Split) * i ) * vec;
                vec *= shotSpeed;
                var t = Instantiate(missilePrefab, transform.position, missilePrefab.transform.rotation);
                t.GetComponent<Rigidbody2D>().velocity = vec;
                // 弾のSEを鳴らす
                SoundManager.instance.PlaySound("BossEnemy");

                
                var v = t.GetComponent<Rigidbody2D>().velocity;
                v *= decSpeed;
                t.GetComponent<Rigidbody2D>().velocity = v;

                // 発射したミサイルを10秒後に破壊（削除）する。
                Destroy(t, 10.0f);
                
                if(v.magnitude <= minSpeed)
                {
                  v = v.normalized * minSpeed;
                }
            }  
          }

            // for (int i = 0; i <= (Angle_Split - 1); i++) {
            //     //n-way弾の端から端までの角度
            //     float AngleRange = PI * (Degree / 180);

            //     //弾インスタンスに渡す角度の計算
            //     if (Angle_Split > 1) _theta = (AngleRange / (Angle_Split - 1)) * i + 0.5f * (PI + AngleRange);
            //     else _theta = 0.5f * PI;

                
            //     // 第三引数：無回転
            //     GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            //     //GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
        
            //     Rigidbody2D missileRb = missile.GetComponent<Rigidbody2D>();



            //     Vector2 bulletV = missileRb.velocity;
            //     bulletV.x = _Velocity_0 * Mathf.Cos(_theta);
            //     bulletV.y = _Velocity_0 * Mathf.Sin(_theta);
            //     missileRb.velocity = bulletV;

            //     // 弾のSEを鳴らす
            //     SoundManager.instance.PlaySound("BossEnemy");
        
            //     // 発射したミサイルを10秒後に破壊（削除）する。
            //     Destroy(missile, 10.0f);
            // }          
      }

    }
}
