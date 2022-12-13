using System;
using Android.Graphics;
using Com.Azure.Android.Communication.UI.Calling;
using Com.Azure.Android.Communication.UI.Calling.Models;
using Java.Interop;
using System.Net;

namespace MyMauiApp.Platforms.Android
{
    internal class RemoteParticipantJoinedHandler : Java.Lang.Object, ICallCompositeEventHandler
    {

        private CallComposite callComposite;

        public RemoteParticipantJoinedHandler(CallComposite callComposite)
        {
            this.callComposite = callComposite;
        }

        public void Disposed()
        {
        }

        public void DisposeUnlessReferenced()
        {
        }

        public void Finalized()
        {
        }

        public void Handle(Java.Lang.Object eventArgs)
        {
            if (eventArgs is CallCompositeRemoteParticipantJoinedEvent)
            {

            }
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

        public void SetJniIdentityHashCode(int value)
        {
        }

        public void SetJniManagedPeerState(JniManagedPeerStates value)
        {
        }

        public void SetPeerReference(JniObjectReference reference)
        {
        }

        public void UnregisterFromRuntime()
        {
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        public void Dispose()
        {

        }
    }
}

