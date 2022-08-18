using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Data : MonoBehaviour
{
    [SerializeField] private CointData CointData = new CointData();

    public void SaveIntoJson()
    {
        //return;
        string file = "CoinData.json";
        string filePath = Path.Combine(Application.streamingAssetsPath, file);
        string json = JsonUtility.ToJson(CointData,true);
        File.WriteAllText(filePath, json);



    }

    public void LoadformJson()
    {
        string file = "CoinData.json";
        string filePath = Path.Combine(Application.streamingAssetsPath, file);
        /**
        if (!File.Exists(filePath))
        {
            CointData.Coint = 0;
            CointData.Level = 1;
            CointData.MaxLevel = 2;
            string json = JsonUtility.ToJson(CointData, true);

            File.WriteAllText(filePath, json);
        }
        **/
#if UNITY_EDITOR
        CointData = JsonUtility.FromJson<CointData>(File.ReadAllText(filePath));
#elif UNITY_ANDROID
        StartCoroutine(LoadFromAndroid(filePath));
#endif
    }

    private IEnumerator LoadFromAndroid(string filePath)
    {
        //Debug.LogError("===LOAD FROM ANDROID");
        using (UnityWebRequest www = UnityWebRequest.Get(filePath))
        {
            yield return www.SendWebRequest();
            //Debug.LogError($"===LOAD FROM ANDROID==={ASCIIEncoding.UTF7.GetString(www.downloadHandler.data)}");
            CointData = JsonUtility.FromJson<CointData>(ASCIIEncoding.UTF7.GetString(www.downloadHandler.data));
        }
    }
    public void SaveCoint(int coint) {
        CointData.Coint += coint;
        SaveIntoJson();
    }
    public int GetCoint()
    {
        return CointData.Coint;
    }
    public void LoadCoint()
    {

    }
    public void SaveLevel()
    {

    }

    public void LoadLevel()
    {

    }
    public int GetLevel()
    {
        return CointData.Level;
    }

    public void SetLevel()
    {

        if (CointData.Level< CointData.MaxLevel)
        {
            CointData.Level++;
        }

    }


    private void Awake()
    {
        LoadformJson();
    }
    private void Start()
    {
        //SaveIntoJson();  
    }
}




[System.Serializable]
public class CointData
{
    public int Coint;
    public int Level;
    public int MaxLevel;


}

