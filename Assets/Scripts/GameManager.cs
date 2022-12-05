using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float initialScrollSpeed;

    private int score;
    private float timer;
    private float scrollSpeed;
    public bool stu = true;

    public static GameManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdateSpeed();
    }


    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        PersonajeDead();
        stu = false;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    private void UpdateScore()
    {
        if (stu)
        {
            int scorePerSeconds = 10;
            timer += Time.deltaTime;
            score = (int)(timer * scorePerSeconds);
            scoreText.text = string.Format("{0:00000}", score);
        }
    }

    public float GetScrollSpeed()
    {
        return scrollSpeed;
    }

    private void UpdateSpeed()
    {
        float speedDivider = 10f;
        scrollSpeed = initialScrollSpeed + timer / speedDivider;
    }

    public void home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void PersonajeDead()
    {
        if (score > EstadoGame.estadoGame.scoreMax)
        {
            Debug.Log("Score M?xima Superada!!! M?xima: " + EstadoGame.estadoGame.scoreMax + " Actual: " + score);
            EstadoGame.estadoGame.scoreMax = score;
            EstadoGame.estadoGame.Save();
        }
        else
        {
            Debug.Log("Score No Superado!!! M?xima: " + EstadoGame.estadoGame.scoreMax + " Actual: " + score);
        }
    }
}
