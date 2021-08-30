using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class QuanityChangerWithPool : QuanityChanger {
        [Header("Reposition Settings")]
        [SerializeField] Vector3 randomRepositionMin;
        [SerializeField] Vector3 randomRepositionMax;

        Pool pool;

        public virtual void Init(Pool pool) {
            this.pool = pool;
        }

        public override void ChangeQuality(INumbered iNumbered) {
            int needSpawn = GetCalculatedValue(iNumbered.count) - iNumbered.count;

            if (needSpawn >= 1) {
                iNumbered.lastQuanityChanger = this;
            }

            for (int i = 0; i < needSpawn; i++) {
                GameObject go = Spawn();
                if (go != null) {
                    InitNewObject(iNumbered , go);
                }
            }
        }

        protected virtual GameObject Spawn() {
            IPoolObject _poolObject = pool.GetFreeObject();

            if (_poolObject is INumbered _numbered) {
                _numbered.lastQuanityChanger = this;
            }
            if(_poolObject is Component) {
                return (_poolObject as Component).gameObject;
            }
            return null;
        }

        protected virtual void Reposition(GameObject go , Vector3 basePos) {
            Vector3 pos = basePos;
            pos.x += Random.Range(randomRepositionMin.x , randomRepositionMax.x);
            pos.y += Random.Range(randomRepositionMin.y , randomRepositionMax.y);
            pos.z += Random.Range(randomRepositionMin.z , randomRepositionMax.z);
            go.transform.position = pos;
        }

        protected virtual void InitNewObject(INumbered iNumbered , GameObject go) {
            Reposition(go, (iNumbered as Component).transform.position);
        }
    }
}
