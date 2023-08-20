using UnityEngine;
using UnityEngine.Pool;

public class Poolable : MonoBehaviour
{
    public IObjectPool<GameObject> pool { get; set; }

    public virtual void Release()
    {        
        pool.Release(gameObject);
    }
}
