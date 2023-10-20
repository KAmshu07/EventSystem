using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStateManager stateManager;

    // Attach the PlayerStateManager component to the GameObject.
    void Awake()
    {
        stateManager = GetComponent<PlayerStateManager>();
    }

    void Update()
    {
        stateManager.Update();
    }

    void OnTriggerEnter(Collider other)
    {
        stateManager.OnTriggerEnter(other);
    }

    void OnTriggerStay(Collider other)
    {
        stateManager.OnTriggerStay(other);
    }

    void OnTriggerExit(Collider other)
    {
        stateManager.OnTriggerExit(other);
    }
}