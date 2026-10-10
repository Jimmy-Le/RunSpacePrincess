using UnityEngine;

public class AccelerateAnimation : MonoBehaviour
{
    
    [SerializeField] public Animator animator;

    [SerializeField] public Transform target;


    [SerializeField] private float farDistance = 8f;
    [SerializeField] private float nearDistance = 2f;

    [SerializeField] private float slowSpeed = 1f;
    [SerializeField] private float fastSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);

        // 0 when far away, 1 when close.
        float closeness = Mathf.InverseLerp(
            farDistance,
            nearDistance,
            distance
        );

        closeness = Mathf.Clamp01(closeness);

        float animationSpeed = Mathf.Lerp(slowSpeed, fastSpeed, closeness);

        animator.SetFloat("animationSpeed", animationSpeed);
        
    }
}
