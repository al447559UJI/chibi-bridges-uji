using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private PlayerActionData data;
    private PlayerInput input;
    
    private SpriteRenderer meleeVisual;
    private PlayerMeleeHitbox meleeHitbox;



    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    public void MeleeAttack()
    {
        
    }
}
