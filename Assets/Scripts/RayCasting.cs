using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject sniperRifle;
    private Camera _camera;
    private bool isRifleEquiped;

    void Start()
    {
        _camera = GetComponent<Camera>();
        isRifleEquiped = false;
        sniperRifle.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    if (hitObject.tag == "VulnerableToPistol" && !isRifleEquiped)
                    {
                        target.ReactToHit();
                    }
                    else if(hitObject.tag == "VulnerableToRifle" && isRifleEquiped)
                    {
                        target.ReactToHit();
                    }
                }
                else StartCoroutine(SphereIndicator(hit.point));
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isRifleEquiped = !isRifleEquiped;
            sniperRifle.SetActive(isRifleEquiped);
            pistol.SetActive(!isRifleEquiped);
        }
        if (Input.GetMouseButton(1) && isRifleEquiped)
        {
            _camera.fieldOfView = 30;
        }
        else
        {
            _camera.fieldOfView = 60;
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }
    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 4;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
