using UnityEngine;
using UnityEngine.Events;

public class PlayerScrap : MonoBehaviour
{
    [SerializeField] private PoleUI poleUI;
    [SerializeField] private PlayerActionData data;

    public UnityEvent<int> onScrapChanged;
    public UnityEvent<string> onScrapGiven;
    public UnityEvent<string> onScrapSpent;
    
    public int currentAmount { get; private set; }

    void Awake()
    {
        // This has to be set before the HUD loads so the UI displays the correct number.
        currentAmount = data.initialScrapAmount;
    }

    /// <summary>
    /// Gives the player the specified amount of scrap.
    /// If the paramter is left empty, it defaults to PlayerActionData.scrapCollectAmount.
    /// </summary>
    public void Give(int amount = -1)
    {
        int amountGiven = amount <= 0 ? data.scrapCollectAmount : amount;

        currentAmount += amountGiven;
        onScrapChanged.Invoke(currentAmount);
        onScrapGiven.Invoke($"+{amountGiven} Scrap");
    }


    public void Spend(int amount)
    {
        if (data.debugInfiniteAmmo) return;

        currentAmount -= amount;
        onScrapChanged.Invoke(currentAmount);
        onScrapSpent.Invoke($"-{amount} Scrap");

        if (!CanAffordPole())
        {
            poleUI.SetRedColor();
        }
    }

    public bool CanAffordPole()
    {
        return currentAmount >= data.poleCost;
    }

    public int GetPoleCost()
    {
        return data.poleCost;
    }
}
