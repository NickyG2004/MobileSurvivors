using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public bool debugMode = true;

    [Header("Dependencies")]
    [SerializeField] private PlayerCharacter _playerCharacter;

    [Header("Spawn Settings")]
    [SerializeField] private Enemy[] _possibleEnemiesToSpawn;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private LayerMask _layersToTest;
    [SerializeField] private float _spawnDistanceFromPlayer = 10f;
    [SerializeField] private float _timeBetweenSpawnRateChange = 10f;
    [SerializeField] private float _spawnRateReductionAmount = .1f;
    [SerializeField] private float _minSpawnRate = .2f;

    private float _timeSinceLastSpawnRateChange = 0f;
    private int _maxSpawnAttempts = 4;

    // spawn routine insance stored here
    private Coroutine _spawnRoutine;

    private void Start()
    {
        // for testing we're using the position of the EnemySpawner
        //Spawn(_possibleEnemiesToSpawn, _spawnPosition);

        // start the spawn coroutine
        //_spawnRoutine = StartCoroutine(SpawnRoutine());

        StartSpawning();
    }

    private void Update()
    {
        // increse our spawn cooldown time tracker
        _timeSinceLastSpawnRateChange += Time.deltaTime;

        // if our time since last spawn rate change is greater than the time between spawn rate changes
        if (_timeSinceLastSpawnRateChange >= _timeBetweenSpawnRateChange)
        {
            // reduce the spawn rate
            _spawnRate -= _spawnRateReductionAmount;

            // if the spawn rate is less than the min spawn rate
            if (_spawnRate < _minSpawnRate)
            {
                // set the spawn rate to the min spawn rate
                _spawnRate = _minSpawnRate;
            }

            // reset the time since last spawn rate change
            _timeSinceLastSpawnRateChange = 0f;
        }
    }

    // coroutine require an IEnumerator return type
    // and a return somewhere in the body
    private IEnumerator SpawnRoutine()
    {
        Vector2 spawnPoint;

        // infinite loop, but we can break out of it
        while (_playerCharacter != null)
        {
            // wait for 1 second
            yield return new WaitForSeconds(_spawnRate);

            // stop spawning and exit early if our player has become invaild
            if (_playerCharacter == null)
            {
                StopSpawning();
                yield break;
            }

            // get a valid spawn point
            spawnPoint = GetValidWorldSpawnPoint();

            // if we have a vaild spawn point, spawn!
            if (spawnPoint != Vector2.zero)
            {
                if (debugMode)
                {
                    Debug.Log("EnemySpawner: Spawning enemy at " + spawnPoint);
                }
                Spawn(ChooseRandomEnemy(), spawnPoint);
            }
        }
    }

    public Vector2 GetValidWorldSpawnPoint()
    {
        // get random point in world space
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // convert player position from Vector3 to Vector2
        Vector2 playerPos = new Vector2(_playerCharacter.transform.position.x, _playerCharacter.transform.position.y);

        // create point outwards from player pos in direction
        Vector2 testPoint = playerPos + (randomDirection * _spawnDistanceFromPlayer);

        // test if there's a collider precent on the test point
        for (int i = 0; i < _maxSpawnAttempts; i++)
        {
            // if no colliders are present, return the test point
            if (Physics2D.OverlapCircle(testPoint, .5f, _layersToTest) == null)
            {
                return testPoint;
            }
            else
            {
                // otherwise rotate 90* and try again
                testPoint = new Vector2(testPoint.y, -testPoint.x);
            }
        }

        if (debugMode)
        {
            Debug.Log("EnemySpawner: Could not find a valid spawn point");
        }

        // we've done our loop and testpoint is still default
        return testPoint = Vector2.zero;
    }

    public void StartSpawning()
    {
        if (_spawnRoutine != null)
        { 
            StopCoroutine(_spawnRoutine);
        }
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (_spawnRoutine != null)
        { 
            StopCoroutine(_spawnRoutine);
        }
    }

    public void DestroyAllEnemies()
    {
        // put all in the scene into an array
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        // loop through the array and destroy each enemy
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }

    private void Spawn(Enemy enemyToSpawn, Vector2 position)
    { 
        // spawn enemy at the position and rotation
        Enemy newEnemy = Instantiate(enemyToSpawn, position, Quaternion.identity);
        newEnemy.Initialize(_playerCharacter);
    }

    private Enemy ChooseRandomEnemy()
    {
        int randomEnemyIndex;

        // choose a random 'index' for the enemy array (min/max)
        randomEnemyIndex = Random.Range(0, _possibleEnemiesToSpawn.Length);

        // return the enemy at the random index
        return _possibleEnemiesToSpawn[randomEnemyIndex];
    }
}
