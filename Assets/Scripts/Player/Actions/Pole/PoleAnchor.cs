using UnityEngine;

public class PoleAnchor : MonoBehaviour
{
    private Pole pole;
    private bool isAnchored = false;

    void Awake()
    {
        pole = GetComponentInParent<Pole>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAnchored)
        {
            pole.Anchor(transform.position);
            isAnchored = true;
        }
    }
}
