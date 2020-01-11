﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaNodeWalk : MonoBehaviour, NinjaNode_Base {
    public static NinjaNodeWalk I;

    Ninja ninja;

    private Rigidbody2D _rigidbody;

    void Awake() {
        I = this;
    }

    public void EnterNode() {
        ninja = Ninja.I;
        ninja.SetAnimation(12);
        _rigidbody = ninja.GetComponent<Rigidbody2D>();
    }

    public void UpdateNode() {
        ninja.JumpIfInput();
        ninja.ThrowIfInput();
        ninja.CrouchIfInput();
        ninja.IdleIfInput();
    }

    public void FixedUpdateNode() {}

    public void ExitNode() {}

    public NinjaNode_Base GetNextNode() {
        return NinjaNodeMock.I;
    }

    public bool IsNodeFinished() {
        return false;
    }

    void OnCollisionExit2D(Collision2D collidingObject) {
        //if (isActive) {
        //    int numContacts = collidingObject.GetContacts(contactPoint2Ds);
        //    if (numContacts == 0) {
        //        ninja.SwitchNode(NinjaNodeFall.I);
        //    }
        //}
    }
}
