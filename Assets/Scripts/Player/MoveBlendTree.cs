using UnityEngine;

public class MoveBlendTree : MonoBehaviour
{
    [SerializeField] private Animator animator;
    float velocityZ = 0;
    float velocityX = 0;
    public float Acceleration = 2f;
    public float Deceleration = 2f;

    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 1f;

    private void Update()
    {
        bool forward = Input.GetKey(KeyCode.W);
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);
        bool back = Input.GetKey(KeyCode.S);
        bool run = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = run ? maxRunVelocity : maxWalkVelocity;

        if (back && velocityZ > -.5f)
        {
            velocityZ -= Time.deltaTime * Acceleration;
        }

        if (forward && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * Acceleration;
        }

        if (left && velocityX > -1)
        {
            velocityX -= Time.deltaTime * Acceleration;
        }

        if (right && velocityX < 1)
        {
            velocityX += Time.deltaTime * Acceleration;
        }
        //-------------------------------------------------------------------------------------//
        if (!back && velocityZ < 0)
        {
            velocityZ += Time.deltaTime * Deceleration;
        }

        if (!forward && velocityZ > 0)
        {
            velocityZ -= Time.deltaTime * Deceleration;
        }

        if (!left && velocityX < 0)
        {
            velocityX += Time.deltaTime * Deceleration;
        }

        if (!right && velocityX > 0)
        {
            velocityX -= Time.deltaTime * Deceleration;
        }
        //-------------------------------------------------------------------------------------//


        if (!right && !left && velocityX != 0 && (velocityX > -1f && velocityX < 1f))
        {
            velocityX = 0f;
        }

        if (forward && run && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }

        else if (forward && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * Deceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (forward && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - .05f))
        {
            velocityZ = currentMaxVelocity;
        }

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);
    }
}
