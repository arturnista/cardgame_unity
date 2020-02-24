using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{

    [SerializeField] private GameObject _mousePosition = default;
    private Grid _grid;

    private Tilemap _groundTilemap;
    private BoundsInt _groundBounds;
    private List<Vector3Int> _allTiles;

    private bool _isMouseInside;

    void Awake()
    {
        DI.Set<MapController>(this);
        _grid = GetComponent<Grid>();
        _groundTilemap = _grid.transform.Find("Ground").GetComponent<Tilemap>();
        CreateBounds();
        _isMouseInside = true;
    }

    void CreateBounds()
    {
        _allTiles = new List<Vector3Int>();
        _groundBounds = new BoundsInt();
        Vector3Int lastTilePosition = Vector3Int.zero;
        bool minDefined = false;

        BoundsInt bounds = _groundTilemap.cellBounds;
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = _groundTilemap.GetTile(tilePosition);
                
                if (tile != null)
                {
                    if (!minDefined)
                    {
                        _groundBounds.min = tilePosition;
                        minDefined = true;
                    }
                    lastTilePosition = tilePosition;
                    _allTiles.Add(tilePosition);
                }
            }
        }

        _groundBounds.max = lastTilePosition;

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

    public BoundsInt WorldBounds()
    {
        return _groundBounds;
    }

    public List<Vector3Int> GetAllGroundTiles()
    {
        return _allTiles;
    }

    public bool IsPositionInsideWorld(Vector3 position)
    {
        Bounds bound = _groundTilemap.localBounds;
        return bound.Contains(position);
    }

    public Vector3 WorldToCell(Vector3 position)
    {
        Vector3 cellPosition = _grid.GetCellCenterLocal(_grid.WorldToCell(position));
        if (IsPositionInsideWorld(cellPosition))
        {
            return cellPosition;
        }
        else
        {
            return Vector3.zero;
        }
    }

}
