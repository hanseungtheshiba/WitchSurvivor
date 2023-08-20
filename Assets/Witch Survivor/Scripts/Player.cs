using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public struct Character
{    
    public string name;
    public float attack;
    public float fireRate;
    public float fireSpeed;
    public float moveSpeed;
    public float range;
    public WeaponInfo[] weapons;
}

public class Player : MonoBehaviour
{
    [SerializeField]
    private Character character;
    [SerializeField]
    private int level;

    private Rigidbody2D rigid;
    private Collider2D col;
    private Animator animator;    
    private bool isReady = false;
    
    // ¹«±â ½½·Ô
    private Weapon[] weapons = null;
    
    public Vector2 InputVector { get; private set; }
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        weapons = GetComponentsInChildren<Weapon>();
    }

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (!isReady) return;
        Vector2 nextPosition = InputVector * character.moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextPosition);
    }

    private void LateUpdate()
    {
        if (!isReady) return;
        animator.SetFloat("Speed", InputVector.magnitude);

        if(InputVector.x != 0) {
            transform.localRotation = InputVector.x < 0 ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void OnMove(InputValue value)
    {
        InputVector = value.Get<Vector2>();
    }

    public void Init(int index = 0)
    {
        character = GameManager.Instance.GetCharacter(index);
        animator.runtimeAnimatorController = GameManager.Instance.GetController(index);
        rigid.simulated = true;
        col.enabled = true;

        for(int i = 0; i < character.weapons.Length; i++)
        {
            SetWeapon(i);
        }

        isReady = true;
    }

    public void SetWeapon(int index)
    {
        WeaponInfo weaponInfo = character.weapons[index];
        weaponInfo.minDamage *= character.attack;
        weaponInfo.maxDamage *= character.attack;
        weaponInfo.range *= character.range;
        weaponInfo.reloadTime *= character.fireRate;
        weapons[index].Init(weaponInfo);
    }
}
