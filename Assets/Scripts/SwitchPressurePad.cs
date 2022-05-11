using UnityEngine;
using Extensions;

public class SwitchPressurePad : MonoBehaviour
{
    public Transform pressurePlate;
    
    private const float DetectionRange = 0.5f;
    private const float ActivatedPositionDelta = 0.05f;
    private const int PlatformMovementSpeed = 5;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    
    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private void Start()
    {
        Vector3 position = pressurePlate.position;
        _startPosition = position;
        _targetPosition = position;
    }

    private void FixedUpdate()
    {
        if (pressurePlate.position == _targetPosition) return;

        pressurePlate.position = Vector3.Lerp(pressurePlate.position, _targetPosition,
            Time.fixedDeltaTime * PlatformMovementSpeed);
    }

    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Box")) return;
        
        Vector3 otherPosition = other.gameObject.transform.position;
        
        if (otherPosition.IsInVerticalRange(pressurePlate.position, DetectionRange))
            ActivatePlate();
        else
            DeactivatePlate();
    }
    private void ActivatePlate()
    {
        Material material = pressurePlate.GetComponent<Renderer>().material;
        material.SetColor(EmissionColor, Color.green);

        _targetPosition = _startPosition + Vector3.down * ActivatedPositionDelta;
    }
    private void DeactivatePlate()
    {
        Material material = pressurePlate.GetComponent<Renderer>().material;
        material.SetColor(EmissionColor, Color.white);

        _targetPosition = _startPosition;
    }
    
}
