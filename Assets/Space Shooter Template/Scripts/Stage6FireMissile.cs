using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6FireMissile : MonoBehaviour
{
   public GameObject missilePrefab;

    public float _Velocity_0, Degree, Angle_Split;

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
    void Update()
    {
        BossShoot();
    }

    // Update is called once per frame
    //IEnumerator BossShoot(float delay)
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
        }
    }
}
