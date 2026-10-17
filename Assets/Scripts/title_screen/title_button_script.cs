using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float hoverScale = 1.08f;
    [SerializeField] private float clickScale = 0.94f;
    [SerializeField] private float hoverRotationDeg = 5.0f;
    [SerializeField] private float clickRotationDeg = -5.0f;
    [SerializeField] private float animationSpeed = 12f;

    private Button button;
    private Vector3 originalScale;
    private Vector3 originalRotation;
    private bool isHovered;
    private bool isPressed;

    private void Awake()
    {
        button = GetComponent<Button>();
        originalScale = transform.localScale;
        originalRotation = transform.localEulerAngles;
    }

    private void Update()
    {
        float targetScale = isPressed ? clickScale : isHovered ? hoverScale : 1f;
        Vector3 target = originalScale * targetScale;

        transform.localScale = Vector3.Lerp(
            transform.localScale,
            target,
            Time.unscaledDeltaTime * animationSpeed
        );
        // Same thing but rotation
        float targetZ = isPressed
            ? clickRotationDeg
            : isHovered
                ? hoverRotationDeg
                : 0f;

        Quaternion targetRotation =
            Quaternion.Euler(originalRotation + new Vector3(0f, 0f, targetZ));

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRotation,
            Time.unscaledDeltaTime * animationSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null && button.interactable)
            isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        isPressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button != null && button.interactable)
            isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    private void OnDisable()
    {
        isHovered = false;
        isPressed = false;

        if (originalScale != Vector3.zero) transform.localScale = originalScale;

        if (originalRotation != Vector3.zero) transform.localEulerAngles = originalRotation;
    }
}
