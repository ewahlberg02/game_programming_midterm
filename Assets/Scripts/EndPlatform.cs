using System;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPlatform : MonoBehaviour
{
    private float time_elapsed;
    private bool run_ended = false;
    private GUIStyle style;
    [SerializeField] GameObject canvas;
    [SerializeField] TextMeshProUGUI timeText;
    


    void Start()
    {
        style = new GUIStyle();
        style.alignment = TextAnchor.LowerCenter;
        style.fontSize = 32;
        style.fontStyle = FontStyle.Bold;
        
        style.normal.textColor = Color.cyan;

        time_elapsed = 0.0f;
        canvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (run_ended) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (!player) return;

        run_ended = true;
        time_elapsed = GameObject.FindFirstObjectByType<StartPlatform>().EndRun();
        EndGame(time_elapsed);
    }

    private void OnGUI()
    {
        if (!run_ended) return;

        float posX = Camera.main.pixelWidth / 2;
        float posY = Camera.main.pixelHeight / 10;

        GUI.Label(new Rect(posX, posY, 64, 32), Math.Round(time_elapsed,2).ToString(), style);
    }

    public void ResetRun() {
        time_elapsed = 0.0f;
        run_ended = false;
    }

    private void EndGame(float time_elapsed)
    {
        canvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        timeText.text = "Time: " + time_elapsed;
        GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>().enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().enabled = false;
    }

    public void quitMenu()
    {   
        Debug.Log("Quit Scene");
        SceneManager.LoadScene("MenuScreen");
    }
}
