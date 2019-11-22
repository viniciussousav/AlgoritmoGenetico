using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            float time = Random.Range(0.0f, 0.5f);
            yield return new WaitForSeconds(time);
            float randomX = Random.Range(-8.3f, 8.3f);
            Instantiate(bullet, new Vector3(randomX, 0.15f, gameObject.transform.position.z), Quaternion.identity);
        }
    }

}
