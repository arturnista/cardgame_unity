﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{

    [SerializeField] private GameObject _mousePosition = default;
    private Grid _grid;
    private bool _isMouseInside;

    void Awake()
    {
        DI.Set<MapController>(this);
        _grid = GetComponent<Grid>();
        _isMouseInside = true;
    }

    void Update()
    {
        if (_isMouseInside)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mousePosition.transform.position = WorldToCell(mousePosition);
        }
    }

    void OnMouseEnter()
    {
        _isMouseInside = true;
    }

    void OnMouseExit()
    {
        _isMouseInside = false;
    }

    // void OnMouseDown()
    // {
    //     Debug.Log("OnMouseDown");
    //     Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     DI.Get<GameController>().SelectPoint(WorldToCell(mousePosition));
    // }

    public Vector3 WorldBounds()
    {
        return GetComponent<BoxCollider2D>().size;
    }

    public Vector3 WorldToCell(Vector3 position)
    {
        return _grid.GetCellCenterLocal(_grid.WorldToCell(position));
    }

}
