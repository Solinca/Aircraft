using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float aircraftSpeed;
    public float rollAngle;
    public float clampAngle;

    private float horizontalInput;
    private float angleY;
    private readonly float lerpTime = 0.1f;

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        angleY = transform.eulerAngles.y;

        if (angleY < clampAngle + 50)
        {
            angleY = Mathf.Clamp(angleY, 0, clampAngle);
        } else if (angleY > 360 - clampAngle - 50)
        {
            angleY = Mathf.Clamp(angleY, 360 - clampAngle, 360);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, angleY + horizontalInput * aircraftSpeed * Time.deltaTime / lerpTime, - horizontalInput * rollAngle), lerpTime);
    }
}
