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

    public Text scoreText;
    public Text generationText;
    public Text timeText;
    public Text bestScoreText;


    public int populationSize;
    private int score;
    private int generation;
    private float time;
    private int bestScore;

    public Vector3 startPosition;
    public List<GameObject> population;
    public List<GameObject> targets;


    void Start()
    {
        time = 0;
        generation = 1;
        score = 0;
        bestScore = 0;
        populationSize = 5;

        scoreText.text = "Pontos: " + score.ToString();
        generationText.text = "Geração nº " + generation.ToString();
        bestScoreText.text = "Melhor pontuação: " + bestScore.ToString();
        
        startPosition = new Vector3(-7f, 0.25f, -2.5f);
        population = new List<GameObject>();
        targets = new List<GameObject>();

        initializePopulation(population, targets);

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = ((int)time).ToString();

        if(population.Count == 0)
        {
            generation += 1;
            generationText.text = "Geração nº " + generation.ToString();
            initializePopulation(population, targets); //aqui na verdade é para criar uma função que reproduta a partir do gene dos dois melhores
        }
        for(int i = 0; i < population.Count; i++)
        {
            if(population.Count == 2)
            {
                //fazer os dois serem os melhores
            }
            
            if(population.Count > 0)
            {
                if(population[i] == null)
                {
                    Destroy(targets[i]);
                    population.RemoveAt(i);
                    targets.RemoveAt(i);
                } else if(targets[i] == null)
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
    

    
}
