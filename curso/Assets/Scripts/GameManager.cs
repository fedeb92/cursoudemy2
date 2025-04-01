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
    public bool fruitsAreRandom;
    public int fruitsCollected;
    public int totalFruits;

    [Header("Checkpoints")]
    public bool canReactivate;

    [Header("Traps")]
    public GameObject arrowPrefab;

    
    
    public void Awake()
    {
        if (instance == null) 
        instance = this;
        else
            Destroy(gameObject);
        
    }

    public void Start()
    {
        CollectFruitsInfo();
    }

    private void CollectFruitsInfo()
    {
        Fruit[] allFruits = FindObjectsByType<Fruit>(FindObjectsSortMode.None);
        totalFruits = allFruits.Length;
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
    public bool FruitsHaveRandomLook() => fruitsAreRandom;

    public void CreateObject(GameObject prefab, Transform target, float delay = 0) 
    {
        StartCoroutine(CreateObjectCourutine(prefab, target, delay));    
    }

    private IEnumerator CreateObjectCourutine(GameObject prefab,Transform target,float delay)
    {
        Vector3 newPosition = target.position;
        yield return new WaitForSeconds(delay);

        GameObject newObject = Instantiate(prefab,newPosition,Quaternion.identity);
    }

}
