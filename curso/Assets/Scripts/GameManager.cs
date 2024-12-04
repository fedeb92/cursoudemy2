using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("player")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay;
    public Player player;
   
    [Header("Fruits Managment")]
    public bool fruitsHaveRandomLook;
    public int fruitsCollected;
    
    public void Awake()
    {
        if (instance == null) 
        instance = this;
        else
            Destroy(gameObject);
        
    }

    public void UpdateRespawnPosition(Transform newRespawnPoint) => respawnPoint = newRespawnPoint; 

    public void RespawnPlayer() => StartCoroutine(RespawnCorutine());
    
    private IEnumerator RespawnCorutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();

    }

    public void AddFruit() => fruitsCollected++;
    public bool FruitsHaveRandomLook() => fruitsHaveRandomLook;


}
