using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnBeatTextFeedback : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textPrefab;
    [SerializeField] float _gravity = 1;
    [SerializeField] float _upForce = 10;
    [SerializeField] float _aliveDuration = 1;
    [SerializeField] float rotationArcDegrees = 20;
    public void Perform()
    {
        StartCoroutine(Burst());
    }

    IEnumerator Burst()
    {
        TextMeshProUGUI txtMesh = Instantiate(_textPrefab, transform);
        txtMesh.transform.position = transform.position;
        txtMesh.transform.eulerAngles = new Vector3(0,0,Random.Range(-rotationArcDegrees, rotationArcDegrees));

        float currDuration = 0;
        Vector3 velocity = txtMesh.transform.up * _upForce;
        while(currDuration < _aliveDuration)
        {
            velocity -= _gravity * Time.deltaTime * Vector3.down;
            
            txtMesh.transform.position += velocity;
            txtMesh.alpha = 1 - currDuration / _aliveDuration; 
            yield return null;
            currDuration += Time.deltaTime;
        }
        Destroy(txtMesh.gameObject);
    }

}
