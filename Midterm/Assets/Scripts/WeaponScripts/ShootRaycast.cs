using System.Collections;
using UnityEngine;

public class ShootRaycast : MonoBehaviour, ShootMode
{
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackRoutine() {
        Vector3 point = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0);
        Ray ray = cam.ScreenPointToRay(point);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Debug.Log("Hit something");
            GameObject hitObject = hit.transform.gameObject;
            /*ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

            if (target != null) {
                target.ReactToHit();
            }
            else {*/
                StartCoroutine(SphereIndicator(hit.point));
            //}
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos) {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        sphere.GetComponent<SphereCollider>().isTrigger = true;


        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }
}
