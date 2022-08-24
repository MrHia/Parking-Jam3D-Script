using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Data : MonoBehaviour
{
    [SerializeField] private CointData CointData = new CointData();

//    public void SaveIntoJson()
//    {
//        //return;
//        string file = "CoinData.json";
//        string filePath = Path.Combine(Application.streamingAssetsPath, file);
//        string json = JsonUtility.ToJson(CointData,true);
//        File.WriteAllText(filePath, json);



//    }

//    public void LoadformJson()
//    {
//        string file = "CoinData.json";
//        string filePath = Path.Combine(Application.streamingAssetsPath, file);
//        /**
//        if (!File.Exists(filePath))
//        {
//            CointData.Coint = 0;
//            CointData.Level = 1;
//            CointData.MaxLevel = 2;
//            string json = JsonUtility.ToJson(CointData, true);

//            File.WriteAllText(filePath, json);
//        }
//        **/
//#if UNITY_EDITOR
//        CointData = JsonUtility.FromJson<CointData>(File.ReadAllText(filePath));
//#elif UNITY_ANDROID
//        StartCoroutine(LoadFromAndroid(filePath));
//#endif
//   }

    //private IEnumerator LoadFromAndroid(string filePath)
    //{
    //    //Debug.LogError("===LOAD FROM ANDROID");
    //    using (UnityWebRequest www = UnityWebRequest.Get(filePath))
    //    {
    //        yield return www.SendWebRequest();
    //        //Debug.LogError($"===LOAD FROM ANDROID==={ASCIIEncoding.UTF7.GetString(www.downloadHandler.data)}");
    //        CointData = JsonUtility.FromJson<CointData>(ASCIIEncoding.UTF7.GetString(www.downloadHandler.data));
    //    }
    //}
    //public void SaveCoint(int coint) {
    //    CointData.Coint += coint;
    //    //SaveIntoJson();
    //}
    //public int GetCoint()
    //{
    //    return CointData.Coint;
    //}
    //public void LoadCoint()
    //{

    //}
    //public void SaveLevel()
    //{

    //}

    //public void LoadLevel()
    //{

    //}
    //public int GetLevel()
    //{
    //    return CointData.Level;
    //}

    //public void SetLevel()
    //{

    //    if (CointData.Level< CointData.MaxLevel)
    //    {
    //        CointData.Level++;
    //    }

    //}


    //private void Awake()
    //{
    //    //LoadformJson();
    //}
    //private void Start()
    //{
    //    //SaveIntoJson();  
    //}

    public void SaveData()
    {
        if (CointData.Level <= CointData.MaxLevel)
        {
            PlayerPrefs.SetInt("Level", CointData.Level);
        }
        //PlayerPrefs.SetInt("Max Level", CointData.Level + 1);
        PlayerPrefs.SetInt("Coint", CointData.Coint);
        PlayerPrefs.Save();
    }
    public void LoadData() {

        if (!PlayerPrefs.HasKey("Level") || !PlayerPrefs.HasKey("Coint") || !PlayerPrefs.HasKey("Max Level"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Level",1);
            PlayerPrefs.SetInt("Max Level", 4);
            PlayerPrefs.SetInt("Coint", 0);
            PlayerPrefs.Save();
        }
        CointData.Level = PlayerPrefs.GetInt("Level");
        CointData.Coint = PlayerPrefs.GetInt("Coint");
        CointData.MaxLevel = PlayerPrefs.GetInt("Max Level");

    }
    public int GetLevel() {

        return CointData.Level;
    }

    public void SetLevel() {

        
        if (CointData.Level < CointData.MaxLevel)
        {
            CointData.Level++;
        }else if(CointData.Level == CointData.MaxLevel)
        {
            CointData.Level = 1;
        }

    }

    public void SetCoint(int sCoint) {
        CointData.Coint += sCoint;
    }
    public int GetCoint()
    {

        return CointData.Coint;
    }




    private void Awake()
    {
        LoadData();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SaveData();

        }else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            LoadData();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            PlayerPrefs.DeleteAll();
            LoadData();
        }


    }



}




[System.Serializable]
public class CointData
{

    public int Coint;
    public int Level;
    public int MaxLevel;
}

