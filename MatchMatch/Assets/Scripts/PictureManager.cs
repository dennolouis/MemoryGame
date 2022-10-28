using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureManager : MonoBehaviour
{

    public Picture PicturePrefab;
    public Transform PicSpawnPosition;
    public Vector2 StartPosition = new Vector2(-700, 50);
    public Vector2 Offset = new Vector2(200, 200);

    [HideInInspector]
    public List<Picture> PictureList;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPictureMesh(4, 5, StartPosition, Offset, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPictureMesh(int row, int col, Vector2 pos, Vector2 offset, bool scaleDown)
    {
        for(int c = 0; c < col; c++){
            for(int r = 0; r < row; r++){
                Picture pic = Instantiate(PicturePrefab, PicSpawnPosition.position, PicSpawnPosition.rotation);
                pic.name = pic.name + 'c' + c + 'r' + r;
                PictureList.Add(pic);
            }
        }

        MovePicture(row, col, pos, offset);
    }

    private void MovePicture(int row, int col, Vector2 pos, Vector2 offset)
    {
        int index = 0;
        for(int c = 0; c < col; c++){
            for(int r = 0; r < row; r++){
                var targetPosition = new Vector3(pos.x + offset.x * r, pos.y - offset.y * c, PicSpawnPosition.position.z);
                StartCoroutine(MoveToPosition(targetPosition, PictureList[index++]));
            }
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, Picture pic)
    {
        int speed = 1000;
        while(pic.transform.position != target)
        {
            pic.transform.position = Vector3.MoveTowards(pic.transform.position, target, speed * Time.deltaTime);
            yield return 0;
        }
    }
}
