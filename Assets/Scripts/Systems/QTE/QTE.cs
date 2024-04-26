using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE : MonoBehaviour {
    
    public new delegate void Success(float stealth);
    public event Success OnSuccess;
    [SerializeField] protected float successStealthReduction = 0f;

    public new delegate void Fail(float stealth);
    public event Fail OnFail;
    [SerializeField] protected float failStealthReduction = 5f;

    protected virtual void FailGame() {
        OnFail?.Invoke(failStealthReduction);
    }

    protected virtual void SucceedGame() {
        GameManager.Instance.returns += 1;
        if (GameManager.Instance.returns == 6)
        {
            Debug.Log("FINISHED GAME");
        }
        OnSuccess?.Invoke(successStealthReduction);
    }
}
