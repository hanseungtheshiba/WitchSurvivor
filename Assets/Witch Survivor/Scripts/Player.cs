using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator animator;

    [SerializeField]
    private float speed;
    public Vector2 InputVector { get; private set; }
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 nextPosition = InputVector * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextPosition);
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", InputVector.magnitude);

        if(InputVector.x != 0) {
            transform.localRotation = InputVector.x < 0 ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void OnMove(InputValue value)
    {
        InputVector = value.Get<Vector2>();
    }
}
