using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Transform _skillPanel;
    [SerializeField] private Image _darkPanelUI;
    [SerializeField] private Image _coolPanelUI;
    [SerializeField] private TextMeshProUGUI _coolText;

    public void UseSkill(float coolTime)
    {
        CalculateCoolPanel(coolTime);
        StartCoroutine(CalculateCoolTime(coolTime));
    }

    IEnumerator CalculateCoolTime(float coolIdx)
    {
        _coolText.enabled = true;
        while(coolIdx > 0)
        {
            coolIdx--;
            _coolText.text = coolIdx.ToString();
            yield return new WaitForSeconds(1f);
        }
        _coolText.enabled = false;
    }

    public void CalculateCoolPanel(float coolTime)
    {
        _darkPanelUI.enabled = true;
        Sequence seq = DOTween.Sequence();
        seq.Append
            (
                DOTween.To(() => _coolPanelUI.fillAmount,
                    f => _coolPanelUI.fillAmount = f,
                    1, 0.2f)
            );
        seq.Append
            (
                DOTween.To(() => _coolPanelUI.fillAmount,
                    f => _coolPanelUI.fillAmount = f,
                    0, coolTime)
            );
        seq.AppendCallback(() => _darkPanelUI.enabled = false);
        seq.Append(_skillPanel.DOLocalRotateQuaternion(_skillPanel.localRotation * Quaternion.Euler(0, 0, 5), 0.1f));
        seq.Append(_skillPanel.DOLocalRotateQuaternion(_skillPanel.localRotation * Quaternion.Euler(0, 0, -5), 0.1f));
        seq.Append(_skillPanel.DOLocalRotateQuaternion(_skillPanel.localRotation * Quaternion.Euler(0, 0, 5), 0.1f));
        seq.Append(_skillPanel.DOLocalRotateQuaternion(_skillPanel.localRotation * Quaternion.Euler(0, 0, 0), 0.1f));
    }

}
