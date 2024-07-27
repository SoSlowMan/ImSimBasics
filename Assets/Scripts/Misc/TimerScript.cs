using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text counterText;

    public float seconds, minutes, miliseconds;

    void Start()
    {
        counterText = GetComponent<Text>() as Text;
    }

    // TODO: change the LevelLoad time to a time, that goes from level to level
    void Update()
    {
        minutes = (int)(Time.timeSinceLevelLoad / 60f);
        seconds = (int)(60 - (Time.timeSinceLevelLoad % 60f));
        miliseconds = (int)(100 - (((Time.timeSinceLevelLoad % 60f) * 100) % 100));
        counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + miliseconds.ToString("00");
    }
}