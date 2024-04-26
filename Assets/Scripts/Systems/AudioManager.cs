using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private AudioSource _sourceSFX;
    private AudioSource _sourceMusic;

    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    [SerializeField] private List<AudioClip> musicClips;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private float fadeDuration = 1.5f;

    private void Awake() {
        _instance = this;
        _sourceSFX = transform.GetChild(0).GetComponent<AudioSource>();
        _sourceMusic = transform.GetChild(1).GetComponent<AudioSource>();
        _sourceMusic.loop = true;
    }

    public void PlaySoundEffect(int index) {
        _sourceSFX.volume = 1;
        _sourceSFX.clip = audioClips[index];
        _sourceSFX.Play();
    }
    
    public void PlaySoundEffect() {
        _sourceSFX.volume = 1;
        _sourceSFX.clip = audioClips[0];
        _sourceSFX.Play();
    }

    public void PlayTrack(int index) {
        _sourceMusic.Stop();
        _sourceMusic.volume = 1;
        _sourceMusic.clip = musicClips[index];
        _sourceMusic.Play();
    }

    public void FadeOut() {
        DOTween.To(() => _sourceMusic.volume, x => _sourceMusic.volume = x, 0f, fadeDuration);
    }

    public void SetVolume(float volume) {
        DOTween.To(() => _sourceMusic.volume, x => _sourceMusic.volume = x, volume, fadeDuration);
    }

}
