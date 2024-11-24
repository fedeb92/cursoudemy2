using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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


}
