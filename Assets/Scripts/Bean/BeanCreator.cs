#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
public class BeanCreator : MonoBehaviour
{
    [SerializeField] private GameObject bean;

    private void Start()
    {
        foreach (GameObject bean in GameObject.FindGameObjectsWithTag("Bean"))
        {
            DestroyImmediate(bean);
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject o = PrefabUtility.InstantiatePrefab(bean, gameObject.transform) as GameObject;
                o.transform.SetPositionAndRotation(new Vector3(i, 0, j), Quaternion.identity);
            }
        }
    }
}
#endif
