using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class CutsceneManager : MonoBehaviour {
    [SerializeField] private List<CutsceneData> cutsceneSequence;
    [SerializeField] private CanvasGroup overlay;
    [SerializeField] private GameObject imagePrefab;

    [Header("Timing")] 
    [SerializeField] private float overlayFadeTime = 2f;
    [SerializeField] private float delayBeforeSequence = 3f;
    [SerializeField] private float sequenceDelay = 1f;
    
    private bool _active = false;
    private int _currSequenceIndex = -1;

    private AudioSource _activeAudioSource;

    private IEnumerator _activeTransition;

    private List<Image> _cutsceneImages;

    private void Start() {
        GameManager.Instance.OnStateTransition += SetActive;
        _cutsceneImages = new List<Image>();
        overlay.alpha = 0f;
        _activeAudioSource = GetComponent<AudioSource>();
    }

    private void SetActive(GameManager.GameState state) {
        if (state is GameManager.GameState.Cutscene) {
            LoadNextSequence();
            _active = true;
            DOTween.To(() => overlay.alpha, x => overlay.alpha = x, 1f, overlayFadeTime);
        }
        else {
            if (_active) {
                _active = false;
                _currSequenceIndex = -1;
                StartCoroutine(DelayedTransition());
                foreach (Image image in _cutsceneImages) {
                    Destroy(image.gameObject);
                }
                _cutsceneImages = new List<Image>();
            }
        }
    }

    private IEnumerator DelayedTransition() {
        yield return new WaitForSeconds(1f);
        DOTween.To(() => overlay.alpha, x => overlay.alpha = x, 0f, overlayFadeTime);
    }

    private void Update() {
        if (_active) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                LoadNextSequence();
            }
        }
    }


    public void LoadNextSequence() {
        if (_currSequenceIndex >= cutsceneSequence.Count - 1) {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Game);
            return;
        } 
        if (_activeTransition == null && _currSequenceIndex < cutsceneSequence.Count) {
            _activeAudioSource.Stop();
            _activeTransition = LoadSequenceAction();
            StartCoroutine(_activeTransition);
        } 
    }

    private IEnumerator LoadSequenceAction() {
        if (_currSequenceIndex >= 0) {
            _cutsceneImages[_currSequenceIndex].transform.DOLocalMove(new Vector3(0f, 200f, 0f), 0.03f).SetEase(Ease.Flash);
            yield return new WaitForSeconds(0.03f);
            _cutsceneImages[_currSequenceIndex].GetComponent<CanvasGroup>().alpha = 0f;
            _currSequenceIndex++;
            _activeAudioSource.clip = cutsceneSequence[_currSequenceIndex].soundClip;
            _activeAudioSource.Play();
            Image frame = Instantiate(imagePrefab, transform.position, Quaternion.identity, transform).GetComponent<Image>();
            frame.sprite = cutsceneSequence[_currSequenceIndex].imageTexture;
            frame.transform.DOLocalMove(new Vector3(0f, 200f, 0f), 0f);
            frame.transform.DOMove(transform.position, 0.1f).SetEase(Ease.Flash);
            _cutsceneImages.Add(frame);
        }
        else {
            _currSequenceIndex++;
            yield return new WaitForSeconds(delayBeforeSequence);
            Image frame = Instantiate(imagePrefab, transform.position, Quaternion.identity, transform).GetComponent<Image>();
            frame.sprite = cutsceneSequence[_currSequenceIndex].imageTexture;
            _activeAudioSource.clip = cutsceneSequence[_currSequenceIndex].soundClip;
            _activeAudioSource.Play();
            _cutsceneImages.Add(frame);
        }

        yield return new WaitForSeconds(sequenceDelay);
        _activeTransition = null;
        yield return null;
    }
}