using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelTimeLine : MonoBehaviour
{
    private PlayableDirector pd;
    private void OnEnable() {
        pd = GetComponent<PlayableDirector>();
        pd.time = 0;
        pd.enabled = true;
    }

    private void Update() {
        if (pd.time > 12f) {
            pd.enabled = false;
        }
    }
}
