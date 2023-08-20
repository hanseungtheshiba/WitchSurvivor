using UnityEngine;

[System.Serializable]
public struct WeaponInfo
{
    public string id;
    public string name;
    public string description;
    public WEAPON_TYPE type;
    public float minDamage;
    public float maxDamage;
    public int pierce;
    public float speed;
    public float range;
    public float knockBack;
    public float effectTime;
    public float reloadTime;
}

[System.Serializable]
public enum WEAPON_TYPE
{
    CLOSE = 0,
    RANGE = 1,
    RANDOM = 2
}

public class Weapon : MonoBehaviour
{    
    private WeaponInfo weaponInfo;

    private bool isReady = false;
    private GameObject attack = null;
    private float timer = 0f;    

    private void Update()
    {
        if (!isReady) return;

        timer += Time.deltaTime;
        
        if(timer >= weaponInfo.reloadTime)
        {
            timer = 0f;
            Fire();
        }
    }

    private void Fire()
    {
        attack = PoolManager.Instance.GetObject(weaponInfo.id);
        Attack attackComponent = attack.GetComponent<Attack>();
        attackComponent.Init(weaponInfo);
        if(weaponInfo.type == WEAPON_TYPE.CLOSE)
        {
            attack.transform.SetParent(transform, false);
        }
    }

    public void Init(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
        isReady = true;
    }
}