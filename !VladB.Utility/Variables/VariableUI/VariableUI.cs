using UnityEngine;


namespace VladB.Utility {
    public abstract class VariableUI<T> : MonoBehaviour {
        [Header("Prefix/Postfix")]
        public string prefix;
        public string postfix;

        protected Variable<T> variable;
        protected T variableValue;

        #region SetVariable
        public virtual void SetVariable(Variable<T> variable) {
            if(this.variable != null) {
                this.variable.OnValueChanged_Ev3 -= UpdateVariableValue;
            }

            this.variable = variable;

            if(this.variable != null) {
                this.variable.OnValueChanged_Ev3 -= UpdateVariableValue;
                this.variable.OnValueChanged_Ev3 += UpdateVariableValue;
            }
            UpdateVariableValue();
        }
        #endregion

        #region UpdateVariableValue
        protected virtual void UpdateVariableValue(T newValue, T oldValue, T deltaValue) {
            this.variableValue = newValue;
        }

        public virtual void UpdateVariableValue() {
            if(variable != null) {
                UpdateVariableValue(variable.value, default ,default);
            }
        }
        #endregion

        #region UpdateVariableValue UI
        public abstract void UpdateVariableUI();
        #endregion

        #region OnEnable/OnDisable/Update Functions
        void OnEnable() => OnEnableFunc();
        protected virtual void OnEnableFunc() {
            if(variable != null) {
                variable.OnValueChanged_Ev3 += UpdateVariableValue;
            }
            UpdateVariableValue();
        }


        void OnDisable() => OnDisableFunc();
        protected virtual void OnDisableFunc() {
            if(variable != null) {
                variable.OnValueChanged_Ev3 -= UpdateVariableValue;
            }
        }

        void Update() => UpdateFunc();
        protected virtual void UpdateFunc() { }
        #endregion
    }
}