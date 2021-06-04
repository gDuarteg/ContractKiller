using UnityEngine;
using UnityEngine.UI;

public class AwarenessBehaviour : MonoBehaviour {

    public Slider awarenessBar;
    public Color Low;
    public Color High;

    public void SetAwareness(float awareness, float maxAwareness) {
        awarenessBar.normalizedValue = 1f;
        awarenessBar.fillRect.GetComponentInChildren<Image>().color = Low;
    }

}
