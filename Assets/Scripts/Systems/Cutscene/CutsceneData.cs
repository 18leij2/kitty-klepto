using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Custom Data/Cutscene Data")]
public class CutsceneData : ScriptableObject {
    public Sprite imageTexture;
    public AudioClip soundClip;
}
