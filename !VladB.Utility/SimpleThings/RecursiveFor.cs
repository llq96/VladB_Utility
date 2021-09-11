using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class RecursiveFor {
        public bool isActive { get; private set; }

        bool isNeedStop;


        public void For(int countFors, int eachLength, Action<int[]> action) {
            int[] lengths = new int[countFors];
            lengths.Act((item, i) => lengths[i] = eachLength);
            For(countFors, lengths, action);
        }

        public void For(int countFors, int[] lengths, Action<int[]> action) {
            if(countFors != lengths.Length) {
                Debug.LogError("Wrong Array Length");
                return;
            }

            if(!isActive) {
                isActive = true;
                isNeedStop = false;
                List<int> ints = (new int[countFors]).ToList();
                DoRecursiveFor(countFors, lengths, action, ints);
                isActive = false;
            }
        }

        void DoRecursiveFor(int countFors, int[] lengths, Action<int[]> action, List<int> ints) {
            for(int i = 0; i < lengths[ints.Count - countFors]; i++) {
                if(isNeedStop) {
                    return;
                }

                ints[ints.Count - countFors] = i;

                if(countFors - 1 == 0) {
                    action?.Invoke(ints.ToArray());
                } else {
                    DoRecursiveFor(countFors - 1, lengths, action, ints);
                }

            }
        }

        public void Stop() {
            isNeedStop = true;
        }
    }
}
