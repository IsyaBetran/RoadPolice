using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BadCarAi : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform[] pathPoint;
    private Transform[] pathPoint2;
    [SerializeField] private GameObject[] theleftpath;
    [SerializeField] private GameObject[] therightpath;


    private float minDist = 3f;
    private int index;
    private bool jalan1 = true;
    private bool kena = false;

    public int locAwal;

    //Raycast
    public float raycastDistance;
    public LayerMask obstacleLayer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Awal();
        RandomTurn();
    }

    private void Update()
    {
        if (jalan1)
        {
            Jalan();
        }
        else
        {
            JalanLagi();
        }
        
    }

    private void Awal()
    {
        pathPoint = new Transform[theleftpath[locAwal].transform.childCount];

        int j = 0;
        for (int i = pathPoint.Length - 1; i > -1; i--)
        {
            pathPoint[i] = theleftpath[locAwal].transform.GetChild(j);
            j++;
        }

    }

    private void RandomTurn()
    {
        //Buat angka random untuk beloknya
        int rnd = Random.Range(0, 4);
        //Buat agar tidak balik lagi dari jalan awal
        while (rnd == locAwal)
        {
            rnd = Random.Range(0, 4);
        }

        //Memasukkan path ke dalam pathpoint
        pathPoint2 = new Transform[therightpath[rnd].transform.childCount];

        for (int i = 0; i < pathPoint2.Length; i++)
        {
            pathPoint2[i] = therightpath[rnd].transform.GetChild(i);
        }
    }

    private void Jalan()
    {
        if (Vector3.Distance(transform.position, pathPoint[index].position) < minDist && index < pathPoint.Length - 1)
        {
            index++;
        }
        if (Vector3.Distance(transform.position, pathPoint[pathPoint.Length - 1].position) < minDist)
        {
            jalan1 = false;
            index = 0;
        }

        if (!kena)
        {
            CheckFrontCollision();
        }

        Vector3 loc = pathPoint[index].position;
        agent.SetDestination(loc);
    }

    private void JalanLagi()
    {
        if (Vector3.Distance(transform.position, pathPoint2[index].position) < minDist && index < (pathPoint2.Length - 1))
        {
            index++;
        }

        if (Vector3.Distance(transform.position, pathPoint2[pathPoint2.Length - 1].position) < minDist)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 loc2 = pathPoint2[index].position;
            agent.SetDestination(loc2);
        }
    }

    private void CheckFrontCollision()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position + transform.forward * 2;
        raycastOrigin += transform.right * 0.5f;
        bool hasFrontCollision = Physics.Raycast(raycastOrigin, transform.forward, out hit, raycastDistance, obstacleLayer);

        Debug.DrawRay(raycastOrigin, transform.forward * raycastDistance, hasFrontCollision ? Color.red : Color.green);

        // Raycast menyentuh collider atau tidak
        if (hasFrontCollision)
        {
            Debug.Log("Melanggar");
            kena = true;
        }
    }

    public void setLokasi(int a)
    {
        locAwal = a;
    }
}
