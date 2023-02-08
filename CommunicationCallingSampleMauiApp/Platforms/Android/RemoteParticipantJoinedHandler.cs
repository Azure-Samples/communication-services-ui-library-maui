using System;
using Android.Graphics;
using Com.Azure.Android.Communication.UI.Calling;
using Com.Azure.Android.Communication.UI.Calling.Models;
using Java.Interop;
using System.Net;

namespace CommunicationCallingSampleMauiApp.Platforms.Android
{
    internal class RemoteParticipantJoinedHandler : Java.Lang.Object, ICallCompositeEventHandler
    {

        private CallComposite callComposite;
        private DataModelInjectionProps? dataModelInjection;

        public RemoteParticipantJoinedHandler(CallComposite callComposite, DataModelInjectionProps? dataModelInjection)
        {
            this.callComposite = callComposite;
            this.dataModelInjection = dataModelInjection;
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
                var participantJoinedEvent = eventArgs as CallCompositeRemoteParticipantJoinedEvent;

                foreach (var id in participantJoinedEvent.Identifiers)
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(dataModelInjection.Value.remoteAvatar))
                        {
                            var context = MainActivity.Instance.ApplicationContext;
                            int resID = context.Resources.GetIdentifier(dataModelInjection.Value.remoteAvatar.Replace(".png", ""), "drawable", context.PackageName);
                            Bitmap avatarBitMap = BitmapFactory.DecodeResource(context.Resources, resID);
                            CallCompositeParticipantViewData personaData = new CallCompositeParticipantViewData();
                            personaData.SetAvatarBitmap(avatarBitMap);
                            personaData.SetDisplayName(dataModelInjection.Value.remoteAvatar);
                            var result = callComposite.SetRemoteParticipantViewData(id, personaData);

                            if (result == CallCompositeSetParticipantViewDataResult.Success)
                            {

                            }
                            if (result == CallCompositeSetParticipantViewDataResult.ParticipantNotInCall)
                            {

                            }
                        }

                    }
                    catch (Exception e)
                    {

                    }

                }
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

