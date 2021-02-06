using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float aircraftSpeed;
    public float rollAngle;

    private float horizontalInput;

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    // Limit the angle available ?
    // Check also the bug when you go from roll right to roll left instantly
    private void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + horizontalInput * aircraftSpeed * Time.deltaTime, - horizontalInput * rollAngle);
    }
}
