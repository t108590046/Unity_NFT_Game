using UnityEngine;

public class coin : MonoBehaviour
{
    #region SIngleton:coin
    public static coin Instance;
    void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public int Coins;

    public void UseCoins(int amount)
    {
        Coins -= amount;
    }
    public bool HasEnoughCoins(int amount)
    {
        return(Coins >= amount);
    }
}
