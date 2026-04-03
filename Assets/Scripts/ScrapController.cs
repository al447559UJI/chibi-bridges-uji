using System;
using UnityEngine;
using UnityEngine.Events;

enum ScrapType
{
    CAN_SMALL,
    CAN_BIG,
    BOX_PIZZA
}

public class ScrapController : MonoBehaviour, IDamageable
{
    [Header("Editor sprite visualization")]
    [SerializeField] private Sprite canSmallSprite;
    [SerializeField] private Sprite canBigSprite;
    [SerializeField] private Sprite boxPizzaSprite;
    [SerializeField] private Sprite randomScrapSprite;

    [Header("Scrap type parameters")]
    [SerializeField] bool randomType = true;
    [SerializeField] private ScrapType type = ScrapType.CAN_SMALL;


    private Animator animator;
    public UnityEvent scrapCollectEvent;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (randomType) type = ChooseRandomType();
    }

    void Start()
    {
        scrapCollectEvent.AddListener(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>().GiveScrap);
        BeginAnimation();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                if (this != null)
                    UpdateSpritePreview();
            };
        }
    }

    private void UpdateSpritePreview()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;

        if (randomType)
        {
            if (randomScrapSprite != null) spriteRenderer.sprite = randomScrapSprite;
        }
        else
        {
            switch (type)
            {
                case ScrapType.CAN_SMALL:
                    if (canSmallSprite != null) spriteRenderer.sprite = canSmallSprite;
                    break;
                case ScrapType.CAN_BIG:
                    if (canBigSprite != null) spriteRenderer.sprite = canBigSprite;
                    break;
                case ScrapType.BOX_PIZZA:
                    if (boxPizzaSprite != null) spriteRenderer.sprite = boxPizzaSprite;
                    break;
            }
        }
    }
#endif
    public void Damage(int damageAmount, DamageType damageType, int direction)
    {
        if (damageType == DamageType.MELEE)
        {
            scrapCollectEvent.Invoke();
            Die();
        }
    }

    private ScrapType ChooseRandomType()
    {
        ScrapType[] values = (ScrapType[])Enum.GetValues(typeof(ScrapType));
        return values[UnityEngine.Random.Range(0, values.Length)];
    }

    private void BeginAnimation()
    {
        switch (type)
        {
            case ScrapType.CAN_SMALL:
                animator.Play("CanSmall");
                break;
            case ScrapType.CAN_BIG:
                animator.Play("CanBig");
                break;
            case ScrapType.BOX_PIZZA:
                animator.Play("BoxPizza");
                break;
            default:
                animator.Play("CanSmall");
                Debug.LogWarning($"BeginAnimation() on ScrapController couldn't find current type: {type}");
                break;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
