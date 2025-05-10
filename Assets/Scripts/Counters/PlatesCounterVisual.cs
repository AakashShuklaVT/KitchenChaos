using UnityEngine;
using System.Collections.Generic;


public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameobjectList;

    private void Awake() {
        plateVisualGameobjectList = new List<GameObject>();
    }
    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned() {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameobjectList.Count, 0);
        plateVisualGameobjectList.Add(plateVisualTransform.gameObject);
    }

    private void PlatesCounter_OnPlateRemoved() {
        GameObject plateGameobject = plateVisualGameobjectList[plateVisualGameobjectList.Count - 1];
        plateVisualGameobjectList.Remove(plateGameobject);
        Destroy(plateGameobject);
    }
}
