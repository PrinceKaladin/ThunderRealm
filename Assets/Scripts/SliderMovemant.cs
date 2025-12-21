using UnityEngine;

public class SliderMovemant : MonoBehaviour
{
    [SerializeField] private float speed = 2f;        // скорость
    [SerializeField] private float moveRange = 200f;  // влево и вправо ОТ ЦЕНТРА

    private bool isActive;
    private float time;

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.localPosition; // ЦЕНТР ДВИЖЕНИЯ
        ResetSlider();
    }

    public void StartMove()
    {
        isActive = true;
    }

    public void StopMove()
    {
        isActive = false;
    }

    public void ResetSlider()
    {
        isActive = false;
        time = 0f;
        transform.localPosition = startPosition;
    }

    private void Update()
    {
        if (!isActive) return;

        time += Time.deltaTime * speed;

        float xOffset = Mathf.Sin(time) * (moveRange * 0.5f);

        transform.localPosition = new Vector3(
            startPosition.x + xOffset,
            startPosition.y,
            startPosition.z
        );
    }

    // 0 = левый край, 0.5 = центр, 1 = правый край
    public float GetNormalizedPosition()
    {
        float normalized = (Mathf.Sin(time) + 1f) * 0.5f;
        return normalized;
    }
}
