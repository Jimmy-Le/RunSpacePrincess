using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public GameObject princess;
    [SerializeField] public Transform despawnZone;

    void Awake()
    {
        instance = this;
    }
}
