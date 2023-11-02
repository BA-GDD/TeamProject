using System.Collections;
using UnityEngine;
using TMPro;
using System.Text;

public class ResultSyntex : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _resultTexts;
    [SerializeField] private string[] _syntextexts;
    [SerializeField] private float _textTurm;

    public void SetSyntex(int num)
    {
        StartCoroutine(TextRender(num));
    }

    IEnumerator TextRender(int num)
    {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < _syntextexts[num].Length; i++)
        {
            sb.Append(_syntextexts[num][i]);
            for(int j = 0; j < _resultTexts.Length; j++)
            {
                _resultTexts[j].text = sb.ToString();
            }
            yield return new WaitForSeconds(_textTurm);
        }
    }
}
