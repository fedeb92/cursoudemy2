using UnityEngine;

public class Trap_SpikedBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Rigidbody2D spikeRb;
    [SerializeField] private float pushForce;

    private void Start()
    {
        Vector2 pushVector = new Vector2(pushForce, 0);

        spikeRb.AddForce(pushVector,ForceMode2D.Impulse);
    }
}
