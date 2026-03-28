using UnityEngine;

public class PoleUI : MonoBehaviour
{

    [Tooltip("Sprite color when the player is allowed to build")]
    [SerializeField] private Color green;

    [Tooltip("Sprite color when the player is not allowed to build")]
    [SerializeField] private Color red; 

    private Transform poleSpawnPoint;

    private SpriteRenderer spriteRenderer;
    private PlayerActions player;



    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<PlayerActions>();
        poleSpawnPoint = GetComponentInChildren<Transform>();
    }

    void Start()
    {
        AllowBuild();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        DenyBuild();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        AllowBuild();
    }

    private void AllowBuild()
    {
        player.SetPolePositionValid(true);
    }

    private void DenyBuild()
    {
        player.SetPolePositionValid(false);
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void Show()
    {
        spriteRenderer.enabled = true;
    }

    public void Hide()
    {
        spriteRenderer.enabled = false;
    }

    public Vector3 GetSpawnPoint()
    {
        return new Vector3(
            poleSpawnPoint.position.x,
            poleSpawnPoint.position.y + 5.5f, // TODO: Dont hardcode this
            0f
        );
    }
}
