using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityGLTF;

public class Downloader : MonoBehaviour
{

    public string url = null;

    [SerializeField]
    private bool loadOnStart = true;

    // Use this for initialization
    void Start()
    {
        if (loadOnStart)
        {
            StartCoroutine(Download(url));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Download(string url)
    {
        Uri uri = new Uri(url);
        string path = Path.Combine(Application.streamingAssetsPath.Replace('\\', '/') + "/", Path.GetFileName(uri.AbsolutePath));

        WWW www;

        byte[] file = null;

        www = new WWW(url);

        yield return www;
        if (www.error == null)
        {
            file = www.bytes;
            www.Dispose();
            File.WriteAllBytes(path, file);
            yield return File.Exists(path);
            print("downloaded already: " + path);
        }
        else
        {
            Debug.Log(www.error);
        }

        //path = Path.ChangeExtension(path, ".bin");
        
        //url = Path.ChangeExtension(url, ".bin");

        //file = null;

        //www = new WWW(url);

        //yield return www;
        //if (www.error == null)
        //{
        //    file = www.bytes;
        //    www.Dispose();
        //    File.WriteAllBytes(path, file);
        //    yield return File.Exists(path);
        //    print("downloaded already: " + path);
        //}
        //else
        //{
        //    Debug.Log(www.error);
        //}

        GetComponent<GLTFComponent>().GLTFUri = Path.GetFileName(uri.AbsolutePath);
        print(GetComponent<GLTFComponent>().GLTFUri);
        StartCoroutine(GetComponent<GLTFComponent>().Load());
    }
}
