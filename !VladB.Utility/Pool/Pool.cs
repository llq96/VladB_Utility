using System.Linq;
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


        public virtual void InitObject(IPoolObject poolObject) {
            poolObject.Init(this);
        }

        public virtual IPoolObject InstantiateObject() {
            tempPoolObject = Instantiate(prefab, parent).GetComponent<IPoolObject>();
            if(tempPoolObject != null) {
                InitObject(tempPoolObject);
                objects.Add(tempPoolObject);
            }
            return tempPoolObject;
        }

        public virtual void InstantiateObjects(int count) {
            for(int i = 0; i < count; i++) {
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

            return ReturnIPoolobject(InstantiateObject());


            static IPoolObject ReturnIPoolobject(IPoolObject poolObject) { //Local Function
                poolObject.isBusy = true;
                return poolObject;
            }
        }

        public virtual void SetActiveAllObjects(bool isActive, bool isMakeAllFree) {
            objects.Act(obj => obj.SetActivePoolObject(isActive));
            if(isMakeAllFree) {
                objects.Act(obj => obj.isBusy = false);
            }
        }

        public virtual IEnumerator SetActiveAllObjects_Cor(bool isActive, bool isMakeAllFree, float delay = 0f) {
            yield return new WaitForSeconds(delay);
            SetActiveAllObjects(isActive, isMakeAllFree);
        }
    }


    public interface IPoolObject {
        public bool isBusy { get; set; }
        public void SetActivePoolObject(bool isActive);
        public void Init(Pool pool);
    }
}