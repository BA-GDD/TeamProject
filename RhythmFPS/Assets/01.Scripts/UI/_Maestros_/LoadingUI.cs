using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class LoadingUIElement
{
    public Sprite loadingImg;
    public string loadingInfoTxt;
}

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private LoadingBar _loadingBar;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private Image _loadingImg;

    [SerializeField] private List<LoadingUIElement> _elementList = new List<LoadingUIElement>();

    private SceneType _saveSceneType;

    public void LoadingStart(SceneType st)
    {
        _saveSceneType = st;

        int idx = Random.Range(0, _elementList.Count);
        LoadingUIElement luie = _elementList[idx];
        _infoText.text = luie.loadingInfoTxt;
        _loadingImg.sprite = luie.loadingImg;

        StartCoroutine(LoadingCo());
    }

    IEnumerator LoadingCo()
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_saveSceneType.ToString());
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            _loadingBar.SetPercent(asyncOperation.progress);
            yield return null;
        }
    }
}
