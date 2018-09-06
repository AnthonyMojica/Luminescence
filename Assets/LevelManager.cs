using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    float MinLoadTime = 2;
   
    public static void Load(string name)
    {
        GameObject Level = new GameObject("LevelManager");
        LevelManager LvlInstance = Level.AddComponent<LevelManager>();

        LvlInstance.StartCoroutine(LvlInstance.CoLoad(name));
    }

    IEnumerator CoLoad(string name)
    {
        Object.DontDestroyOnLoad(this.gameObject);
        Application.LoadLevel("LoadScreen");

        //Render the Loading Screen
        yield return null;
        MinLoadTime -= Time.deltaTime;

        yield return new WaitForSeconds(2);
            Application.LoadLevel(name);
            Destroy(this.gameObject);
    }

}