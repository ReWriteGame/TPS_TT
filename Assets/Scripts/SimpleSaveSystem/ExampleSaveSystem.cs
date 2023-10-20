using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ExampleSaveSystem : MonoBehaviour
{
    [SerializeField] private IStorageService storageService;
    private const string key = "saveKey";
    ExampleSaveItem test1;
    private void Start()
    {
        storageService = new JsonToFileStorageService();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        { 
         ExampleSaveItem test = new ExampleSaveItem();
            test.name = "4toto";
            test.attack = 5665.43f;
            storageService.Save(key, test);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            storageService.Load<ExampleSaveItem>(key, data => test1 = data);
            Debug.Log(test1.name);
        }
    }
}


public class ExampleSaveItem
{ 
    [JsonProperty(PropertyName = "Name Object")]
    public string name;
    [JsonProperty(PropertyName = "Attack Object")]
    public float attack;
   
}