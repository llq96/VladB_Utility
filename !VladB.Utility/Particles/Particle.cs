using System.Collections;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace VladB.Utility {
    [RequireComponent(typeof(Timer))]
    public class Particle : MonoBehaviour, IParticle, IPoolObject { //TODO Верунть закоментированное
        [Header("Settings")]
        public InitType initType = InitType.Awake;

        [Header("Particles")]
        public ParticleSystem[] particles;

        //[Header("Timer")]
        [HideInInspector] Timer timer;

        //[Header("Sound")]
        //public AudioSource soundPLay;

        //[Header("Vibration")]
        //public bool isVibrateOnPlay = true;
        //public int vibrateTimeMs = 60;

        #region Awake/Start/Update Methods
        void Awake() => AwakeFunc();
        protected virtual void AwakeFunc() {
            if(initType == InitType.Awake) {
                Init();
            }
        }

        void Start() => StartFunc();
        protected virtual void StartFunc() {
            if(initType == InitType.Start) {
                Init();
            }
        }

        void Update() => UpdateFunc();
        protected virtual void UpdateFunc() { }
        #endregion

        public void Init() {
            timer = GetComponent<Timer>();

            timer.OnEndTime -= EndTimer;
            timer.OnEndTime += EndTimer;

            if(particles.Count() == 0) {
                particles = GetComponentsInChildren<ParticleSystem>(true);
            }
        }

        #region Play
        public virtual void Play() {
            gameObject.SetActive(true);

            Run_Particles();
            //Run_Sound();
            //Run_Vibration();

            timer.TimerSetActive(false, false);
            timer.maxTimeValue = GetMaxDuration();
            timer.TimerSetActive(true, true);

            isBusy = true;
        }

        public virtual void Play(Vector3 position) {
            transform.position = position;
            Play();
        }


        //Осторожно, не тестировал
        public virtual void PlayWithDelays(float delayParticles, float delaySound = 0f, float delayVibration = 0f) {
            if(delayParticles > 0f) {
                StartCoroutine(Run_Partciles_Cor(delayParticles));
            } else {
                Run_Particles();
            }

            //if (_delaySound > 0f) {
            //    StartCoroutine(Run_Sounds_Cor(_delaySound));
            //} else {
            //    Run_Sound();
            //}

            //if (_delayVibration > 0f) {
            //    StartCoroutine(Run_Vibration_Cor(_delayVibration));
            //} else {
            //    Run_Vibration();
            //}
        }

        #endregion


        #region Run Particles/Sound/Vibration
        protected virtual void Run_Particles() {
            particles.Act(p => p.TryDo(x => x.Play()));
        }

        //void Run_Sound() {
        //    if (soundPLay) {
        //        soundPLay.Play();
        //    }
        //}

        //void Run_Vibration() {
        //    if (isVibrateOnPlay) {
        //        Vibration.Vibrate(vibrateTimeMs);
        //    }
        //}
        #endregion


        #region Coroutines
        protected virtual IEnumerator Run_Partciles_Cor(float delay) {
            yield return new WaitForSecondsRealtime(delay);
            Run_Particles();
        }

        //IEnumerator Run_Sounds_Cor(float delay) {
        //    yield return new WaitForSecondsRealtime(delay);
        //    Run_Sound();

        //}

        //IEnumerator Run_Vibration_Cor(float delay) {
        //    yield return new WaitForSecondsRealtime(delay);
        //    Run_Vibration();
        //}
        #endregion


        #region Stop
        public virtual void Stop() {
            particles.Act(p => p.TryDo(x => x.Clear(true)));
            timer.TimerSetActive(false, true);
            isBusy = false;
        }
        #endregion


        #region IPoolObject Realization
        public virtual bool isBusy { get; set; }
        public virtual void SetActivePoolObject(bool _isActive) {
            //if (_isActive) {
            //    Play();
            //} else {
            //    Stop();
            //}
        }

        public virtual void Init(Pool _pool) {
            Init();
        }
        #endregion


        #region Other
        protected virtual void EndTimer() {
            isBusy = false;
        }

        protected virtual float GetMaxDuration() {
            return particles.Where(p => p != null).Max(p => p.main.duration);
        }


        #endregion
        public void SetColor(Color newColor) {
            particles.Act(p => {
                //Change MainColor
                var main = p.main;
                main.startColor = newColor;

                //Change colors in gradient in ColorOverLifeTime
                var coloroverLifetime = p.colorOverLifetime;
                if(coloroverLifetime.enabled) {
                    var newGradient = new Gradient();
                    var newColorKeys = new GradientColorKey[] { new GradientColorKey(newColor , 0f),
                                                                  new GradientColorKey(newColor, 1f) };
                    newGradient.SetKeys(newColorKeys, coloroverLifetime.color.gradient.alphaKeys);
                    coloroverLifetime.color = newGradient;
                }
            });
        }
    }


    public interface IParticle {
        public void Play();
        public void Stop();
    }


}