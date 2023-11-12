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
    /// 위치를 조정해준다.
    /// </summary>
    /// <param name="yPos">변경할 Y값의 수이다.</param>
    public void On(float yPos)
    {
        modifyYPos = yPos;

        StartCoroutine(OnCo());
    }

    /// <summary>
    /// 위치를 원래 위치로 변경해준다.
    /// </summary>
    public void Off()
    {
        modifyYPos = originYPos;

        StartCoroutine(OnCo());
    }

    /// <summary>
    /// 자신의 위치의 YPos를 보간해 주어 서서히 움직이게 해준다.
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
