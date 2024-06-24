using System;
using Android.Graphics;
using AndroidX.ViewBinding;
using Com.Azure.Android.Communication.Common;
using Com.Azure.Android.Communication.UI.Calling;
using Com.Azure.Android.Communication.UI.Calling.Models;
using Java.Interop;
using Java.Util;
using static Android.Renderscripts.ScriptGroup;

namespace CommunicationCallingSampleMauiApp.Platforms.Android
{
    public class Composite : IComposite
    {
        CallComposite callComposite;

        public void joinCall(string name, string acsToken, string callID, CallType callType, LocalizationProps? localization, DataModelInjectionProps? dataModelInjection, OrientationProps? orientationProps, CallControlProps? callControlProps)
        {
            CommunicationTokenCredential credentials = new CommunicationTokenCredential(acsToken);

            int layoutDirection = (int)(localization.Value.isLeftToRight != true ? FlowDirection.LeftToRight : FlowDirection.RightToLeft);
            CallCompositeCallScreenControlBarOptions callScreenControlBarOptions = new CallCompositeCallScreenControlBarOptions();
            var isLeaveCallConfirmationEnabled = callControlProps.Value.isDisableLeaveCallConfirmation ? CallCompositeLeaveCallConfirmationMode.AlwaysDisabled : CallCompositeLeaveCallConfirmationMode.AlwaysEnabled;
            callScreenControlBarOptions.SetLeaveCallConfirmation(isLeaveCallConfirmationEnabled);
            CallCompositeCallScreenOptions callScreenOptions = new CallCompositeCallScreenOptions();
            callScreenOptions.SetControlBarOptions(callScreenControlBarOptions);

            CallCompositeTelecomManagerOptions callCompositeTelecomManagerOptions = new CallCompositeTelecomManagerOptions(
                CallCompositeTelecomManagerIntegrationMode.SdkProvidedTelecomManager,
                        "applicationid");

            callComposite =
                new CallCompositeBuilder()
                .Localization(new CallCompositeLocalizationOptions(Java.Util.Locale.ForLanguageTag(localization.Value.locale), layoutDirection))
                .SetupScreenOrientation(GetOrientation(orientationProps.Value.setupScreenOrientation))
                .CallScreenOrientation(GetOrientation(orientationProps.Value.callScreenOrientation))
                .Multitasking(new CallCompositeMultitaskingOptions(Java.Lang.Boolean.True, Java.Lang.Boolean.True))
                .CallScreenOptions(callScreenOptions)
                .ApplicationContext(MainActivity.Instance)
                .DisplayName(name)
                .Credential(credentials)
                .TelecomManagerOptions(callCompositeTelecomManagerOptions)
                .Build();


            callComposite.AddOnErrorEventHandler(new EventHandler());
            callComposite.AddOnRemoteParticipantJoinedEventHandler(new RemoteParticipantJoinedHandler(callComposite, dataModelInjection));
            callComposite.AddOnCallStateChangedEventHandler(new CallStateChangedEventHandler());
            callComposite.AddOnDismissedEventHandler(new CallCompositeDismissedEventHandler());
            callComposite.AddOnUserReportedEventHandler(new CallCompositeUserReportedEventHandler());
            callComposite.AddOnIncomingCallEventHandler(new EventHandler());
            callComposite.AddOnIncomingCallCancelledEventHandler(new EventHandler());

            CallCompositeParticipantViewData personaData = null;

            CallCompositeLocalOptions localOptions = new CallCompositeLocalOptions()
                .SetSkipSetupScreen(callControlProps.Value.isSkipSetupON)
                .SetCameraOn(callControlProps.Value.isCameraON)
                .SetMicrophoneOn(callControlProps.Value.isMicrophoneON)
                .SetAudioVideoMode(CallCompositeAudioVideoMode.AudioAndVideo);

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

            if (callType == CallType.TeamsCall)
            {
                CallCompositeTeamsMeetingLinkLocator locator = new CallCompositeTeamsMeetingLinkLocator(callID);

                if (personaData != null)
                {
                    localOptions.SetParticipantViewData(personaData);
                }

                callComposite.Launch(MainActivity.Instance, locator, localOptions);
            }
            else if (callType == CallType.OneToN)
            {
                if (personaData != null)
                {
                    localOptions.SetParticipantViewData(personaData);
                }
                List<CommunicationIdentifier> participants = new List<CommunicationIdentifier>();
                List<string> mris = new List<string>(callID.Split(','));
                foreach (string mri in mris)
                {
                    participants.Add(new CommunicationUserIdentifier(mri));
                }
                callComposite.Launch(MainActivity.Instance, participants, localOptions);
            }
            else
            {
                CallCompositeGroupCallLocator locator = new CallCompositeGroupCallLocator(UUID.FromString(callID));

                if (personaData != null)
                {
                    localOptions.SetParticipantViewData(personaData);
                }

                callComposite.Launch(MainActivity.Instance, locator, localOptions);
            }

            // to dismiss composite
            // callComposite.Dismiss();
        }

        public void handlePushNotification()
        {
            // value is payload received from APNS or EventGrid for incoming call
            Dictionary<string, string> value = new Dictionary<string, string>();
            callComposite.HandlePushNotification(new CallCompositePushNotification(value));
        }

        public void registerPushNotification()
        {
            callComposite?.RegisterPushNotification("device registeration token from Firebase");
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
                if (eventArgs is CallCompositeIncomingCallEvent)
                {
                    var callEvent = eventArgs as CallCompositeIncomingCallEvent;
                    Console.WriteLine(callEvent.CallId.ToString());
                }
                if (eventArgs is CallCompositeIncomingCallCancelledEvent)
                {
                    var callEvent = eventArgs as CallCompositeIncomingCallCancelledEvent;
                    Console.WriteLine(callEvent.CallId.ToString());
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
                    Console.WriteLine("CallCompositeDismissedEvent" + dismissedEvent.UserMessage);
                    Console.WriteLine("CallCompositeDismissedEvent" + info.LogFiles.ToString);
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

