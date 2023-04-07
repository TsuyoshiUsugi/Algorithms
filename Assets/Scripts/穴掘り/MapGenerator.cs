using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject _wall;
    [SerializeField] GameObject _path;

    MazeCreator maze; //MazeCreator�^�̕ϐ����`
    int[,] mazeDatas; //���H�f�[�^�p��int�^�̓񎟌��z��̕ϐ����`

    private void Start()
    {
        LoadMapData();
    }

    /*��*/
    void LoadMapData()
    {
        //MazeCreator���C���X�^���X���i��Ƃ���13�~13�̃}�b�v�j
        maze = new MazeCreator(101, 101);
        //���H�f�[�^�p�񎟌��z��𐶐�
        mazeDatas = new int[101, 101];
        //���H�f�[�^�쐬���擾
        mazeDatas = maze.CreateMaze();
        //�}�b�v�̏c�̒����擾
        int row = mazeDatas.GetLength(1);
        //�}�b�v�̉��̒����擾
        int col = mazeDatas.GetLength(0);

        //�}�b�v�e�[�u���쐬
        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < col; x++)
            {
                //���H�f�[�^��MAP_TYPE�ɃL���X�g���ă}�b�v�e�[�u���Ɋi�[
                if (mazeDatas[x, y] == 0) //��
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
