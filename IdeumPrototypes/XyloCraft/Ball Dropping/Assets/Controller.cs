using System.Collections.Generic;
using UnityEngine;

namespace TE.Examples
{
    /// <summary>
    /// A simple example of using tangible engine
    /// </summary>
    public class Controller : MonoBehaviour, IOnTangibleAdded, IOnTangibleRemoved, IOnTangibleUpdated, IOnEnginePatternsChanged
    {
        //public float offset;
        public RectTransform CanvasRForm;
        public Transform ChildRoot;
        public Dictionary<int, GameObject> emitters = new Dictionary<int, GameObject>();

        private GameObject CurrentEmitter;

        private Queue<ExampleTangibleData> _pool = new Queue<ExampleTangibleData>();

        private Dictionary<int, ExampleTangibleData> _tangibleMap = new Dictionary<int, ExampleTangibleData>();

        private Vector3 _offset;

        /// <summary>
        /// Gets a reference to or create an ExampleTangibleData instance that can be used.
        /// </summary>
        /// <param name="id">The tangible id to reference this instance</param>
        /// <returns></returns>
        private ExampleTangibleData GetExampleTangible(int id)
        {
            ExampleTangibleData e;
            if (!_tangibleMap.TryGetValue(id, out e))
            {
                if (_pool.Count <= 0)
                {
                    var o = Instantiate(CurrentEmitter, ChildRoot, true);
                    e = new ExampleTangibleData(o);
                    _tangibleMap[id] = e;
                    return e;
                }
                else
                {
                    e = _pool.Dequeue();
                    _tangibleMap[id] = e;
                }
            }
            e.DoShow();
            return e;
        }

        /// <summary>
        /// Removes an ExampleTangibleData instance from the _tangibleMap, returns the instance to the _pool, and then hides the instance.
        /// </summary>
        /// <param name="id"></param>
        private void ReturnExampleTangible(int id)
        {
            ExampleTangibleData e;
            if (_tangibleMap.TryGetValue(id, out e))
            {
                _tangibleMap.Remove(id);
                e.DoHide();
                _pool.Enqueue(e);
            }
        }

        void Start()
        {
            var r = CanvasRForm.rect;
            _offset = new Vector3(r.width, r.height) * -0.5f;
            TangibleEngine.Subscribe(this);

        }

        public void OnEnginePatternsChanged(List<Pattern> patterns)
        {
            Debug.Log("Patterns Updated");
        }

        public void OnTangibleAdded(Tangible t)
        {
            if (CurrentEmitter)
            {
                Debug.Log(t.X + " " + t.Y);
                Debug.Log("Tangible added: " + t.Id);
                var e = GetExampleTangible(t.Id);
                Debug.Log(e.Transform.position);
                e.Update(t, _offset);
            }
            
        }

        public void OnTangibleRemoved(Tangible t)
        {
            if (CurrentEmitter)
            {
                Debug.Log("Tangible removed: " + t.Id);
                ReturnExampleTangible(t.Id);
                GameObject destroyEmitter = emitters[t.Id];
                Destroy(destroyEmitter);
            }
        }

        public void OnTangibleUpdated(Tangible t)
        {
            if (CurrentEmitter)
            {
                ExampleTangibleData e;
                if (_tangibleMap.TryGetValue(t.Id, out e))
                {
                    e.Transform.position = Camera.main.ScreenToWorldPoint(new Vector3(t.X, t.Y));
                    e.Update(t, _offset);
                }
                float z_distance = Camera.main.WorldToScreenPoint(e.Transform.position).z;
                e.Transform.position = Camera.main.ScreenToWorldPoint(new Vector3(t.X, t.Y, z_distance));
            }
        }

        public void SetEmitter(GameObject newEmitter)
        {
            CurrentEmitter = newEmitter;
        }

    }
}