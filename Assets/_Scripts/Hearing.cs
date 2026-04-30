using UnityEngine;

public class Hearing : SenseBase
{ 
    [Header("Hearing sense parameters:")]
    [SerializeField]
    private float hearDistance = 10f; // Maximum distance for hearing
    [SerializeField]
    private Color idleColor;
    [SerializeField]
    private Color alertColor = Color.red;
    
    private Transform _playerTransform;
    private Vector3 _rayDirection;

    protected override void Initialize()
    { 
        idleColor = gameObject.GetComponentInChildren<Renderer>().material.color;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
        if (Vector3.SqrMagnitude(_rayDirection) < hearDistance * hearDistance)
        {
            if (_playerTransform.GetComponent<PointAndClickController>().PlayerIsMoving()) 
            {
                    colorToUse = alertColor;
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
        Gizmos.DrawWireSphere(transform.position, hearDistance);
    }
}
