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
        transform.eulerAngles = new Vector3(cameraPitch, player.transform.eulerAngles.y, 0);
        transform.position = player.transform.position + transform.up * Mathf.Sin(cameraPitch * Mathf.Deg2Rad) * cameraDistance - transform.forward * Mathf.Cos(cameraPitch * Mathf.Deg2Rad) * cameraDistance;
    }
}
