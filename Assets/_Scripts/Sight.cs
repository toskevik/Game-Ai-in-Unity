using UnityEngine; 

public class Sight : SenseBase
{
    [Header("Sight sense parameters:")]
    [SerializeField]
    private float fieldOfView = 90f; // Field of view in degrees
    [SerializeField]
    private float viewDistance = 10f; // Maximum distance to see
    [SerializeField]
    private Color idleColor;
    [SerializeField]
    private Color alertColor = Color.red;
    
    private Transform _playerTransform, _playerBodyTransform;
    private Vector3 _rayDirection;
    private GunController _gunController;

    protected override void Initialize()
    { 
        idleColor = gameObject.GetComponentInChildren<Renderer>().material.color;
        _gunController = GetComponentInChildren<GunController>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _playerBodyTransform = _playerTransform.GetChild(0); // Assuming the player's body is the first child
        FlipColor(idleColor);
    }


    protected override void UpdateSense()
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime >= detectionRate)
        {
            DetectPlayer();
        }
    }

    private void DetectPlayer()
    {
        ElapsedTime = 0f;
        _rayDirection = _playerTransform.position - transform.position;

        var colorToUse = idleColor;
        if (Vector3.SqrMagnitude(_rayDirection) < viewDistance * viewDistance)
        {
            if (Vector3.Angle(_rayDirection, transform.forward) < fieldOfView / 2)
            {
                    colorToUse = alertColor;
                    // Fire projectile every time a player is detected in sight
                    _gunController.FireGun(_playerBodyTransform);
            }
        }
        FlipColor(colorToUse);
    }

    private void FlipColor(Color color)
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = color;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
        var leftBoundary = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward * viewDistance;
        var rightBoundary = Quaternion.Euler(0, fieldOfView / 2, 0) * transform.forward * viewDistance;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}