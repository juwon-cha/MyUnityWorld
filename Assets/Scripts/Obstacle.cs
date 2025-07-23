using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // ��ֹ��� ���Ϸ� �̵��� �� �󸶳� �̵������� ���� ��ġ
    public float HighPosY = 1f;
    public float LowPosY = -1f;

    // ž�� ���� ������ ������ �󸶳� ���������� ���� ��ġ
    public float HoleSizeMin = 1f;
    public float HoleSizeMax = 3f;

    public Transform TopObject;
    public Transform BottomObject;

    public float WidthPadding = 4f;

    private GameManager mGameManager;

    private void Start()
    {
        mGameManager = GameManager.Instance;
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(HoleSizeMin, HoleSizeMax);
        float halfHoleSize = holeSize / 2;

        // �ڽ� ������Ʈ(Top, Bottom) localPosition�� �����ϸ� �θ��� ��ġ�� �������� ������
        TopObject.localPosition = new Vector3(0, halfHoleSize, 0); // HoleSize �ݸ�ŭ ���� �ø�
        BottomObject.localPosition = new Vector3(0, -halfHoleSize, 0); // HoleSize �ݸ�ŭ �Ʒ��� ����

        // �θ� ������Ʈ(Obstacle)
        // ���� �������� ������ ��ֹ� �ڿ��ٰ� WidthPadding ��ŭ ���ؼ� ��ġ
        Vector3 placePosition = lastPosition + new Vector3(WidthPadding, 0, 0);
        placePosition.y = Random.Range(LowPosY, HighPosY); // ��ֹ� ���̴� ����

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player)
        {
            mGameManager.AddScore(1);
        }
    }
}
