using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : Poolable
{
    public float speed = 2f;
    public float health;
    public float maxHealth;
    [SerializeField]
    private RuntimeAnimatorController[] animatorController;

    // 내부 사용 변수
    private Rigidbody2D target;
    private Rigidbody2D rigid;
    private Animator animator;    
    private Vector2 dirVector;
    private SortingGroup sortingGroup;
    
    private bool isLive = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sortingGroup = GetComponent<SortingGroup>();
    }

    private void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        if (!isLive) return;
        dirVector = target.position - rigid.position;
        Vector2 nextPosition = dirVector.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextPosition);
    }

    private void LateUpdate()
    {
        if (!isLive) return;

        if (dirVector.x != 0)
        {
            transform.localRotation = dirVector.x < 0 ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);
        }

        sortingGroup.sortingOrder = Mathf.CeilToInt(rigid.position.y);
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animatorController[data.index];
        maxHealth = data.health;
        health = data.health;
    }
}
