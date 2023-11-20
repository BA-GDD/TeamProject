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
    [Header("�� ������ ������ �ȴ�.")]
    public MapList[] mapList;

    [Space(10)]
    [Header("��ŭ ���� �ű���ΰ��� �����ָ� �ȴ�.")]
    public float modifyPos;
    private int curIdx = 0;

    [Space(10)]
    [Header("���긦 �ٽ� ���� ���ֽ����� ��.")]
    [SerializeField] private NavMeshSurface _surface;

    private int _totalBeat;
    private bool _isUp = false;
    private List<MapList> _upList = new List<MapList>();
    private void Awake()
    {
        _surface.UpdateNavMesh(_surface.navMeshData);
    }
    private void Start()
    {
        RhythmManager.instance.onNotedTimeAction += CheckBeatUpdateMap;
    }

    private void CheckBeatUpdateMap()
    {
        ++_totalBeat;
        if (_totalBeat >= 15)
        {
            if (_isUp)
            {
                MapDown();
            }
            else
            {
                List<MapList> list = new List<MapList>(mapList);

                int cnt = Random.Range(1, 3);
                for (int i = 0; i < cnt; i++)
                {
                    int index = Random.Range(0, list.Count);
                    MapUp(index);
                    _upList.Add(list[index]);
                    list.RemoveAt(index);
                }
            }
            _totalBeat = 0;
            _isUp = !_isUp;
        }
    }

    /// <summary>
    /// ����Ʈ�� �ִ� ���� On�Լ��� ȣ�����ش�.
    /// </summary>
    public void MapUp(int idx)
    {
        for (int i = 0; i < mapList[idx].list.Length; i++)
        {
            float randYPos = Random.Range(-1.0f, 1.0f);
            mapList[idx].list[i].On(modifyPos + randYPos);
        }
        StartCoroutine(WaitUpdateNavMesh());
    }

    public IEnumerator WaitUpdateNavMesh()
    {
        yield return new WaitForSeconds(5.1f);
        _surface.UpdateNavMesh(_surface.navMeshData);
    }
    /// <summary>
    /// ����Ʈ�� �ִ� ���� Off�Լ��� ȣ�����ش�.
    /// </summary>
    public void MapDown()
    {

        for (int i = 0; i < _upList.Count; i++)
        {
            for (int j = 0; j < _upList[i].list.Length; j++)
            {
                _upList[i].list[j].Off();
            }
        }
    }
}
