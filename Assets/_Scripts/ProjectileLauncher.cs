using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileLauncher : MonoBehaviour
{
    public float launchAngle = 45f;
    public float maxDistance = 50f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Launch(Transform target)
    {
        if (target == null) return;

        var targetPos = target.position;
        var distanceToTarget = Vector3.Distance(transform.position, targetPos);

        // Check if target is within the allowed distance
        if (distanceToTarget > maxDistance)
        {
            Debug.LogWarning("Target is out of range.");
            return;
        }

        var velocity = CalculateVelocity(targetPos, launchAngle);
        
        if (!float.IsNaN(velocity.x))
        {
            _rb.isKinematic = false;
            _rb.linearVelocity = velocity; // In Unity 6+, use .linearVelocity. Use .velocity for older versions.
        }
    }

    private Vector3 CalculateVelocity(Vector3 targetPos, float angle)
    {
        var direction = targetPos - transform.position;
        var h = direction.y; // Vertical displacement
        direction.y = 0; // Get horizontal direction
        var d = direction.magnitude; // Horizontal distance

        var a = angle * Mathf.Deg2Rad; // Convert to radians
        var g = Physics.gravity.magnitude;

        // Kinematic formula for initial velocity
        var v2 = (g * d * d) / (2 * Mathf.Cos(a) * Mathf.Cos(a) * (d * Mathf.Tan(a) - h));

        if (v2 <= 0) return Vector3.zero;

        var v = Mathf.Sqrt(v2);

        // Create the velocity vector in the direction of the target
        var velocity = direction.normalized * Mathf.Cos(a);
        velocity.y = Mathf.Sin(a);
        
        return velocity * v;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 1f);
        }
    }
}