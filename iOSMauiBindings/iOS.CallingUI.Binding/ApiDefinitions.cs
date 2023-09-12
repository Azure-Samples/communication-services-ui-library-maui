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

	// @interface CommunicationUIProxy : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21CommunicationUI_Proxy20CommunicationUIProxy")]
	interface CommunicationUIProxy
	{
        // - (void)startExperienceWithGroupCall:(GroupCallObjectProxy * _Nonnull)groupCall token:(NSString * _Nonnull)token localData:(CommunicationLocalDataOptionProxy * _Nullable)localData theme:(CommunicationThemeProxy * _Nullable)theme localization:(CommunicationLocalizationProxy * _Nullable)localization orientationProxy:(CommunicationScreenOrientationProxy * _Nullable)orientationProxy errorCallback:(void (^ _Nullable)(CommunicationErrorProxy * _Nonnull))errorCallback onRemoteParticipantJoinedCallback:(void (^ _Nullable)(NSArray<NSString *> * _Nonnull))onRemoteParticipantJoinedCallback onCallStateChangedCallback:(void (^ _Nullable)(CommunicationCallStateProxy * _Nonnull))onCallStateChangedCallback onDismissedCallback:(void (^ _Nullable)(CommunicationDismissedProxy * _Nonnull))onDismissedCallback;
        [Export ("startExperienceWithGroupCall:token:localData:theme:localization:orientationProxy:errorCallback:onRemoteParticipantJoinedCallback:onCallStateChangedCallback:onDismissedCallback:")]
		void StartExperienceWithGroupCall (GroupCallObjectProxy groupCall, string token, [NullAllowed] CommunicationLocalDataOptionProxy localData, [NullAllowed] CommunicationThemeProxy theme, [NullAllowed] CommunicationLocalizationProxy localization, [NullAllowed] CommunicationScreenOrientationProxy orientationProxy, [NullAllowed] Action<CommunicationErrorProxy> errorCallback, [NullAllowed] Action<NSArray<NSString>> onRemoteParticipantJoinedCallback, [NullAllowed] Action<CommunicationCallStateProxy> onCallStateChangedCallback, [NullAllowed] Action<CommunicationDismissedProxy> onDismissedCallback);

        // (void)startExperienceWithTeamsMeeting:(TeamsMeetingObjectProxy * _Nonnull)teamsMeeting token:(NSString * _Nonnull)token localData:(CommunicationLocalDataOptionProxy * _Nullable)localData theme:(CommunicationThemeProxy * _Nullable)theme localization:(CommunicationLocalizationProxy * _Nullable)localization orientationProxy:(CommunicationScreenOrientationProxy * _Nullable)orientationProxy errorCallback:(void (^ _Nullable)(CommunicationErrorProxy * _Nonnull))errorCallback onRemoteParticipantJoinedCallback:(void (^ _Nullable)(NSArray<NSString *> * _Nonnull))onRemoteParticipantJoinedCallback onCallStateChangedCallback:(void (^ _Nullable)(CommunicationCallStateProxy * _Nonnull))onCallStateChangedCallback onDismissedCallback:(void (^ _Nullable)(CommunicationDismissedProxy * _Nonnull))onDismissedCallback;
        [Export ("startExperienceWithTeamsMeeting:token:localData:theme:localization:orientationProxy:errorCallback:onRemoteParticipantJoinedCallback:onCallStateChangedCallback:onDismissedCallback:")]
		void StartExperienceWithTeamsMeeting (TeamsMeetingObjectProxy teamsMeeting, string token, [NullAllowed] CommunicationLocalDataOptionProxy localData, [NullAllowed] CommunicationThemeProxy theme, [NullAllowed] CommunicationLocalizationProxy localization, [NullAllowed] CommunicationScreenOrientationProxy orientationProxy, [NullAllowed] Action<CommunicationErrorProxy> errorCallback, [NullAllowed] Action<NSArray<NSString>> onRemoteParticipantJoinedCallback, [NullAllowed] Action<CommunicationCallStateProxy> onCallStateChangedCallback, [NullAllowed] Action<CommunicationDismissedProxy> onDismissedCallback);

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
