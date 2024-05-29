using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SpawnZoneScript : MonoBehaviour
{
    [Tooltip("Are for the text to spawn in")]
    [SerializeField] Vector3 spawnZoneArea = new(1, 1, 1);
    [Tooltip("Use this instead of the model scale")]
    [SerializeField] Vector3 letterSize = new(1, 1, 1);
    [Tooltip("The smallest size for the letters")]
    [SerializeField] Vector3 letterEndSize = new(0.1f, 0.1f, 0.1f);
    [Tooltip("Every bounce will make letter smaller by sizeStep (only x and y axis)")]
    [SerializeField] float sizeStep = 0.1f;
    [Tooltip("How many seconds will the text be visible. Space will automatically let it fall")]
    [SerializeField] float timeBeforeFall = 5.0f;
    [Tooltip("Modify this so the letters are readable")]
    [SerializeField] float spaceBetweenLetters = 0.5f;
    [Tooltip("Use or old new letters")]
    [SerializeField] bool useFixed = true;

    Dictionary<string, GameObject> prefabs;
    Dictionary<string, string> specialCharacters =  new Dictionary<string, string>
    {
        { ".", "tecka" },
        { ",", "carka" },
        { "?", "otaznik"},
        { "-", "pomlcka"},
        { "–", "pomlcka"},
        { "/", "lomitko"},
        { "(", "zavorka1"},
        { ")", "zavorka2"},
        {"„", "uvozovky1"},
        {"“", "uvozovky2"},
        { "!", "vykricnik"},
        { "\"", "uvozovky"},
        { ":", "dvojtecka"},
        { ";", "carka_dvojtecka"},
    };

    int quotations_count = 0;

    void Start() {
        string[] prefabPaths = useFixed 
            ? Directory.GetFiles("Assets/Resources/Letters-Fixed", "*.prefab") 
            : Directory.GetFiles("Assets/Resources/Letters", "*.prefab");
        
        prefabs = new();

        foreach (string path in prefabPaths)
        {
            string prefabName = Path.GetFileNameWithoutExtension(path);
            GameObject prefab = useFixed 
                ? Resources.Load<GameObject>("Letters-Fixed/" + prefabName)
                : Resources.Load<GameObject>("Letters/" + prefabName);
            if (prefab != null)
            {
                string letter = prefab.name;
                prefabs.Add(letter, prefab);
                // Debug.Log(letter);
            }
            else
            {
                Debug.LogWarning("Failed to load prefab at path: " + path);
            }
        }
    }

    public void SpawnString(string inputStr) {
        string input = inputStr.ToUpper();
        Debug.Log(input);
        Vector3 startPosition = CalculateStartPosition(input.Length);
        int letterPositionIndex = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == ' ')
            {
                // white space
                letterPositionIndex++;
                continue;
            }
            if (input[i] == '\n'){
                continue;
            }

            //TODO ch, dz, dž -> very bad calculations of space
            // if (i + 1 < input.Length - 1)
            // {
            //     string doubleChar = input.Substring(i, 2);
            //     if (IsSpecialDoubleCharacter(doubleChar))
            //     {
            //         SpawnCharacter(doubleChar, startPosition, letterPositionIndex);
            //         i++; // Skip the next character as it's part of the double character
            //         continue;
            //     }
            // }

            SpawnCharacter(input[i].ToString(), startPosition, i);
            letterPositionIndex++;
        }
    }

    private void SpawnCharacter(string character, Vector3 startPosition, int index)
    {
        int scaleFactor = 165;
        character = specialCharacters.ContainsKey(character)
            ? GetSpecialCharacterPrefabName(character)
            : character;
        character = useFixed 
            ? character+"-fixed" 
            : character;
        Vector3 letterPosition = new Vector3(startPosition.x + (spaceBetweenLetters + letterSize.x / scaleFactor) * index, startPosition.y, startPosition.z);
        GameObject instantiatedPrefab = Instantiate(prefabs[character], letterPosition, Quaternion.Euler(0, 180, 0));
        LetterManipulation manipulationScript = instantiatedPrefab.AddComponent<LetterManipulation>();
        instantiatedPrefab.transform.localScale = letterSize;
        manipulationScript.timeBeforeFall = timeBeforeFall;
        manipulationScript.endScale = letterEndSize;
        manipulationScript.stepSize = sizeStep;

        // Debug.Log($"Character '{character}' instantiated at: {letterPosition}");
    }
    string GetSpecialCharacterPrefabName(string specialCharacter) {
        if (specialCharacter == "\"") {
            quotations_count++; 
            return specialCharacters[specialCharacter]+((quotations_count % 2) + 1).ToString();
        }
        return specialCharacters[specialCharacter];
    }

    Vector3 CalculateStartPosition(int stringLength) {
        Vector3 randPosition = GetRandomPosition();
        Vector3 defaultPosition = new (transform.position.x - spawnZoneArea.x/2, transform.position.y, randPosition.z);

        float potentialEnd = randPosition.x + (spaceBetweenLetters + letterSize.x) * (stringLength-1);

        if (potentialEnd > transform.position.x + spawnZoneArea.x / 2)
            return defaultPosition;

        return randPosition;
    }

    Vector3 GetRandomPosition() {
        float randomX = UnityEngine.Random.Range(transform.position.x - spawnZoneArea.x / 2, transform.position.x + spawnZoneArea.x / 2);
        float randomY = UnityEngine.Random.Range(transform.position.y - spawnZoneArea.y / 2, transform.position.y + spawnZoneArea.y / 2);
        float randomZ = UnityEngine.Random.Range(transform.position.z - spawnZoneArea.z / 2, transform.position.z + spawnZoneArea.z / 2);
        return new Vector3(randomX, randomY, randomZ);
    }

    void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.red;

        Gizmos.DrawCube(
            transform.position,
            spawnZoneArea
        );
 
        Gizmos.color = Color.white;
#endif
    }
}
