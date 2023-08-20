using UnityEngine;

public class Attack : Poolable
{
    public WeaponInfo WeaponInfo { get; private set; }

    public void Init(WeaponInfo weaponInfo)
    {
        WeaponInfo = weaponInfo;
        if(WeaponInfo.type == WEAPON_TYPE.CLOSE)
        {
            transform.localScale = Vector3.one * weaponInfo.range;
        }    
    }

    private void OnEnable()
    {
        Invoke("Release", WeaponInfo.effectTime);
    }

    private void FixedUpdate()
    {
        if (WeaponInfo.type == WEAPON_TYPE.CLOSE) return;
    }

    public override void Release()
    {
        if(gameObject.activeInHierarchy)
        {
            transform.localScale = Vector3.one;
            transform.SetParent(null, false);
            base.Release();
        }        
    }
}
