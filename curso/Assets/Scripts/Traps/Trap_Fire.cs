using System.Collections;
using UnityEngine;

public class Trap_Fire : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float offDuration;
    [SerializeField] private Trap_FireButton fireButton;
    private Animator anim;
    private CapsuleCollider2D fireCollider;
    private bool isActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        fireCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        if (fireButton == null)
        {
            Debug.LogWarning("you dont have fire button on" + gameObject.name + "!");
        }
        SetFire(true);
    }

    public void SwitchOffFire()
    {
        if(isActive == false)
            return;
            StartCoroutine(FireCorutine());
        
         
    }

    private IEnumerator FireCorutine()
    {
        SetFire(false);

        yield return new WaitForSeconds(offDuration);

        SetFire(true);
    }



    private void SetFire(bool active)
    {
        anim.SetBool("active", active);
        fireCollider.enabled = active;
        isActive = active;
    }
}
