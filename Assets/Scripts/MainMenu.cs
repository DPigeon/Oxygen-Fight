using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public void MenuInteractions(string name) {
        Application.LoadLevel(name);
    }

    public void SelectMode(bool mode) {
        ModeSelection.mode = mode;
    }
}
