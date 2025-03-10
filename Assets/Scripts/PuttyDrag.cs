using UnityEngine;

public class PuttyDrag : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 targetPosition;
    private Vector2 originalScale;
    private Vector2 dragOffset;

    [SerializeField] private float followSpeed = 10f;   // How fast the object moves to the cursor
    [SerializeField] private float stretchFactor = 0.2f; // How much it stretches
    [SerializeField] private float returnSpeed = 5f;    // How fast it returns to normal size

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isDragging)
        {
            // Move towards the target position smoothly
            Vector2 newPosition = Vector2.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            transform.position = newPosition;

            // Calculate direction for stretching effect
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            float stretchAmount = Vector2.Distance(targetPosition, transform.position) * stretchFactor;

            // Apply stretching
            transform.localScale = new Vector2(originalScale.x + stretchAmount * Mathf.Abs(direction.x),
                                               originalScale.y - stretchAmount * Mathf.Abs(direction.y));
        }
        else
        {
            // Smoothly return to original scale when not dragging
            transform.localScale = Vector2.Lerp(transform.localScale, originalScale, returnSpeed * Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        dragOffset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        targetPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + dragOffset;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}
