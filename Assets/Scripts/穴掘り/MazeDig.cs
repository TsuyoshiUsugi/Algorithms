using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 穴掘り法のスクリプト
/// </summary>
public class MazeDig : MonoBehaviour
{
    [SerializeField] GameObject _tile;

    int[] _startPos = new int[2] {5, 5};
        
    MazeTile[,] _maze;
    int _width = 11;
    int _height = 11;

    enum Direction
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    List<Cell> _startCells = new List<Cell>();

    public void Start()
    {
        //高さか幅が5以下だとうまく生成できないので例外を返す
        if (_width < 5 || _height < 5) throw new ArgumentOutOfRangeException();

        //偶数だとうまく生成されないので奇数にする
        if (_width % 2 == 0) _width++;
        if (_height % 2 == 0) _height++;

        _maze = new MazeTile[_width, _height];

        CreateMaze();
    }

    void CreateMaze()
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                //周囲を壁にしたいので最初は周囲を道にする
                if (x == 0 || y == 0 || x == _width - 1 || y == _height - 1)
                {
                    var tile = Instantiate(_tile);
                    MazeTile mazeTile = tile.GetComponent<MazeTile>();
                    mazeTile.SetTile(x, y, TileType.Road);
                    _maze[x, y] = mazeTile;
                }
                //それ以外は壁にする
                else
                {
                    var tile = Instantiate(_tile);
                    MazeTile mazeTile = tile.GetComponent<MazeTile>();
                    mazeTile.SetTile(x, y, TileType.Wall);
                    _maze[x, y] = mazeTile;
                }
            }
        }

        Dig(_startPos[0], _startPos[1]);

        //今のままでは周囲の壁の部分が通路なので壁にする
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (x == 0 || y == 0 || x == _width - 1 || y == _height - 1)
                {
                    _maze[x, y].SetTileType(TileType.Wall);
                }
            }
        }
    }

    /// <summary>
    /// 実際に壁を掘っていく関数
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void Dig(int x, int y)
    {

        while (true)
        {
            //掘れる方向を格納する
            List<Direction> directions = new ();

            //各方向の2マスがどちらも壁なら掘る
            if (_maze[x, y - 1].GetTileType == TileType.Wall && _maze[x, y - 2].GetTileType == TileType.Wall)
                directions.Add(Direction.UP);
            if (_maze[x + 1, y].GetTileType == TileType.Wall && _maze[x + 2, y].GetTileType == TileType.Wall)
                directions.Add(Direction.RIGHT);
            if (_maze[x, y + 1].GetTileType == TileType.Wall && _maze[x, y + 2].GetTileType == TileType.Wall)
                directions.Add(Direction.DOWN);
            if (_maze[x - 1, y].GetTileType == TileType.Wall && _maze[x - 2, y].GetTileType == TileType.Wall)
                directions.Add(Direction.LEFT);

            //掘れる壁がないなら抜ける
            if (directions.Count == 0)
            {
                return;
            }

            SetRoad(x, y);

            //次に掘る方向をランダムに決める
            int directionIndex = UnityEngine.Random.Range(0, directions.Count);

            switch (directions[directionIndex])
            {
                case Direction.UP:
                    SetRoad(x, y--);
                    SetRoad(x, y--);
                    break;
                case Direction.RIGHT:
                    SetRoad(x++, y);
                    SetRoad(x++, y);
                    break;
                case Direction.DOWN:
                    SetRoad(x, y++);
                    SetRoad(x, y++);
                    break;
                case Direction.LEFT:
                    SetRoad(x--, y);
                    SetRoad(x--, y);
                    break;
            }

            Cell cell = GetStartCell();

            if (cell != null)
            {
                Dig(cell.X, cell.Y);
            }
        }
    }

    /// <summary>
    /// 道を生成する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void SetRoad(int x, int y)
    {
        _maze[x, y].SetTileType(TileType.Road);

        //座標が縦横ともに奇数なら次の起点候補に追加する。
        if (x % 2 == 1 && y % 2 == 1)
        {
            _startCells.Add(new Cell() { X = x, Y = y });
        }
    }

    Cell GetStartCell()
    {
        if (_startCells.Count == 0) return null;

        int index = UnityEngine.Random.Range(0, _startCells.Count);
        Cell cell = _startCells[index];
        _startCells.RemoveAt(index);
        return cell;
    }
}

public class Cell
{
    public int X, Y;
}