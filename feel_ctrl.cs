// #name Modify Feel Gauge
// #author ghorsington
// #desc Allows to fill the Feel Gauge during H with the additional mouse buttons.

using UnityEngine;

public static class FeelControl {
    private static GameObject gameObject;
    public static void Main() {
        gameObject = new GameObject();
        gameObject.AddComponent<MB>();
    }

    public static void Unload() {
        UnityEngine.Object.Destroy(gameObject);
        gameObject = null;
    }

    class MB : MonoBehaviour {
        void Awake() {
            DontDestroyOnLoad(this);
        }

        void Update() {
            if(Input.GetMouseButtonDown(3)) {
                var inst = Singleton<HSceneFlagCtrl>.Instance;
                if(inst) inst.feel_f = 1f;
            }

            if(Input.GetMouseButtonDown(4)) {
                var inst = Singleton<HSceneFlagCtrl>.Instance;
                if(inst) inst.feel_m = 1f;
            }
        }
    }
}