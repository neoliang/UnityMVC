using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace ViewComponent
{
    public class FUIListView : FUIView
    {

        FUIListElementView _template;
        List<FUIListElementView> _elements;
        RectTransform content;
        private ScrollRect scrollRect;
        Vector2 scrollSize;
        protected override void OnInit()
        {
            base.OnInit();
            _template = GetComponentInChildren<FUIListElementView>();
            _template.gameObject.SetActive(false);
            _elements = new List<FUIListElementView>();
            content = _template.transform.parent as RectTransform;
            scrollRect = GetComponent<ScrollRect>();

        }
        protected override void OnViewInited()
        {
            base.OnViewInited();
            content.pivot = new Vector2(0, 1);
            content.anchorMin = new Vector2(0, 1);
            content.anchorMax = new Vector2(0, 1);
            content.anchoredPosition = Vector2.zero;
            content.localRotation = Quaternion.identity;
            content.localScale = Vector3.one;
            scrollSize = (transform as RectTransform).rect.size;
        }
        public int ElementCount
        {
            get
            {
                return _elements.Count;
            }
            set
            {
                if (value < 0) { value = 0; }
                if (value == _elements.Count) { return; }
                if (value < _elements.Count)
                {
                    while (_elements.Count > value)
                    {
                        DestroyElement(_elements[_elements.Count - 1]);
                        _elements.RemoveAt(_elements.Count - 1);
                    }
                }
                else
                {
                    while (_elements.Count < value)
                    {
                        _elements.Add(CreateElement(_elements.Count));
                    }
                }
                UpdateScrollArea();
            }
        }
        public FUIListElementView GetElement(int idx)
        {
            if (idx < 0 || idx > _elements.Count)
                return null;
            return _elements[idx];
        }
        float GetCurrentElementSize()
        {
            return _elements.Count * (_template.transform as RectTransform).rect.size.y;
        }
        private void UpdateScrollArea()
        {
            float cy = GetCurrentElementSize();
            if (cy < scrollSize.y)
            {
                content.sizeDelta = new Vector2(content.sizeDelta.x, scrollSize.y);
            }
            else
            {
                content.sizeDelta = new Vector2(content.sizeDelta.x, cy);
            }
        }
        public void ScrollToBottom()
        {
            float y = GetCurrentElementSize() - scrollSize.y;
            if (y > 0)
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x, y);
            }

        }
        public void ScrollToTop()
        {
            content.anchoredPosition = Vector2.zero;
        }
        private FUIListElementView CreateElement(int idx)
        {
            var newElement = Instantiate(_template.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
            newElement.transform.parent = content;
            newElement.transform.localScale = Vector3.one;
            newElement.SetActive(true);
            var tRect = _template.transform as RectTransform;
            var rectTrans = newElement.transform as RectTransform;
            rectTrans.anchoredPosition = new Vector2(tRect.anchoredPosition.x, tRect.anchoredPosition.y - tRect.sizeDelta.y * idx);
            return newElement.GetComponent<FUIListElementView>();
        }

        private void DestroyElement(FUIListElementView fUIListElementView)
        {
            Destroy(fUIListElementView.gameObject);
        }
    }
}