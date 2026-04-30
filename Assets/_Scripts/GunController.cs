using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    private void Start()
    {
        // Since 10% are left-handed - move the launching position to opposite side of parent transform 
        if (Random.Range(0f, 1f) <= 0.1f)
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
    }

    public void FireGun(Transform target)
    {
        var projectile = Instantiate(projectilePrefab, transform.position, target.rotation);
        projectile.GetComponent<ProjectileLauncher>().Launch(target);
    }
}
