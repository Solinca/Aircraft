using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float cameraPitch;
    public float cameraDistance;

    private GameObject aircraft;
    private readonly float lineOfSightAdjustment = 20f;

    private void Start()
    {
        aircraft = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = aircraft.transform.position + new Vector3(0, Mathf.Sin(cameraPitch * Mathf.Deg2Rad) * cameraDistance, -Mathf.Cos(cameraPitch * Mathf.Deg2Rad) * cameraDistance);
        transform.eulerAngles = aircraft.transform.eulerAngles + new Vector3(cameraPitch - lineOfSightAdjustment, 0, 0);
    }
}
