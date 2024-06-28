using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderByCollision : MonoBehaviour
{
    enum SceneLoadMode
    {
        NextScene,
        PreviousScene,
        NumberedScene
    }
    [SerializeField] SceneLoadMode _getSceneMode;
    [SerializeField, DrawIf("_getSceneMode", SceneLoadMode.NumberedScene)] int _sceneNum;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (_getSceneMode)
            {
                case SceneLoadMode.NextScene:
                    try
                    {
                        GameManager.Instance.LoadScene(GameManager.Instance.CurrScene + 1);
                    }catch(Exception e)
                    {
                        Debug.LogException(e);
                        Debug.LogError("there is no next scene, check build settings");
                    }
                    break;
                case SceneLoadMode.PreviousScene:
                    var prevScene = GameManager.Instance.CurrScene - 1;
                    GameManager.Instance.LoadScene(prevScene < 0 ? 0 : prevScene);
                    break;
                default:
                    try
                    {
                        GameManager.Instance.LoadScene(_sceneNum);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                        Debug.LogError($"there is no scene numbered {_sceneNum}, check build settings");
                    }
                break;
            }
            
        }
    }
}
