using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE : MonoBehaviour {
    
    public new delegate void Success();
    public event Success OnSuccess;
    [SerializeField] protected float successSuspicion;

    public new delegate void Fail();
    public event Fail OnFail;
    [SerializeField] protected float failSuspicion;

    protected virtual void FailGame() {
        OnFail?.Invoke();
    }

    protected virtual void SucceedGame() {
        OnSuccess?.Invoke();
    }
}
