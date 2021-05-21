using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwarenessBehaviour : MonoBehaviour {

    public Slider Slider;
    public Color Low;
    public Color High;

    public void SetAwareness(float awareness, float maxAwareness) {
        Slider.value = awareness;
        Slider.maxValue = maxAwareness;

        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);
    }

    // Update is called once per frame
    void Update() {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position);
    }
}
