using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bar;
    private float maxBarSizeX = 1.5f;
    private float maxBarSizeY = .5f;

    public void SetSize(int currentHealth, int maxHealth)
    {
        float newSize = currentHealth * maxBarSizeX / maxHealth;

        if (newSize < 0) newSize = 0;
        else if (newSize > maxBarSizeX) newSize = maxBarSizeX;

        bar.size = new Vector2(newSize, maxBarSizeY);
    }


}
