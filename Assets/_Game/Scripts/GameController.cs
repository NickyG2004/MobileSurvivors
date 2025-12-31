using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool debugMode = true;

    [SerializeField] private float _timeToWin = 30f;

    [Header("Upgrade Dependencies")]
    [SerializeField] WeaponData _magicWandData;
    [SerializeField] WeaponData _electroBallData;

    public float ElapsedTime { get; private set; }

    public bool HasWon { get; private set; }

    public List<IUpgrade> PossibleUpgrades { get; private set;} = new List<IUpgrade>();

    public UnityEvent OnWin;
    public UnityEvent OnLose;

    private void Awake()
    {
        SetupUpgrades();
    }

    private void Start()
    {
        if (debugMode)
        {
            Debug.Log("GameController: Start!");
        }

        ElapsedTime = 0f;
        HasWon = false;
    }

    private void Update()
    {
        // increase our elapsed time
        ElapsedTime += Time.deltaTime;
        if (debugMode)
        {
            Debug.Log("GameController: Elapsed Time: " + ElapsedTime);
        }

        // check win condition
        if (ElapsedTime >= _timeToWin && HasWon == false)
        {
            EnterWinState();
        }
    }

    public void EnterWinState()
    {
        if (debugMode)
        {
            Debug.Log("GameController: Win!");
        }

        HasWon = true;
        OnWin?.Invoke();
    }

    public void EnterLoseState()
    {
        if (debugMode)
        {
            Debug.Log("GameController: Lose!");
        }
        OnLose?.Invoke();
    }

    private void SetupUpgrades()
    {
        // add all possible upgrades at the beginning
        PossibleUpgrades.Add(new HealthUpgrade());
        PossibleUpgrades.Add(new ElectroBallUpgrade(_electroBallData));
        PossibleUpgrades.Add(new MoveSpeedUpgrade());
        PossibleUpgrades.Add(new MagicWandUpgrade(_magicWandData));
        // PossibleUpgrades.Add(new CooldownUpgrade());
        // add elecro ball upgrade later

        // longterm we could let the designer define this with a scriptable object.
    }

    public List<IUpgrade> GetUpgradeChoices(int numberOfChoices)
    {
        // create a temporary list that we can remove upgrades from so we don't pick the same 
        // choice multiple times. 

        // Temporary list should start with the same upgrades as PossibleUpgrades
        List<IUpgrade> tempPossibleUpgrades = new List<IUpgrade>(PossibleUpgrades);

        // prep our list of upgrade choices to use when we find one
        List<IUpgrade> upgradeChoices = new List<IUpgrade>();

        // pull our upgrades from possible
        for (int i = 0; i < numberOfChoices; i++)
        {
            // if we've run out of upgrades, break
            if (tempPossibleUpgrades.Count == 0)
            {
                break;
            }

            // pick a random index from the temp list
            int randomIndex = Random.Range(0, tempPossibleUpgrades.Count);

            // grab the upgrade at that index and store it
            IUpgrade upgradeOption = tempPossibleUpgrades[randomIndex];
            upgradeChoices.Add(upgradeOption);

            // remove that upgrade from the temp list so we don't pick it again
            tempPossibleUpgrades.RemoveAt(randomIndex);
        }

        return upgradeChoices;
    }

    public void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
