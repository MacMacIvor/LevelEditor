using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class savingScript : MonoBehaviour
{
    public GameObject cube;


    GameObject[] allObjectsInScene;
    List<string> objectsWords = new List<string>();
    private int currentSave = -1;
    //list.Add("Hi");
    //String[] str = list.ToArray();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentSave++;
            string filePath = Application.dataPath + "/SaveData";

            //File.Delete(filePath + "/Saves.txt");

            File.WriteAllText(filePath + "Saves.txt", ""); //Supposed to clear the file, Doesn't work :( - I would say needs to be looked at but we are soon brigning this to c++ anyway

            allObjectsInScene = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject objects in allObjectsInScene)
                if (objects.activeInHierarchy && objects.tag != "Untagged" && objects.tag != "MainCamera")
                {
                    print(objects.tag + " is an active object");
                    objectsWords.Add(objects.tag);
                    objectsWords.Add(objects.transform.position.x.ToString());
                    objectsWords.Add(objects.transform.position.y.ToString());
                    objectsWords.Add(objects.transform.position.z.ToString());
                    objectsWords.Add(objects.transform.rotation.x.ToString());
                    objectsWords.Add(objects.transform.rotation.y.ToString());
                    objectsWords.Add(objects.transform.rotation.z.ToString());



                    string[] str = objectsWords.ToArray();


                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(filePath, "Saves" + currentSave + ".txt")))
                    {
                        foreach (string line in str)
                            outputFile.WriteLine(line);
                    }
                }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            allObjectsInScene = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject objects in allObjectsInScene)
                if (objects.activeInHierarchy && objects.tag != "Untagged" && objects.tag != "MainCamera")
                {
                    Destroy(objects.gameObject);
                }


            int count = 0;
            Vector3 objectPosition = new Vector3(0,0,0);
            Vector3 objectAngles = new Vector3(0, 0, 0);
            string objectName = "";

            string filePath = Application.dataPath + "/SaveData";
            string objectsData;
            
            using (var line = new StreamReader(Path.Combine(filePath, "Saves" + currentSave + ".txt")))
            {
                while ((objectsData = line.ReadLine()) != null)
                {
                    switch (count)
                    {
                        case 0:
                            objectName = objectsData;
                            break;

                        case 1:
                            objectPosition.x = float.Parse(objectsData);
                            break;

                        case 2:
                            objectPosition.y = float.Parse(objectsData);
                            break;

                        case 3:
                            objectPosition.z = float.Parse(objectsData);
                            break;

                        case 4:
                            objectAngles.x = float.Parse(objectsData);
                            break;

                        case 5:
                            objectAngles.y = float.Parse(objectsData);
                            break;

                        case 6:
                            objectAngles.z = float.Parse(objectsData);
                            break;
                    }
                    
                    count++;
                    if (count > 6)
                    {
                        count = 0;

                        switch (objectName)
                        {
                            case "NormalCube":
                                GameObject cloneOfObject = Instantiate(cube) as GameObject;
                                cube.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                                cube.transform.position = objectPosition;
                                cube.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                                break;

                        }

                        /*
                         case "":
                                GameObject cloneOfObject = Instantiate() as GameObject;
                                .name += Random.Range(Random.Range(-1000000.0f, 0.0f), Random.Range(0.0f, 1000000.0f));
                                .transform.position = objectPosition;
                                .transform.rotation.x = objectAngles.x;
                                .transform.rotation.y = objectAngles.y;
                                .transform.rotation.z = objectAngles.z;
                                break;

                         */
                    }
                }
            }
        }

    }
    
    
}


