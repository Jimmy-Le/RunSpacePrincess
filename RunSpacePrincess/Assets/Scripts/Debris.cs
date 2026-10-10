using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float orbitRadius = 3f;
    [SerializeField] private float degreesPerSecond = 180f;

    private float angle;

    private void Update()
    {
        angle += degreesPerSecond * Time.deltaTime;

        float radians = angle * Mathf.Deg2Rad;

        Vector2 offset = new Vector2(
            Mathf.Cos(radians),
            Mathf.Sin(radians)
        ) * orbitRadius;

        transform.position = (Vector2)center.position + offset;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Princess"))
        {
			GameManager.instance.PauseGame(true);
            collision.gameObject.GetComponent<Animator>().SetTrigger("Death");
        }
    }
}
