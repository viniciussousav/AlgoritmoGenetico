using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerGameObject;
    public GameObject pointGameObject;

    public Text generationText;

    public int populationSize;
    private int generation;
    public int quantityActive;

    public Vector3 startPosition;
    public List<GameObject> population;
    public List<GameObject> targets;


    void Start()
    {
        generation = 1;
        populationSize = 5;
        quantityActive = populationSize;

        generationText.text = "Geração nº " + generation.ToString();
        
        startPosition = new Vector3(-7f, 0.25f, -2.5f);
        population = new List<GameObject>();
        targets = new List<GameObject>();

        initializePopulation(population, targets);

    }

    // Update is called once per frame
    void Update()
    {

        if(quantityActive == 0)
        {
            for(int i = 0; i < population.Count; i++)
            {
                Destroy(population[i]);
                Destroy(targets[i]);
            }
            population.Clear();
            targets.Clear();

            quantityActive = populationSize;
            generation += 1;
            generationText.text = "Geração nº " + generation.ToString();
            initializePopulation(population, targets); //aqui na verdade é para criar uma função que reproduza a partir do gene dos dois melhores
        } else
        {
            for (int i = 0; i < population.Count; i++)
            {
                if (population[i].activeSelf == false)
                {
                    Destroy(targets[i]);
                }
                else if (targets[i] ==  null)
                {
                    float randomX = Random.Range(-8.3f, 8.3f);
                    float randomZ = Random.Range(-9f, 2f);
                    Vector3 randomPosition = new Vector3(randomX, 0.15f, randomZ);
                    targets[i] = Instantiate(pointGameObject, randomPosition, Quaternion.identity);
                    population[i].GetComponent<MovimentPlayer>().setTarget(targets[i]);
                    population[i].GetComponent<MovimentPlayer>().updateDestination();
                }
                
            }
        }
    }

    public void initializePopulation(List<GameObject> population, List<GameObject> target)
    {
        for (int i = 0; i < populationSize; i++)
        {
            float randomX = Random.Range(-8.3f, 8.3f);
            float randomZ = Random.Range(-9f, 2f);
            Vector3 randomPosition = new Vector3(randomX, 0.15f, randomZ);
            target.Add(Instantiate(pointGameObject, randomPosition, Quaternion.identity));
        }

        float randomScale;
        for(int i = 0; i < populationSize; i++)
        {
            randomScale = Random.Range(0.5f, 2f);
            population.Add(Instantiate(playerGameObject, startPosition, Quaternion.identity));
            population[i].GetComponent<MovimentPlayer>().setTarget(target[i]);
            population[i].GetComponent<Transform>().localScale = new Vector3(randomScale, randomScale, randomScale);
            population[i].GetComponent<NavMeshAgent>().speed = Random.Range(1f, 4f);
        }
    }
    
    public float fitnessFunction(GameObject element)
    {
        float score = element.GetComponent<MovimentPlayer>().getScore();
        float time = element.GetComponent<MovimentPlayer>().getLifeTime();

        return score / time; //mudar essa função
    }

    public GameObject[] selection()
    {
        int n = population.Count;
        for(int i = 0; i < n - 1; i++)
        {
            for(int j = 0; j < n - i - 1; j++)
            {
                if(fitnessFunction(population[j+1]) > fitnessFunction(population[j + 1]))
                {
                    GameObject aux = population[j];
                    population[j] = population[j + 1];
                    population[j + 1] = aux;
                }
            }
        }

        GameObject[] selectionResult = { population[0], population[1] };
        return selectionResult;
    }

    public void crossover()
    {
        GameObject[] selectionResult = selection();
        for(int i = 0; i < population.Count; i++)
        {
            population.Add(Instantiate(playerGameObject, startPosition, Quaternion.identity));
            for (int j = 0; j < 2; j++)
            {
                int r = Random.Range(0, 2);
                if(j == 0)
                {
                    population[i].GetComponent<NavMeshAgent>().velocity = selectionResult[r].GetComponent<NavMeshAgent>().velocity;
                    
                } else if(j == 1)
                {
                    population[i].transform.localScale = selectionResult[r].transform.localScale;

                }
            }
        }
    }
    

}
