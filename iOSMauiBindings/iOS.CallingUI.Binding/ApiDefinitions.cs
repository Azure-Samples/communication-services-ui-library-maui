using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace iOS.CallingUI.Binding
{
	// @interface CommunicationErrorProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy23CommunicationErrorProxy")]
	interface CommunicationErrorProxy
	{
		// @property (copy, nonatomic) NSString * _Nonnull code;
		[Export ("code")]
		string Code { get; set; }

		// @property (nonatomic, strong) NSError * _Nullable error;
		[NullAllowed, Export ("error", ArgumentSemantic.Strong)]
		NSError Error { get; set; }
	}

	// @interface CommunicationCallStateProxy: NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy27CommunicationCallStateProxy")]
	interface CommunicationCallStateProxy
	{
		// @property (copy, nonatomic) NSString * _Nonnull code;
		[Export ("code")]
		string Code { get; set; }
	}

	// @interface CommunicationDismissedProxy: NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy27CommunicationDismissedProxy")]
	interface CommunicationDismissedProxy
    {
        // @property (copy, nonatomic) NSString * _Nullable errorCode;
        [NullAllowed, Export("errorCode")]
		string ErrorCode { get; set; }

		// @property (nonatomic, strong) NSError * _Nullable error;
		[NullAllowed, Export ("error", ArgumentSemantic.Strong)]
		NSError Error { get; set; }
	}

	// @interface CallScreenControlBarOptionsProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy32CallScreenControlBarOptionsProxy")]
	interface CallScreenControlBarOptionsProxy
	{
        // @property (copy, nonatomic) NSString * _Nullable leaveCallConfirmationMode;
        [Export ("leaveCallConfirmationMode")]
		string LeaveCallConfirmationMode { get; set; }
	}

	// @interface CallScreenOptionsProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy22CallScreenOptionsProxy")]
	interface CallScreenOptionsProxy
	{
		// @property (nonatomic, strong) CallScreenControlBarOptionsProxy * _Nullable callScreenControlBarOptions;
		[NullAllowed, Export ("callScreenControlBarOptions", ArgumentSemantic.Strong)]
		CallScreenControlBarOptionsProxy CallScreenControlBarOptions { get; set; }
	}

	// @interface CommunicationLocalDataOptionProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy33CommunicationLocalDataOptionProxy")]
	interface CommunicationLocalDataOptionProxy
	{
		// @property (nonatomic, strong) CommunicationPersonaDataProxy * _Nullable personaData;
		[NullAllowed, Export ("personaData", ArgumentSemantic.Strong)]
		CommunicationPersonaDataProxy PersonaData { get; set; }

        // @property(nonatomic) BOOL skipSetupScreen;
		[NullAllowed, Export ("skipSetupScreen")]
		bool SkipSetupScreen { get; set; }

		// @property(nonatomic) BOOL microphoneOn;
		[NullAllowed, Export ("microphoneOn")]
		bool MicrophoneOn { get; set; }

		//@property(nonatomic) BOOL cameraOn;
		[NullAllowed, Export ("cameraOn")]
		bool CameraOn { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable audioVideoMode;
		[NullAllowed, Export ("audioVideoMode")]
		string AudioVideoMode { get; set; }

        // -(void)setLocalDataOptionProperties:(CommunicationPersonaDataProxy * _Nonnull)personaData;
        [Export ("setLocalDataOptionProperties:")]
		void SetLocalDataOptionProperties (CommunicationPersonaDataProxy personaData);
	}

	// @interface CommunicationLocalizationProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy30CommunicationLocalizationProxy")]
	interface CommunicationLocalizationProxy
	{
		// @property (copy, nonatomic) NSLocale * _Nonnull locale;
		[Export ("locale", ArgumentSemantic.Copy)]
		NSLocale Locale { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull localizableFilename;
		[Export ("localizableFilename")]
		string LocalizableFilename { get; set; }

		// @property (nonatomic) BOOL isLeftToRight;
		[Export ("isLeftToRight")]
		bool IsLeftToRight { get; set; }
	}

    // @interface CommunicationScreenOrientationProxy : NSObject
    [BaseType(typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy35CommunicationScreenOrientationProxy")]
    interface CommunicationScreenOrientationProxy
    {
        // @property (nonatomic, copy) NSString * _Nonnull callScreenOrientation;
        [Export("callScreenOrientation")]
        string CallScreenOrientation { get; set; }

        // @property (nonatomic, copy) NSString * _Nonnull setupScreenOrientation;
        [Export("setupScreenOrientation")]
        string SetupScreenOrientation { get; set; }
    }

    // @interface CommunicationPersonaDataProxy : NSObject
    [BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy29CommunicationPersonaDataProxy")]
	interface CommunicationPersonaDataProxy
	{
		// @property (nonatomic, strong) UIImage * _Nullable avatarImage;
		[NullAllowed, Export ("avatarImage", ArgumentSemantic.Strong)]
		UIImage AvatarImage { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull renderDisplayName;
		[Export ("renderDisplayName")]
		string RenderDisplayName { get; set; }

		// -(void)setPersonaDataProperties:(UIImage * _Nullable)avatarImage renderDisplayName:(NSString * _Nonnull)renderDisplayName;
		[Export ("setPersonaDataProperties:renderDisplayName:")]
		void SetPersonaDataProperties ([NullAllowed] UIImage avatarImage, string renderDisplayName);
	}

	// @interface CommunicationThemeProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy23CommunicationThemeProxy")]
	interface CommunicationThemeProxy
	{
		// @property (nonatomic, strong) UIColor * _Nonnull primaryColor;
		[Export ("primaryColor", ArgumentSemantic.Strong)]
		UIColor PrimaryColor { get; set; }
	}

	// @interface VersionsProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy13VersionsProxy")]
	interface VersionsProxy
	{
		// @property (copy, nonatomic) NSString * _Nullable callingUIVersion;
		[NullAllowed, Export ("callingUIVersion")]
		string CallingUIVersion { get; set; }
	}

	// @interface DebugInfoProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy14DebugInfoProxy")]
	interface DebugInfoProxy
	{
		// @property (copy, nonatomic) NSArray<CallHistoryRecordProxy *> * _Nullable callHistoryRecords;
		[NullAllowed, Export ("callHistoryRecords", ArgumentSemantic.Copy)]
		CallHistoryRecordProxy[] CallHistoryRecords { get; set; }

		// @property (copy, nonatomic) NSArray<NSURL *> * _Nullable logFiles;
		[NullAllowed, Export ("logFiles", ArgumentSemantic.Copy)]
		NSUrl[] LogFiles { get; set; }

		// @property (nonatomic, strong) VersionsProxy * _Nullable versions;
		[NullAllowed, Export ("versions", ArgumentSemantic.Strong)]
		VersionsProxy Versions { get; set; }
	}

	// @interface CallCompositeUserReportedIssueProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy35CallCompositeUserReportedIssueProxy")]
	interface CallCompositeUserReportedIssueProxy
	{
		// @property (copy, nonatomic) NSString * _Nullable userMessage;
		[NullAllowed, Export ("userMessage")]
		string UserMessage { get; set; }

		// @property (nonatomic, strong) DebugInfoProxy * _Nullable debugInfo;
		[NullAllowed, Export ("debugInfo", ArgumentSemantic.Strong)]
		DebugInfoProxy DebugInfo { get; set; }
	}

	// @interface CallHistoryRecordProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy22CallHistoryRecordProxy")]
	interface CallHistoryRecordProxy
	{
		// @property (copy, nonatomic) NSDate * _Nullable callStartedOn;
		[NullAllowed, Export ("callStartedOn", ArgumentSemantic.Copy)]
		NSDate CallStartedOn { get; set; }

		// @property (copy, nonatomic) NSArray<NSString *> * _Nullable callIds;
		[NullAllowed, Export ("callIds", ArgumentSemantic.Copy)]
		string[] CallIds { get; set; }
	}

	// @interface CommunicationUIProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy20CommunicationUIProxy")]
	interface CommunicationUIProxy
	{
		// -(void)startExperienceWithGroupCall:(GroupCallObjectProxy * _Nonnull)groupCall token:(NSString * _Nonnull)token localData:(CommunicationLocalDataOptionProxy * _Nullable)localData theme:(CommunicationThemeProxy * _Nullable)theme localization:(CommunicationLocalizationProxy * _Nullable)localization orientationProxy:(CommunicationScreenOrientationProxy * _Nullable)orientationProxy enableMultitasking:(BOOL)enableMultitasking enableSystemPictureInPictureWhenMultitasking:(BOOL)enableSystemPictureInPictureWhenMultitasking callScreenOptionsProxy:(CallScreenOptionsProxy * _Nullable)callScreenOptionsProxy errorCallback:(void (^ _Nullable)(CommunicationErrorProxy * _Nonnull))errorCallback onRemoteParticipantJoinedCallback:(void (^ _Nullable)(NSArray<NSString *> * _Nonnull))onRemoteParticipantJoinedCallback onCallStateChangedCallback:(void (^ _Nullable)(CommunicationCallStateProxy * _Nonnull))onCallStateChangedCallback onDismissedCallback:(void (^ _Nullable)(CommunicationDismissedProxy * _Nonnull))onDismissedCallback onUserReportedIssueCallback:(void (^ _Nullable)(CallCompositeUserReportedIssueProxy * _Nonnull))onUserReportedIssueCallback;
		[Export ("startExperienceWithGroupCall:token:localData:theme:localization:orientationProxy:enableMultitasking:enableSystemPictureInPictureWhenMultitasking:callScreenOptionsProxy:errorCallback:onRemoteParticipantJoinedCallback:onCallStateChangedCallback:onDismissedCallback:onUserReportedIssueCallback:")]
		void StartExperienceWithGroupCall (GroupCallObjectProxy groupCall, string token, [NullAllowed] CommunicationLocalDataOptionProxy localData, [NullAllowed] CommunicationThemeProxy theme, [NullAllowed] CommunicationLocalizationProxy localization, [NullAllowed] CommunicationScreenOrientationProxy orientationProxy, bool enableMultitasking, bool enableSystemPictureInPictureWhenMultitasking, [NullAllowed] CallScreenOptionsProxy callScreenOptionsProxy, [NullAllowed] Action<CommunicationErrorProxy> errorCallback, [NullAllowed] Action<NSArray<NSString>> onRemoteParticipantJoinedCallback, [NullAllowed] Action<CommunicationCallStateProxy> onCallStateChangedCallback, [NullAllowed] Action<CommunicationDismissedProxy> onDismissedCallback, [NullAllowed] Action<CallCompositeUserReportedIssueProxy> onUserReportedIssueCallback);

		// -(void)startExperienceWithTeamsMeeting:(TeamsMeetingObjectProxy * _Nonnull)teamsMeeting token:(NSString * _Nonnull)token localData:(CommunicationLocalDataOptionProxy * _Nullable)localData theme:(CommunicationThemeProxy * _Nullable)theme localization:(CommunicationLocalizationProxy * _Nullable)localization orientationProxy:(CommunicationScreenOrientationProxy * _Nullable)orientationProxy enableMultitasking:(BOOL)enableMultitasking enableSystemPictureInPictureWhenMultitasking:(BOOL)enableSystemPictureInPictureWhenMultitasking callScreenOptionsProxy:(CallScreenOptionsProxy * _Nullable)callScreenOptionsProxy errorCallback:(void (^ _Nullable)(CommunicationErrorProxy * _Nonnull))errorCallback onRemoteParticipantJoinedCallback:(void (^ _Nullable)(NSArray<NSString *> * _Nonnull))onRemoteParticipantJoinedCallback onCallStateChangedCallback:(void (^ _Nullable)(CommunicationCallStateProxy * _Nonnull))onCallStateChangedCallback onDismissedCallback:(void (^ _Nullable)(CommunicationDismissedProxy * _Nonnull))onDismissedCallback onUserReportedIssueCallback:(void (^ _Nullable)(CallCompositeUserReportedIssueProxy * _Nonnull))onUserReportedIssueCallback;
		[Export ("startExperienceWithTeamsMeeting:token:localData:theme:localization:orientationProxy:enableMultitasking:enableSystemPictureInPictureWhenMultitasking:callScreenOptionsProxy:errorCallback:onRemoteParticipantJoinedCallback:onCallStateChangedCallback:onDismissedCallback:onUserReportedIssueCallback:")]
		void StartExperienceWithTeamsMeeting (TeamsMeetingObjectProxy teamsMeeting, string token, [NullAllowed] CommunicationLocalDataOptionProxy localData, [NullAllowed] CommunicationThemeProxy theme, [NullAllowed] CommunicationLocalizationProxy localization, [NullAllowed] CommunicationScreenOrientationProxy orientationProxy, bool enableMultitasking, bool enableSystemPictureInPictureWhenMultitasking, [NullAllowed] CallScreenOptionsProxy callScreenOptionsProxy, [NullAllowed] Action<CommunicationErrorProxy> errorCallback, [NullAllowed] Action<NSArray<NSString>> onRemoteParticipantJoinedCallback, [NullAllowed] Action<CommunicationCallStateProxy> onCallStateChangedCallback, [NullAllowed] Action<CommunicationDismissedProxy> onDismissedCallback, [NullAllowed] Action<CallCompositeUserReportedIssueProxy> onUserReportedIssueCallback);

		// -(BOOL)setRemoteWithParticipantDataOption:(CommunicationPersonaDataProxy * _Nonnull)participantDataOption rawId:(NSString * _Nonnull)rawId error:(NSError * _Nullable * _Nullable)error onCompletionCallback:(void (^ _Nullable)(BOOL, NSError * _Nullable))onCompletionCallback;
		[Export ("setRemoteWithParticipantDataOption:rawId:error:onCompletionCallback:")]
		bool SetRemoteWithParticipantDataOption (CommunicationPersonaDataProxy participantDataOption, string rawId, [NullAllowed] out NSError error, [NullAllowed] Action<bool, NSError> onCompletionCallback);

		// -(NSArray<NSString *> * _Nonnull)getAvailableLanguages __attribute__((warn_unused_result("")));
		[Export ("getAvailableLanguages")]
		string[] AvailableLanguages { get; }

        // -(void)dismiss;
        [Export("dismiss")]
        void Dismiss();

        // -(NSString * _Nonnull)getCallStateCode __attribute__((warn_unused_result("")));
        [Export("getCallStateCode")]
        string CallStateCode { get; }
    }

	// @interface GroupCallObjectProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy20GroupCallObjectProxy")]
	interface GroupCallObjectProxy
	{
		// @property (copy, nonatomic) NSString * _Nonnull groupId;
		[Export ("groupId")]
		string GroupId { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull displayName;
		[Export ("displayName")]
		string DisplayName { get; set; }

		// -(void)setGroupCallProperties:(NSString * _Nonnull)groupId displayName:(NSString * _Nonnull)displayName;
		[Export ("setGroupCallProperties:displayName:")]
		void SetGroupCallProperties (string groupId, string displayName);
	}

	// @interface TeamsMeetingObjectProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy23TeamsMeetingObjectProxy")]
	interface TeamsMeetingObjectProxy
	{
		// @property (copy, nonatomic) NSString * _Nonnull teamsMeetingLink;
		[Export ("teamsMeetingLink")]
		string TeamsMeetingLink { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull displayName;
		[Export ("displayName")]
		string DisplayName { get; set; }

		// -(void)setTeamsMeetingsProperties:(NSString * _Nonnull)teamsMeetingLink displayName:(NSString * _Nonnull)displayName;
		[Export ("setTeamsMeetingsProperties:displayName:")]
		void SetTeamsMeetingsProperties (string teamsMeetingLink, string displayName);
	}
}
