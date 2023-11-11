using System.Collections;
using System.Collections.Generic;
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
    /// ����Ʈ�� �ִ� ���� On�Լ��� ȣ�����ش�.
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
    }

    /// <summary>
    /// ����Ʈ�� �ִ� ���� Off�Լ��� ȣ�����ش�.
    /// </summary>
    public void MapDown()
    {
        for (int i = 0; i < mapList[curIdx].list.Length; i++)
        {
            mapList[curIdx].list[i].Off();
        }
    }
}
