using UnityEngine;
using UnityEngine.UI;

public class DevConsole : MonoBehaviour
{
    public GameObject ui;
    public InputField input;
    float _scale;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
            Open();
    }

    public void Open()
    {
        _scale = Time.timeScale;
        Time.timeScale = 0;
        ui.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Compile()
    {
        ui.SetActive(false);
        Time.timeScale = _scale;
        Cursor.lockState = CursorLockMode.Locked;

        /// TODO: Pass input.text to LUA
    }
}
