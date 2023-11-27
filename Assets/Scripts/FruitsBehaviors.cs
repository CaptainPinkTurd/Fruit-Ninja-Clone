using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitsBehaviors : MonoBehaviour
{
    [SerializeField] GameObject slicedFruit;
    internal static bool gameOver = false;
    private void CreatesSlicedFruit()
    {
        GameObject inst = Instantiate(slicedFruit, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);

        Rigidbody[] slicedParts = inst.GetComponentsInChildren<Rigidbody>(); 
        foreach(var part in slicedParts) 
        {
            //Must first assign the 2 sliced parts of the fruit a rigidbody first in order for this to work
            part.transform.rotation = Random.rotation;
            part.AddExplosionForce(Random.Range(500f, 1000f), transform.position, 5f);
        }
        Destroy(inst, 5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blade") && gameObject.CompareTag("Bomb"))
        {
            gameOver = true;
        }
        if (collision.CompareTag("Blade"))
        {
            CreatesSlicedFruit();
            ScoreScript.AddScore();
        }      
    }
}
