using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VladB.Utility;

namespace VladB.EffectsSystem {
    public class EffectsReciever : MonoBehaviour {
        [Header ("Effects list")]
        public List<Effect> effects = new List<Effect>();

        public Action OnAddAnyEffect;
        public Action<Effect> OnAddThisEffect;
        public Action<Type> OnAddAnyEffectWithType;

        public Action OnRemoveAnyEffect;
        public Action<Effect> OnRemoveThisEffect;
        public Action<Type> OnRemoveAnyEffectWithType;


        public void AddEffect<T1,T2>(T2 effectDataSource) where T1 : Effect where T2 : EffectData_Base {
            if(effectDataSource.isFullUnique) {
                if(effects.Any(ef => ef is T1)) {
                    Debug.Log("Effects Already Exist On Reciever");
                    return;
                }
            }

            if(effectDataSource.isUniqueBySource) {
                if(effects.Any(ef => ef is T1 && ef.data.source == effectDataSource.source)) {
                    Debug.Log("Effects From This Source Already Exist On Reciever");
                    return;
                }
            }

            if(effectDataSource.isUniqueByGUID) {
                if(effects.Any(ef => ef is T1 && ef.data.guid == effectDataSource.guid)) {
                    Debug.Log("Effects WithThisGUID Already Exist On Reciever");
                    return;
                }
            }

            var newEffect = gameObject.AddComponent<T1>() as Effect;
            effectDataSource.CopyDataTo(newEffect);
            effects.Add(newEffect);

            OnAddAnyEffect?.Invoke();
            OnAddThisEffect?.Invoke(newEffect);
            OnAddAnyEffectWithType?.Invoke(newEffect.GetType());
        }

        public void RemoveEffects_ByDataGUID(EffectData_Base data) {
            RemoveEffects_ByGUID(data.guid);
        }

        public void RemoveEffects_ByGUID(string guid) {
            effects.Where(ef => ef.data.guid == guid).ToList().Act(ef => RemoveEffect(ef));
        }

        public void RemoveEffects_BySource(IEffectSource source) {
            effects.Where(ef => ef.data.source == source).ToList().Act(ef => RemoveEffect(ef));
        }

        public void RemoveEffect(Effect effect) {
            if(effect) {
                if(effects.Contains(effect)) {
                    effects.Remove(effect);

                    OnRemoveAnyEffect?.Invoke();
                    OnRemoveThisEffect?.Invoke(effect);
                    OnRemoveAnyEffectWithType?.Invoke(effect.GetType());

                    Destroy(effect);
                } else {
                    Debug.LogError("Not Exist Effect");
                }
            } else {
                Debug.LogError("effect == null");
            }
        }

        public List<T> GetEffectsWithType<T>() where T : Effect {
            return effects.Where(e => e is T).OfType<T>().OrderBy(e => e.data.priority).ToList();
        }

    }
}