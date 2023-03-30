using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideRoom : MonoBehaviour
{
    [SerializeField] GameObject _tile;

    //�}�b�v�S�̂̉���
    const int _mapWidth = 50;

    //�}�b�v�S�̂̏c��
    const int _mapHeight = 50;

    //���G���A�̐�
    int _areaNum = 4;

    //�����̑傫�������߂邽�߂͈̔�
    //�������镔���̑傫���̍ŏ��l
    int _minMapSize = 3;

    //�������镔���̑傫���̍ő�l
    int _maxMapSize = 7;

    //��̃G���A�̉����̑傫��
    int _areaSize = 1;

    //�o�����G���A�̒��S���WX
    int _randomPosWidth;

    //�o�����G���A�̒��S���WY
    int _randomPosHeight;

    int _roomSize;

    int _keepBackSide;�@//�������������̈�ԍŌ�ɂ���I�u�W�F�N�g�̂����W���Ǘ�����ϐ��H
    int _keepFrontSide;�@//�������������̈�ԍŏ��ɂ���I�u�W�F�N�g�̂����W���Ǘ�����ϐ��H
    int _keepPosHeight; //�������������̈�ԍŏ��ɂ���I�u�W�F�N�g�̂����W���Ǘ�����ϐ��H
    int _count; //����ڂ̃I�u�W�F�N�g�����Ǘ�����ϐ��H

    private void Start()
    {
        //���݂̃G���A�̏I�_���WX
        var currentAreaStartPoint = 1;

        //�ЂƂO�̃G���A�̏I�_���WX
        var preAreaStartPoint = 1;

        //�G���A�T�C�Y�����߂�B�����𐶐��������G���A�̐��Ŋ����Ă���B
        _areaSize = _mapWidth / _areaNum;

        //��������G���A�̐������n�߂�
        for (int i = 0; i < _areaNum; i++)
        {
            preAreaStartPoint = currentAreaStartPoint;

            //�ŏ��̋��̏ꍇ
            if (i == 0)
            {
                currentAreaStartPoint = _areaSize;
                //�����̒����畔������鎞�ɒ��S�ƂȂ���W��X���W������o��
                _randomPosWidth = Random.Range(1, currentAreaStartPoint);
            }
            //�Ō�̋��̏ꍇ
            else if (i == _areaNum - 1)
            {
                //�Ō�̋�悾���炱���Ȃ�͓̂��R
                currentAreaStartPoint = _mapWidth - 1;
                Debug.Log(preAreaStartPoint + "�O��̍ő�̕�");
                Debug.Log(currentAreaStartPoint + "����̍ő�̕�");
                _randomPosWidth = Random.Range(preAreaStartPoint, currentAreaStartPoint);
            }
            //�Ԃ̋��̏ꍇ
            else
            {
                currentAreaStartPoint += _areaSize;
                Debug.Log(preAreaStartPoint + "�O��̍ő�̕�");
                Debug.Log(currentAreaStartPoint + "����̍ő�̕�");
                _randomPosWidth = Random.Range(preAreaStartPoint, currentAreaStartPoint);
            }

            _randomPosHeight = Random.Range(1, _mapHeight);

            //�����̑傫���������_���Ō��߂�
            _roomSize = Random.Range(_minMapSize, _maxMapSize);

            for (int x = _randomPosWidth - _roomSize; x < _randomPosWidth + _roomSize; x++)�@//�������畔���̐����B���������̂܂܂ł͂͂ݏo�Ȃ����H
            {
                for (int y = _randomPosHeight - _roomSize; y < _randomPosHeight + _roomSize; y++)
                {
                    if (x > 0 && x < currentAreaStartPoint)�@//�����Ő����͏o���Ă���
                    {
                        if (x > preAreaStartPoint)
                        {
                            if (y > 0 && y < _mapHeight)
                            {
                                var tile = Instantiate(_tile);
                                tile.transform.position = new Vector3(x, y, 0);

                                //�������畔�����Ȃ��鏈��
                                //�Ȃ����͑O�̕����̍Ō��X���W�̃^�C���ƍ�������������
                                //��ԍŏ��ɐ����������̍��W���Ȃ���悤�ɃI�u�W�F�N�g��
                                //��������

                                //�����͈�ԍŌ��X���W��ێ����Ă���
                                _keepBackSide = x;

                                _count++;�@
                            }
                        }
                    }

                    //�����ŃJ�E���g���P�Ȃ��ԍŏ���X���W��ێ�����
                    if (_count == 1)
                    {
                        _keepFrontSide = x;
                    }

                }
            }
            _count = 0;

            
            if (i != _areaNum - 1)�@//���������G���A�̍Ō���̃}�X���獡��̍ő啝�܂œ����Ȃ���H
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
                    //�����Ȃ���R�[�h�H
                    for (int road = _keepPosHeight; road < _randomPosHeight; road++)
                    {
                        var tile = Instantiate(_tile);
                        tile.transform.position = new Vector3(preAreaStartPoint, road, 0);
                    }
                }
                else
                {
                    //�����Ȃ��邽�߂̃R�[�h�H
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
