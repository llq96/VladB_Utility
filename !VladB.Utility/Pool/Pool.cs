using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class Pool : MonoBehaviour {
        [Header("Main Settings")]
        [SerializeField] GameObject prefab;
        [SerializeField] Transform parent;
        [SerializeField] int maxObjects = 999;

        public List<IPoolObject> objects = new List<IPoolObject>();
        IPoolObject tempPoolObject;


        public virtual void InitObject(IPoolObject _object) {
            _object.Init(this);
        }

        public virtual IPoolObject InstantiateObject() {
            tempPoolObject = Instantiate(prefab, parent).GetComponent<IPoolObject>();
            if(tempPoolObject != null) {
                InitObject(tempPoolObject);
                objects.Add(tempPoolObject);
            }
            return tempPoolObject;
        }

        public virtual void InstantiateObjects(int _count) {
            for(int i = 0; i < _count; i++) {
                InstantiateObject();
            }
        }

        public virtual IPoolObject GetFreeObject() {
            for(int i = 0; i < objects.Count; i++) {
                if(!(objects[i] is Component)) {
                    Debug.Log("Null");
                    continue;
                }
                if(!objects[i].isBusy) {
                    return ReturnIPoolobject(objects[i]);
                }
            }

            if(objects.Count >= maxObjects) {
                return null;
            }

            tempPoolObject = InstantiateObject();
            return ReturnIPoolobject(tempPoolObject);

            //Local Function
            IPoolObject ReturnIPoolobject(IPoolObject _poolObject) {
                _poolObject.isBusy = true;
                return _poolObject;
            }
        }

        public virtual void SetActiveAllObjects(bool _isActive, bool _isMakeAllFree) {
            for(int i = 0; i < objects.Count; i++) {
                objects[i].SetActive(_isActive);
                if(_isMakeAllFree) {
                    objects[i].isBusy = false;
                }
            }
        }

        public virtual IEnumerator SetActiveAllObjects_Cor(bool _isActive, bool _isMakeAllFree, float _delay = 0f) {
            yield return new WaitForSeconds(_delay);
            SetActiveAllObjects(_isActive, _isMakeAllFree);
        }
    }

    public interface IPoolObject {
        public bool isBusy { get; set; }
        public void SetActive(bool _isActive);
        public void Init(Pool _pool);
    }
}