using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeDig2 : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] Button _generateButton;
    [SerializeField] GameObject _wallTile;
    [SerializeField] GameObject _pathTile;

    [Header("設定値")]
    /// <summary> 幅 </summary>
    [SerializeField] int _w = 31;

    /// <summary> 高さ </summary>
    [SerializeField] int _h = 31;

    /// <summary> ダンジョンのマスの情報 </summary>
    BlockType[,] _maze;

    /// <summary> 最初の掘る地点Wの情報 </summary>
    [SerializeField]　int _startPointW = 3;
    
    /// <summary> 最初の掘る地点Hの情報 </summary>
    [SerializeField]　int _startPointH = 3;

    /// <summary> これまでに掘った地点の座標 </summary>
    List<(int w, int h)> _alreadyDiggedPoint = new();

    /// <summary>
    /// マスの種類
    /// </summary>
    enum BlockType
    {
        Path,
        Wall,
    }

    /// <summary>
    /// 掘る方向
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
    /// 初期化処理
    /// ダンジョンの配列情報を格納する配列を初期化する
    /// 全域をカベにして外側を通路にする
    /// </summary>
    private void Init()
    {
        _maze = new BlockType[_w, _h];
        _alreadyDiggedPoint = new();

        //全体を壁にしてそとを通路にする
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
    /// ダンジョンの各タイルの情報を読み取り表示する
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
    /// タイルを生成する
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
    /// 掘る関数
    /// 掘れる間は掘り続ける
    /// </summary>
    void Dig(int w, int h)
    {
        while (true)
        {

            _maze[w, h] = BlockType.Path;
         
            //四方向を確認して掘れるならdirectionリストに格納する
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

            //掘れる方向がなければこれまで掘ってきた穴から掘れる箇所を取ってきて掘る
            if (directions.Count == 0) break;
            
            //ランダムに掘る方向を決め掘る。掘った地点をリストに入れる
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
