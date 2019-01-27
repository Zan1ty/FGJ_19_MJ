using UnityEngine;
using UnityEngine.UI;

public class DevConsole : MonoBehaviour
{
    public GameObject ui;
    public InputField input;
    public Text gameDisplay;
    public Dropdown drones;

    private void Start()
    {
        input.text = @"-- Acceleration is available in currentAccelerationValue
-- Position is available in currentPositionValues (1 = x, 2 = y, 3 = z)
-- Rotation is available in currentOrientationValues (1 = pitch, 2 = yaw, 3 = roll)

function doControl()
    deadzone = 1
	power = 20
    if currentPositionValues[2] < 1 then
        power = 40
	end
	
	fl = power
	fr = power
	bl = power
	br = power
	
	if currentOrientationValues[1] < 180 and currentOrientationValues[1] > deadzone then
		fl = fl + currentOrientationValues[1]
		fr = fr + currentOrientationValues[1]
	end
	
	if currentOrientationValues[1] > 180 and currentOrientationValues[1] < 360 - deadzone then
		bl = bl + (360 - currentOrientationValues[1])
		br = br + (360 - currentOrientationValues[1])
	end
	
	if currentOrientationValues[3] < 180 and currentOrientationValues[3] > deadzone then
		bl = bl + currentOrientationValues[3]
		fl = fl + currentOrientationValues[3]
	end
	
	if currentOrientationValues[3] > 180 and currentOrientationValues[1] < 360 - deadzone then
		br = br + (360 - currentOrientationValues[3])
		fr = fr + (360 - currentOrientationValues[3])
	end
	
	return bl, br, fl, fr
end

return doControl()";
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
            Open();
    }

    public void Open()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Drone");
        drones.options.Clear();
        foreach (GameObject obj in objs)
            drones.options.Add(new Dropdown.OptionData(obj.name));
        Time.timeScale = 0;
        ui.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Compile()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        gameDisplay.text = input.text;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Drone");
        Drone drone = objs[drones.value].GetComponent<Drone>();
        drone.script = input.text;
        drone.manual = false;
        ui.SetActive(false);
    }
}
