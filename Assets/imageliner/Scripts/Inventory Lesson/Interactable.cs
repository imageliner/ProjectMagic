using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected GameObject interactCanvas;
    protected bool inInteractRange;

    public UnityEvent interactEvent;

    protected virtual void Start()
    {
        interactCanvas.SetActive(false);
        inInteractRange = false;
    }

    public virtual void OnInteract()
    {
        interactEvent?.Invoke();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactCanvas.SetActive(true);
            inInteractRange = true;
            other.GetComponent<PlayerInputHandler>().interactableList.Add(this);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactCanvas.SetActive(false);
            inInteractRange = false;
            other.GetComponent<PlayerInputHandler>().interactableList.Remove(this);
        }
    }
}
