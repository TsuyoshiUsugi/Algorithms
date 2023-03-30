using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 穴掘り法で使われるタイルのスクリプト
/// </summary>
public class MazeTile : MonoBehaviour
{
    [SerializeField] Material _wallMaterial;
    [SerializeField] Material _roadMaterial;
    SpriteRenderer _spriteRenderer;

    //タイルの種類を格納する
    [SerializeField] TileType _tileType;
    public TileType GetTileType { get => _tileType; set => _tileType = value; }

    private void Start()
    {
        
    }

    /// <summary>
    /// タイルの初期化
    /// </summary>
    /// <param name="tileType"></param>
    public void SetTile(int x, int y, TileType tileType)
    {
        this.gameObject.transform.position = new Vector3(x, y, 0);

        _tileType = tileType;
        SetTileType(_tileType);
    }

    /// <summary>
    /// タイルタイプを設定し、タイルの色を変える
    /// </summary>
    /// <param name="tileType"></param>
    public void SetTileType(TileType tileType)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (tileType == TileType.Wall)
        {
            _spriteRenderer.material = _wallMaterial;
        }
        else if(tileType == TileType.Road)
        {
            _spriteRenderer.material = _roadMaterial;
        }
    }
}

/// <summary>
/// タイルのタイプ
/// </summary>
public enum TileType
{
    Wall,
    Road,
}
