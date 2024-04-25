using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Selectable3D : MonoBehaviour {
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private Color enterColor = Color.yellow;
    [SerializeField] private Color clickColor = Color.green;
    [SerializeField] private static float selectDelay = 0.2f;
    private Material[] oldMaterials;
    private MeshRenderer _renderer;
    private int _matID;

    protected void Awake() {
        _renderer = GetComponent<MeshRenderer>();
        oldMaterials = _renderer.materials;
        Material[] selectedMaterials = new Material[oldMaterials.Length + 1];
        for (int i = 0; i < oldMaterials.Length; i++) {
            selectedMaterials[i] = oldMaterials[i];
        }
        _matID = oldMaterials.Length;
        selectedMaterials[_matID] = selectedMaterial;
        _renderer.materials = selectedMaterials;
    }

    private void ToggleMaterial(bool select) {
        if (select) {
            _renderer.materials[_matID].DOFloat(1f, "_Alpha", selectDelay);
        } else {
            _renderer.materials[_matID].DOFloat(0f, "_Alpha", selectDelay); 
        }
    }

    private void SetMaterialColor(Color color) {
        _renderer.materials[_matID].SetColor("_Color", color);
    }

    protected virtual void OnMouseDown() {
        StartCoroutine(OnMouseDownAction());
    }

    protected virtual IEnumerator OnMouseDownAction() {
        SetMaterialColor(clickColor);
        ToggleMaterial(true);
        yield return null;
    }

    protected virtual void OnMouseEnter() {
        StartCoroutine(OnMouseEnterAction());
    }

    protected virtual IEnumerator OnMouseEnterAction() {
        ToggleMaterial(true);
        SetMaterialColor(enterColor);
        yield return null;
    }

    protected void OnMouseExit() {
        ToggleMaterial(false);
    }
}
