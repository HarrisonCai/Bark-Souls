using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.AI.Navigation;
public class MazeGenerator : MonoBehaviour
{
    public NavMeshSurface surface;
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;
    public int mult;

    private MazeCell[,] _mazeGrid;
    public GameObject archer, melee;
    void Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(mult*x, 0, mult*z), Quaternion.identity);
                if (!((x>10 && 10<20)&&(z>10 && z<20))&&Random.Range(0, 10) < 0.7f)
                {

                    Instantiate(archer, new Vector3(x * mult, 0.2f, z * mult), Quaternion.identity);
                    Instantiate(melee, new Vector3(x * mult, 0.2f, z * mult), Quaternion.identity);
                }
            }
        }
        _mazeGrid[0, 0].ClearBackWall();
        GenerateMaze(null, _mazeGrid[0, 0]);
        
        for (int x=11; x <= 19; x++)
        {
            for(int z = 11; z <= 19; z++)
            {
                _mazeGrid[x, z].ClearBackWall();
                _mazeGrid[x, z].ClearFrontWall();
                _mazeGrid[x, z].ClearRightWall();
                _mazeGrid[x, z].ClearLeftWall();
            }
        }
        surface.BuildNavMesh();
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);

            }
      
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x/mult;
        int z = (int)currentCell.transform.position.z/mult;

        if (x + 1 < _mazeWidth )
        {
            var cellToRight = _mazeGrid[x + 1, z];

            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];

            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];

            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {

            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {


            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
      
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
           
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

}
