using UnityEngine;

public class IncreaseVisibility : MonoBehaviour
{
    [SerializeField] public Transform princess;

    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] private float visibleDistance = 5f;
    [SerializeField] private float hiddenDistance = 8f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = Vector2.Distance(transform.position, princess.position);

        // 0 when close, 1 when far away.
        float transparency = Mathf.InverseLerp(
            visibleDistance,
            hiddenDistance,
            distance
        );

        // Clamp protects against distances beyond hiddenDistance.
        transparency = Mathf.Clamp01(transparency);

        Color color = spriteRenderer.color;
        color.a = 1f - transparency;

        spriteRenderer.color = color;
    }
}
