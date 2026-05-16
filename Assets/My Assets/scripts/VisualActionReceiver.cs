using UnityEngine;

public class VisualActionReceiver : MonoBehaviour {
    [SerializeField] private Transform visualTarget;
    [SerializeField] private Renderer targetRenderer;

    [Header("Pulse Settings")]
    [SerializeField] private float kickScale = 1.5f;
    [SerializeField] private float returnSpeed = 3f;

    private Vector3 originalScale;
    private float pulseAmount;

    private void Start() {
        if (visualTarget != null) {
            originalScale = visualTarget.localScale;
        }
    }

    private void Update() {
        if (visualTarget == null) {
            return;
        }

        pulseAmount = Mathf.Lerp(pulseAmount, 0f, Time.deltaTime * returnSpeed);

        float scaleMultiplier = Mathf.Lerp(1f, kickScale, pulseAmount);
        visualTarget.localScale = originalScale * scaleMultiplier;
    }

    public void OnKick() {
        Debug.Log("Kick event triggered");
        pulseAmount = 1f;

        if (targetRenderer != null) {
            targetRenderer.material.color = Color.red;
        }
    }

    public void OnSnare() {
        Debug.Log("Snare event triggered");

        if (targetRenderer != null) {
            targetRenderer.material.color = Color.cyan;
        }
    }
}
