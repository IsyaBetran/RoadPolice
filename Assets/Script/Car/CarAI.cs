using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform[] pathPoint;
    private Transform[] pathPoint2;
    [SerializeField] private GameObject[] theleftpath;
    [SerializeField] private GameObject[] therightpath;
 

    private float minDist = 3f;
    private int index;
    private bool jalan1 = true;

    public int locAwal;
    private float agentRadius;

    //Raycast
    public float raycastDistance; 
    public LayerMask obstacleLayer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Awal();
        RandomTurn();
        agentRadius = agent.radius;
    }

    private void Update()
    {
        if(jalan1)
        {
            Jalan();
        }
        else
        {
            JalanLagi();
        }
        CheckFrontCollision();
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
        while(rnd == locAwal)
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
        if(Vector3.Distance(transform.position, pathPoint[index].position ) < minDist && index < pathPoint.Length-1)
        {
            index++;      
        }
        if (Vector3.Distance(transform.position, pathPoint[pathPoint.Length - 1].position) < minDist)
        {
            jalan1 = false;
            index = 0;
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

        if(Vector3.Distance(transform.position, pathPoint2[pathPoint2.Length - 1].position ) < minDist)
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
        Vector3 raycastOrigin2 = transform.position + transform.forward * 2;
        raycastOrigin += transform.right * 0.5f;
        raycastOrigin2 -= transform.right * 0.5f;
        bool hasFrontCollision = Physics.Raycast(raycastOrigin, transform.forward, out hit, raycastDistance, obstacleLayer);
        bool hasFrontCollision2 = Physics.Raycast(raycastOrigin2, transform.forward, out hit, raycastDistance, obstacleLayer);

        Debug.DrawRay(raycastOrigin, transform.forward * raycastDistance, hasFrontCollision ? Color.red : Color.green);
        Debug.DrawRay(raycastOrigin2, transform.forward * raycastDistance, hasFrontCollision2 ? Color.red : Color.green);

        // Raycast menyentuh collider atau tidak
        if (hasFrontCollision || hasFrontCollision2)
        {
            agent.isStopped = true;   
        }
        else if(Vector3.Distance(transform.position, pathPoint2[pathPoint2.Length - 1].position) > minDist)
        {
            agent.isStopped = false;
        }
    }

    public void setLokasi(int a)
    {
        locAwal = a;
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 direction = contact.point - transform.position;
            direction.y = 0f; 

            float distance = direction.magnitude;
            float safeDistance = agentRadius - distance;

            if (safeDistance > 0f)
            {
                Vector3 safeOffset = direction.normalized * safeDistance;
                agent.Move(safeOffset);
            }
        }
    }

}
