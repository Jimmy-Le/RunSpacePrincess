using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public void Death()
    {
        GameManager.instance.GameOver();
    }

    public void Win()
    {
        GameManager.instance.Win();
    }
}
