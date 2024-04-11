using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingGridPlacer : BuildingPlacer
{

    public float cellSize;
    public Vector2 gridOffset;

    public Renderer gridRenderer;

#if UNITY_EDITOR
    private void OnValidate() {
        UpdateGridVisual();
    }
#endif

    private void Start() {
        UpdateGridVisual();
        EnableGridVisual(false);
    }

    private void Update() {
        if (_buildingPrefab != null) {

            // Right Click on Mouse exit building mode
            if (Input.GetMouseButtonDown(1)) {
                Destroy(_toBuild);
                _buildingPrefab = null;
                _toBuild = null;
                EnableGridVisual(false);
                return;
            }
            
            // Hides building when mouse is over UI
            if (EventSystem.current.IsPointerOverGameObject()) {
                if (_toBuild.activeSelf) {
                    _toBuild.SetActive(false);
                    return;
                }
            } else if (!_toBuild.activeSelf) {
                _toBuild.SetActive(true);
            }

            // Rotate preview with Spacebar
            if (Input.GetKeyDown(KeyCode.Space)) {
                _toBuild.transform.Rotate(Vector3.up, 90f);
            }

            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 1000f, groundLayer)) {
                if (!_toBuild.activeSelf) {
                    _toBuild.SetActive(true);
                }
                _toBuild.transform.position = ClampToNearest(_hit.point, cellSize);

                // Left Click on Mouse place building
                if (Input.GetMouseButtonDown(0)) {
                    BuildingManager m = _toBuild.GetComponent<BuildingManager>();
                    if (m.hasValidPlacement) {
                        m.SetPlacementMode(PlacementMode.Fixed);
                    
                        // Exit building mode
                        _buildingPrefab = null;
                        _toBuild = null;
                        EnableGridVisual(false);
                    }
                }
            } else if (_toBuild.activeSelf) {
                _toBuild.SetActive(false);
            }
        }
    }

    protected override void PrepareBuilding() {
        base.PrepareBuilding();
        EnableGridVisual(true);
    }

    private Vector3 ClampToNearest(Vector3 pos, float threshold) {
        float t = 1f / threshold;
        Vector3 v = ((Vector3)Vector3Int.FloorToInt(pos * t)) / t;

        // Offset to center of cell
        float s = threshold / 2.0f;
        v.x += s + gridOffset.x;
        v.z += s + gridOffset.y;

        return v;
    }

    private void EnableGridVisual(bool on) {
        if (gridRenderer == null) return;
        gridRenderer.gameObject.SetActive(on);
        print(gridRenderer.gameObject.activeSelf);
    }

    private void UpdateGridVisual() {
        Debug.Log("UpdateGridVisual is called");
        if (gridRenderer == null) return;
        gridRenderer.sharedMaterial.SetVector("_Cell_Size", new Vector4(cellSize, cellSize, 0, 0));
    }
}
