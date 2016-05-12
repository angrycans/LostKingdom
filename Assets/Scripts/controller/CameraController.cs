using UnityEngine;
using System.Collections;

namespace ACANS
{
	public class CameraController : MonoBehaviour {

		//private Vector3 BeginPosition = Vector3.zero;
		//private Vector3 EndPosition   = Vector3.zero;

		public Camera _Camera;
		public GameObject GameView;
		public Vector2 ViewRect;
		public Vector2 ScrollRect=Vector3.zero;

		// Use this for initialization
		void Start () {
			Debug.Log(" screen.w="+Screen.width+" screen.h="+Screen.height);
			ViewRect.x = 1000;
			ViewRect.y = 1000;
			ScrollRect.x = 10000;
			ScrollRect.y = 10000;

			//FingerGestures.OnFingerDragEnd += OnFingerDragEnd;  
			//Debug.Log ("  ------ px");

		}

		// Update is called once per frame
		void Update () {


		}

		void OnDrag(DragGesture gesture) { 
			Vector3 screenPos2D = gesture.DeltaMove;
			//screenPos2D.z = 0f;            

			//change to screen position
			if (_Camera != null) {
				Vector3 cameraPos2D = _Camera.WorldToScreenPoint(_Camera.transform.position);
				cameraPos2D = cameraPos2D - screenPos2D;                

				//move camera position
				_Camera.transform.position = CalculateOffset ( _Camera.ScreenToWorldPoint(cameraPos2D));
			}else{
				Vector3 cameraPos2D = Camera.main.WorldToScreenPoint(Camera.main.transform.position);
				cameraPos2D = cameraPos2D - screenPos2D;                

				//move camera position
				Camera.main.transform.position = CalculateOffset ( Camera.main.ScreenToWorldPoint(cameraPos2D));
			}


			if (gesture.Phase == ContinuousGesturePhase.Ended) {
				Debug.Log ("OnDrag end");
			}
			//Camera.main.GetComponent<tk2dCamera> ().ZoomFactor = 2;
		}

		void OnTap(TapGesture gesture) { 
			//GetComponent<Particle>
			/*
		Debug.Log ("TapGesture");

		GameObject hit = GameObject.Find("hitstone");

		Debug.Log ("hit =" + hit.activeSelf );

		if (!hit.GetComponent<ParticleSystem>().isPlaying) {

			hit.GetComponent<ParticleSystem>().Play();
						//hit.SetActive (true);

				}

*/

		}


		private Vector3 BeginPosition = Vector3.zero;
		private Vector3 EndPosition   = Vector3.zero;
		private bool    m_PinchFlag  =  false;

		public const float ORTHOGRAPHICSIZECABOVE         = 1.0f;
		public const float ORTHOGRAPHICSIZECBELOW         = 0.5f;
		public const float ORTHOGRAPHICSIZEDEFAULT         = 1.0f;
		public const float PINCHSCALEFACTOR                      = 0.01f;
		public const float PINCHSCALEFACTOR_MIN                      = 0.2f;
		public const float PINCHSCALEFACTOR_MAX                      = 2.0f;



		void OnPinch( PinchGesture gesture )
		{
			Debug.Log ("OnPinch gesture.Delta="+gesture.Delta);

			if(gesture.Phase == ContinuousGesturePhase.Started)
			{
				BeginPosition = this.transform.position;
				m_PinchFlag = true;
			}
			if(gesture.Phase == ContinuousGesturePhase.Updated)
			{

				if (gesture.Delta > 0){
					Camera.main.GetComponent<tk2dCamera> ().ZoomFactor += gesture.Delta * PINCHSCALEFACTOR;
					Camera.main.GetComponent<tk2dCamera>().ZoomFactor = Mathf.Min(Camera.main.GetComponent<tk2dCamera> ().ZoomFactor,PINCHSCALEFACTOR_MAX);
				}else{
					Camera.main.GetComponent<tk2dCamera> ().ZoomFactor += gesture.Delta * PINCHSCALEFACTOR;
					Camera.main.GetComponent<tk2dCamera>().ZoomFactor = Mathf.Max(Camera.main.GetComponent<tk2dCamera> ().ZoomFactor,PINCHSCALEFACTOR_MIN);
				}
				//Camera.main.GetComponent<tk2dCamera> ().ZoomFactor = gesture.Delta * PINCHSCALEFACTOR;
				/*
			if (this.transform.camera.orthographicSize > ORTHOGRAPHICSIZECBELOW && gesture.Delta > 0) 
			{
				if (this.transform.camera.orthographicSize - ORTHOGRAPHICSIZECBELOW < gesture.Delta * PINCHSCALEFACTOR) 
				{
					this.transform.camera.orthographicSize = ORTHOGRAPHICSIZECBELOW;
				}
				else 
				{
					this.transform.camera.orthographicSize -= gesture.Delta * PINCHSCALEFACTOR;
				}
			} 
			else if (transform.camera.orthographicSize < ORTHOGRAPHICSIZECABOVE && gesture.Delta < 0) 
			{
				if (ORTHOGRAPHICSIZECABOVE - this.transform.camera.orthographicSize < Mathf.Abs(gesture.Delta) * PINCHSCALEFACTOR) 
				{
					this.transform.camera.orthographicSize = ORTHOGRAPHICSIZECABOVE;
				} 
				else 
				{
					this.transform.camera.orthographicSize -= gesture.Delta * PINCHSCALEFACTOR;
				}
			}
			*/


				//Camera.main.transform.position = BeginPosition; //CalculateOffset (Camera.main.transform.position);
			}
			if(gesture.Phase == ContinuousGesturePhase.Ended)
			{
				Invoke("setPinchFlagFalse", 0.15f);
			}
		}



		Vector3 CalculateOffset (Vector3 tmpPosition)
		{    
			//Debug.Log ("x="+tmpPosition.x+" y="+tmpPosition.y);
			/*
		if (tmpPosition.x < 0) 
		{
			tmpPosition.x = 0;
		}
		if (tmpPosition.x > ScrollRect.x) 
		{
			tmpPosition.x =ScrollRect.x+0;
		}
		if (tmpPosition.y > ScrollRect.y)
		{
			tmpPosition.y = ScrollRect.y;
		}
		if (tmpPosition.y < 0 ) 
		{
			tmpPosition.y = 0;
		}
		*/

			if (tmpPosition.x < 0) 
			{
				tmpPosition.x = tmpPosition.x/2;
			}
			if (tmpPosition.x > ScrollRect.x) 
			{
				tmpPosition.x =ScrollRect.x+(ScrollRect.x-tmpPosition.x)/2;
			}
			if (tmpPosition.y > ScrollRect.y)
			{
				tmpPosition.y = ScrollRect.y+(ScrollRect.y-tmpPosition.y)/2;
			}
			if (tmpPosition.y < 0 ) 
			{
				tmpPosition.y = tmpPosition.y/2;
			}

			return tmpPosition;
		}




	}

}

