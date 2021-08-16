// !!!
// !!! ОДНА из строчек додлжна быть раскоменчена в зависимости от того есть ли FireBase в проекте
// !!!
/*

#define IsUseFirebaseAnalytics //ВКЛючает отправку аналитики
//#undef IsUseFirebaseAnalytics //ВЫКЛючает отправку аналитики

using UnityEngine;

#if IsUseFirebaseAnalytics
using Firebase.Analytics;
#endif

namespace VladB.Utility {
    public class AnalyticsEventer{

        public static void AddLog(string eventName, params AnalyticsEventer_Parameter[] eventParameters) {
#if IsUseFirebaseAnalytics
        if (Analytics.Instance == null) {
            return;
        }

        AnalitycsEvent analitycsEvent = new AnalitycsEvent(eventName);

        for (int i = 0; i < eventParameters.Length; i++) {
            analitycsEvent.AddParameter(new Parameter(eventParameters[i].parameterName, eventParameters[i].parameterValue));
        }

        Analytics.Instance.AddLog(analitycsEvent);


        if (Analytics.Instance.debug) {
            string log = $"EventName = <color=white>{eventName}</color>";
            for (int i = 0; i < eventParameters.Length; i++) {
                log += "\n" + eventParameters[i].ToString();
            }
            Debug.Log(log);
        }

#endif
        }

        //void Examples() {
        //    //...
        //    AnalyticsEventer.AddLog("eventName1");
        //    //...

        //    //...
        //    AnalyticsEventer_Parameter par1 = new AnalyticsEventer_Parameter("Name1", "Value1");
        //    AnalyticsEventer_Parameter par2 = new AnalyticsEventer_Parameter("Name2", "Value2");
        //    AnalyticsEventer.AddLog("eventName2", par1, par2);
        //    //...

        //    //...
        //    AnalyticsEventer.AddLog("eventName3",
        //        new AnalyticsEventer_Parameter("Name3", "Value3"),
        //        new AnalyticsEventer_Parameter("Name4", "Value4"),
        //        new AnalyticsEventer_Parameter("Name5", "Value5")
        //    );
        //    //...

        //}

    }

    public class AnalyticsEventer_Parameter {
        public string parameterName;
        public string parameterValue;

        public AnalyticsEventer_Parameter(string _parameterName, string _parameterValue) {
            parameterName = _parameterName;
            parameterValue = _parameterValue;
        }

        public override string ToString() {
            return $"Name = <color=cyan>{parameterName}</color>    ,    Value = <color=yellow>{parameterValue}</color>";
        }
    }
}


*/