using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    [SerializeField] private AudioClip[] ladderLanded;
    [SerializeField] private AudioClip[] ladderLost;

    public static SoundManager Instance { get; private set; }

    private float volume = 1f;

    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start() {
        GameManager.Instance.OnLadderAdded += GameManager_OnLadderAdded;
        GameManager.Instance.OnLadderLost += GameManager_OnLadderLost;
    }

    private void GameManager_OnLadderLost(object sender, System.EventArgs e) {
        PlaySound(ladderLost, Camera.main.transform.position);
    }

    private void GameManager_OnLadderAdded(object sender, GameManager.OnLadderAddedEventArgs e) {
        PlaySound(ladderLanded, Camera.main.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        Debug.Log("Playing: " + audioClip + " at volume " + volumeMultiplier * volume);
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volumeMultiplier = 1f) {
        PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volumeMultiplier * volume);
    }

    public void SetVolume(float newVolume) {
        volume = newVolume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}
