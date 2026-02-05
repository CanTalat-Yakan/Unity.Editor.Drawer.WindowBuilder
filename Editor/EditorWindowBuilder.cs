#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    public partial class EditorWindowBuilder : EditorWindow
    {
        public EditorWindowBuilder ShowAsWindow()
        {
            base.Show();
            base.Focus();
            base.position = _desiredPosition;
            _initialization?.Invoke();
            return this;
        }

        public EditorWindowBuilder ShowAsUtility()
        {
            base.ShowUtility();
            base.Focus();
            base.position = _desiredPosition;
            base.titleContent = new GUIContent(_desiredTitle);
            _initialization?.Invoke();
            return this;
        }

        public EditorWindowBuilder ShowAsPopup()
        {
            base.ShowPopup();
            base.Focus();
            _closeOnLostFocus = true;
            _initialization?.Invoke();
            return this;
        }

        public EditorWindowBuilder ShowAsDropDown(Rect rect, Vector2? size)
        {
            size ??= s_minSize;
            base.ShowAsDropDown(GUIUtility.GUIToScreenRect(rect), size.Value);
            base.Focus();
            _closeOnLostFocus = true;
            _initialization?.Invoke();
            return this;
        }

        private EditorApplication.CallbackFunction _editorApplicationCallback;
        public EditorWindowBuilder AddUpdate(Action updateAction)
        {
            RemoveUpdate();
            _editorApplicationCallback = new(updateAction);
            EditorApplication.update += _editorApplicationCallback;
            return this;
        }

        public void RemoveUpdate()
        {
            if (_editorApplicationCallback is null)
                return;

            EditorApplication.update -= _editorApplicationCallback;
            _editorApplicationCallback = null;
        }

        private bool _drawBorder;
        public EditorWindowBuilder SetDrawBorder()
        {
            _drawBorder = true;
            return this;
        }

        private Action _initialization;
        public EditorWindowBuilder SetInitialization(Action initialization)
        {
            _initialization = initialization;
            return this;
        }

        private Action _preProcessAction;
        public EditorWindowBuilder SetPreProcess(Action preProcess)
        {
            _preProcessAction = preProcess;
            return this;
        }

        private Action _postProcessAction;
        public EditorWindowBuilder SetPostProcess(Action postProcess)
        {
            _postProcessAction = postProcess;
            return this;
        }

        private Action _headerAction;
        private EditorWindowStyle _headerSkin;
        public EditorWindowBuilder SetHeader(Action header, EditorWindowStyle skin = EditorWindowStyle.None)
        {
            _headerAction = header;
            _headerSkin = skin;
            return this;
        }

        private Action _paneAction;
        private GenericMenu _paneMenu;
        private EditorPaneStyle _paneStyle;
        private EditorWindowStyle _paneSkin;
        public EditorWindowBuilder SetPane(Action pane, EditorPaneStyle style = EditorPaneStyle.Left, EditorWindowStyle skin = EditorWindowStyle.None, GenericMenu genericMenu = null)
        {
            _paneAction = pane;
            _paneMenu = genericMenu;
            _paneStyle = style;
            _paneSkin = skin;
            return this;
        }

        private Action _bodyAction;
        private GenericMenu _bodyMenu;
        private EditorWindowStyle _bodySkin;
        public EditorWindowBuilder SetBody(Action body, EditorWindowStyle skin = EditorWindowStyle.None, GenericMenu genericMenu = null)
        {
            _bodyAction = body;
            _bodyMenu = genericMenu;
            _bodySkin = skin;
            return this;
        }

        private Action _footerAction;
        private EditorWindowStyle _footerSkin;
        public EditorWindowBuilder SetFooter(Action footer, EditorWindowStyle skin = EditorWindowStyle.None)
        {
            _footerAction = footer;
            _footerSkin = skin;
            return this;
        }

        public EditorWindowBuilder GetCloseEvent(out Action closeEvent)
        {
            closeEvent = base.Close;
            return this;
        }

        public EditorWindowBuilder GetRepaintEvent(out Action repaintEvent)
        {
            repaintEvent = base.Repaint;
            return this;
        }
    }
}
#endif