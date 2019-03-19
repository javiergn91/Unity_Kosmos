using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileState
    {
        Idle,
        Destination,
        Path
    }

    public Material destMaterial;
    public Material pathMaterial;

    private Material defaultMaterial;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;
    }

    public void SetState(TileState newState)
    {
        switch (newState)
        {
            case TileState.Idle:
                meshRenderer.material = defaultMaterial;
                break;

            case TileState.Destination:
                meshRenderer.material = destMaterial;
                break;

            case TileState.Path:
                meshRenderer.material = pathMaterial;
                break;
        }
    }
}
