using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WormText:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private Canvas _worldSpaceCanvas;
        private GameObject _parentWorm;


        void Awake()
        {
            _worldSpaceCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }

        public GameObject SetLevel(int level,GameObject parentWorm)
        {
            _textMesh.text = level.ToString();
            _textMesh.transform.SetParent(_worldSpaceCanvas.transform);
            _parentWorm = parentWorm;
            return gameObject;
        }
    }
}