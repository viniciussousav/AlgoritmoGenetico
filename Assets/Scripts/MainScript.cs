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

    GameObject currentPoint;
    GameObject currentPlayer;

    public Text scoreText;
    public Text generationText;
    public Text timeText;
    public Text bestScoreText;

    private int score;
    private int training;
    private float time;
    private int bestScore;

    public Vector3 startPosition;
    
    void Start()
    {
        time = 0;
        training = 1;
        score = 0;
        bestScore = 0;

        scoreText.text = "Pontos: " + score.ToString();
        generationText.text = "Treinamento nº " + training.ToString();
        bestScoreText.text = "Melhor pontuação: " + bestScore.ToString();

        startPosition = new Vector3(-7f, 0.25f, -2.5f);
        currentPlayer = Instantiate(playerGameObject, startPosition, Quaternion.identity);

        float randomX = Random.Range(-8.3f, 8.3f);
        float randomZ = Random.Range(-9f, 2f);
        Vector3 randomPosition = new Vector3(randomX, 0.15f, randomZ);

        currentPoint = Instantiate(pointGameObject, randomPosition, Quaternion.identity);
        currentPlayer.GetComponent<NavMeshAgent>().SetDestination(currentPoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = ((int)time).ToString();

        if(bestScore < score)
        {
            bestScore = score;
            bestScoreText.text = "Melhor pontuação: " + bestScore.ToString();
        }

        if (currentPoint == null)
        {
            score += 1;
            scoreText.text = "Pontos: " + score.ToString();

            float randomX = Random.Range(-8.3f, 8.3f);
            float randomZ = Random.Range(-9f, 2f);
            Vector3 randomPosition = new Vector3(randomX, 0.15f, randomZ);

            currentPoint = Instantiate(pointGameObject, randomPosition, Quaternion.identity);
            currentPlayer.GetComponent<NavMeshAgent>().SetDestination(currentPoint.transform.position);
        }

        if(currentPlayer == null)
        {
            time = 0;
            training += 1;
            score = 0;
            scoreText.text = "Pontos: " + score.ToString();
            generationText.text = "Treinamento  " + training.ToString();

            currentPlayer = Instantiate(playerGameObject, startPosition, Quaternion.identity);
            currentPlayer.GetComponent<NavMeshAgent>().SetDestination(currentPoint.transform.position);
        }
    }
}
