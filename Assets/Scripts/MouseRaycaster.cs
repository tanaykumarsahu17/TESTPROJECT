using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseRaycaster : MonoBehaviour
{
    public Camera mainCamera;      // assign your Main Camera in Inspector
    public TextMeshProUGUI infoText; // assign a UI Text (TMP) object

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); //cast a ray from mouse to world
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            TileInfo tile = hit.collider.GetComponent<TileInfo>(); //check if hit object has TileInfo
            if (tile != null)
            {
                infoText.text = tile.GetInfo(); //show tile info on UI
            }
        }
        else
        {
            
            infoText.text = ""; //clear text when not hovering any tile
        }
    }
}

    