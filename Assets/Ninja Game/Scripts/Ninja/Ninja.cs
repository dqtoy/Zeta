﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NinjaNodeMock))]
[RequireComponent(typeof(NinjaNodeIdle))]
[RequireComponent(typeof(NinjaNodeWalk))]
[RequireComponent(typeof(NinjaNodeJump))]
[RequireComponent(typeof(NinjaNodeThrow))]
[RequireComponent(typeof(NinjaNodeCrouch))]
[RequireComponent(typeof(NinjaNodeJumpThrow))]
[RequireComponent(typeof(NinjaNodeCrouchThrow))]
[RequireComponent(typeof(NinjaNodeFall))]
public class Ninja : MonoBehaviour {

    public static Ninja I;


    GameInput_Base gameInput;
    NinjaNode_Base ninjaNode;

    public float speed = 5.0f;
    public float jumpForce = 800;
    public float horizontalMovementScalar = 500;
    public float horizontalMaxSpeed = 10;
    public float shurikenHitSFXDelay = 0.25f;

    SpriteRenderer _spriteRenderer;
    Rigidbody2D _rigidbody2D;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        I = this;
    }

    void Start() {
        gameInput = GameInputForUser.I;
        ninjaNode = NinjaNodeIdle.I;
        ninjaNode.EnterNode();
        Toolbox.Log(ninjaNode.GetType().Name + ": Enter");
    }

    void Update() {

        ninjaNode.UpdateNode();

        if (gameInput.KeyDownForLeft()) {
            FaceLeft();
        }
        if (gameInput.KeyDownForRight()) {
            FaceRight();
        }

        if(transform.position.y < -10) {
            Vector3 newPosition = new Vector3(transform.position.x, 15, transform.position.z);
            transform.position = newPosition;
        }
    }

    public void EnableControls() {
        gameInput = GameInputForUser.I;
    }

    public void DisableControls() {
        gameInput = GameInputMock.I;
    }

    public void FaceLeft() {
        _spriteRenderer.flipX = true;
    }

    public void FaceRight() {
        _spriteRenderer.flipX = false;
    }

    void FixedUpdate() {
        ninjaNode.FixedUpdateNode();

        MoveHorizontal();
    }

    public void SwitchNode(NinjaNode_Base _ninjaNode) {
        ninjaNode.ExitNode();
        Toolbox.Log(ninjaNode.GetType().Name + ": Exit");

        ninjaNode = _ninjaNode;
        ninjaNode.EnterNode();
        Toolbox.Log(ninjaNode.GetType().Name + ": Enter");
    }

    public void SetAnimation(int animation) {
        GetComponent<Animator>().SetInteger("animation", animation);
    }

    public Vector3 GetVelocity() {
        return GetComponent<Rigidbody2D>().velocity;
    }

    public void MoveHorizontal() {
        if (Math.Abs(_rigidbody2D.velocity.x) < horizontalMaxSpeed) {
            if (gameInput.KeyForLeft()) {
                Vector2 movement = new Vector2(-1, 0);
                Vector2 horizontalForce = horizontalMovementScalar * movement;
                _rigidbody2D.AddForce(horizontalForce);
            }
            if (gameInput.KeyForRight()) {
                Vector2 movement = new Vector2(1, 0);
                Vector2 horizontalForce = horizontalMovementScalar * movement;
                _rigidbody2D.AddForce(horizontalForce);
            }
        }
    }

    public void WalkIfInput() {
        if ((gameInput.KeyForLeft() || gameInput.KeyForRight())) {
            SwitchNode(NinjaNodeWalk.I);
        }
    }

    public void JumpIfInput() {
        if (gameInput.KeyForJump()) {
            SwitchNode(NinjaNodeJump.I);
        }
    }

    public void ThrowIfInput() {
        if (gameInput.KeyDownForThrow()) {
            Throw();
        }
    }

    public void JumpThrowIfInput() {
        if (gameInput.KeyDownForThrow()) {
            SwitchNode(NinjaNodeJumpThrow.I);
        }
    }

    public void Throw() {
        SwitchNode(NinjaNodeThrow.I);
    }

    public void CrouchIfInput() {
        if (gameInput.KeyForCrouch()) {
            SwitchNode(NinjaNodeCrouch.I);
        }
    }

    public void CrouchThrowIfInput() {
        if (gameInput.KeyDownForThrow()) {
            SwitchNode(NinjaNodeCrouchThrow.I);
        }
    }

    public void ThrowShuriken() {
        ActorSFXManager.I.Play(0);

        GameObject goShuriken = Toolbox.Create("Shuriken");
        goShuriken.transform.position = transform.position;
        AgentShuriken shuriken = goShuriken.GetComponent<AgentShuriken>();
        
        if(isFacingLeft()) {
            shuriken.LaunchLeft();
        } else {
            shuriken.LaunchRight();
        }
    }

    bool isFacingLeft() {
        return _spriteRenderer.flipX == true;
    }
}