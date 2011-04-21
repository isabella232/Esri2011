using System;
using System.Diagnostics;
using ESRI.ArcGIS.Framework;
using EsriDE.Samples.ContentLoader.UI.Contract;
using EsriDE.Samples.ContentLoader.UI.Wpf;
using EsriDE.Samples.IocContainer.CastleWindsor;
using Button = ESRI.ArcGIS.Desktop.AddIns.Button;

namespace EsriDE.Samples.ContentLoader.UI
{
    public class ContentLoaderStartButton : Button, IAddInButtonView
    {
#warning APF->APF/ALE 2010-09-23: Debugcode, löschen wenn das Parent-Problem gelöst wurde
        /// <summary>
        /// Dieser Code erlaubt einen schnellen Zugriff auf das Window um zu testen, ob der Parent richtig gesetzt wurde.
        /// </summary>
        private void OpenWindowForDebugging()
        {
            try
            {
                new ContentLoaderWindow(null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private EnabledState _enabledState = EnabledState.Enabled;

        public ContentLoaderStartButton()
        {
            Debug.WriteLine("ContentLoaderStartButton.ctor()");
#warning APF->APF/ALE 2010-09-23: Debugcode, löschen wenn das Parent-Problem gelöst wurde
            // OpenWindowForDebugging();

            _enabledState =
                null != ArcMap.Application
                    ? EnabledState.Enabled
                    : EnabledState.Disabled;

            try
            {
                SetMouseCursor(CursorId.Wait);
                ContainerFacade.CreateContainer();

                var presenter = ContainerFacade.ResolveType<IAddInButtonPresenter>();
                presenter.SetView(this);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            finally
            {
                SetMouseCursor(CursorId.Default);
            }
        }

        #region IAddInButtonView Members

        public int CorrespondingWindowHandle
        {
            get { return ArcMap.Application.hWnd; }
        }

        public event Action Clicked = delegate { };

        public void SetEnabledState(EnabledState enabledState)
        {
            _enabledState = enabledState;
        }

        #endregion

        protected override void OnClick()
        {
            try
            {
                ArcMap.Application.CurrentTool = null;
                OnClicked();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        [DebuggerStepThrough]
        protected override void OnUpdate()
        {
            Enabled = (_enabledState == EnabledState.Enabled);
        }

        protected virtual void OnClicked()
        {
            Clicked();
        }

        private static void SetMouseCursor(CursorId cursorId)
        {
            IMouseCursor cursor = new MouseCursorClass();
            cursor.SetCursor((int) cursorId);
        }

        #region Nested type: CursorId

        private enum CursorId
        {
            Default = 0,
            Wait = 2
        }

        #endregion
    }
}