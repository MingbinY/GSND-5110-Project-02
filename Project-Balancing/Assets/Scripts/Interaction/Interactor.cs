using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    [SerializeField] UnityEvent onInteractEvent;
    public float interactionDist = 1.5f;
    public LayerMask interactableLayer;
    Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionDist, interactableLayer)){
            Interactable interacable = hit.collider.gameObject.GetComponent<Interactable>();
            if (interacable)
            {
                onInteractEvent = interacable.onInteract;
            }
            else
            {
                onInteractEvent = null;
            }
        }
        else
        {
            onInteractEvent = null;
        }

        if (Input.GetKeyDown(KeyCode.F) && onInteractEvent !=null)
        {
            onInteractEvent.Invoke();
        }
    }
}
