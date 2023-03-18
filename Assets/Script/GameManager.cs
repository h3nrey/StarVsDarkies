using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utils;
public class GameManager : MonoBehaviour {
    public static GameManager Game;

    public string actualScene;

    public UnityEvent onWinPhase;

    private void Start() {
        onWinPhase.AddListener(() => print("Win Phase"));
        actualScene = SceneManager.GetActiveScene().name;
    }

    private void Awake() {
        Game = this;
    }

    public void CallScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }


    public void CallNextPhase(float cooldown) {
        Coroutines.DoAfter(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1), cooldown, this);
    }
}
