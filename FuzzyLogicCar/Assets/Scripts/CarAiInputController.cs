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
        private Rigidbody carBody = null;

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
            carBody = GetComponent<Rigidbody>();
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
            m_Car.Move(h, v, handbrake, handbrake);
#else
            m_Car.Move(h, v, handbrake, 0f);
            
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

        public float GetRateOfDisplacement()
        {
            float wideAngle = Mathf.PI / 4;
            Vector3 forward = new Vector3(0,0,1);
            float cosTheta = Vector3.Dot(forward, carBody.velocity.normalized);
            float theta = Mathf.Cos(cosTheta);
            if (carBody.velocity.x < 0)
            {
                theta *= -1;
            }
            theta = fClamp(theta, -wideAngle, wideAngle);
            float displacementRate = theta / wideAngle;
            return displacementRate;
        }
        float fClamp(float x, float min, float max)
        {
            float val = x;

            if (x < min)
            {
                val = min;
            }
            else if (x > max)
            {
                val = max;
            }

            return val;
        }
    }
}

