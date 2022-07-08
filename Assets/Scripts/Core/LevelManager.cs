using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadLastRemainingLevel();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        bool isLastLevel = SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings;
        if (isLastLevel)
        {
            GameManager.Instance.ResetData();
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadLastRemainingLevel()
    {
        bool isLastLevelFinished = GameManager.Instance.LastFinishedLevel + 1 >= SceneManager.sceneCountInBuildSettings;
        if (isLastLevelFinished)
        {
            GameManager.Instance.ResetData();
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(GameManager.Instance.LastFinishedLevel + 1);
        }
    }
}
