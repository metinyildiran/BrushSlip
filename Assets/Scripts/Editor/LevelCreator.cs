#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class LevelCreator : EditorWindow
{
    private readonly List<GameObject> platformReferences = new List<GameObject>();
    private int levelCount;

    [MenuItem("Tools/Qubits/Level Creator")]
    public static void ShowLevelCreator()
    {
        GetWindow<LevelCreator>("Level Creator");
    }

    private void OnFocus()
    {
        platformReferences.Clear();
        Addressables.LoadAssetsAsync<GameObject>("Platforms", platform =>
        {
            platformReferences.Add(platform);
        });

        levelCount = 1;
    }

    private void OnGUI()
    {
        #region Level Creator
        levelCount = Slider("Level Count", levelCount, 100);

        if (GUILayout.Button($"Create {levelCount} Levels"))
        {
            EditorCoroutineUtility.StartCoroutine(CreateScene(), this);
        }
        #endregion
    }

    private IEnumerator CreateScene()
    {
        for (int i = 0; i < levelCount; i++)
        {
            Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            SetRenderSettings();

            AsyncOperationHandle<IList<GameObject>> essentials = Addressables.LoadAssetsAsync<GameObject>("Essentials", o =>
            {
                Utils.SpawnPrefab(o);
            });

            yield return essentials;

            SpawnPlatforms();

            int sceneCount = GetSceneCount();

            EditorSceneManager.SaveScene(newScene, $"Assets/Scenes/Level {sceneCount}.unity");
        }
    }

    private void SpawnPlatforms()
    {
        int number = Random.Range(0, platformReferences.Count - 1);

        GameObject platform = Utils.SpawnPrefab(platformReferences[number]);

        platform.transform.SetPositionAndRotation(Vector3.up, Quaternion.identity);
    }

    private int GetSceneCount()
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo("Assets/Scenes/");
        return dir.GetFiles().Length / 2;
    }

    private void SetRenderSettings()
    {
        RenderSettings.reflectionIntensity = 1;
        RenderSettings.skybox = Resources.Load<Material>("Materials/M_GradientSkyBackground");
        RenderSettings.ambientIntensity = 0.3f;
    }

    private void MakeSceneDirty()
    {
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    private int Slider(string label, int scale, int maxValue = 20)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label);
        scale = (int) EditorGUILayout.Slider(scale, 1, maxValue);
        GUILayout.EndHorizontal(); return scale;
    }

    private void CenteredLabel(string text)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(text, EditorStyles.boldLabel);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}
#endif