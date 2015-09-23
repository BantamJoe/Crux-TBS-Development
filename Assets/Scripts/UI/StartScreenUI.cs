using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;



public class StartScreenUI : MonoBehaviour {

    List<string> validDirectories;
    string DirectoryListing;

    public Text directoryListing;

    public InputField moduleName;
    public InputField username;
    public InputField password;

    // Use this for initialization
    void Start()
    {
        validDirectories = new List<string>();

        DirectoryListing = "Your directory for placing game files is \n" +
            Application.persistentDataPath + ".\n Your valid modules are currently:\n";

        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        DirectoryInfo[] info = dir.GetDirectories();
        foreach (DirectoryInfo f in info)
        {
            validDirectories.Add(f.Name);
            DirectoryListing += f.Name + "\n";
        }
        directoryListing.text = DirectoryListing;
    }

    public void loadModule()
    {
        if (validDirectories.Contains(moduleName.text))
        {
            Debug.Log("Your chosen module exists");
        }
        else
        {
            Debug.Log("Module not found!");
        }

    }


	
	// Update is called once per frame
	void Update () {
	
	}
}
