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

    [SerializeField]
    private List<Material> _materialList = new List<Material>();
    private List<string> _texturePathList = new List<string>();
    private Material _firstMaterial;
    private string _firstTexturePath;

    // Start is called before the first frame update
    void Start()
    {
        LoadMaterials();
        SpawnPictureMesh(4, 5, StartPosition, Offset, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadMaterials()
    {
        string materialPath = GameSettings.Instance.GetMaterialDirectoryName();
        string texturePath = GameSettings.Instance.GetCardFaceDirectoryName();
        int pairNumber = (int)GameSettings.Instance.GetPairNumber();

        const string matBaseName = "Pic ";
        string firstMaterialName = "Back";

        for(int i = 1; i <= 20; i++)
        {
            string currentPath = materialPath + matBaseName + i;
            Material mat = Resources.Load(currentPath, typeof(Material)) as Material;
            _materialList.Add(mat);

            string currentTexturePath = texturePath + i;
            _texturePathList.Add(currentTexturePath);

            _firstTexturePath = texturePath + firstMaterialName;
            _firstMaterial = Resources.Load(materialPath, typeof(Material)) as Material;
        }
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
        ApplyTextures();
        MovePicture(row, col, pos, offset);
    }

    private void ApplyTextures()
    {
        int i = 0;
        foreach(var o in PictureList)
        {
            o.SetFirstMaterial(_firstMaterial, "");
            o.SetSecondMaterial(_materialList[i++], "");
            o.ApplySecondMaterial();
        }
    }
    // public void ApplyTextures()
    // {

    //     print("Size: " + _materialList.Count);

    //     int rndIndex = Random.Range(0, _materialList.Count);
    //     var appliedTimes = new int[_materialList.Count];

    //     for(int i = 0; i < _materialList.Count; i++)
    //     {
    //         appliedTimes[i] = 0;
    //     }
        
    //     foreach(var o in PictureList)
    //     {
    //         var randPrev = rndIndex;
    //         var counter = 0;
    //         var forceMat = false;
    //         print(rndIndex);
    //         while(appliedTimes[rndIndex] >= 2 || ((randPrev == rndIndex) && !forceMat))
    //         {
    //             rndIndex = Random.Range(0, _materialList.Count);
    //             counter++;
    //             if(counter > 100)
    //             {
    //                 for(var j = 0; j < _materialList.Count; j++)
    //                 {
    //                     if(appliedTimes[j] < 2)
    //                     {
    //                         rndIndex = j;
    //                         forceMat = true;
    //                     }
    //                 }

    //                 if(!forceMat) return;
    //             }
    //         }

    //         o.SetFirstMaterial(_firstMaterial, _firstTexturePath);
    //         o.ApplyFirstMaterial();
    //         o.SetSecondMaterial(_materialList[rndIndex], _texturePathList[rndIndex]);

    //         o.ApplySecondMaterial();

    //         appliedTimes[rndIndex]++;
    //         forceMat = false;
    //     }
    // }

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
