using System;
using UnityEngine;

public class EndPlatform : MonoBehaviour
{
    private float time_elapsed;
    private bool run_ended = false;
    private GUIStyle style;

    void Start()
    {
        style = new GUIStyle();
        style.alignment = TextAnchor.LowerCenter;
        style.fontSize = 32;
        style.fontStyle = FontStyle.Bold;
        
        style.normal.textColor = Color.cyan;

        time_elapsed = 0.0f;
    }

    private void OnTriggerEnter(Collider other) {
        if (run_ended) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (!player) return;

        run_ended = true;
        time_elapsed = GameObject.FindFirstObjectByType<StartPlatform>().EndRun();
    }

    private void OnGUI()
    {
        if (!run_ended) return;

        float posX = Camera.main.pixelWidth / 2;
        float posY = Camera.main.pixelHeight / 10;

        GUI.Label(new Rect(posX, posY, 64, 32), MathF.Floor(time_elapsed).ToString(), style);
    }

    public void ResetRun() {
        time_elapsed = 0.0f;
        run_ended = false;
    }
}
