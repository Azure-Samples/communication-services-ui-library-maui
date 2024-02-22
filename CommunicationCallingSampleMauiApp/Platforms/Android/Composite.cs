using System;
using Android.Graphics;
using Com.Azure.Android.Communication.Common;
using Com.Azure.Android.Communication.UI.Calling;
using Com.Azure.Android.Communication.UI.Calling.Models;
using Java.Interop;
using Java.Util;

namespace CommunicationCallingSampleMauiApp.Platforms.Android
{
    public class Composite : IComposite
    {
        public void joinCall(string name, string acsToken, string callID, bool isTeamsCall, LocalizationProps? localization, DataModelInjectionProps? dataModelInjection, OrientationProps? orientationProps, CallControlProps? callControlProps)
        {
            CommunicationTokenCredential credentials = new CommunicationTokenCredential(acsToken);


            int layoutDirection = (int)(localization.Value.isLeftToRight ? FlowDirection.LeftToRight : FlowDirection.RightToLeft);

            CallComposite callComposite =
                new CallCompositeBuilder()
                .Localization(new CallCompositeLocalizationOptions(Java.Util.Locale.ForLanguageTag(localization.Value.locale), layoutDirection))
                .SetupScreenOrientation(GetOrientation(orientationProps.Value.setupScreenOrientation))
                .CallScreenOrientation(GetOrientation(orientationProps.Value.callScreenOrientation))
                .Multitasking(new CallCompositeMultitaskingOptions(Java.Lang.Boolean.True, Java.Lang.Boolean.True))
                .Build();


            callComposite.AddOnErrorEventHandler(new EventHandler());
            callComposite.AddOnRemoteParticipantJoinedEventHandler(new RemoteParticipantJoinedHandler(callComposite, dataModelInjection));
            callComposite.AddOnCallStateChangedEventHandler(new CallStateChangedEventHandler());
            callComposite.AddOnDismissedEventHandler(new CallCompositeDismissedEventHandler());
            callComposite.AddOnUserReportedEventHandler(new CallCompositeUserReportedEventHandler());

            CallCompositeParticipantViewData personaData = null;

            CallCompositeLocalOptions localOptions = new CallCompositeLocalOptions()
                .SetSkipSetupScreen(callControlProps.Value.isSkipSetupON)
                .SetCameraOn(callControlProps.Value.isCameraON)
                .SetMicrophoneOn(callControlProps.Value.isMicrophoneON);
                //.SetAudioVideoMode(CallCompositeAudioVideoMode.AudioOnly);

            if (dataModelInjection != null)
            {
                var context = MainActivity.Instance.ApplicationContext;
                int resID = context.Resources.GetIdentifier(dataModelInjection.Value.localAvatar.Replace(".png", ""), "drawable", context.PackageName);
                Bitmap avatarBitMap = BitmapFactory.DecodeResource(context.Resources, resID);
                personaData = new CallCompositeParticipantViewData();
                personaData.SetAvatarBitmap(avatarBitMap);
                var displayName = dataModelInjection.Value.localAvatar.Length == 0 ? name : dataModelInjection.Value.localAvatar;
                personaData.SetDisplayName(displayName);

            }


            if (isTeamsCall)
            {

                CallCompositeTeamsMeetingLinkLocator locator = new CallCompositeTeamsMeetingLinkLocator(callID);


                CallCompositeRemoteOptions remoteOptions = new CallCompositeRemoteOptions(locator, credentials, name);


                if (personaData != null)
                {
                    localOptions.SetParticipantViewData(personaData);
                }

                callComposite.Launch(MainActivity.Instance, remoteOptions, localOptions);
            }
            else
            {

                CallCompositeGroupCallLocator locator = new CallCompositeGroupCallLocator(UUID.FromString(callID));

                CallCompositeRemoteOptions remoteOptions = new CallCompositeRemoteOptions(locator, credentials, name);

                if (personaData != null)
                {
                    localOptions.SetParticipantViewData(personaData);
                }

                callComposite.Launch(MainActivity.Instance, remoteOptions, localOptions);
            }

            // to dismiss composite
            // callComposite.Dismiss();
        }

        public List<string> languages()
        {
            List<String> localeStrings = new List<String>();

            foreach (Java.Util.Locale locale in CallCompositeSupportedLocale.SupportedLocales)
            {
                localeStrings.Add(locale.Language);
            }

            return localeStrings;
        }

        public List<string> orientations()
        {
            List<String> orientationStrings = new List<String>();

            foreach (CallCompositeSupportedScreenOrientation orientation in CallCompositeSupportedScreenOrientation.Values())
            {
                orientationStrings.Add(orientation.ToString());
            }

            return orientationStrings;
        }

        private CallCompositeSupportedScreenOrientation GetOrientation(string orientation)
        {
            CallCompositeSupportedScreenOrientation _orientation = CallCompositeSupportedScreenOrientation.User;
            switch (orientation)
            {
                case "PORTRAIT":
                    _orientation = CallCompositeSupportedScreenOrientation.Portrait;
                    break;
                case "LANDSCAPE":
                    _orientation = CallCompositeSupportedScreenOrientation.Landscape;
                    break;
                case "REVERSE_LANDSCAPE":
                    _orientation = CallCompositeSupportedScreenOrientation.ReverseLandscape;
                    break;
                case "USER_LANDSCAPE":
                    _orientation = CallCompositeSupportedScreenOrientation.UserLandscape;
                    break;
                case "FULL_SENSOR":
                    _orientation = CallCompositeSupportedScreenOrientation.FullSensor;
                    break;
                case "USER":
                    _orientation = CallCompositeSupportedScreenOrientation.User;
                    break;
                default:
                    _orientation = CallCompositeSupportedScreenOrientation.User;
                    break;
            }
            return _orientation;
        }

        public class EventHandler : Java.Lang.Object, ICallCompositeEventHandler
        {
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
                if (eventArgs is CallCompositeErrorEvent)
                {
                    var error = eventArgs as CallCompositeErrorEvent;
                    Console.WriteLine(error.ErrorCode.ToString());
                }
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

        private class CallCompositeUserReportedEventHandler: Java.Lang.Object, ICallCompositeEventHandler
        {
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
                if (eventArgs is CallCompositeUserReportedIssueEvent)
                {
                    var dismissedEvent = eventArgs as CallCompositeUserReportedIssueEvent;
                    var info = dismissedEvent.DebugInfo;
                    Console.WriteLine("Inderpal CallCompositeDismissedEvent" + dismissedEvent.UserMessage);
                    Console.WriteLine("Inderpal CallCompositeDismissedEvent" + info.LogFiles.ToString);
                }
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


        private class CallStateChangedEventHandler : Java.Lang.Object, ICallCompositeEventHandler
        {
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
                if (eventArgs is CallCompositeCallStateChangedEvent)
                {
                    var callState = eventArgs as CallCompositeCallStateChangedEvent;
                    Console.WriteLine(callState.Code.ToString());
                }
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

        private class CallCompositeDismissedEventHandler : Java.Lang.Object, ICallCompositeEventHandler
        {
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
                if (eventArgs is CallCompositeDismissedEvent)
                {
                    var dismissedEvent = eventArgs as CallCompositeDismissedEvent;
                    Console.WriteLine("CallCompositeDismissedEvent");
                }
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
}

