using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private SliderMovemant slider;
    [SerializeField] private float successZone = 0.1f;

    [Header("UI")]
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject tapButton;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text timerText;

    [Header("Game Settings")]
    [SerializeField] private int coinsPerSuccess = 10;
    [SerializeField] private int requiredSuccesses = 5;
    [SerializeField] private float gameTime = 30f;

    private int coins;
    private int successCount;
    private float currentTime;
    private bool gameActive;

    private bool timerPaused; // 👈 НОВОЕ

    public Animator right;
    public Animator wrong;

    public LifeController lifeController;

    public Transform WinWindow;
    public Transform LoseWindow;

    public AudioSource winsound;
    public AudioSource losesound;
    public AudioSource clickSound;

    private void OnEnable()
    {
        RestartGame();
    }

    private void Update()
    {
        if (!gameActive || timerPaused) return;

        currentTime -= Time.deltaTime;
        UpdateTimerText(currentTime);

        if (currentTime <= 0f)
        {
            Lose();
        }
    }

    // 🔁 ПОЛНЫЙ РЕСТАРТ
    public void RestartGame()
    {
        gameActive = false;
        timerPaused = false;

        coins = 0;
        successCount = 0;
        currentTime = gameTime;

        coinsText.text = "0";
        UpdateTimerText(currentTime);

        startButton.SetActive(true);
        tapButton.SetActive(false);

        slider.ResetSlider();
    }

    public void StartGame()
    {
        successCount = 0;

        startButton.SetActive(false);
        tapButton.SetActive(true);

        lifeController.ResetLives();

        gameActive = true;
        timerPaused = false;

        slider.StartMove();
    }

    public void Tap()
    {
        if (!gameActive) return;

        float pos = slider.GetNormalizedPosition();

        if (Mathf.Abs(pos - 0.5f) <= successZone)
        {
            CorrectTap();
            clickSound.Play();
        }
        else
        {
            WrongTap();
        }
    }

    // ⏸️ ПАУЗА ТАЙМЕРА
    public void PauseTimer()
    {
        timerPaused = true;
    }

    // ▶️ ПРОДОЛЖЕНИЕ ТАЙМЕРА
    public void ResumeTimer()
    {
        timerPaused = false;
    }

    private void CorrectTap()
    {
        coins += coinsPerSuccess;
        successCount++;
        coinsText.text = coins.ToString();

        right.Play("RightClick");

        if (successCount >= requiredSuccesses)
        {
            Win();
        }
    }

    private void WrongTap()
    {
        wrong.Play("BadClick");
        lifeController.LoseLife();
        losesound.Play();
    }

    private void Win()
    {
        gameActive = false;
        slider.StopMove();
        tapButton.SetActive(false);

        WinWindow.gameObject.SetActive(true);
        winsound.Play();
    }

    private void Lose()
    {
        gameActive = false;
        slider.StopMove();
        tapButton.SetActive(false);

        LoseWindow.gameObject.SetActive(true);
        losesound.Play();
    }

    private void UpdateTimerText(float time)
    {
        time = Mathf.Clamp(time, 0f, Mathf.Infinity);

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
