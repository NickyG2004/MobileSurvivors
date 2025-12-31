using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected abstract void Collect(PlayerCharacter playercharacter);

    [Header("Debug Settings")]
    public bool debugMode = true;

    
    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        // Check if the collider is the player
        if (otherCollision.TryGetComponent(out PlayerCharacter playercharacter))
        {
            // Implement pickup logic here

            // call collect function
            Collect(playercharacter);

            // play sound

            // play particle effect

            // For example, destroy the pickup object
            Destroy(this.gameObject);
        }
    }
}
