using System;
using Foundation;
using iOS.CallingUI.Binding;
using UIKit;
using System;
using System.Collections.Generic;

namespace CommunicationCallingSampleMauiApp.Platforms.iOS
{
    public class Composite : IComposite
    {
        CommunicationUIProxy _p = new CommunicationUIProxy();
        DataModelInjectionProps? _dataModelInjection;

        public void joinCall(string name, string acsToken, string callID, bool isTeamsCall, LocalizationProps? localization, DataModelInjectionProps? dataModelInjection)
        {
            CommunicationLocalizationProxy localizationProxy = null;
            if (!(localization is null))
            {
                localizationProxy = new CommunicationLocalizationProxy();
                localizationProxy.Locale = Foundation.NSLocale.FromLocaleIdentifier(localization.Value.locale);
                localizationProxy.IsLeftToRight = localization.Value.isLeftToRight;
            }
            CommunicationLocalDataOptionProxy localDataOption = new CommunicationLocalDataOptionProxy();

            if (!(dataModelInjection is null))
            {
                _dataModelInjection = dataModelInjection;
                UIImage avatar = UIImage.FromBundle(dataModelInjection.Value.localAvatar);
                CommunicationPersonaDataProxy personaDataProxy = new CommunicationPersonaDataProxy();
                var displayName = dataModelInjection.Value.localAvatar.Length == 0 ? name : dataModelInjection.Value.localAvatar;
                personaDataProxy.SetPersonaDataProperties(avatar, displayName);
                localDataOption.SetLocalDataOptionProperties(personaDataProxy);
            }

            if (isTeamsCall)
            {
                TeamsMeetingObjectProxy _teamsMeetingObject = new TeamsMeetingObjectProxy();
                _teamsMeetingObject.SetTeamsMeetingsProperties(callID, name);
                _p.StartExperienceWithTeamsMeeting(teamsMeeting: _teamsMeetingObject, 
                token: acsToken, 
                localData: localDataOption, 
                theme: null, localization: localizationProxy, errorCallback: null, 
                onRemoteParticipantJoinedCallback: null,
                (callstate) => onCallStateChanged(callstate),
                (exited)=> onExited(exited));
            }
            else
            {
                GroupCallObjectProxy _groupCallObject = new GroupCallObjectProxy();
                _groupCallObject.SetGroupCallProperties(callID, name);
                _p.StartExperienceWithGroupCall(_groupCallObject,
                 acsToken,
                  localDataOption,
                   null,
                   localizationProxy,
                    (error) => handleError(error),
                     (rawIds) => onRemoteParticipant(rawIds),
                    (callstate) => onCallStateChanged(callstate),
                    (exited)=> onExited(exited));
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

        private void onExited(CommunicationExitProxy exited)
        {
            Console.WriteLine("onExited " + exited.Code);
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
    }
}