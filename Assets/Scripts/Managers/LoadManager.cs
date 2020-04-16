using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadManager : Singleton<LoadManager>
{
    public void Load()
    {
        var savesPath = Application.persistentDataPath + Consts.savesPath;

        var json = File.ReadAllText(savesPath);

        
    } 
}
