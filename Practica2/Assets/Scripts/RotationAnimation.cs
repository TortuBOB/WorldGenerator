using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    public Vector3 rotationAngle;
    public float rotationSpeed;

    void Update()
    {
        transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
    }
}
