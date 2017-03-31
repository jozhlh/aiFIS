using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarAiInputController : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        [SerializeField]
        [Range(-1.0f, 1.0f)]
        private float accelerator = 0.0f;

        [SerializeField]
        [Range(-1.0f, 1.0f)]
        private float steering = 0.0f;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
           // float h = CrossPlatformInputManager.GetAxis("Horizontal");
           // float v = CrossPlatformInputManager.GetAxis("Vertical");

            float v = accelerator;
            float h = steering;

#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }

        public void SetSteering(float steerVal)
        {
            steering = steerVal;
        }

        public float Steering()
        {
            return steering;
        }

        public void SetAcceleration(float driveVal)
        {
            accelerator = driveVal;
        }
    }
}

