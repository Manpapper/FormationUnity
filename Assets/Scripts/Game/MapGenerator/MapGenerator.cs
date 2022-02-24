using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Map Config")]
    [SerializeField] int height = 0;
    [SerializeField] int width = 0;
    [SerializeField] Tilemap floorMap = null;
    [SerializeField] Tile[] dirtTiles = null;
    [SerializeField] Tile[] dirtOnlyTiles = null;
    [SerializeField] Tile[] grassTiles = null;

    IDictionary<int, int> tileEquivalentStorage = new Dictionary<int, int> {
        { 1, 1 },
        { 2, 11 },
        { 3, 1 },
        { 4, 3 },
        { 5, 2 },
        { 6, 3 },
        { 7, 2 },
        { 8, 0 },
        { 12, 3 },
        { 14, 3 },
        { 15, 2 },
        { 16, 5 },
        { 20, 4 },
        { 24, 5 },
        { 28, 4 },
        { 30, 4 },
        { 32, 9 },
        { 48, 5 },
        { 56, 5 },
        { 58, 4 },
        { 60, 4 },
        { 62, 4 },
        { 64, 7 },
        { 65, 8 },
        { 80, 6 },
        { 96, 7 },
        { 112, 6 },
        { 120, 6 },
        { 128, 10 },
        { 129, 1 },
        { 131, 1 },
        { 132, 1 },
        { 135, 2 },
        { 143, 2 },
        { 192, 7 },
        { 193, 8 },
        { 195, 8 },
        { 224, 7 },
        { 225, 8 },
        { 227, 8 },
        { 240, 6 },
        { 248, 6 },
   };

    private IDictionary<int, int> erreurs = new Dictionary<int, int>();

    private FloorType[,] typeOfGround;

    private enum FloorType
    {
        DIRT,
        GRASS
    }

    private enum DirectionCardinaux
    {
        NORTH = 1,
        EAST = 4,
        SOUTH = 8,
        WEST = 16
    }

    private enum Direction
    {
        NORTH = 1,
        NORTH_EAST = 2,
        EAST = 4,
        SOUTH_EAST = 8,
        SOUTH = 16,
        SOUTH_WEST = 32,
        WEST = 64,
        NORTH_WEST = 128
    }

    //private int[,] tFloor = new int[width, height];
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Init()
    {
        typeOfGround = new FloorType[width, height];

        InitGridWithFloorType(FloorType.GRASS);

        GenerateAlley(FloorType.DIRT);

        Vector3Int currentCell = floorMap.WorldToCell(transform.position);

        currentCell.x -= width / 2;
        currentCell.y += height / 2;
        floorMap.SetTile(currentCell, dirtOnlyTiles[0]);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (typeOfGround[x, y] == FloorType.DIRT)
                {
                    FloorType _north_tile = typeOfGround[x, (y - 1 < 0) ? 0 : y - 1];
                    FloorType _north_east_tile = typeOfGround[((x + 1 >= width - 1) ? width - 1 : x + 1), ((y - 1 < 0) ? 0 : y - 1)];
                    FloorType _east_tile = typeOfGround[((x + 1 >= width - 1) ? width - 1 : x + 1), y];
                    FloorType _south_east_tile = typeOfGround[((x + 1 >= width - 1) ? width - 1 : x + 1), ((y + 1 >= height - 1) ? height - 1 : y + 1)];
                    FloorType _south_tile = typeOfGround[x, ((y + 1 >= height - 1) ? height - 1 : y + 1)];
                    FloorType _south_west_tile = typeOfGround[((x - 1 < 0) ? 0 : x - 1), ((y + 1 >= height - 1) ? height - 1 : y + 1)];
                    FloorType _west_tile = typeOfGround[((x - 1 < 0) ? 0 : x - 1), y];
                    FloorType _north_west_tile = typeOfGround[((x - 1 < 0) ? 0 : x - 1), ((y - 1 < 0) ? 0 : y - 1)];

                    int tileCalculatedNumber = ((int)Direction.NORTH) * ((int)_north_tile) + ((int)Direction.NORTH_EAST) * ((int)_north_east_tile) + ((int)Direction.EAST) * ((int)_east_tile) + ((int)Direction.SOUTH_EAST) * ((int)_south_east_tile) + ((int)Direction.SOUTH) * ((int)_south_tile) + ((int)Direction.SOUTH_WEST) * ((int)_south_west_tile) + ((int)Direction.WEST) * ((int)_west_tile) + ((int)Direction.NORTH_WEST) * ((int)_north_west_tile);

                    if (tileEquivalentStorage.ContainsKey(tileCalculatedNumber))
                    {
                        floorMap.SetTile(currentCell, dirtTiles[tileEquivalentStorage[tileCalculatedNumber]]);
                    }
                    else
                    {
                        floorMap.SetTile(currentCell, getRandomTile(dirtOnlyTiles));
                    }
                }
                else
                {
                    floorMap.SetTile(currentCell, getRandomTile(grassTiles));
                }

                currentCell.x += 1;
            }
            currentCell.x -= width;
            currentCell.y -= 1;
        }

    }

    private void InitGridWithFloorType(FloorType floorType)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                typeOfGround[x, y] = floorType;
            }
        }
    }

    private void GenerateAlley(FloorType floorType)
    {
        List<DirectionCardinaux> directionList = new List<DirectionCardinaux>();
        initDirectionList(directionList);

        DirectionCardinaux startAlley = directionList.ToArray()[Random.Range(0, directionList.Count)];
        directionList.Remove(startAlley);

        //Direction endAlley = directionList.ToArray()[Random.Range(0, directionList.Count)];

        if (startAlley == DirectionCardinaux.NORTH || startAlley == DirectionCardinaux.SOUTH)
        {
            int randomStartWidth = Random.Range(2, width - 2);
            int randomEndWidth = Random.Range(2, width - 2);

            if (startAlley == DirectionCardinaux.NORTH)
            {
                updateTileMapForAlley(randomStartWidth, randomEndWidth, 0, height - 1);
            }
            else if (startAlley == DirectionCardinaux.SOUTH)
            {
                updateTileMapForAlley(randomStartWidth, randomEndWidth, height - 1, 0);
            }
        }
        else if (startAlley == DirectionCardinaux.WEST || startAlley == DirectionCardinaux.EAST)
        {
            int randomStartHeight = Random.Range(2, height - 2);
            int randomEndHeight = Random.Range(2, height - 2);

            if (startAlley == DirectionCardinaux.WEST)
            {
                updateTileMapForAlley(0, width - 1, randomStartHeight, randomEndHeight);
            }
            else if (startAlley == DirectionCardinaux.EAST)
            {
                updateTileMapForAlley(width - 1, 0, randomStartHeight, randomEndHeight);
            }
        }

    }

    void updateTileMapForAlley(int xStart, int xEnd, int yStart, int yEnd)
    {
        if (yStart == 0)
        {
            for (int j = yStart; j < yEnd; j++)
            {
                xStart = updateVertical(xStart, xEnd, j);
            }
        }
        else if (yEnd == 0)
        {
            for (int j = yStart; yEnd < j; j--)
            {
                xStart = updateVertical(xStart, xEnd, j);
            }
        }
        else if (xStart == 0)
        {
            for (int i = 0; i < xEnd; i++)
            {
                yStart = updateHorizontal(yStart, yEnd, i);
            }
        }
        else if (xEnd == 0)
        {
            for (int i = xStart; xEnd < i; i--)
            {
                yStart = updateHorizontal(yStart, yEnd, i);
            }
        }

    }
    private int updateHorizontal(int yStart, int yEnd, int i)
    {
        updateSurroundingCells(i, yStart, FloorType.DIRT);

        if (yStart != yEnd && i == width / 2)
        {
            if (yStart < yEnd)
            {
                for (int j = yStart; j != yEnd; j++)
                {
                    updateSurroundingCells(i, j, FloorType.DIRT);
                    yStart = j;
                }
            }
            else if (yStart > yEnd)
            {
                for (int j = yStart; j != yEnd; j--)
                {
                    updateSurroundingCells(i, j, FloorType.DIRT);
                    yStart = j;
                }
            }
        }
        return yStart;
    }

    private int updateVertical(int xStart, int xEnd, int j)
    {
        updateSurroundingCells(xStart, j, FloorType.DIRT);

        if (xStart != xEnd && j == height / 2)
        {
            if (xStart < xEnd)
            {
                for (int i = xStart; i < xEnd + 1; i++)
                {
                    updateSurroundingCells(i, j, FloorType.DIRT);
                    xStart = i;
                }
            }
            else if (xStart > xEnd)
            {
                for (int i = xStart; xEnd < i; i--)
                {
                    updateSurroundingCells(i, j, FloorType.DIRT);
                    xStart = i;
                }
            }
        }
        return xStart;
    }

    void updateSurroundingCells(int i, int j, FloorType floorType)
    {
        typeOfGround[i, (j - 1 < 0) ? 0 : j - 1] = floorType;
        typeOfGround[((i + 1 >= width - 1) ? width - 1 : i + 1), ((j - 1 < 0) ? 0 : j - 1)] = floorType;
        typeOfGround[((i + 1 >= width - 1) ? width - 1 : i + 1), j] = floorType;
        typeOfGround[((i + 1 >= width - 1) ? width - 1 : i + 1), ((j + 1 >= height - 1) ? height - 1 : j + 1)] = floorType;
        typeOfGround[i, ((j + 1 >= height - 1) ? height - 1 : j + 1)] = floorType;
        typeOfGround[((i - 1 < 0) ? 0 : i - 1), ((j + 1 >= height - 1) ? height - 1 : j + 1)] = floorType;
        typeOfGround[((i - 1 < 0) ? 0 : i - 1), j] = floorType;
        typeOfGround[((i - 1 < 0) ? 0 : i - 1), ((j - 1 < 0) ? 0 : j - 1)] = floorType;
        typeOfGround[i, j] = floorType;
    }

    List<DirectionCardinaux> initDirectionList(List<DirectionCardinaux> list)
    {
        foreach (DirectionCardinaux direction in DirectionCardinaux.GetValues(typeof(DirectionCardinaux)))
        {
            list.Add(direction);
        }
        return list;
    }


    private Tile getRandomTile(Tile[] tiles)
    {
        return tiles[Random.Range(0, tiles.Length)];
    }
}
