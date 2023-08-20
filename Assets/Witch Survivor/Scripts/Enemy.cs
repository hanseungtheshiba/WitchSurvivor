using System.Collections;
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
    private Collider2D col;
    private Animator animator;    
    private Vector2 dirVector;
    private SortingGroup sortingGroup;
    private WaitForFixedUpdate wait;
    
    private bool isLive = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sortingGroup = GetComponent<SortingGroup>();
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();        
        health = maxHealth;
        isLive = true;
        col.enabled = true;
        rigid.simulated = true;
        animator.SetBool("Dead", false);
    }

    private void FixedUpdate()
    {
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }
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

        sortingGroup.sortingOrder = Mathf.RoundToInt(rigid.position.y);
    }

    public void Init(SpawnData data)
    {        
        maxHealth = data.health;
        health = data.health;
        speed = data.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLive) return;

        if (collision.gameObject.CompareTag("Attack"))
        {
            Attack atk = collision.gameObject.GetComponent<Attack>();
            if (atk != null)
            {
                
                health -= Random.Range(atk.WeaponInfo.minDamage, atk.WeaponInfo.maxDamage);
                StartCoroutine(Knockback(atk.WeaponInfo.knockBack));

                if (health > 0f)
                {
                    animator.SetTrigger("Hit");
                }
                else
                {                    
                    StartCoroutine(Dead());
                }
            }
        }
    }

    private IEnumerator Knockback(float knockBack)
    {
        yield return wait;
        DoKnockback(knockBack);
    }

    private IEnumerator Dead()
    {        
        animator.SetBool("Dead", true);
        isLive = false;
        col.enabled = false;
        DoKnockback();
        yield return new WaitForSeconds(0.5f);
        rigid.simulated = false;        
        Release();
    }

    private void DoKnockback(float distance = 2f)
    {
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        Vector3 directionVector = transform.position - playerPosition;
        rigid.AddRelativeForce(directionVector.normalized * distance, ForceMode2D.Impulse);
    }
}
