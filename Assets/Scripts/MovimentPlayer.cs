using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MovimentPlayer : MonoBehaviour
{
    NavMeshAgent navmesh;
    //public Transform position;


    // Start is called before the first frame update
    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        navmesh.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Point")
        {
            Destroy(collision.gameObject);
        }
    }
}
