using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour
{
    public GameObject spawnPoint;
    private Transform[] spawn;

    public GameObject[] rambu;
    public GameObject[] melanggar;

    public GameObject[] car;

    private float timer = 4.0f;
    private float timerLLH = 10.0f;
    private float timerLLK = 2.0f;
    private int count = 0;

    private void Start()
    {
        spawn = new Transform[spawnPoint.transform.childCount];

        for (int i = 0; i < spawn.Length; i++)
        {
            spawn[i] = spawnPoint.transform.GetChild(i);
        }
        Spawn();
        rambu[count].SetActive(false);
        melanggar[count].SetActive(false);
    }

    private void Update()
    {
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Spawn();
            timer = 4.0f;
        }

        if(timerLLH > 0f)
        {
            timerLLH -= Time.deltaTime;
        }
        else
        {
            rambu[count].SetActive(true);
            melanggar[count].SetActive(true);

            if (timerLLK > 0)
            {
                timerLLK -= Time.deltaTime;
            }
            else
            {
                count++;
                if(count > rambu.Length - 1)
                {
                    count = 0;
                }
                rambu[count].SetActive(false);
                melanggar[count].SetActive(false);
                timerLLK = 2;
                timerLLH = 10.0f;
            }
        }
    }

    private void Spawn()
    {
        int rnd = Random.Range(0, 4);
        int i = Random.Range(0, 2);
        GameObject spawnCar = Instantiate(car[i], spawn[rnd].position, spawn[rnd].rotation);
        if (spawnCar != null)
        {
            try
            {
                spawnCar.GetComponent<CarAI>().setLokasi(rnd);
            }
            catch
            {
                spawnCar.GetComponent<BadCarAi>().setLokasi(rnd);
            }
        }
    }
}
