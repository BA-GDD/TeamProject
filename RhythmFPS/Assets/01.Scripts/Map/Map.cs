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
