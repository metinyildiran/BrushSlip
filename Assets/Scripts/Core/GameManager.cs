using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : TouchPress
{
    public static GameManager Instance { get; private set; }

    public int LastFinishedLevel { get; private set; }
    public bool IsGameFinished { get; private set; }
    private int beanCount;
    private bool isGameStarted;

    public Action OnGameStarted;
    public Action OnGameFinished;
    public Action OnGameFailed;
    public Action<int> OnScoreChanged;

    protected override void Awake()
    {
        base.Awake();

        StartCoroutine(LoadData());

        Instance = this;

        beanCount = GameObject.FindGameObjectsWithTag("Bean").Length;

        Application.targetFrameRate = 60;
        Screen.SetResolution(720, 1280, false);
    }

    protected override void OnTouchPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (isGameStarted) return;

        isGameStarted = true;

        OnGameStarted?.Invoke();
    }

    public void DecreaseBean()
    {
        StartCoroutine(nameof(_DecreaseBean));
    }

    private IEnumerator _DecreaseBean()
    {
        beanCount--;

        if (beanCount == 0)
        {
            StartCoroutine(SaveData());

            IsGameFinished = true;

            OnGameFinished?.Invoke();
        }

        yield return null;
    }

    #region Saving and Loading

    [Serializable]
    public class Data
    {
        public int lastFinishedLevel;
    }

    private IEnumerator SaveData()
    {
        Data data = new Data
        {
            lastFinishedLevel = SceneManager.GetActiveScene().buildIndex
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        yield return data;
    }

    private IEnumerator LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Data data = JsonUtility.FromJson<Data>(json);

            LastFinishedLevel = data.lastFinishedLevel;

            yield return data;
        }
        else
        {
            LastFinishedLevel = 0;

            yield return null;
        }
    }

    public IEnumerator ResetData()
    {
        Data data = new Data
        {
            lastFinishedLevel = 0
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        StartCoroutine(LoadData());

        yield return data;
    }
    #endregion
}
