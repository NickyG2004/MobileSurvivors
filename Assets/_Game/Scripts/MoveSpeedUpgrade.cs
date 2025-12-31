using UnityEngine;

public class MoveSpeedUpgrade : IUpgrade
{
    [Header("Debug Settings")]
    public bool isDebug = false;

    // [Header("General Settings")]
    private float _moveSpeedIncreaseAmount = 2f;


    public void Upgrade(PlayerCharacter playerCharacter)
    {
        // Upgrade max health!
        if (playerCharacter.TryGetComponent(out PlayerMovement playermovement))
        {
            playermovement.IncreaseMoveSpeed(_moveSpeedIncreaseAmount);
        }
    }
}
