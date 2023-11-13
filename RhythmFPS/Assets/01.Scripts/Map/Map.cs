using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    const float modifyTime = 5f;

    private float originYPos;
    private float modifyYPos;

    private float modifySpeed = 0.1f;

    float yPos;

    /// <summary>
    /// ��ġ�� �������ش�.
    /// </summary>
    /// <param name="yPos">������ Y���� ���̴�.</param>
    public void On(float yPos)
    {
        modifyYPos = yPos;

        StartCoroutine(OnCo());
    }

    /// <summary>
    /// ��ġ�� ���� ��ġ�� �������ش�.
    /// </summary>
    public void Off()
    {
        modifyYPos = originYPos;

        StartCoroutine(OnCo());
    }

    /// <summary>
    /// �ڽ��� ��ġ�� YPos�� ������ �־� ������ �����̰� ���ش�.
    /// </summary>
    /// <returns></returns>
    private IEnumerator OnCo()
    {
        float percent = 0;
        float timer = 0;
        while (percent <= 1)
        {
            timer += Time.deltaTime * modifySpeed;
            percent = timer / modifyTime;
            yPos = Mathf.Lerp(yPos, modifyYPos, percent);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            yield return null;
        }
    }
}
