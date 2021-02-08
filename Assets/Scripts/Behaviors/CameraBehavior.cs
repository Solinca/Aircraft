using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float cameraPitch;
    public float cameraDistance;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(cameraPitch, player.transform.eulerAngles.y, 0), 0.3f);
        transform.position = player.transform.position + transform.up * Mathf.Sin(cameraPitch * Mathf.Deg2Rad) * cameraDistance - transform.forward * Mathf.Cos(cameraPitch * Mathf.Deg2Rad) * cameraDistance;
    }
}
