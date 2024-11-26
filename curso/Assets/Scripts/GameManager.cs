using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Fruits Managment")]
    public bool fruitsHaveRandomLook;
    public int fruitsCollected;
    public Player player;
    public void Awake()
    {
        if (instance == null) 
        instance = this;
        else
            Destroy(gameObject);
        
    }
    public void AddFruit() => fruitsCollected++;
    public bool FruitsHaveRandomLook() => fruitsHaveRandomLook;


}
