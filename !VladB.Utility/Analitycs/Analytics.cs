/*


namespace VladB.Utility{
	
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	using System;
	using System.Threading.Tasks;

	//#if !UNITY_IOS
	using Firebase;
	using Firebase.Analytics;
	//#endif

	public class Analytics : MonoBehaviour {
		public static Analytics Instance;

		public bool dntDestroyOnLoad = false;
		public bool destroyOnIOS = false;
		public bool debug = false;

		protected bool firebaseInitialized = false;

		public delegate void AnalyticsInitEvent();
		public static event AnalyticsInitEvent OnAnalyticsInit;

		//#if !UNITY_IOS
		DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
		//#endif

		void Awake(){
			InitAwake ();
		}

		void InitAwake(){
			#if UNITY_IOS
			if(destroyOnIOS){
				Destroy (gameObject);
			}
			#endif

			if (Application.isPlaying) {
				if(dntDestroyOnLoad){
					DontDestroyOnLoad(gameObject);
					if(Analytics.Instance != null){
						Destroy(gameObject);
					}else{
						Instance = this;
					}
				}else{
					Instance = this;
				}
			}
		}

		//#if !UNITY_IOS

		// When the app starts, check to make sure that we have
		// the required dependencies to use Firebase, and if not,
		// add them if possible.
		public virtual void Start() {
#if !UNITY_EDITOR
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
				dependencyStatus = task.Result;
				if (dependencyStatus == DependencyStatus.Available) {
					InitializeFirebase();
				} else {
					Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
				}
			});
#endif
		}

		// Handle initialization of the necessary firebase modules:
		void InitializeFirebase() {
			DebugLog("Enabling data collection.");
			FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

			DebugLog("Set user properties.");
			// Set the user's sign up method.
			FirebaseAnalytics.SetUserProperty(FirebaseAnalytics.UserPropertySignUpMethod, Application.platform.ToString());
			
			// Set default session duration values.
			FirebaseAnalytics.SetMinimumSessionDuration(new TimeSpan(0, 0, 10));
			FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));

			firebaseInitialized = true;

			if(OnAnalyticsInit != null){
				OnAnalyticsInit ();
			}
		}

		// Output text to the debug log text field, as well as the console.
		public void DebugLog(string s) {
			if (debug) {
				print(s);
			}
		}

        public void AddLog(AnalitycsEvent aEvent) {
            DebugLog("Logging a " + aEvent.eventName + " event.");

#if !UNITY_EDITOR
            if (aEvent.GetParameters().Length > 0) {
				FirebaseAnalytics.LogEvent (aEvent.eventName, aEvent.GetParameters());
			} else {
				FirebaseAnalytics.LogEvent (aEvent.eventName);
			}
#endif
		}
	}

	[System.Serializable]
	public class AnalitycsEvent{
		public string eventName;
		private List<Parameter> parameters = new List<Parameter>();

		/// <summary>
		/// Use FirebaseAnalytics class to add default name for events or just any string for custom event name.
		/// Use FirebaseAnalytics class to add default name for parameter. Group of methods starts with "Parameter".
		/// Parameter class input data:
		/// Parameter(string, string), Parameter(string, long), Parameter(string, double),
		/// </summary>
		public AnalitycsEvent(string name){
			eventName = name;
		}

		/// <summary>
		/// Example:
		/// AnalitycsEvent event = new AnalitycsEvent(
		/// FirebaseAnalytics.EventLevelUp,
		///	new Parameter(FirebaseAnalytics.ParameterLevel, 5),
		///	new Parameter(FirebaseAnalytics.ParameterCharacter, "mrspoon"),
		///	new Parameter("hit_accuracy", 3.14f)
		/// );
		/// </summary>
		public AnalitycsEvent(string name, params Parameter[] eventParameters){
			eventName = name;

			for(int i = 0; i < eventParameters.Length; i++){
				if(eventParameters[i] != null){
					parameters.Add (eventParameters[i]);	
				}
			}
		}

		public AnalitycsEvent(string name, List<AnalitycsParameter> eventParameters){
			eventName = name;

			foreach(AnalitycsParameter aParameter in eventParameters){
				if (aParameter.valueType == ParameterValueType.Long) {
					parameters.Add (new Parameter (aParameter.name, Convert.ToInt64 (aParameter.value)));
				} else if (aParameter.valueType == ParameterValueType.Double) {
					parameters.Add (new Parameter (aParameter.name, Convert.ToDouble (aParameter.value)));
				} else {
					parameters.Add (new Parameter (aParameter.name, aParameter.value));
				}
			}
		}

		public void AddParameter(Parameter newParameter){
			parameters.Add (newParameter);
		}

		public void AddParameter(params Parameter[] newParameters){
			for(int i = 0; i < newParameters.Length; i++){
				parameters.Add (newParameters[i]);
			}
		}

		public Parameter[] GetParameters(){
			return parameters.ToArray ();
		}

		//#endif
	}

	[System.Serializable]
	public class AnalitycsParameter{
		public ParameterValueType valueType;
		public string name;
		public string value;

		public AnalitycsParameter(string name, string value){
			this.name = name;
			this.value = value;
		}
	}

	public enum AnalitycsEventType{
        TotalScore,
        GameModeClick,
        ChestRewardEarned,
        StarClick,
        SequinBuy,
        HelixGame,
        BuyItem,
        RouletteFree,
        RouletteVideo,
        GameHG,
        Custom
    }

	public enum ParameterValueType{
		String,
		Long,
		Double
	}
}


*/