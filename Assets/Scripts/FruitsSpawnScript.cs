using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UIElements;

public class FruitsSpawnScript : MonoBehaviour //also serve as the animation script for losing life
{
    [SerializeField] GameObject[] fruits;
    [SerializeField] Transform[] fruitsTransform;
    [SerializeField] Animator[] livesAnimator;
    internal static int missedFruit;
    private Queue<GameObject> fruitsQueueDelete = new Queue<GameObject>();  
    private IEnumerator GenerateFruits()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));

            Vector3 spawnPos = new Vector3(Random.Range(-10f, 10f), -9f, 5f);
            int chosenFruit = Random.Range(0, fruits.Length);

            GameObject fruit = Instantiate(fruits[chosenFruit], spawnPos, fruitsTransform[chosenFruit].rotation);
            fruitsQueueDelete.Enqueue(fruit);
            
            if (spawnPos.x < 0f)
            {
                fruit.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 7.5f), Random.Range(15f, 18f)), ForceMode2D.Impulse);
            }
            else
            {
                fruit.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-7.5f, 1f), Random.Range(15f, 18f)), ForceMode2D.Impulse);
            }
        }
        
    }
    private void Start()
    {
        missedFruit = -1;
        StartCoroutine(GenerateFruits());
    }
    private void Update()
    {
        if(fruitsQueueDelete.Count > 0)
        {
            if (fruitsQueueDelete.Peek() != null)
            {
                MissedFruit(fruitsQueueDelete.Peek());
            }
            else
            {
                fruitsQueueDelete.Dequeue();
                //dequeue this when a fruit has been sliced and turn into a different gameobject, leaving the queue with a null object in line
            }
        } 
    }
    private void MissedFruit(GameObject fruit)
    {
        while(fruit.transform.position.y >= -9f)
        {
            return;
        }

        /*if a fruit has been sliced before it fell out of the screen
          the gameobject "fruit" in this function will have been destroyed before it escape from the while loop*/

        fruitsQueueDelete.Dequeue();

        if (fruit.CompareTag("Bomb"))
        {
            Destroy(fruit);
            return;
        }

        Destroy(fruit);

        missedFruit++;

        livesAnimator[missedFruit].SetBool("MissedFruit", true);

        Invoke("GameOverActivate", 0.4f);
    }

    private void GameOverActivate()
    {
        if(missedFruit >= 2)
        {
            FruitsBehaviors.gameOver = true;
        }
    }
}
