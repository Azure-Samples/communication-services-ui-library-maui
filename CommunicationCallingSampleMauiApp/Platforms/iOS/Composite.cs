using System;
using Foundation;
using iOS.CallingUI.Binding;
using UIKit;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Platform;

namespace CommunicationCallingSampleMauiApp.Platforms.iOS
{
    public class Composite : IComposite
    {
        CommunicationUIProxy _p = new CommunicationUIProxy();
        DataModelInjectionProps? _dataModelInjection;

        public void joinCall(string name, string acsToken, string callID, CallType callType, LocalizationProps? localization, DataModelInjectionProps? dataModelInjection, OrientationProps? orientationProps, CallControlProps? callControlProps)
        {
            KeyboardAutoManagerScroll.Disconnect();

            var isLeaveCallConfirmationEnabled = callControlProps.Value.isDisableLeaveCallConfirmation ? "always_disabled" : "always_enabled";
            CallScreenControlBarOptionsProxy callScreenControlBarOptionsProxy = new CallScreenControlBarOptionsProxy();
            callScreenControlBarOptionsProxy.LeaveCallConfirmationMode = isLeaveCallConfirmationEnabled;
            CallScreenOptionsProxy callScreenOptions = new CallScreenOptionsProxy();
            callScreenOptions.CallScreenControlBarOptions = callScreenControlBarOptionsProxy;

            CommunicationLocalizationProxy localizationProxy = null;
            if (!(localization is null))
            {
                localizationProxy = new CommunicationLocalizationProxy();
                localizationProxy.Locale = Foundation.NSLocale.FromLocaleIdentifier(localization.Value.locale);
                localizationProxy.IsLeftToRight = localization.Value.isLeftToRight;
            }
            CommunicationLocalDataOptionProxy localDataOption = new CommunicationLocalDataOptionProxy();
            localDataOption.SkipSetupScreen = callControlProps.Value.isSkipSetupON;
            localDataOption.MicrophoneOn = callControlProps.Value.isMicrophoneON;
            localDataOption.CameraOn = callControlProps.Value.isCameraON;
            localDataOption.AudioVideoMode = "audioAndVideo";
            CommunicationScreenOrientationProxy screenOrientationProxy = new CommunicationScreenOrientationProxy();
            screenOrientationProxy.CallScreenOrientation = orientationProps.Value.callScreenOrientation;
            screenOrientationProxy.SetupScreenOrientation = orientationProps.Value.setupScreenOrientation;

            if (!(dataModelInjection is null))
            {
                _dataModelInjection = dataModelInjection;
                UIImage avatar = UIImage.FromBundle(dataModelInjection.Value.localAvatar);
                CommunicationPersonaDataProxy personaDataProxy = new CommunicationPersonaDataProxy();
                var displayName = dataModelInjection.Value.localAvatar.Length == 0 ? name : dataModelInjection.Value.localAvatar;
                personaDataProxy.SetPersonaDataProperties(avatar, displayName);
                localDataOption.SetLocalDataOptionProperties(personaDataProxy);
            }

            _p.CreateCallCompositeWithToken(token: acsToken,
                displayName: name,
                theme: null,
                localization: localizationProxy,
                orientationProxy: screenOrientationProxy,
                enableMultitasking: true,
                enableSystemPictureInPictureWhenMultitasking: true,
                enableCallKit: true,
                callScreenOptionsProxy: callScreenOptions,
                errorCallback: null,
                onRemoteParticipantJoinedCallback: null,
                (callstate) => onCallStateChanged(callstate),
                (dismissed) => onDismissed(dismissed),
                (issue) => onUserReportedIssueCallback(issue),
                (mri) => onIncomingCallProvideRemoteInfo(mri),
                (incomingCall) => onIncomingCall(incomingCall),
                (incomingCallCancelled) => onIncomingCallCancelled(incomingCallCancelled),
                (callId) => onIncomingCallAcceptedFromCallKit(callId));

            if (callType == CallType.TeamsCall)
            {
                TeamsMeetingObjectProxy _teamsMeetingObject = new TeamsMeetingObjectProxy();
                _teamsMeetingObject.SetTeamsMeetingsProperties(callID, name);
                CallKitRemoteInfoProxy callKitRemoteInfo = new CallKitRemoteInfoProxy();
                callKitRemoteInfo.CxHandleName = "TeamsCall";
                callKitRemoteInfo.DisplayName = name;
                _p.StartExperienceWithTeamsMeeting(teamsMeeting: _teamsMeetingObject,
                localData: localDataOption,
                callKitRemoteInfo: callKitRemoteInfo);
            }
            else if (callType == CallType.OneToN)
            {
                CallKitRemoteInfoProxy callKitRemoteInfo = new CallKitRemoteInfoProxy();
                callKitRemoteInfo.CxHandleName = "Outgoing Call";
                callKitRemoteInfo.DisplayName = name;
                _p.StartExperienceWithParticipants(participants: callID,
                localData: localDataOption,
                callKitRemoteInfo: callKitRemoteInfo);
            }
            else
            {
                GroupCallObjectProxy _groupCallObject = new GroupCallObjectProxy();
                _groupCallObject.SetGroupCallProperties(callID, name);
                CallKitRemoteInfoProxy callKitRemoteInfo = new CallKitRemoteInfoProxy();
                callKitRemoteInfo.CxHandleName = "GroupCall";
                callKitRemoteInfo.DisplayName = name;
                _p.StartExperienceWithGroupCall(groupCall: _groupCallObject,
                localData: localDataOption,
                callKitRemoteInfo: callKitRemoteInfo);
            }
        }

        public List<String> languages()
        {
            return new List<String>(_p.AvailableLanguages);
        }

        private void handleError(CommunicationErrorProxy error)
        {
            Console.WriteLine("handleCall errorCode " + error.Code);
        }

        private void onDismissed(CommunicationDismissedProxy dismissed)
        {
            _p.Dismiss();
            Console.WriteLine("onDismissed " + dismissed.ErrorCode);
        }

        private void onUserReportedIssueCallback(CallCompositeUserReportedIssueProxy issue)
        {
            Console.WriteLine("onUserReportedIssueCallback " + issue.UserMessage);
        }

        private CallKitRemoteInfoProxy onIncomingCallProvideRemoteInfo(String mri)
        {
            CallKitRemoteInfoProxy proxy = new CallKitRemoteInfoProxy();
            proxy.CxHandleName = "handleName";
            proxy.DisplayName = "displayName";
            return proxy;
        }

        private void onIncomingCall(IncomingCallProxy incomingCall)
        {
            Console.WriteLine("onIncomingCall " + incomingCall.CallId);
        }

        private void onIncomingCallCancelled(IncomingCallCancelledProxy incomingCallCancelled)
        {
            Console.WriteLine("onIncomingCallCancelled " + incomingCallCancelled.CallId);
        }

        private void onIncomingCallAcceptedFromCallKit(String callId)
        {
            Console.WriteLine("onIncomingCallAcceptedFromCallKit " + callId);
        }

        private void onCallStateChanged(CommunicationCallStateProxy callstate)
        {
            Console.WriteLine("CallStateCode " + _p.CallStateCode);
            Console.WriteLine("onCallStateChanged " + callstate.Code);
        }

        private void onRemoteParticipant(NSArray<NSString> rawIds)
        {
            if (!(_dataModelInjection is null))
            {
                foreach (NSString rawId in rawIds.ToArray())
                {
                    CommunicationPersonaDataProxy participantOption = new CommunicationPersonaDataProxy();
                    UIImage avatar = UIImage.FromBundle(_dataModelInjection.Value.remoteAvatar);
                    participantOption.SetPersonaDataProperties(avatar, "");
                    NSError error;
                    _p.SetRemoteWithParticipantDataOption(participantOption, rawId, out error, null);
                }
            }
        }

        public List<string> orientations()
        {
            List<String> orientationStrings = new List<String>();

            orientationStrings.Add("portrait");
            orientationStrings.Add("landscape");
            orientationStrings.Add("landscapeRight");
            orientationStrings.Add("landscapeLeft");
            orientationStrings.Add("allButUpsideDown");

            return orientationStrings;
        }
    }
}