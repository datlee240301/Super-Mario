using io.lockedroom.Games.SuperMario;
using UnityEngine;

public class SavePoint : MonoBehaviour
{ 
    public static SavePoint instance;
    public string key_score = "Score_save";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S)) Save();
        if (Input.GetKeyUp(KeyCode.L)) Load();
        if (Input.GetKeyUp(KeyCode.X)) DeleteDate();
    }

    public void Save() {
        PlayerPrefs.SetInt(key_score, Mario.Instance.points);
        PlayerPrefs.SetString(key_score, Mario.Instance.PoinText.text);
        Debug.Log("save");
    }

    public void Load() {
        // Lấy giá trị điểm đã lưu từ PlayerPrefs
        PlayerPrefs.GetInt(key_score);
        PlayerPrefs.GetString(key_score, Mario.Instance.PoinText.text);
        Mario.Instance.PoinText.text = PlayerPrefs.GetString(key_score);
        Debug.Log("load");
    }
    
    public void DeleteDate() {
        PlayerPrefs.DeleteKey(key_score);
    }
}
