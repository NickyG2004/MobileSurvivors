using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameHUDController : MonoBehaviour
{
    [Header("Debug Settings")]
    public bool debugMode = false;

    [Header("General Settings")]
    [SerializeField] private GameController _gameController;
    [SerializeField] private PlayerCharacter _playerCharacter;
    private Health _playerHealth = null;
    private Coins _playerCoins = null;

    [Header("Sound Effect Settings")]
    [SerializeField] AudioClip _buttonClickSound = null;
    [Range(0, 1)]
    [SerializeField] private float _buttonClickSoundVolume = 1f;

    [SerializeField] AudioClip _displayLoseScreenSound = null;
    [Range(0, 1)]
    [SerializeField] private float _displayLoseScreenSoundVolume = 1f;

    [SerializeField] AudioClip _displayWinScreenSound = null;
    [Range(0, 1)]
    [SerializeField] private float _displayWinScreenSoundVolume = 1f;

    private UIDocument _document;
    private VisualElement _LoseMenuVisualTree;
    private VisualElement _WinMenuVisualTree;
    private Button _loseRetryButton;
    private Button _loseQuitButton;
    private Button _winPlayAgainButton;
    private Button _winQuitButton;

    // EXP Bar Variables
    private VisualElement _playerHUDVisualTree;
    private VisualElement _expBarFill;

    // Health bar Varibles 
    private VisualElement _healthBarFill;

    // timer variables
    private Label _elapsedTimeLabel;

    // level variables
    private Label _playerLevelLabel;

    // coin variables
    private Label _playerCoinLabel;

    private void Awake()
    {
        _playerHealth = _playerCharacter.GetComponent<Health>();
        _playerCoins = _playerCharacter.GetComponent<Coins>();

        _document = GetComponent<UIDocument>();

        // Get menu sub objects (trees)
        _LoseMenuVisualTree = _document.rootVisualElement.Q("LoseMenuVisualTree");

        _WinMenuVisualTree = _document.rootVisualElement.Q("WinMenuVisualTree");

        // Assign Button callbacks
        _loseRetryButton = _LoseMenuVisualTree.Q("RetryButton") as Button;
        _loseQuitButton = _LoseMenuVisualTree.Q("QuitButton") as Button;

        _winPlayAgainButton = _WinMenuVisualTree.Q("PlayAgainButton") as Button;
        _winQuitButton = _WinMenuVisualTree.Q("QuitButton") as Button;

        // Get EXP Bar sub objects (trees)
        _playerHUDVisualTree = _document.rootVisualElement.Q("PlayerHUDVisualTree");
        _expBarFill = _playerHUDVisualTree.Q("ProgressBarFill");

        // Get Health bar sub objects (trees)
        _healthBarFill = _playerHUDVisualTree.Q("HealthBarFill");
        _playerHUDVisualTree.style.display = DisplayStyle.Flex;

        // Get timer sub objects (trees)
        _elapsedTimeLabel = _document.rootVisualElement.Q("ElapsedTimeLable") as Label;

        // Get level sub objects (trees)
        _playerLevelLabel = _document.rootVisualElement.Q("LevelCountLabel") as Label;

        // Get coin sub objects (trees)
        _playerCoinLabel = _document.rootVisualElement.Q("CoinCountLabel") as Label;

    }

    private void Start()
    {
        DisableAllDisplays();
        // SetEXPBarPrecent(30, 100);
    }

    private void Update()
    {
        // call method to update timer label
        UpdateElapsedTimeLabel();

        UpdateLevelLabel();
        UpdateCoinLabel();
    }

    private void UpdateLevelLabel()
    {
        if (_playerCharacter != null)
        {
            _playerLevelLabel.text = _playerCharacter.LVL.ToString();
        }
    }

    private void UpdateCoinLabel()
    {
        if (_playerCoins != null)
        {
            _playerCoinLabel.text = _playerCoins.GetCurrentCoins().ToString();
        }
    }

    private void UpdateElapsedTimeLabel()
    {
        //_elapsedTimeLabel.text = _gameController.ElapsedTime.ToString();

        // save local variable for calculation
        float elapsedTime = _gameController.ElapsedTime;

        // convert to minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime/60) % 60;
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // format string
        string testElapsedTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        // update UI
        _elapsedTimeLabel.text = testElapsedTime;
    }

    private void OnEnable()
    {
        // Assign Button callbacks
        _loseRetryButton.RegisterCallback<ClickEvent>(OnLoseRetryButtonClick);
        _loseQuitButton.RegisterCallback<ClickEvent>(OnLoseQuitButtonClick);
        _winPlayAgainButton.RegisterCallback<ClickEvent>(OnWinPlayAgainButtonClick);
        _winQuitButton.RegisterCallback<ClickEvent>(OnWinQuitButtonClick);

        // Assign GameController Events
        _gameController.OnLose.AddListener(DisplayLoseMenu);
        _gameController.OnWin.AddListener(DisplayWinMenu);

        // Assign PlayerCharacter Events
        _playerCharacter.OnEXPGained += SetEXPBarPrecent;

        _playerHealth.OnHealthChanged += SetHealthBarPrecent;
    }

    private void OnDisable()
    {
        // Remove Button callbacks
        _loseRetryButton.UnregisterCallback<ClickEvent>(OnLoseRetryButtonClick);
        _loseQuitButton.UnregisterCallback<ClickEvent>(OnLoseQuitButtonClick);
        _winPlayAgainButton.UnregisterCallback<ClickEvent>(OnWinPlayAgainButtonClick);
        _winQuitButton.UnregisterCallback<ClickEvent>(OnWinQuitButtonClick);

        // Remove GameController Events
        _gameController.OnLose.RemoveListener(DisplayLoseMenu);
        _gameController.OnWin.RemoveListener(DisplayWinMenu);

        // Remove PlayerCharacter Events
        _playerCharacter.OnEXPGained -= SetEXPBarPrecent;

        _playerHealth.OnHealthChanged -= SetHealthBarPrecent;
    }

    private void OnLoseRetryButtonClick(ClickEvent evt)
    {
        // Restart the game
        if (debugMode)
        {
            Debug.Log("GameHUDController: Lose Button Retry Clicked");
        }

        // add button clicked sound effect*
        AudioHelper.PlayClip2D(_buttonClickSound, _buttonClickSoundVolume);

        _LoseMenuVisualTree.style.display = DisplayStyle.None;
        _gameController.ReloadLevel();
    }

    private void OnLoseQuitButtonClick(ClickEvent evt)
    {
        // Quit the game
        if (debugMode)
        {
            Debug.Log("GameHUDController: Lose Button Quit Clicked");
        }

        // add button clicked sound effect*
        AudioHelper.PlayClip2D(_buttonClickSound, _buttonClickSoundVolume);

        _LoseMenuVisualTree.style.display = DisplayStyle.None;
        _gameController.QuitGame();
    }

    private void OnWinPlayAgainButtonClick(ClickEvent evt)
    {
        // Restart the game
        if (debugMode)
        {
            Debug.Log("GameHUDController: Win Button Play Again Clicked");
        }

        // add button clicked sound effect*
        AudioHelper.PlayClip2D(_buttonClickSound, _buttonClickSoundVolume);

        _WinMenuVisualTree.style.display = DisplayStyle.None;
        _gameController.ReloadLevel();
    }

    private void OnWinQuitButtonClick(ClickEvent evt)
    {
        // Quit the game
        if (debugMode)
        {
            Debug.Log("GameHUDController: Win Button Quit Clicked");
        }

        // add button clicked sound effect*
        AudioHelper.PlayClip2D(_buttonClickSound, _buttonClickSoundVolume);

        _WinMenuVisualTree.style.display = DisplayStyle.None;
        _gameController.QuitGame();
    }

    private void DisableAllDisplays()
    {
        _LoseMenuVisualTree.style.display = DisplayStyle.None;
        _WinMenuVisualTree.style.display = DisplayStyle.None;
    }

    public void DisplayLoseMenu()
    { 
        _LoseMenuVisualTree.style.display = DisplayStyle.Flex;
        _playerHUDVisualTree.style.display = DisplayStyle.None;

        // add lose sound effect*
        AudioHelper.PlayClip2D(_displayLoseScreenSound, _displayLoseScreenSoundVolume);
    }

    public void DisplayWinMenu()
    {
        _WinMenuVisualTree.style.display = DisplayStyle.Flex;
        _playerHUDVisualTree.style.display = DisplayStyle.None;

        // add win sound effect*
        AudioHelper.PlayClip2D(_displayWinScreenSound, _displayWinScreenSoundVolume);
    }

    private void SetEXPBarPrecent(float currentLevelEXP, float expForLevelUp)
    {
        // Formula: 1/max * current is 0-1 value. * 100 to get percentage
        float percentage = ((1 / expForLevelUp) * currentLevelEXP) * 100;

        if (debugMode)
        {
            Debug.Log("GameHUDController: EXP Bar Percentage: " + percentage + "%");
        }

        // set width as percentage
        _expBarFill.style.width = Length.Percent(percentage);
    }

    private void SetHealthBarPrecent(int curentHealth, int maxHealth)
    {
        float percentage = ((1f / (float)maxHealth) * (float)curentHealth) * 100f;

        if (debugMode)
        {
            Debug.Log("GameHUDController: Health Bar Percentage: " + percentage + "%");
        }

        // set width as percentage 
        _healthBarFill.style.width= Length.Percent(percentage);
    }
}
