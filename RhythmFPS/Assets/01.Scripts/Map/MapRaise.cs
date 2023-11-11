using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public struct MapList
{
    public Map[] list;
}

public class MapRaise : MonoBehaviour
{
    [Header("맵 구조를 넣으면 된다.")]
    public MapList[] mapList;

    [Space(10)]
    [Header("얼만큼 높이 옮길것인가를 적어주면 된다.")]
    public float modifyPos;
    private int curIdx = 0;

    [Space(10)]
    [Header("내브를 다시 설정 해주시위한 거.")]
    [SerializeField] private NavMeshSurface _surface;
    private void Awake()
    {
        _surface.UpdateNavMesh(_surface.navMeshData);
    }

    private void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            MapUp();
        }
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            MapDown();
        }

    }

    /// <summary>
    /// 리스트에 있는 맵의 On함수를 호출해준다.
    /// </summary>
    public void MapUp()
    {
        int randIdx = Random.Range(0,mapList.Length);
        curIdx = randIdx;

        for(int i = 0; i < mapList[randIdx].list.Length; i++)
        {
            float randYPos = Random.Range(-1.0f, 1.0f);
            mapList[randIdx].list[i].On(modifyPos + randYPos);
        }
        StartCoroutine(WaitUpdateNavMesh());
    }

    public IEnumerator WaitUpdateNavMesh()
    {
        yield return new WaitForSeconds(1.7f);
        _surface.UpdateNavMesh(_surface.navMeshData);
    }
    /// <summary>
    /// 리스트에 있는 맵의 Off함수를 호출해준다.
    /// </summary>
    public void MapDown()
    {
        for (int i = 0; i < mapList[curIdx].list.Length; i++)
        {
            mapList[curIdx].list[i].Off();
        }
    }
}
