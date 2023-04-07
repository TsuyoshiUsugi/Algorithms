using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeDig2 : MonoBehaviour
{
    [Header("�Q��")]
    [SerializeField] Button _generateButton;
    [SerializeField] GameObject _wallTile;
    [SerializeField] GameObject _pathTile;

    [Header("�ݒ�l")]
    /// <summary> �� </summary>
    [SerializeField] int _w = 31;

    /// <summary> ���� </summary>
    [SerializeField] int _h = 31;

    /// <summary> �_���W�����̃}�X�̏�� </summary>
    BlockType[,] _maze;

    /// <summary> �ŏ��̌@��n�_W�̏�� </summary>
    [SerializeField]�@int _startPointW = 3;
    
    /// <summary> �ŏ��̌@��n�_H�̏�� </summary>
    [SerializeField]�@int _startPointH = 3;

    /// <summary> ����܂łɌ@�����n�_�̍��W </summary>
    List<(int w, int h)> _alreadyDiggedPoint = new();

    /// <summary>
    /// �}�X�̎��
    /// </summary>
    enum BlockType
    {
        Path,
        Wall,
    }

    /// <summary>
    /// �@�����
    /// </summary>
    enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }

    // Start is called before the first frame update
    void Start()
    {
        _generateButton.onClick.AddListener(() => MakeMaze());
    }

    public void MakeMaze()
    {
        Init();

        Dig(_startPointW, _startPointH);

        Terminate();

        SetTile();
    }

    private void Terminate()
    {

        for (int i = 0; i < _w; i++)
        {
            for (int j = 0; j < _h; j++)
            {
                if (i == 0 || j == 0 || i == _w - 1 || j == _h - 1)
                {
                    _maze[i, j] = BlockType.Wall;
                }
            }
        }
    }

    /// <summary>
    /// ����������
    /// �_���W�����̔z������i�[����z�������������
    /// �S����J�x�ɂ��ĊO����ʘH�ɂ���
    /// </summary>
    private void Init()
    {
        _maze = new BlockType[_w, _h];
        _alreadyDiggedPoint = new();

        //�S�̂�ǂɂ��Ă��Ƃ�ʘH�ɂ���
        for (int i = 0; i < _w; i++)
        {
            for (int j = 0; j < _h; j++)
            {
                if (i == 0 || j == 0 || i == _w - 1 || j == _h - 1)
                {
                    _maze[i, j] = BlockType.Path;
                }
                else
                {
                    _maze[i, j] = BlockType.Wall;

                }
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
                GenerateTile(i, j);
            }
        }
    }

    /// <summary>
    /// �^�C���𐶐�����
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    private void GenerateTile(int i, int j)
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

    /// <summary>
    /// �@��֐�
    /// �@���Ԃ͌@�葱����
    /// </summary>
    void Dig(int w, int h)
    {
        while (true)
        {

            _maze[w, h] = BlockType.Path;
         
            //�l�������m�F���Č@���Ȃ�direction���X�g�Ɋi�[����
            List<Direction> directions = new List<Direction>();
            if (_maze[w, h + 1] == BlockType.Wall && _maze[w, h + 2] == BlockType.Wall)
            {
                directions.Add(Direction.Up);
            }
            if (_maze[w + 1, h] == BlockType.Wall && _maze[w + 2, h] == BlockType.Wall)
            {
                directions.Add(Direction.Right);
            }
            if (_maze[w, h - 1] == BlockType.Wall && _maze[w, h - 2] == BlockType.Wall)
            {
                directions.Add(Direction.Down);
            }
            if (_maze[w - 1, h] == BlockType.Wall && _maze[w - 2, h] == BlockType.Wall)
            {
                directions.Add(Direction.Left);
            }

            //�@���������Ȃ���΂���܂Ō@���Ă���������@���ӏ�������Ă��Č@��
            if (directions.Count == 0) break;
            
            //�����_���Ɍ@����������ߌ@��B�@�����n�_�����X�g�ɓ����
            int digDir = Random.Range(0, directions.Count);
            switch (directions[digDir])
            {
                case Direction.Up:
                    SetPath(w, h + 1);
                    SetPath(w, h + 2);
                    break;
                case Direction.Right:
                    SetPath(w + 1, h);
                    SetPath(w + 2, h);
                    break;
                case Direction.Down:
                    SetPath(w, h - 1);
                    SetPath(w, h - 2);
                    break;
                case Direction.Left:
                    SetPath(w - 1, h);
                    SetPath(w - 2, h);
                    break;
            }

            if (_alreadyDiggedPoint.Count != 0)
            {
                int nextDigPointIndex = Random.Range(0, _alreadyDiggedPoint.Count);

                int width = _alreadyDiggedPoint[nextDigPointIndex].w;
                int height = _alreadyDiggedPoint[nextDigPointIndex].h;
                _alreadyDiggedPoint.RemoveAt(nextDigPointIndex);
                Dig(width, height);
            }
        }
        
    }

    void SetPath(int w, int h)
    {
        _maze[w, h] = BlockType.Path;
        if (w % 2 == 1 && h % 2 == 1)
        {
            _alreadyDiggedPoint.Add((w, h));
        }
    }
}
