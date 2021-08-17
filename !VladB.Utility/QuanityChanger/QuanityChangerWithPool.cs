using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class QuanityChangerWithPool : QuanityChanger {
        [Header("Reposition Settings")]
        [SerializeField] Vector3 randomRepositionMin;
        [SerializeField] Vector3 randomRepositionMax;

        Pool pool;

        public virtual void Init(Pool _pool) {
            pool = _pool;
        }

        public override void ChangeQuality(INumbered _numbered) {
            int needSpawn = GetCalculatedValue(_numbered.count) - _numbered.count;

            if (needSpawn >= 1) {
                _numbered.lastQuanityChanger = this;
            }

            for (int i = 0; i < needSpawn; i++) {
                GameObject go = Spawn();
                if (go != null) {
                    InitNewObject(_numbered , go);
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

        protected virtual void Reposition(GameObject go , Vector3 _basePos) {
            Vector3 pos = _basePos;
            pos.x += Random.Range(randomRepositionMin.x , randomRepositionMax.x);
            pos.y += Random.Range(randomRepositionMin.y , randomRepositionMax.y);
            pos.z += Random.Range(randomRepositionMin.z , randomRepositionMax.z);
            go.transform.position = pos;
        }

        protected virtual void InitNewObject(INumbered _numbered , GameObject go) {
            Reposition(go, (_numbered as Component).transform.position);
        }
    }
}
