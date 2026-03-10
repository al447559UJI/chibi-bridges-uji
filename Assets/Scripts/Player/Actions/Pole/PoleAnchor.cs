using UnityEngine;

public class PoleAnchor : MonoBehaviour
{
    private Pole pole;

    void Awake()
    {
        pole = GetComponentInParent<Pole>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        pole.Anchor(transform.position);
    }
}
