using UnityEngine;

public class LifeController : MonoBehaviour
{
    [Header("Lives objects (from first to last)")]
    public GameObject[] lifeObjects;

    private int currentLifeIndex;
    public Transform GamePLay;
    public Transform LoseWindow;
    void OnEnable()
    {
        ResetLives();
    }

    // --------------------
    // RESET
    // --------------------

   public void ResetLives()
    {
        // выключаем все индикаторы
        foreach (GameObject obj in lifeObjects)
        {
            obj.SetActive(false);
        }

        // начинаем с последнего
        currentLifeIndex = lifeObjects.Length - 1;
    }

    // --------------------
    // DAMAGE
    // --------------------

    public void LoseLife()
    {
        if (currentLifeIndex < 0)
            return;

        lifeObjects[currentLifeIndex].SetActive(true);
        currentLifeIndex--;

        if (currentLifeIndex < 0)
        {
            OnLose();
        }
    }

    // --------------------
    // LOSE
    // --------------------

    void OnLose()
    {
        LoseWindow.gameObject.SetActive(true);
        GamePLay.gameObject.SetActive(true);
    }
}
