using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Selectica
{
    [Serializable]
    public class HarvestCursorComponent : EntityComponent_d
    {
        private static readonly int Offset = Shader.PropertyToID("_Offset");
        private const float MIN_OFFSET = 0.0f;
        private const float MAX_OFFSET = 1.0f;

        private float _offset;
        private Coroutine _harvestRoutine;
        private Collider[] _buffer = new Collider[16];
        private List<HarvestProvider> _cachedProviders = new();
        
        private GameObject _cursorObject;
        private Material _cursorMaterial;
        private Plane _surface = new(Vector3.up, Vector3.zero);
        
        private float HarvestRadius => Mathf.Min(Mathf.Abs(_cursorObject.transform.lossyScale.x),
            Mathf.Abs(_cursorObject.transform.lossyScale.y)) * 0.5f;

        public HarvestCursorComponent(ComponentData data) : base(data) { }
        
        protected override void OnBind()
        {
            var harvestCursorPrefab = R.HarvestCursorPrefab;
            _cursorObject = Object.Instantiate(harvestCursorPrefab, Entity.Instance.transform, false);
            _cursorMaterial = _cursorObject.GetComponent<MeshRenderer>().sharedMaterial;
            _cursorObject.SetActive(false);
            G.Resolve<States>().OnStateChanged += OnStateChanged;
        }

        private void OnStateChanged(GameState newState)
        {
            if (newState.GetType() == typeof(HarvestState))
            {
                Main.IsCursorVisible = false;
                _harvestRoutine = G.Resolve<Coroutines>().Start(
                    HarvestRoutine(), Entity.Instance);
                Coroutines.Add(_harvestRoutine);
            }
            else
            {
                if (_harvestRoutine != null) G.Resolve<Coroutines>()
                    .Stop(_harvestRoutine, Entity.Instance);
                Coroutines.Remove(_harvestRoutine);
                
                Main.IsCursorVisible = true;
                if (_cursorObject.activeInHierarchy)
                { _cursorObject.SetActive(false); Main.IsCursorVisible = true; }

                foreach (var cachedProvider in _cachedProviders)
                { cachedProvider.StopHarvest(); }
                _cachedProviders.Clear();
            }
        }
        
        private IEnumerator HarvestRoutine()
        {
            var camera = Camera.main;
            var mouse = Mouse.current;
            
            if (camera == null || mouse == null)
            { Debug.LogError($"{nameof(Camera)} or {nameof(Mouse)} is null"); yield break; }
            
            while (Main.IsPlaying)
            {
                var ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (_surface.Raycast(ray, out var hit))
                { Entity.Instance.transform.position = ray.GetPoint(hit); }

                if (!_cursorObject.activeInHierarchy)
                { yield return null; yield return null; _cursorObject.SetActive(true); }
                
                var overlapCount = Physics.OverlapSphereNonAlloc(Entity.Instance.transform.position,
                    HarvestRadius, _buffer, LayerMask.GetMask("Harvestable"));
                
                var providersNoLongerHarvest = new List<HarvestProvider>(_cachedProviders);
                for (var i = 0; i < overlapCount; i++)
                {
                    var provider = _buffer[i].GetComponent<HarvestProvider>();
                    if (provider == null) continue;

                    if (_cachedProviders.Contains(provider))
                    { providersNoLongerHarvest.Remove(provider); continue; }
                        
                    provider.StartHarvest();
                    _cachedProviders.Add(provider);
                }

                foreach (var provider in providersNoLongerHarvest)
                { _cachedProviders.Remove(provider); provider.StopHarvest(); }
                
                _offset = Mathf.Clamp(_offset + Time.deltaTime, MIN_OFFSET, MAX_OFFSET);
                if (_offset >= MAX_OFFSET) { _offset = MIN_OFFSET + Time.deltaTime; }
                _cursorMaterial.SetFloat(Offset, _offset);
                yield return null;
            }
        }

        protected override void OnUnbind() => G.Resolve<States>().OnStateChanged -= OnStateChanged;
    }
}