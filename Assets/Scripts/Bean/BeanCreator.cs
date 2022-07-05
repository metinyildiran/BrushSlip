#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
#endif

#if UNITY_EDITOR
[RequireComponent(typeof(BoxCollider), typeof(MeshCollider))]
public class BeanCreator : MonoBehaviour
{
    private GameObject bean;
    private CapsuleCollider beanCollider;

    private BoxCollider platformCollider;

    private AsyncOperationHandle<IList<GameObject>> Setup()
    {
        AsyncOperationHandle<IList<GameObject>> beanHandle = Addressables.LoadAssetsAsync<GameObject>("Bean", _bean =>
        {
            bean = _bean;

            beanCollider = bean.GetComponent<CapsuleCollider>();
        });

        platformCollider = GetComponent<BoxCollider>();

        platformCollider.isTrigger = true;

        transform.localScale = Vector3.one;
        gameObject.tag = "Platform";
        gameObject.layer = LayerMask.NameToLayer("Platform");

        transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        return beanHandle;
    }

    public IEnumerator SpawnOnPlane()
    {
        ClearBeans();
        yield return Setup();

        Vector2 area = new Vector2(platformCollider.size.x / beanCollider.radius / 2, platformCollider.size.z / beanCollider.radius / 2);

        for (int i = 0; i < area.x; i++)
        {
            for (int j = 0; j < area.y; j++)
            {
                Vector3 newPos;
                newPos.x = (gameObject.transform.position.x - platformCollider.size.x / 2) + (i * beanCollider.radius * 2) + beanCollider.radius;
                newPos.y = gameObject.transform.position.y + 0.01f;
                newPos.z = (gameObject.transform.position.z - platformCollider.size.z / 2) + (j * beanCollider.radius * 2) + beanCollider.radius;

                Ray ray = new Ray(newPos, Vector3.down);
                //Debug.DrawRay(newPos, Vector3.down, Color.red, 1.0f);

                if (Physics.Raycast(ray))
                {
                    GameObject o = Utils.SpawnPrefab(bean, gameObject);

                    o.transform.SetPositionAndRotation(newPos, Quaternion.identity);

                    yield return null;
                }
            }
        }
    }

    public void ClearBeans()
    {
        foreach (GameObject bean in GetAllBeans())
        {
            if (bean.transform.IsChildOf(transform))
            {
                DestroyImmediate(bean);
            }
        }
    }

    [ContextMenu("Clear All Beans")]
    private void ClearAllBeans()
    {
        foreach (GameObject bean in GetAllBeans())
        {
            DestroyImmediate(bean);
        }
    }

    [ContextMenu("Spawn All Beans")]
    public void SpawnAllBeans()
    {
        foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            EditorCoroutineUtility.StartCoroutine(platform.GetComponent<BeanCreator>().SpawnOnPlane(), this);
        }
    }

    private GameObject[] GetAllBeans()
    {
        return GameObject.FindGameObjectsWithTag("Bean");
    }
}
#endif
