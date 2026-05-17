using UnityEngine;

public class VisualActionReceiver : MonoBehaviour {
    [SerializeField] private Transform visualTarget1;
    [SerializeField] private Transform visualTarget2;
    [SerializeField] private Renderer targetRenderer1;
    [SerializeField] private Renderer targetRenderer2;

    [Header("Pulse Settings")]
    [SerializeField] private float kickScale = 1.5f;
    [SerializeField] private float returnSpeed = 6f;

    private Vector3 originalScale1;
    private Vector3 originalScale2;
    private float pulseAmount1;
    private float pulseAmount2;

    private void Start() {
        if (visualTarget1 != null) {
            originalScale1 = visualTarget1.localScale;
        }
        if (visualTarget2 != null) {
            originalScale2 = visualTarget2.localScale;
        }
    }

    private void Update() {
        // Update target 1 (kick)
        if (visualTarget1 != null) {
            pulseAmount1 = Mathf.Lerp(pulseAmount1, 0f, Time.deltaTime * returnSpeed);
            float scaleMultiplier1 = Mathf.Lerp(1f, kickScale, pulseAmount1);
            visualTarget1.localScale = originalScale1 * scaleMultiplier1;
        }

        // Update target 2 (snare)
        if (visualTarget2 != null) {
            pulseAmount2 = Mathf.Lerp(pulseAmount2, 0f, Time.deltaTime * returnSpeed);
            float scaleMultiplier2 = Mathf.Lerp(1f, kickScale, pulseAmount2);
            visualTarget2.localScale = originalScale2 * scaleMultiplier2;
        }
    }

    public void OnKick() {
        Debug.Log("Kick event triggered");
        pulseAmount1 = 1f;

        if (targetRenderer1 != null) {
            targetRenderer1.material.color = Color.red;
        }
    }

    public void OnSnare() {
        Debug.Log("Snare event triggered");
        pulseAmount2 = 0.5f;

        if (targetRenderer2 != null) {
            targetRenderer2.material.color = Color.cyan;
        }
    }
}
