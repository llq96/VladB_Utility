using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VladB.EffectsSystem;
using VladB.Utility;
using VladB.GameTemplate;

namespace VladB.EffectsSystem {
    public class Effect : MonoBehaviour {
        [HideInInspector]public EffectData_Base data;

        //public void CopyTo(Effect component) {
        //    data = new EffectData_Base();
        //    data.CopyDataTo(component.data);
        //}

        public void UpdateEffect() {

        }


        #region Debug
        [ContextMenu ("DebugLogData")]
        public void DebugLogData() {
            Debug.Log(GetDebugLogData());
        }

        public virtual string GetDebugLogData() {
            return data?.GetDebugLog();
        }
        #endregion
    }
}