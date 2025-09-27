using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIScript : MonoBehaviour
{
    [SerializeField]
    Image timer_image;
    [SerializeField]
    float total_time = 180.0f;
    float current_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current_time += Time.deltaTime;
        timer_image.fillAmount = 1.0f - (current_time / total_time);
    }

    public float GetCurrentTime() { return current_time; }
    public float GetTotalTime() { return total_time; }
    public float NormalizeTime() {  return current_time / total_time; }
}
