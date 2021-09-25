using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerInMeshRenderer : MonoBehaviour
{
    public string sortingLayerName;
    public int sortingOrder;

    private void Start()
    {
        MeshRenderer mesh = this.GetComponent<MeshRenderer>();

        mesh.sortingLayerName = sortingLayerName;
        mesh.sortingOrder = sortingOrder;
    }
}
