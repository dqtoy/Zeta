﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorTitleScreen : MonoBehaviour {

    public static ActorTitleScreen I;

    GameObject goPanel;


    void Awake() {
        I = this;

        goPanel = transform.Find("Panel").gameObject;
    }

    // Start is called before the first frame update
    void Start() {
        HidePanel();
    }

    public void ShowPanel() {
        goPanel.SetActive(true);
    }

    public void HidePanel() {
        goPanel.SetActive(false);
    }
}
