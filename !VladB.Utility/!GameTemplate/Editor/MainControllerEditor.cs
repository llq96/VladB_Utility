using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VladB.GameTemplate;
using VladB.Utility;

[CustomEditor(typeof(MainController))]
public class MainControllerEditor : Editor {
    MainController script => target as MainController;

    bool isAnyListModified;

    public override void OnInspectorGUI() {
        serializedObject.Update();

        //Update Data
        Update_ExecutionOrder_ByState();

        //Drawing
        EditorGUILayout.PropertyField(serializedObject.FindProperty("controllersObjects"), true);

        GUIContent label = new GUIContent($"Lists By State {(isAnyListModified ? "(MODIFIED)" : "")}");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("listsByState"), label, true);

        if(GUILayout.Button("Reset listsByState")) {
            script.listsByState.Clear();
            OnInspectorGUI();
        }


        serializedObject.ApplyModifiedProperties();
        //base.OnInspectorGUI();
    }

    public void Update_ExecutionOrder_ByState() {
        //Удаляет null объекты, повторы и не именющие IController объекты
        script.controllersObjects = script.controllersObjects.Where(go => go != null)
            .Where(go => go.GetComponent<IController>() != null)
            .Distinct().ToList();

        //Удаляет повторы списоков с одинаковым GameStateEnum
        script.listsByState = script.listsByState.GroupBy(x => x.gameState).Select(gr => gr.First()).ToList();


        int enums = Enum.GetNames(typeof(GameStateEnum)).Length;
        for(int i = 0; i < enums; i++) {
            // Добавляет новый список если не существовало списка с таким GameStateEnum
            if(!script.listsByState.Exists(x => x.gameState == (GameStateEnum)i)) {
                script.listsByState.Add(new ListOfControllers((GameStateEnum)i));
            }

            // Список с нужным GameStateEnum
            ListOfControllers innerList = script.listsByState.Where(x => x.gameState == (GameStateEnum)i).First();

            // Добавляет объекты из главного списка в список с конкретным GameStateEnum, если таких объектов там нет
            script.controllersObjects.Where(go => go != null)
                .Where(go => !innerList.controllersObjects.Contains(go))
                .Act(go => innerList.controllersObjects.Add(go));
        }



        // Удаляет null объекты, и не имеющиеся в главном списке контроллеры
        for(int i = 0; i < script.listsByState.Count; i++) {
            script.listsByState[i].controllersObjects = script.listsByState[i].controllersObjects
                .Where(go => go != null)
                .Where(go => script.controllersObjects.Contains(go))
                .Distinct()
                .ToList();
        }

        isAnyListModified = false;
        // Добавляет приписку "(MODIFIED)" в редакторе если списки не абсолютно одинаковы
        for(int i = 0; i < script.listsByState.Count; i++) {
            bool isModified = script.listsByState[i].controllersObjects.SequenceEqual(script.controllersObjects) == false;
            script.listsByState[i].UpdateEditorName(isModified);
            isAnyListModified = isModified || isAnyListModified;
        }
    }
}


