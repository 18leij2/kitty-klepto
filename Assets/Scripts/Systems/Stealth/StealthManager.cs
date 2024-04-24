using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthManager : MonoBehaviour {
    private StealthManager _instance;
    public StealthManager Instance => Instance;

    [SerializeField] private float maxSuspicion;
}
