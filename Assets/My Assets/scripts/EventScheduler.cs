using System;
using UnityEngine;

[Serializable]
public class MusicEvent {
    public float time;
    public string action;
}

[Serializable]
public class MusicEventList {
    public MusicEvent[] events;
}

public class EventScheduler : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextAsset eventJson;
    [SerializeField] private VisualActionReceiver visualReceiver;

    private MusicEventList eventData;
    private int nextEventIndex = 0;
    private float previousSongTime = 0f;

    private void Awake() {
        if (eventJson == null) {
            Debug.LogError("No event JSON assigned.");
            return;
        }

        eventData = JsonUtility.FromJson<MusicEventList>(eventJson.text);

        if (eventData == null || eventData.events == null) {
            Debug.LogError("Failed to parse music event JSON.");
            return;
        }

        Array.Sort(eventData.events, (a, b) => a.time.CompareTo(b.time));
    }

    private void Update() {
        if (audioSource == null || visualReceiver == null || eventData == null) {
            return;
        }

        float currentSongTime = audioSource.time;

        // Detect when the looping AudioSource wraps back to the beginning.
        if (currentSongTime < previousSongTime) {
            nextEventIndex = 0;
        }

        while (nextEventIndex < eventData.events.Length && currentSongTime >= eventData.events[nextEventIndex].time) {
            MusicEvent musicEvent = eventData.events[nextEventIndex];

            TriggerEvent(musicEvent);

            nextEventIndex++;
        }

        previousSongTime = currentSongTime;
    }

    private void TriggerEvent(MusicEvent musicEvent) {
        switch (musicEvent.action) {
            case "kick":
                visualReceiver.OnKick();
                break;

            case "snare":
                visualReceiver.OnSnare();
                break;

            default:
                Debug.LogWarning($"Unknown music event action: {musicEvent.action}");
                break;
        }
    }
}
