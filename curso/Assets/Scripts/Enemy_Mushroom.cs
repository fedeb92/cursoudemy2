using UnityEngine;

public class Enemy_Mushroom : Enemy
{


    protected override void Update()
    {
        base.Update();
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        handleMovement();
        HandleCollision();

        if(isGrounded)
            HandleTurnArund();

        void HandleTurnArund()
        {
            if (!isGroundInFrontDetected || isWallDetected)
            {
                Flip();
                idleTimer = idleDuration;
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    private void handleMovement ()
    {
        if (idleTimer > 0)
            return;
       
        rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocity.y);
    }

 
}
