using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldController : MonoBehaviour
{
    
    private static int s_Current = -1;

    [SerializeField] private List<SelectEncounter> _encounters = default;

    Camera _camera;

    void Awake()
    {
        s_Current += 1;
        _camera = Camera.main;
        foreach (var item in _encounters)
        {
            item.Disable();
        }

        _encounters[s_Current].Enable();

    }

}
