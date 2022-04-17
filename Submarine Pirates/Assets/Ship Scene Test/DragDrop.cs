using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class DragDrop : MonoBehaviour
{

    [SerializeField]
    private InputAction mouseClick;
    [SerializeField]
    private float mouseDragPhysicsSpeed = 0;
    [SerializeField]
    private float mouseDragSpeed = 0.1f;
    
    private Camera mainCamera;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    private Vector3 velocity = Vector3.zero;

    private void Awake() {
        mainCamera = Camera.main;
    }

    private void OnEnable() {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable() {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context) {
        
        // Send a Ray to the mouse location
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        // 3D objects
        RaycastHit hit;
        // objects need colliders
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider != null &&(hit.collider.gameObject.CompareTag("Draggable") || hit.collider.
                gameObject.layer == LayerMask.NameToLayer("Draggable") || hit.collider.gameObject.
                GetComponent<IDrag>() != null)) 
            {
                //start the dragging
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }

        // 2D object
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        if (hit2D.collider != null &&(hit2D.collider.gameObject.CompareTag("Draggable") || hit2D.collider.
                gameObject.layer == LayerMask.NameToLayer("Draggable") || hit2D.collider.gameObject.
                GetComponent<IDrag>() != null)) 
        {
            //Debug.Log("Object: " + hit2D.collider.gameObject.name);
            //start the dragging

            hit2D.collider.gameObject.tag = "Dragging";
            StartCoroutine(DragUpdate(hit2D.collider.gameObject));
        }

    }

    private IEnumerator DragUpdate(GameObject clickedObject) {

        float initialDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position);
        
        //grab the following components if the object has them.
        clickedObject.TryGetComponent<Rigidbody2D>(out var rb);
        // IDrag is a custom component to make custom physics.
        clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
        iDragComponent?.onStartDrag();

        while (mouseClick.ReadValue<float>() != 0) {
            //move the object to the current location on screen.
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (rb != null) {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.velocity = direction * mouseDragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            else {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.
                    GetPoint(initialDistance), ref velocity, mouseDragPhysicsSpeed);
                yield return null;
            }
        }
        clickedObject.tag = "Draggable";
        iDragComponent?.onEndDrag();
    }
}
