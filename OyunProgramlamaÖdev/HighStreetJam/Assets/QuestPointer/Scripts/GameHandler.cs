/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {

    [SerializeField] private Window_QuestPointer windowQuestPointer;

    private void Start() {
        Window_QuestPointer.QuestPointer questPointer_1 = windowQuestPointer.CreatePointer(new Vector3(200, 45));
        FunctionUpdater.Create(() => {
            if (Vector3.Distance(Camera.main.transform.position, new Vector3(200, 45)) < 40f) {
                windowQuestPointer.DestroyPointer(questPointer_1);
                return true;
            } else {
                return false;
            }
        });
        
        Window_QuestPointer.QuestPointer questPointer_2 = windowQuestPointer.CreatePointer(new Vector3(190, -32));
        FunctionUpdater.Create(() => {
            if (Vector3.Distance(Camera.main.transform.position, new Vector3(190, -32)) < 40f) {
                windowQuestPointer.DestroyPointer(questPointer_2);
                return true;
            } else {
                return false;
            }
        });
        
        Window_QuestPointer.QuestPointer questPointer_3 = windowQuestPointer.CreatePointer(new Vector3(-70, 200));
        FunctionUpdater.Create(() => {
            if (Vector3.Distance(Camera.main.transform.position, new Vector3(-70, 200)) < 40f) {
                windowQuestPointer.DestroyPointer(questPointer_3);
                return true;
            } else {
                return false;
            }
        });
    }
}
*/