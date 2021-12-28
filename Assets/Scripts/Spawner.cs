using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float ELEMENT_WIDTH = 1.76f;
    private const float ELEMENT_HEIGHT = 0.52f;
    private const int GRID_SIZE = 3;
    private const float OFFSET = 1.5f;

    [SerializeField] private GameObject _element;
    [SerializeField] private Transform _wrapper;

    private List<int> _bricks;
    public List<int> Bricks => _bricks;

    private void Awake()
    {
        Dispatcher.OnScoreAdd += RemoveBrick;
    }

    public void Generation()
    {
        var brickId = 0;
        _bricks = new List<int>();
        
        float gridWidth = GRID_SIZE * ELEMENT_WIDTH;
        float gridHeight = GRID_SIZE * ELEMENT_HEIGHT;
        float minX = -gridWidth / 2 + ELEMENT_WIDTH / 2;
        float maxY = gridHeight / 2 - ELEMENT_HEIGHT / 2 + OFFSET;

        var gridSizeX = GRID_SIZE;

        for (var y = 0; y < GRID_SIZE; y++)
        {
            for (var x = 0; x < gridSizeX; x++)
            {
                var position = new Vector2(minX + x * ELEMENT_WIDTH, maxY - y * ELEMENT_HEIGHT);
                Instantiate(_element, position, Quaternion.identity, _wrapper);
                _bricks.Add(brickId);
                brickId++;
            }
            
            minX += ELEMENT_WIDTH / 2;
            gridSizeX--;
        }
    }

    private void RemoveBrick()
    {
        _bricks.RemoveAt(_bricks.Count - 1);
    }

    private void OnDestroy()
    {
        Dispatcher.OnScoreAdd -= RemoveBrick;
    }
}
