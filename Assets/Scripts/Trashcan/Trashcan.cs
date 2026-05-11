using UnityEngine;

public class Trashcan : MonoBehaviour
{
    [SerializeField] private float cooldown = 5f;

    private PlayerScrap playerScrap;
    
    private ItemDropBehavior dropBehavior;
    private Animator animator;
    private bool isOnCooldown = false;
    private float lastCooldownTime = -1;


    public bool isOpen {get; private set;} = false;
    
    void Awake()
    {
        dropBehavior = GetComponent<ItemDropBehavior>();
        animator = GetComponent<Animator>();
        playerScrap = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScrap>();
    }

    void Start()
    {
        DebugRegistry.Register("IsOpen", () => isOpen);
        SwitchStateIfAble(playerScrap.currentAmount);
    }

    public void HandleAttack()
    {
        if (isOpen)
        {
            dropBehavior.DropItems(5, gameObject.transform.position);
            SwitchState(false);
            lastCooldownTime = Time.time;
            isOnCooldown = true;
        }
    }

    void Update()
    {
        if (isOnCooldown)
        {
            if (Time.time - lastCooldownTime >= cooldown)
            {
                isOnCooldown = false;
                SwitchStateIfAble(playerScrap.currentAmount);
            }
        }
    }

    public void SwitchState(bool newState)
    {
        if (newState == isOpen || isOnCooldown) return;

        isOpen = newState;
        animator.Play(isOpen ? "Open" : "Closed");
    }

    void OnEnable()
    {
        playerScrap.onScrapChanged.AddListener(SwitchStateIfAble);
    }

    void OnDisable()
    {
        playerScrap.onScrapChanged.RemoveListener(SwitchStateIfAble);
    }

    private void SwitchStateIfAble(int currentAmount)
    {
        if (playerScrap != null && currentAmount < playerScrap.GetPoleCost())
        {
            SwitchState(true);
        } else
        {
            SwitchState(false);
        }
    }
}
