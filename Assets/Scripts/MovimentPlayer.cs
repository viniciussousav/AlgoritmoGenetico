using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class MovimentPlayer : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent nav;
    public float lifeTime;
    public int score;
    private Text textLife;
    public Text textPrefab;
    public Text textScore;
    private Vector3 offsetTime;
    private Vector3 offsetScore;
    
    //public Transform position;


    // Start is called before the first frame update
    void Start()
    {
        offsetTime = new Vector3(0, 1.2f, -0.5f);
        offsetScore = new Vector3(0, 1.2f, -1f);


        textLife = Instantiate(textPrefab, FindObjectOfType<Canvas>().transform);
        textScore = Instantiate(textPrefab, FindObjectOfType<Canvas>().transform);

        score = 0;
        lifeTime = 0;
        nav = gameObject.GetComponent<NavMeshAgent>();
        nav.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        
        textLife.text = Math.Round(lifeTime, 2).ToString();
        textScore.text = score.ToString();

        textLife.transform.position = gameObject.transform.position + offsetTime;
        textScore.transform.position = gameObject.transform.position + offsetScore;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == target)
        {
            score += 1;
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(textLife);
            Destroy(textScore);
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

    public float getScore()
    {
        return score;
    }

    public float getLifeTime()
    {
        return lifeTime;
    }
}
