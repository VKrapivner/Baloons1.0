using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonkeyDragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject monkeyPrefab;
    private GameObject draggingIcon;
    private RectTransform draggingPlane;
    private Canvas canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)//Creates a visual representation(icon) of the monkey that follows the cursor
    {
        draggingIcon = new GameObject("DraggingIcon"); //Referens till??
        draggingIcon.transform.SetParent(canvas.transform, false);
        draggingIcon.transform.SetAsLastSibling();

        var image = draggingIcon.AddComponent<Image>();
        image.sprite = GetComponent<Image>().sprite;
        image.SetNativeSize();

        var group = draggingIcon.AddComponent<CanvasGroup>();
        group.alpha = 0.6f;

        draggingPlane = canvas.transform as RectTransform; //Set the dragging plane

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)//Updates the position of the dragging icon to follow the mouse
    {
        //PlaceMonkeyInWorld(eventData);

        if (draggingIcon != null)
        {
            SetDraggedPosition(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)//Converts the screen position to a world position and instantiates the monkey prefab in the game world
    {
        Debug.Log("OnEndDrag called");

        if (draggingIcon != null) 
            Destroy(draggingIcon);

        PlaceMonkeyInWorld(eventData);

    }

    private void SetDraggedPosition(PointerEventData eventData)//Helper method to position the dragging icon
    {
        Vector2 globalMousePos; //Vector3?

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(draggingPlane, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            //draggingIcon.transform.position = globalMousePos;
            draggingIcon.transform.position = canvas.transform.TransformPoint(globalMousePos);

        }
    }

    //private void PlaceMonkeyInWorld(PointerEventData eventData)
    //{
    //    Debug.Log("PlaceMonkeyInWorld called");

    //    Vector3 worldPosition = Camera.main.ScreenToViewportPoint(eventData.position);
    //    worldPosition.z = 0f; //Layer?

    //    Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
    //    if (hitCollider != null && hitCollider.gameObject.CompareTag("GameField"))
    //    {
    //        // Instantiate the monkey at the world position
    //        Instantiate(monkeyPrefab, worldPosition, Quaternion.identity);
    //    }
    //    else
    //    {
    //        Debug.Log("Cannot place monkey outside the game field.");
    //    }
    //}

    private void PlaceMonkeyInWorld(PointerEventData eventData)
    {
        Debug.Log("PlaceMonkeyInWorld called");

        Camera cam = eventData.pressEventCamera != null ? eventData.pressEventCamera : Camera.main;

        float zDistance = 0f - cam.transform.position.z;//get the distance from the camera to the game plane(usually z = 0)

        Vector3 screenPosition = new Vector3(eventData.position.x, eventData.position.y, zDistance);//create a screen position with the correct z value

        Vector3 worldPosition = cam.ScreenToWorldPoint(screenPosition);//convert the screen position to a world position

        Debug.Log("World Position: " + worldPosition);

        Instantiate(monkeyPrefab, worldPosition, Quaternion.identity);
        Debug.Log("Camera Position: " + cam.transform.position);
        Debug.Log("Screen Position: " + screenPosition);
        Debug.Log("World Position: " + worldPosition);


    }
}
