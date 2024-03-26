using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityEditorSnapToGrid : MonoBehaviour
{
    [SerializeField] bool xAxis = true, yAxis = true, zAxis = true;
    [SerializeField] Vector3 positioningStep = new Vector3(1f, 1f, 1f),
                             positioningOffset = new Vector3(0.5f, 0.5f, 0.5f),
                             positionOffset = new Vector3(0f, 0f, 0f),
                             scalingStep = new Vector3(1f, 1f, 1f);

    bool isPositioned = false;

    void SnapToGrid()
    {
        Vector3 pos;
        if (!isPositioned)
        {
            isPositioned = true;
            pos = transform.position;
            transform.position = new Vector3(
                pos.x + positionOffset.x,
                pos.y + positionOffset.y,
                pos.z + positionOffset.z
            );
        }

        pos = transform.position;
        transform.position = new Vector3(
            xAxis ? Mathf.Round((pos.x - positioningOffset.x) / positioningStep.x) * positioningStep.x + positioningOffset.x : pos.x,
            yAxis ? Mathf.Round((pos.y - positioningOffset.y) / positioningStep.y) * positioningStep.y + positioningOffset.y : pos.y,
            zAxis ? Mathf.Round((pos.z - positioningOffset.z) / positioningStep.z) * positioningStep.z + positioningOffset.z : pos.z
        );

        scalingStep.x = Mathf.Min(1f, scalingStep.x);
        scalingStep.y = Mathf.Min(1f, scalingStep.y);
        scalingStep.z = Mathf.Min(1f, scalingStep.z);

        Vector3 localScale = transform.localScale;
        transform.localScale = new Vector3(
            Mathf.Round(localScale.x / scalingStep.x) * scalingStep.x,
            Mathf.Round(localScale.y / scalingStep.y) * scalingStep.y,
            Mathf.Round(localScale.z / scalingStep.z) * scalingStep.z
        );
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        SnapToGrid();
    }
#endif
}
