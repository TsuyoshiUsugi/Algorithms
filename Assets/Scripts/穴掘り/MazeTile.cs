using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���@��@�Ŏg����^�C���̃X�N���v�g
/// </summary>
public class MazeTile : MonoBehaviour
{
    [SerializeField] Material _wallMaterial;
    [SerializeField] Material _roadMaterial;
    SpriteRenderer _spriteRenderer;

    //�^�C���̎�ނ��i�[����
    [SerializeField] TileType _tileType;
    public TileType GetTileType { get => _tileType; set => _tileType = value; }

    private void Start()
    {
        
    }

    /// <summary>
    /// �^�C���̏�����
    /// </summary>
    /// <param name="tileType"></param>
    public void SetTile(int x, int y, TileType tileType)
    {
        this.gameObject.transform.position = new Vector3(x, y, 0);

        _tileType = tileType;
        SetTileType(_tileType);
    }

    /// <summary>
    /// �^�C���^�C�v��ݒ肵�A�^�C���̐F��ς���
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
/// �^�C���̃^�C�v
/// </summary>
public enum TileType
{
    Wall,
    Road,
}
