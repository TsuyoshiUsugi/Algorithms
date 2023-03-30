using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideRoom : MonoBehaviour
{
    [SerializeField] GameObject _tile;

    //マップ全体の横幅
    const int _mapWidth = 50;

    //マップ全体の縦幅
    const int _mapHeight = 50;

    //作るエリアの数
    int _areaNum = 4;

    //部屋の大きさを決めるための範囲
    //生成する部屋の大きさの最小値
    int _minMapSize = 3;

    //生成する部屋の大きさの最大値
    int _maxMapSize = 7;

    //一つのエリアの横幅の大きさ
    int _areaSize = 1;

    //出来たエリアの中心座標X
    int _randomPosWidth;

    //出来たエリアの中心座標Y
    int _randomPosHeight;

    int _roomSize;

    int _keepBackSide;　//生成した部屋の一番最後にあるオブジェクトのｘ座標を管理する変数？
    int _keepFrontSide;　//生成した部屋の一番最初にあるオブジェクトのｘ座標を管理する変数？
    int _keepPosHeight; //生成した部屋の一番最初にあるオブジェクトのｘ座標を管理する変数？
    int _count; //何回目のオブジェクトかを管理する変数？

    private void Start()
    {
        //現在のエリアの終点座標X
        var currentAreaStartPoint = 1;

        //ひとつ前のエリアの終点座標X
        var preAreaStartPoint = 1;

        //エリアサイズを決める。横幅を生成したいエリアの数で割っている。
        _areaSize = _mapWidth / _areaNum;

        //ここからエリアの生成を始める
        for (int i = 0; i < _areaNum; i++)
        {
            preAreaStartPoint = currentAreaStartPoint;

            //最初の区画の場合
            if (i == 0)
            {
                currentAreaStartPoint = _areaSize;
                //横幅の中から部屋を作る時に中心となる座標のX座標を割り出す
                _randomPosWidth = Random.Range(1, currentAreaStartPoint);
            }
            //最後の区画の場合
            else if (i == _areaNum - 1)
            {
                //最後の区画だからこうなるのは当然
                currentAreaStartPoint = _mapWidth - 1;
                Debug.Log(preAreaStartPoint + "前回の最大の幅");
                Debug.Log(currentAreaStartPoint + "今回の最大の幅");
                _randomPosWidth = Random.Range(preAreaStartPoint, currentAreaStartPoint);
            }
            //間の区画の場合
            else
            {
                currentAreaStartPoint += _areaSize;
                Debug.Log(preAreaStartPoint + "前回の最大の幅");
                Debug.Log(currentAreaStartPoint + "今回の最大の幅");
                _randomPosWidth = Random.Range(preAreaStartPoint, currentAreaStartPoint);
            }

            _randomPosHeight = Random.Range(1, _mapHeight);

            //部屋の大きさをランダムで決める
            _roomSize = Random.Range(_minMapSize, _maxMapSize);

            for (int x = _randomPosWidth - _roomSize; x < _randomPosWidth + _roomSize; x++)　//ここから部屋の生成。しかしこのままでははみ出ないか？
            {
                for (int y = _randomPosHeight - _roomSize; y < _randomPosHeight + _roomSize; y++)
                {
                    if (x > 0 && x < currentAreaStartPoint)　//ここで制限は出来ている
                    {
                        if (x > preAreaStartPoint)
                        {
                            if (y > 0 && y < _mapHeight)
                            {
                                var tile = Instantiate(_tile);
                                tile.transform.position = new Vector3(x, y, 0);

                                //ここから部屋をつなげる処理
                                //つなげ方は前の部屋の最後のX座標のタイルと今回作った部屋の
                                //一番最初に生成した横の座標をつなげるようにオブジェクトを
                                //生成する

                                //ここは一番最後のX座標を保持している
                                _keepBackSide = x;

                                _count++;　
                            }
                        }
                    }

                    //ここでカウントが１なら一番最初のX座標を保持する
                    if (_count == 1)
                    {
                        _keepFrontSide = x;
                    }

                }
            }
            _count = 0;

            
            if (i != _areaNum - 1)　//生成したエリアの最後尾のマスから今回の最大幅まで道をつなげる？
            {
                for (int road = _keepBackSide + 1; road <= currentAreaStartPoint; road++)
                {
                    var tile = Instantiate(_tile);
                    tile.transform.position = new Vector3(road, _randomPosHeight, 0);
                }
            }

            if (i > 0)
            {
                for (int aisle = _keepFrontSide; aisle >= preAreaStartPoint; aisle--)
                {
                    var tile = Instantiate(_tile);
                    tile.transform.position = new Vector3(aisle, _randomPosHeight, 0);
                }

                if (_randomPosHeight > _keepPosHeight)
                {
                    //道をつなげるコード？
                    for (int road = _keepPosHeight; road < _randomPosHeight; road++)
                    {
                        var tile = Instantiate(_tile);
                        tile.transform.position = new Vector3(preAreaStartPoint, road, 0);
                    }
                }
                else
                {
                    //道をつなげるためのコード？
                    for (int road = _randomPosHeight + 1; road < _randomPosHeight; road++)
                    {
                        var tile = Instantiate(_tile);
                        tile.transform.position = new Vector3(preAreaStartPoint, road, 0);
                    }
                }
            }

            _keepPosHeight = _randomPosHeight;
        }

    }
}
