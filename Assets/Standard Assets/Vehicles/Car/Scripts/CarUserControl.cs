using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car;
        private Steering s;
		bool isTrainingMode;
		float lastJumpTime;

        private void Awake()
        {
            m_Car = GetComponent<CarController>();
            s = new Steering();
            s.Start();
			isTrainingMode = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name.ToLower ().Contains ( "training" );
			s.isTrainingMode = isTrainingMode;
			lastJumpTime = -5;
        }

		void LateUpdate ()
//        private void FixedUpdate()
        {
            s.UpdateValues();
            m_Car.Move(s.H, s.V, s.V, 0f);
			if ( isTrainingMode )
			{
				if ( CrossPlatformInputManager.GetButtonDown ( "Jump" ) )
					lastJumpTime = Time.time;
				
				if ( m_Car.IsGrounded () && (Time.time - lastJumpTime < 0.3f ) )
				{
					lastJumpTime = -1;
					m_Car.GetComponent<Rigidbody> ().AddForce ( Vector3.up * 10, ForceMode.VelocityChange );
					m_Car.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
				}
			}
        }
    }
}
