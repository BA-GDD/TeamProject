using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon : Weapon
{
    public Transform _firePos;

    private void Awake()
    {
        _currentBullet = _maxBullet;
    }

    public override void Fire()
    {

        if (_currentBullet <= 0)
        {
            lackOfAmmoEvent?.Invoke();
            return;
        }
        if (isReadyReload == true)
        {
            _animator.SetRigBoolIsReload(false);
            _animator.SetRigTriggerReload(false);
            _animator.SetRigTriggerReloadCancel(true);
            isReadyReload = false;
        }
        fireFeedback?.Invoke();
        UIManager.Instanace.HandleShootGun?.Invoke();
        print("맞음!");
        _currentBullet--;
        _animator.SetRigTriggerFIre(true);
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, 1000f, _whatIsEnemy))
        {
            if (hit.transform.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                ComboManager.Instance.AddCombo();
                damageable.TakeDamage(5);
            }
        }
        return;
    }

    public override void Reload()
    {
        print("리로드");
        if (_currentBullet == _maxBullet) return;
        if (isReadyReload == true)
        {

            _animator.SetRigTriggerReload(true);
            
        }
        else
        {
            _animator.SetRigBoolIsReload(true);
            isReadyReload = true;
        }

    }
}
