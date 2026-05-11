using UnityEngine;

public class ItemDropBehavior : MonoBehaviour
{
    [SerializeField] GameObject item;


    public void DropItems(int amount, Vector3 position)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newItem = Instantiate(item, position, Quaternion.identity);
            ScrapDrop scrapDrop = newItem.GetComponent<ScrapDrop>();
            scrapDrop.ApplyForce(GetRandomForce());
        }
    }

    private Vector2 GetRandomForce()
    {
        float minX = -2;
        float maxX = 2;
        float minY = 3;
        float maxY = 6;

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY); // keep this > 0 for upward

        return new Vector2(x, y);
    }
}
