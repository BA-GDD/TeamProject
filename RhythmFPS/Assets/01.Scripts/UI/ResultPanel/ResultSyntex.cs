using System.Collections;
using UnityEngine;
using TMPro;
using System.Text;
using DG.Tweening;

public class ResultSyntex : MonoBehaviour
{
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TextMeshProUGUI[] _resultTexts;
    [SerializeField] private string[] _syntextexts;
    [SerializeField] private float _textTurm;
    [SerializeField] private LobbyBtn _btnTrm;
    [SerializeField] private Transform _btnGroup;

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

        Sequence seq = DOTween.Sequence();
        Transform t = Instantiate(_btnTrm, _btnGroup).transform;
        t.GetComponent<LobbyBtn>().gameOverUI = _resultPanel;
        seq.Append(t.DOScale(new Vector3(1, 1, 1), 0.3f));
        yield return new WaitForSeconds(0.4f);
        
    }
}
