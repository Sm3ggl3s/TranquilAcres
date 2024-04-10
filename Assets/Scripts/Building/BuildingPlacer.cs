using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
    public static BuildingPlacer Instance;

    //Layer
    public LayerMask groundLayer;

    //Building
    private GameObject _buildingPrefab;
    private GameObject _toBuild;

    //Camera
    private Camera _mainCamera;

    // Raycast
    private Ray _ray;
    private RaycastHit _hit;

    private void Awake() {
        _mainCamera = Camera.main;
        _buildingPrefab = null;
    }

    private void Update() {
        if (_buildingPrefab != null) {

            if (EventSystem.current.IsPointerOverGameObject()) {
                if (_toBuild.activeSelf == true) {
                    _toBuild.SetActive(false);
                }
            } else if (_toBuild.activeSelf == false) {
                _toBuild.SetActive(true);
            }

            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 1000f, groundLayer)) {
                if (_toBuild.activeSelf == false) {
                    _toBuild.SetActive(true);
                }
                _toBuild.transform.position = _hit.point;

                // Left Click on Mouse place building
                if (Input.GetMouseButtonDown(0)) {
                    BuildingManager m = _toBuild.GetComponent<BuildingManager>();
                    m.SetPlacementMode(PlacementMode.Fixed);

                    _buildingPrefab = null;
                    _toBuild = null;
                }
            } else if (_toBuild.activeSelf == true) {
                _toBuild.SetActive(false);
            }
        }
    }

    public void SetBuildingPrefab(GameObject building) {
        _buildingPrefab = building;
        PrepareBuilding();
    }   

    private void PrepareBuilding() {
        if (_toBuild != null) {
            Destroy(_toBuild);
        }

        _toBuild = Instantiate(_buildingPrefab);
        _toBuild.SetActive(false);

        BuildingManager m = _toBuild.GetComponent<BuildingManager>();

        m.isFixed = false;
        m.SetPlacementMode(PlacementMode.Valid);
    }
}
