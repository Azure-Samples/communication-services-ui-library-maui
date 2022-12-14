using System;
using Foundation;
using iOS.CallingUI.Binding;

namespace MyMauiApp.Platforms.iOS
{
    public class Composite
    {
        public void JoinCall(string name, string acsToken, string callId, bool isTeamsCall) 
        {
            if (isTeamsCall)
            {
                JoinTeamsCall(name, acsToken, callId);
            }
            else
            {
                JoinGroupCall(name, acsToken, callId);
            }
        }

        private void JoinTeamsCall(string name, string acsToken, string callId)
        {
            CommunicationUIProxy _p = new CommunicationUIProxy();
            TeamsMeetingObjectProxy teamsCall = new TeamsMeetingObjectProxy();
            teamsCall.SetTeamsMeetingsProperties(callId, name);
            _p.StartExperienceWithTeamsMeeting(teamsCall, acsToken, null, null, null, (error) => OnHandleError(error), (rawIds) => OnRemoteParticipantJoined(rawIds));
        }

        private void JoinGroupCall(string name, string acsToken, string callId)
        {
            CommunicationUIProxy _p = new CommunicationUIProxy();
            GroupCallObjectProxy groupCall = new GroupCallObjectProxy();
            groupCall.SetGroupCallProperties(callId, name);
            _p.StartExperienceWithGroupCall(groupCall, acsToken, null, null, null, (error) => OnHandleError(error), (rawIds) => OnRemoteParticipantJoined(rawIds));
        }

        private void OnHandleError(CommunicationErrorProxy error)
        {
            Console.WriteLine("OnHandleError error code " + error.Code);
        }

        private void OnRemoteParticipantJoined(NSArray<NSString> rawIds)
        {
            Console.WriteLine("OnRemoteParticipantJoined list " + rawIds);
        }
    }
}