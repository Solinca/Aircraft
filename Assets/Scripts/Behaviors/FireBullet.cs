using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float fireRate;
    public GameObject bullet;
    public Transform[] weapons;

    private List<GameObject> bulletPool = new List<GameObject>();
    private float cooldown = 1f;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateNewBullet();
        }
    }

    private void Update()
    {
        cooldown += Time.deltaTime;

        if ((Input.GetKey("space") || Input.GetKey("joystick button 0")) && cooldown >= fireRate)
        {
            cooldown = 0f;

            foreach (Transform weapon in weapons)
            {
               GetBullet().GetComponent<Bullet>().Initialize(weapon);
            }
        }
    }

    private GameObject GetBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }

        return CreateNewBullet();
    }

    private GameObject CreateNewBullet()
    {
        GameObject tempBullet = Instantiate(bullet);
        bulletPool.Add(tempBullet);
        tempBullet.SetActive(false);

        return tempBullet;
    }
}
