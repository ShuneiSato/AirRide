using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] GameObject _parentObj;
    Ride _ride;
    public BoxCollider _col;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += ActiveSceneChanged;

        _parentObj = transform.root.gameObject;
        _ride = _parentObj.GetComponent<Ride>();
        _col = GetComponent<BoxCollider>();

        _col.enabled = false;
    }
    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        StartCoroutine("SceneStart");
    }
    public void EndAttack()
    {
        _col.enabled = false;
    }
    public void DestroyObj()
    {
        Destroy(_parentObj.gameObject);
    }

    IEnumerator SceneStart()
    {
        yield return new WaitForSeconds(0.2f);
        _parentObj = transform.root.gameObject;
        _ride = _parentObj.GetComponent<Ride>();
        _col = GetComponent<BoxCollider>();

        _col.enabled = false;
    }
}
