using System;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    private float time_elapsed;
    private bool run_began = false;
    private GUIStyle style;

    void Start()
    {
        style = new GUIStyle();
        style.alignment = TextAnchor.LowerCenter;
        style.fontSize = 32;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (run_began) {
            time_elapsed += Time.deltaTime;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (run_began) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (!player) return;

        run_began = true;
        time_elapsed = 0.0f;
    }

    private void OnTriggerEnter(Collider other) {
        if (run_began) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (!player) return;

        run_began = false;
        time_elapsed = 0.0f;

        GameObject.FindFirstObjectByType<EndPlatform>().ResetRun();
    }

    private void OnGUI()
    {
        if (!run_began) return;

        float posX = Camera.main.pixelWidth / 2;
        float posY = Camera.main.pixelHeight / 10;

        GUI.Label(new Rect(posX, posY, 64, 32), Math.Round(time_elapsed,2).ToString(), style);
    }

    public float EndRun() {
        run_began = false;
        return time_elapsed;
    }
}
