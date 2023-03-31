using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���@��@�̃X�N���v�g
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
        //����������5�ȉ����Ƃ��܂������ł��Ȃ��̂ŗ�O��Ԃ�
        if (_width < 5 || _height < 5) throw new ArgumentOutOfRangeException();

        //�������Ƃ��܂���������Ȃ��̂Ŋ�ɂ���
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
                //���͂�ǂɂ������̂ōŏ��͎��͂𓹂ɂ���
                if (x == 0 || y == 0 || x == _width - 1 || y == _height - 1)
                {
                    var tile = Instantiate(_tile);
                    MazeTile mazeTile = tile.GetComponent<MazeTile>();
                    mazeTile.SetTile(x, y, TileType.Road);
                    _maze[x, y] = mazeTile;
                }
                //����ȊO�͕ǂɂ���
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

        //���̂܂܂ł͎��͂̕ǂ̕������ʘH�Ȃ̂ŕǂɂ���
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
    /// ���ۂɕǂ��@���Ă����֐�
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void Dig(int x, int y)
    {

        while (true)
        {
            //�@���������i�[����
            List<Direction> directions = new ();

            //�e������2�}�X���ǂ�����ǂȂ�@��
            if (_maze[x, y - 1].GetTileType == TileType.Wall && _maze[x, y - 2].GetTileType == TileType.Wall)
                directions.Add(Direction.UP);
            if (_maze[x + 1, y].GetTileType == TileType.Wall && _maze[x + 2, y].GetTileType == TileType.Wall)
                directions.Add(Direction.RIGHT);
            if (_maze[x, y + 1].GetTileType == TileType.Wall && _maze[x, y + 2].GetTileType == TileType.Wall)
                directions.Add(Direction.DOWN);
            if (_maze[x - 1, y].GetTileType == TileType.Wall && _maze[x - 2, y].GetTileType == TileType.Wall)
                directions.Add(Direction.LEFT);

            //�@���ǂ��Ȃ��Ȃ甲����
            if (directions.Count == 0)
            {
                return;
            }

            SetRoad(x, y);

            //���Ɍ@������������_���Ɍ��߂�
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
    /// ���𐶐�����
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void SetRoad(int x, int y)
    {
        _maze[x, y].SetTileType(TileType.Road);

        //���W���c���Ƃ��Ɋ�Ȃ玟�̋N�_���ɒǉ�����B
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