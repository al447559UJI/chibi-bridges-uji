using UnityEngine;

enum PoleState
{
    FALL,
    ROTATE,
    STOP
}

public class Pole : MonoBehaviour
{
    [Tooltip("Position of the PoleAnchor GameObject.")]
    [SerializeField] private Transform anchorPoint;
    [Tooltip("PoleData ScriptableObject")]
    [SerializeField] private PoleData data;

    public int destroyScrapAmount {get; private set;}

    private PoleState state;

    private JointMotor2D motor;
    private HingeJoint2D hinge;
    private Rigidbody2D rb;
    private int direction = 1;
    private int damageAmount;
    private DamageType damageType;

    void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        motor = new JointMotor2D
        {
            motorSpeed = direction * data.motorSpeed,
            maxMotorTorque = data.maxMotorTorque
        };

        hinge.motor = motor;
        hinge.useMotor = false;
        hinge.enabled = false;

        state = PoleState.FALL;
        damageType = DamageType.MELEE;

        rb.gravityScale = data.gravityScale;
        destroyScrapAmount = data.destroyScrapAmount;
    }

    public void Anchor(Vector2 anchorPoint)
    {
        hinge.enabled = true;
        hinge.anchor = transform.InverseTransformPoint(anchorPoint);
        hinge.connectedAnchor = anchorPoint;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && state == PoleState.FALL)
        {
            hinge.useMotor = true;
        }

        switch (state)
        {
            case PoleState.FALL:
                state = PoleState.ROTATE;
                break;
            case PoleState.ROTATE:
                if (collision != null && collision.gameObject.layer == LayerMask.NameToLayer("Damageable"))
                {
                    HandleEnemyCollision(collision);
                }
                else
                {
                    state = PoleState.STOP;
                    hinge.useMotor = false;
                    rb.bodyType = RigidbodyType2D.Static;
                }
                break;
        }

    }

    private void HandleEnemyCollision(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(damageAmount, damageType, direction);
        }
    }

    public void Initialize(int dir, int damage)
    {
        direction = dir;
        damageAmount = damage;
    }

    public void DestroyPole()
    {
        Destroy(gameObject);
    }
}
