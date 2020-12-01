using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public float yOffset;
    public float xOffset;

    public GameObject genTrigger;
    public float genTriggerXPos;
    public GameObject remTrigger;
    public float remTriggerXPos;

    public GameObject[] chunks;
    public int startingX = 20;
    public int currentChunk = 0;

    static int previouslyUsedIndex;
    static int removalIndex;

    public List<GameObject> createdChunks = new List<GameObject>();

    private void Awake()
    {
        removalIndex = 0;
        previouslyUsedIndex = 0;
    }

    public void LoadNext()
    {
        int newIndex = previouslyUsedIndex;

        if (chunks.Length != 1)
        {
            while (newIndex == previouslyUsedIndex)
            {
                newIndex = Random.Range(0, chunks.Length);
            }
        }

        GameObject newChunk = Instantiate(chunks[newIndex], new Vector2(startingX + (currentChunk * 10), -5), Quaternion.identity);

        createdChunks.Add(newChunk);

        Vector2 genTriggerCoords = new Vector2(xOffset + (genTriggerXPos * createdChunks.Count-1), yOffset);
        Vector2 remTriggerCoords = new Vector2(xOffset + (remTriggerXPos * createdChunks.Count-1), yOffset);

        GameObject newGenTrigger = Instantiate(genTrigger, genTriggerCoords, Quaternion.identity);
        newGenTrigger.name = currentChunk.ToString();
        newGenTrigger.transform.SetParent(newChunk.transform);

        GameObject newRemTrigger = Instantiate(remTrigger, remTriggerCoords, Quaternion.identity);
        newRemTrigger.name = currentChunk.ToString();
        newRemTrigger.transform.SetParent(newChunk.transform);

        currentChunk++;
        previouslyUsedIndex = newIndex;
    }

    public void RemoveChunk()
    {
        print("activated remove chunk function");
        createdChunks[removalIndex].SetActive(false);
        removalIndex++;
    }
}
