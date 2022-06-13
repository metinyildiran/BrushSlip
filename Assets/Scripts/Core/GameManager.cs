using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : TouchPress
{
    public static GameManager Instance { get; private set; }
    public int LastFinishedLevel { get; private set; }
    private bool isGameStarted;

    public Action OnGameStarted;

    private void OnGUI()
    {
        ((int)(1 / Time.smoothDeltaTime)).PrintScreen();
    }

    protected override void Awake()
    {
        base.Awake();

        LoadData();

        Instance = this;

        Application.targetFrameRate = 60;
    }

    protected override void OnTouchPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (isGameStarted) return;

        isGameStarted = true;

        OnGameStarted?.Invoke();
    }

    #region Saving and Loading

    [Serializable]
    public class Data
    {
        public int lastFinishedLevel;
    }

    private void SaveData()
    {
        var data = new Data
        {
            lastFinishedLevel = SceneManager.GetActiveScene().buildIndex
        };

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private void LoadData()
    {
        var path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<Data>(json);

            LastFinishedLevel = data.lastFinishedLevel;
        }
        else
        {
            LastFinishedLevel = 0;
        }
    }

    public void ResetData()
    {
        var data = new Data
        {
            lastFinishedLevel = 0
        };

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        LoadData();
    }
    #endregion
}