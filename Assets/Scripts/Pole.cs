using UnityEngine;

enum PoleState
{
    FALL,
    ROTATE,
    STOP
}

public class Pole : MonoBehaviour
{

    [SerializeField] private Transform anchorPoint;

    private PoleState state;

    private JointMotor2D motor;
    private HingeJoint2D hinge;
    private int direction;

    void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();
    }

    void Start()
    {

        motor = new JointMotor2D
        {
            motorSpeed = direction * 100f,
            maxMotorTorque = 200f
        };

        hinge.autoConfigureConnectedAnchor = false;
        hinge.connectedBody = null;
        hinge.motor = motor;
        hinge.useMotor = false;
        hinge.enabled = false;

        state = PoleState.FALL;
    }
    public void Anchor(Vector2 anchorPoint)
    {
        hinge.enabled = true;
        hinge.anchor = transform.InverseTransformPoint(anchorPoint);
        hinge.connectedAnchor = anchorPoint;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {

            if (state == PoleState.FALL)
            {
                hinge.useMotor = true;
            }
            else
            {
                hinge.useMotor = false;
            }
        }

        if (state == PoleState.FALL)
        {
            state = PoleState.ROTATE;
        }
        else if (state == PoleState.ROTATE)
        {
            state = PoleState.STOP;
        }
    }

    public void Initialize(int dir)
    {
        direction = dir;
    }

}
