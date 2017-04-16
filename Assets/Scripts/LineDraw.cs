using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineDraw : MonoBehaviour {

    public static List<Vector3> lineQueue;

    // Use this for initialization
    void Awake() {
        lineQueue = new List<Vector3>();
    }

    private void OnGUI() {
        while (lineQueue.Count > 0) {
            Debug.Log("DRAW");
            Drawing.DrawLine(GameManager.Instance.ActiveBuilding.transform.position, lineQueue[0], GameManager.Instance.ActiveBuilding.color,10);
            lineQueue.RemoveAt(0);
        }
    }
}
