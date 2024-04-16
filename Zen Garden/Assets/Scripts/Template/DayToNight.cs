using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts {
    public class DayToNight : MonoBehaviour {

        [SerializeField] private Material skybox; // Reference to the skybox that will rotate and change exposure
        [SerializeField] private Light sunlight; // Reference to the directional light representing the sun

        private float dayDuration = 180; // Duration of a full day in seconds
        private float timeScale = 0.1f; // Time scaler

        private static readonly int rotation = Shader.PropertyToID("_Rotation");
        private static readonly int exposure = Shader.PropertyToID("_Exposure");

        void Update() {
            // Proportion the current time of day as a value between 0 and 1
            float timeOfDay = Mathf.PingPong(Time.time / dayDuration, 1f);

            skybox.SetFloat(rotation, Time.time * timeScale); // Adjust rotation of the skybox slowly using timeScale
            skybox.SetFloat(exposure, Mathf.Clamp(Mathf.Sin(timeOfDay * Mathf.PI), 0.15f, 1f)); // Adjust exposure of skybox from night to day to night etc.


            // Update sun position and intensity based on time of day
            sunlight.transform.rotation = Quaternion.Euler(new Vector3(timeOfDay * 360f - 90f, 0f, 0f)); // Adjust position of sun from night to day to night etc.
            sunlight.intensity = Mathf.Lerp(0.4f, 1f, timeOfDay); // Adjust intensity from night to day to night etc.
        }
    }
}