using UnityEngine;

public class SpawnOnDeath : MonoBehaviour
{
    [Header("Debug Settings")]
    public bool debugMode = true;

    [Header("General Settings")]
    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] private Health _healhToWatch;

    //[Header("Sound Effect Settings")]
    //[SerializeField] AudioClip _enemyDeathSound = null;
    //[Range(0, 1)]
    //[SerializeField] private float _enemyDeathSoundVolume = 1f;

    private void OnEnable()
    {
        // If we have a health component, add spwan as callback
        // to the OnKilled event
        if (_healhToWatch != null)
        {
            _healhToWatch.OnKilled?.AddListener(Spawn);
        }
    }

    private void OnDisable()
    {
        // If we have a health component, remove spwan as callback
        // to the OnKilled event
        if (_healhToWatch != null)
        {
            _healhToWatch.OnKilled?.RemoveListener(Spawn);
        }
    }

    private void Spawn()
    {
        if (debugMode)
        {
            Debug.Log("SpawnOnDeath: Spawning object on death");
        }

        // Spawn the prefab at the current position and rotation
        if (_prefabToSpawn != null)
        {
            Instantiate(_prefabToSpawn, transform.position, Quaternion.identity);
        }

        // play death sound effect
        //AudioHelper.PlayClip2D(_enemyDeathSound, _enemyDeathSoundVolume);
    }
}
