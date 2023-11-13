using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private SkillUI _skillcoolFunc;
    [SerializeField] private PlayerHpBar _playerHpBar;
    [SerializeField] private BossEnemyHpBar _bossHpBar;
    [SerializeField] private ComboCountUI _comboCount;
    [SerializeField] private BulletCountUI _bulletCount;
    [SerializeField] private AimRythmer _aimRhyther;
    // ����Ʈ �г� ���� ��� ���� �ؾ���.

    private void Start()
    {
        UIManager.Instanace.HandleInGameStartEvent += _aimRhyther.SpawnRhythm;
        UIManager.Instanace.HandleUseSkill += _skillcoolFunc.UseSkill;
        UIManager.Instanace.HandlePlayerGetDamage += _playerHpBar.DiminishValue;
        UIManager.Instanace.HandleBossGetDamage += _bossHpBar.DiminishValue;
        UIManager.Instanace.HandlePlusCombo += _comboCount.ComboPlus;
        UIManager.Instanace.HandleResetCombo += _comboCount.ResetCombo;
        UIManager.Instanace.HandleShootGun += _bulletCount.FireBullet;
        UIManager.Instanace.HandleReload += _bulletCount.ReChargingBullet;
        UIManager.Instanace.HandleGameClear += UIManager.Instanace.UIHud.ActiveResultPanel;

        UIManager.Instanace.HandleInGameStartEvent?.Invoke();
    }

    private void OnDestroy()
    {
        UIManager.Instanace.HandleInGameStartEvent -= _aimRhyther.SpawnRhythm;
        UIManager.Instanace.HandleUseSkill -= _skillcoolFunc.UseSkill;
        UIManager.Instanace.HandlePlayerGetDamage -= _playerHpBar.DiminishValue;
        UIManager.Instanace.HandleBossGetDamage -= _bossHpBar.DiminishValue;
        UIManager.Instanace.HandlePlusCombo -= _comboCount.ComboPlus;
        UIManager.Instanace.HandleResetCombo -= _comboCount.ResetCombo;
        UIManager.Instanace.HandleShootGun -= _bulletCount.FireBullet;
        UIManager.Instanace.HandleReload -= _bulletCount.ReChargingBullet;
        UIManager.Instanace.HandleGameClear -= UIManager.Instanace.UIHud.ActiveResultPanel;
    }
}
