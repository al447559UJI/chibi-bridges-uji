using UnityEngine;

public class PoleHitbox : MonoBehaviour
{

    private Pole pole;
    public int destroyScrapAmount = 95;

    void Start()
    {
        pole = GetComponentInParent<Pole>();
        destroyScrapAmount = pole.destroyScrapAmount;
    }

    public void DestroyPole()
    {
        pole.DestroyPole();
    }

}
