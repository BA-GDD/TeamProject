using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private float originYPos;
    private float modifyYPos;

    [SerializeField]
    private float modifySpeed = 15.0f;
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
        while (true)
        {
            yPos = Mathf.Lerp(yPos, modifyYPos, Time.deltaTime * modifySpeed);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            if(Mathf.Abs(modifyYPos - yPos) <= .1f)
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
