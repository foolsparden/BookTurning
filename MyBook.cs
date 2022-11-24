using UnityEngine;
using System.Collections;
using DG.Tweening;
class MyBook :MonoBehaviour
{
    public GameObject prefab;
    public Material mat;
    public Texture[] t1;
    int curPage = 0;
    int maxPage =0;
    int targetPage = 0;
    [SerializeField]
    GameObject leftPage;
    [SerializeField]
    GameObject rightPage;
    WaitForSeconds spaceTime;
    private void Awake()
    {
        
    }
    private void Start()
    {
        spaceTime = new WaitForSeconds(0.1f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Turning(1));
            leftPage.GetComponent<MeshRenderer>().material.SetTexture("_MainTex",t1[curPage-2]);
            leftPage.GetComponent<MeshRenderer>().material.SetTexture("_SecTex", t1[curPage + 1 -2]);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Turning(-1));
            rightPage.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", t1[curPage +2]);
            rightPage.GetComponent<MeshRenderer>().material.SetTexture("_SecTex", t1[curPage + 1+ 2]);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(Skip(6,1));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(Skip(6, -1));
        }
    }
    IEnumerator Turning(int direction)
    {
        var turnPage = targetPage == -1 ? 1 : Mathf.Abs(targetPage - curPage);
        var go = Instantiate(prefab, gameObject.transform);
        go.transform.localPosition = Vector3.zero;
        yield return null;
        var angle = direction == 1 ? 180 : 0;
        var mat = go.GetComponent<MeshRenderer>().material;
        mat.SetInt("_Angle",180-angle);
        mat.DOFloat(angle, "_Angle", 1f);
        mat.SetTexture("_MainTex", t1[curPage + 2 * direction]);
        mat.SetTexture("_SecTex", t1[curPage + 1 + 2 * direction]);
        curPage += 2*direction;
        yield return new WaitForSeconds(2);
        Destroy(go.gameObject);
        Debug.Log(mat.GetInt("_Angle"));
    }
    IEnumerator Skip(int num,int dir)
    {
        for (int i = 0; i < num; i++)
        {
            StartCoroutine(Turning(dir));
            yield return spaceTime;
        }
        rightPage.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", t1[curPage + 2]);
        rightPage.GetComponent<MeshRenderer>().material.SetTexture("_SecTex", t1[curPage + 1 + 2]);
    }
}

