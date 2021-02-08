using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float fireRate;
    public GameObject bullet;
    public Transform[] bulletSpawns;

    private List<Bullet> bulletPool = new List<Bullet>();
    private float cooldown = 1f;

    private void Start()
    {
        for (int i = 0; i < 10 / fireRate; i++)
        {
            CreateNewBullet();
        }
    }

    private void Update()
    {
        cooldown += Time.deltaTime;

        if ((Input.GetKey("space") || Input.GetKey("joystick button 0")) && cooldown >= UpgradeManager.Instance.GetFinalStat(UpgradeStatType.COOLDOWN, fireRate))
        {
            cooldown = 0f;

            foreach (Transform bulletSpawn in bulletSpawns)
            {
               GetBullet().Initialize(bulletSpawn);
            }
        }
    }

    private Bullet GetBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].gameObject.activeInHierarchy)
            {
                return bulletPool[i];
            }
        }

        return CreateNewBullet();
    }

    private Bullet CreateNewBullet()
    {
        Bullet tempBullet = Instantiate(bullet).GetComponent<Bullet>();
        bulletPool.Add(tempBullet);

        return tempBullet;
    }
}
