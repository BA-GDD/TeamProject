using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.SceneManagement; //3

public class DeleteMissingScript : Editor
{
    [MenuItem("사용불가능_class삭제/오브잭트_최상위를_선택후_사용")]
    private static void RemoveAllMissingScriptComponents()
    {

        Object[] deepSelectedObjects = EditorUtility.CollectDeepHierarchy(Selection.gameObjects);

        Debug.Log(deepSelectedObjects.Length);

        int componentCount = 0;
        int gameObjectCount = 0;

        foreach (Object obj in deepSelectedObjects)
        {
            if (obj is GameObject go)
            {
                int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);

                //Debug.LogFormat("<color=cyan>{0}</color>", count);

                if (count > 0)
                {
                    Undo.RegisterCompleteObjectUndo(go, "Remove Missing Scripts");

                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);

                    componentCount += count;
                    gameObjectCount++;
                }

            }
        }

    }
}
