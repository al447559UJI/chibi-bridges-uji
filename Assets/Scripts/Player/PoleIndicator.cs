using UnityEngine;

public class PoleIndicator : MonoBehaviour
{

    [Tooltip("Sprite color when the player is allowed to build")]
    [SerializeField] private Color green;

    [Tooltip("Sprite color when the player is not allowed to build")]
    [SerializeField] private Color red; 

    public bool canBuild {get; private set;}

    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        AllowBuild();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (canBuild)
        {
            DenyBuild();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        AllowBuild();
    }

    private void AllowBuild()
    {
        canBuild = true;
        SetColor(green);
    }

    private void DenyBuild()
    {
        canBuild = false;
        SetColor(red);
    }

    private void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
