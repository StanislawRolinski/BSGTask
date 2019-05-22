using System.Collections;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    private void Start()
    {
        SceneController.sceneController.enemies.Add(gameObject);
    }
    public void ReactToHit()
    {
        WanderingAI behavior = GetComponent<WanderingAI>();
        if(behavior!=null)
        {
            behavior.SetAlive(false);
        }
        StartCoroutine(Die());
    }
    private IEnumerator Die()
    {
        SceneController.sceneController.RemoveEnemy(gameObject);
        transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
