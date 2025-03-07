using UnityEngine;
using UnityEngine.UI;

public class DropdownModeSwitch : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject[] gameObjects;

    private void Start()
    {
        Debug.Log($"dropdown target objects count : {gameObjects.Length}");
        dropdown.onValueChanged.AddListener(selected =>
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].SetActive(selected == i);
            }

            Debug.Log($"dropdown changed: {selected}");
        });
    }
}