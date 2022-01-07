using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VladB.Utility;

namespace VladB.EffectsSystem {
    [System.Serializable]
    public class EffectData_Base {
        [Header("Source")]
        public IEffectSource source;

        [Header("Priority")]
        [Range(0, 255)] public int priority;

        [Header("Unique?")]
        public bool isFullUnique;//������ ����������, �� ����� ������ ���� �������� ����� ������ �� ��� ����� ���������������
        public bool isUniqueBySource;// ����� ������ ��������� ���������� ��������, �� ������ 1 �� ����� ���������
        public bool isUniqueByGUID = true;// ����� ������ ��������� ���������� ��������, �� ������ ���� ������ 1
        public string guid = "";

        [Header("Other")]
        public bool isNeedUpdate;


        public EffectData_Base() {
            if(guid.IsNullOrEmpty()) {
                guid = Extensions.GetRandomGUID();
            }
        }

        public virtual void CopyDataTo(Effect newEffect) {
            newEffect.data = MemberwiseClone() as EffectData_Base;
            //newData.source = source;
            //newData.priority = priority;
            //newData.isUnique = isUnique;
            //newData.isUniqueByThis = isUniqueByThis;
        }

        public virtual string GetDebugLog() {
            string log = "";
            log += $"source = {source} \n";
            log += $"priority = {priority} \n";
            log += $"isFullUnique = {isFullUnique} \n";
            log += $"isUniqueBySource = {isUniqueBySource} \n";
            log += $"isUniqueByGUID = {isUniqueByGUID} \n";
            log += $"guid = {guid} \n";
            return log;
        }
    }


    public interface IEffectSource { }
}