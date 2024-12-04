using UnityEngine;

public class PlayerAnimationsEvents : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();    
    }
    public void FinishRespawn() => player.RespawnFinsish(true);
}
