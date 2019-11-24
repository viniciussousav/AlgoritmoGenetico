using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MovimentPlayer : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent nav;
    public float lifeTime;
    public int score;

    //public Transform position;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lifeTime = 0;
        nav = gameObject.GetComponent<NavMeshAgent>();
        nav.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == target)
        {
            score += 1;
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            Camera.main.GetComponent<MainScript>().quantityActive -= 1;
        }
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
    }

    public void updateDestination()
    {
        nav.SetDestination(target.transform.position);
    }
}
