using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private Rigidbody2D rigid;
    private Vector2 moveDirection = Vector2.zero;
    private Animator animator = null;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + moveDirection * Time.fixedDeltaTime * speed);
    }

    private void LateUpdate()
    {
        animator.SetFloat("moveSpeed", moveDirection.magnitude);
        if(moveDirection.x < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(moveDirection.x > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
}
