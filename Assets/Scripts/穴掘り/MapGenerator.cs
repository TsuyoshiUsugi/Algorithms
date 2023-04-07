using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject _wall;
    [SerializeField] GameObject _path;

    MazeCreator maze; //MazeCreator型の変数を定義
    int[,] mazeDatas; //迷路データ用にint型の二次元配列の変数を定義

    private void Start()
    {
        LoadMapData();
    }

    /*略*/
    void LoadMapData()
    {
        //MazeCreatorをインスタンス化（例として13×13のマップ）
        maze = new MazeCreator(101, 101);
        //迷路データ用二次元配列を生成
        mazeDatas = new int[101, 101];
        //迷路データ作成＆取得
        mazeDatas = maze.CreateMaze();
        //マップの縦の長さ取得
        int row = mazeDatas.GetLength(1);
        //マップの横の長さ取得
        int col = mazeDatas.GetLength(0);

        //マップテーブル作成
        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < col; x++)
            {
                //迷路データをMAP_TYPEにキャストしてマップテーブルに格納
                if (mazeDatas[x, y] == 0) //道
                {
                    Instantiate(_path, new Vector3(x, y, 0), Quaternion.identity);
                }
                else if (mazeDatas[x, y] == 1) //kabe
                {
                    Instantiate(_wall, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }
}
