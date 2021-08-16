using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class ParticlesPool :  SimplePool {
        public Particle GetFreeParticle() {
            IPoolObject obj = GetFreeObject();
            if (obj != null) {
                return obj as Particle;
            } else {
                return null;
            }
        }
    }
}


