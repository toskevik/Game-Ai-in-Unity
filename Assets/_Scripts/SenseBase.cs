using UnityEngine;

public class SenseBase : MonoBehaviour
{
    public float detectionRate = 1f;

    protected float ElapsedTime = 0f;

    protected virtual void Initialize()
    {
        
    }

    protected virtual void UpdateSense()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        ElapsedTime = 0f;
        Initialize();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateSense();
    }
}