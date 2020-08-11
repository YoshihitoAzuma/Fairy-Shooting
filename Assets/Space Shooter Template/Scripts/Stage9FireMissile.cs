using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage9FireMissile : MonoBehaviour
{
    public GameObject missilePrefab;

    public float Degree, Angle_Split;

    private float currentTime = 0f;

    private int timeCount = 0;

    // 弾を発射する時間間隔
    public int shotTime = 0;
    public float shotSpeed = 0f;
    public float span = 0f;

    //public float eulerPower = 0f;

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

    public void BossShoot()
    {
        currentTime += Time.deltaTime;
        
        // 渦巻き弾
        timeCount++;
         // 指定した時間ごとに弾を生成
        if( currentTime > span )
        {
            if( timeCount % shotTime == 0 )
            {
                //Debug.Log(span+"毎に弾を生成");
                for (int i = 0; i < Angle_Split; i++)
                {
                    Vector2 vec = new Vector2(0.0f, 1.0f);
                    vec = Quaternion.Euler( 0, 0, Angle_Split* timeCount ) * vec;
                    vec.Normalize();
                    // Nway弾をDegree度ずらして発射
                    vec = Quaternion.Euler( 0, 0, (Degree / Angle_Split) * i ) * vec;
                    vec *= shotSpeed;
                    var q = Quaternion.Euler( 0,0, -Mathf.Atan2( vec.x, vec.y )* Mathf.Rad2Deg );
                    var t = Instantiate(missilePrefab, transform.position, q);
                    t.GetComponent<Rigidbody2D>().velocity = vec;
                    //弾のSEを鳴らす
                    SoundManager.instance.PlaySound("BossEnemy");

                    Destroy(t, 10.0f);
                }
                // 減速
                shotSpeed -= decSpeed;
                if(shotSpeed <= minSpeed)
                {
                    shotSpeed = minSpeed;
                }
            }
            currentTime = 0f;      
        }
    }
}
