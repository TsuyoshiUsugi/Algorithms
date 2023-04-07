using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDig2 : MonoBehaviour
{
    [Header("�Q��")]
    [SerializeField] GameObject _wallTile;
    [SerializeField] GameObject _pathTile;

    int _w = 5;
    int _h = 5;
    BlockType[,] _maze;

    enum BlockType
    {
        Path,
        Wall,
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();

        SetTile();
    }

    /// <summary>
    /// �_���W�����̔z������i�[����z�������������
    /// </summary>
    private void Init()
    {
        _maze = new BlockType[_w, _h];

        //�S�̂�ǂɂ���
        for (int i = 0; i < _w; i++)
        {
            for (int j = 0; j < _h; j++)
            {
                _maze[i, j] = BlockType.Wall;
            }
        }
    }

    /// <summary>
    /// �_���W�����̊e�^�C���̏���ǂݎ��\������
    /// </summary>
    void SetTile()
    {
        for (int i = 0; i < _w; i++)
        {
            for (int j = 0; j < _h; j++)
            {
                if (_maze[i, j] == BlockType.Wall)
                {
                    Instantiate(_wallTile, new Vector2(i, j), Quaternion.identity);
                }
                else
                {
                    Instantiate(_pathTile, new Vector2(i, j), Quaternion.identity);
                }
            }
        }
    }
}
