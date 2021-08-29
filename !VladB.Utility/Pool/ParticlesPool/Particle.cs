using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    [RequireComponent(typeof(Timer))]
    public class Particle : MonoBehaviour, IParticle, IPoolObject { //TODO ������� �����������������

        [Header("Particles")]
        public ParticleSystem[] particles;

        //[Header("Timer")]
        [HideInInspector] Timer timer;

        //[Header("Sound")]
        //public AudioSource soundPLay;

        //[Header("Vibration")]
        //public bool isVibrateOnPlay = true;
        //public int vibrateTimeMs = 60;

        public void Start() {
            Init();
        }

        public void Init() {
            timer = GetComponent<Timer>();

            timer.OnEndTime -= EndTimer;
            timer.OnEndTime += EndTimer;
        }

        #region Play
        public virtual void Play() {
            gameObject.SetActive(true);
            //if (transform.parent != null) {
            //    transform.parent.gameObject.SetActive(true);
            //}

            Run_Particles();
            //Run_Sound();
            //Run_Vibration();

            timer.TimerSetActive(false, false);
            timer.maxTimeValue = GetMaxDuration();
            timer.TimerSetActive(true, true);

            isBusy = true;
        }



        //���������, �� ����������
        public virtual void PlayWithDelays(float _delayParticles, float _delaySound = 0f, float _delayVibration = 0f) {
            if(_delayParticles > 0f) {
                StartCoroutine(Run_Partciles_Cor(_delayParticles));
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
            for(int i = 0; i < particles.Length; i++) {
                particles[i].Play();
            }
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
        protected virtual IEnumerator Run_Partciles_Cor(float _delay) {
            yield return new WaitForSecondsRealtime(_delay);
            Run_Particles();
        }

        //IEnumerator Run_Sounds_Cor(float _delay) {
        //    yield return new WaitForSecondsRealtime(_delay);
        //    Run_Sound();

        //}

        //IEnumerator Run_Vibration_Cor(float _delay) {
        //    yield return new WaitForSecondsRealtime(_delay);
        //    Run_Vibration();
        //}
        #endregion


        #region Stop
        public virtual void Stop() {
            for(int i = 0; i < particles.Length; i++) {
                particles[i].Clear(true);
            }
        }
        #endregion


        #region IPoolObject Realization
        public virtual bool isBusy { get; set; }
        public virtual void SetActive(bool _isActive) {
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
            float result = 0f;
            for(int i = 0; i < particles.Length; i++) {
                if(particles[i]) {
                    result = Mathf.Max(result, particles[i].main.duration);
                }
            }
            return result;
        }
        #endregion

    }

    public interface IParticle {
        public void Play();
        public void Stop();
    }
}