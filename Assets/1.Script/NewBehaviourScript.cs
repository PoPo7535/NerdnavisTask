using UnityEngine;

public class MovingObject : MonoBehaviour
{

    static public MovingObject instance;


    public string currentMapName; // transferMap 스크립에 있는 transferMapName 변수의 값을 저장.

    public LayerMask layerMask;

    public float speed; // 캐릭터들의 움직임을 담당할 변수 선언

    private Vector3 vector; // 3개의 값을 동시에 가지고 있는 변수(x, y, z축)

    public readonly float runSpeed;
    private float applyRunSpeed; // Shift 키를 누른 경우에만 적용. 실제 적용되는 값.
    private bool applyRunFlag = false;

    public int walkCount;
    private int currentWalkCount;


    private bool canMove = true;

    private Animator animator;
    private Rigidbody2D rigidbody;
    
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int DirX = Animator.StringToHash("DirX");
    private static readonly int DirY = Animator.StringToHash("DirY");

    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody2D>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void FixedUpdate()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        var vel = new Vector2(x, y).normalized * (speed * Time.fixedDeltaTime);
        if (applyRunFlag)
            vel *= runSpeed;
        animator.SetBool(Walking, false);
        rigidbody.velocity = vel;
        if (vel is { x: 0, y: 0 })
            return;

        animator.SetFloat(DirX, rigidbody.velocity.x);
        animator.SetFloat(DirY, rigidbody.velocity.x != 0 ? 0 : rigidbody.velocity.y);
   
        animator.SetBool(Walking, true);
    
    }

    void Update()
    {
        applyRunFlag = Input.GetKey(KeyCode.LeftShift);
    }
}