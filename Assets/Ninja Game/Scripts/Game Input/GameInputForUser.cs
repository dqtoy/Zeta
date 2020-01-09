﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputForUser : GameInput_Base {

    public static readonly GameInputForUser I = new GameInputForUser();

    private GameInputForUser() {}

    public override bool KeyDownForLeft() {
        return Input.GetKeyDown(KeyCode.A);
    }

    public override bool KeyForLeft() {
        return Input.GetKey(KeyCode.A);
    }

    public override bool KeyDownForRight() {
        return Input.GetKeyDown(KeyCode.D);
    }

    public override bool KeyForRight() {
        return Input.GetKey(KeyCode.D);
    }

    public override bool KeyForCrouch() {
        return Input.GetKey(KeyCode.S);
    }

    public override bool KeyDownForJump() {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public override bool KeyForJump() {
        return Input.GetKey(KeyCode.Space);
    }

    public override bool KeyDownForThrow() {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }
}