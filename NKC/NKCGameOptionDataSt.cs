using System;
using ClientPacket.Common;
using NKM;

namespace NKC
{
	// Token: 0x02000685 RID: 1669
	public struct NKCGameOptionDataSt
	{
		// Token: 0x060035D5 RID: 13781 RVA: 0x00116E8E File Offset: 0x0011508E
		public static ValueTuple<int, int> GetAspect(NKCGameOptionDataSt.GameOptionGraphicAspectRatio ratio)
		{
			switch (ratio)
			{
			case NKCGameOptionDataSt.GameOptionGraphicAspectRatio.Ratio4_3:
				return new ValueTuple<int, int>(4, 3);
			default:
				return new ValueTuple<int, int>(16, 9);
			case NKCGameOptionDataSt.GameOptionGraphicAspectRatio.Ratio21_9:
				return new ValueTuple<int, int>(21, 9);
			}
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x00116EC0 File Offset: 0x001150C0
		public void Init()
		{
			this.m_bActionCamera = true;
			this.m_bTrackCamera = true;
			this.m_bViewSkillCutIn = true;
			this.m_bUsePvPAutoRespawn = NKM_GAME_AUTO_RESPAWN_TYPE.OFF;
			this.m_bAutoSyncFriendDeck = true;
			this.m_bDisabledGraphicQuality = false;
			this.m_eGraphicQuality = NKC_GAME_OPTION_GRAPHIC_QUALITY.HIGH;
			this.m_CameraShakeLevel = NKCGameOptionDataSt.GameOptionCameraShake.None;
			this.m_bUseGameEffect = false;
			this.m_bUseCommonEffect = false;
			this.m_bUseHitEffect = true;
			this.m_EffectOpacity = 100;
			this.m_eNKM_NATIONAL_CODE = NKM_NATIONAL_CODE.NNC_KOREA;
			this.m_bKeyStringView = false;
			this.m_AnimationQuality = NKCGameOptionDataSt.GraphicOptionAnimationQuality.Normal;
			this.m_GameFrameLimit = NKCGameOptionDataSt.GraphicOptionGameFrameLimit.Sixty;
			this.m_QualityLevel = NKCGameOptionDataSt.GraphicOptionQualityLevel.High;
			this.m_LoginCutin = NKCGameOptionDataSt.GraphicOptionLoginCutin.Always;
			this.m_AspectRatio = NKCGameOptionDataSt.GameOptionGraphicAspectRatio.Ratio16_9;
			this.m_SoundVolumes = new int[4];
			for (int i = 0; i < 4; i++)
			{
				if (i == 1)
				{
					this.m_SoundVolumes[i] = 60;
				}
				else if (i == 2)
				{
					this.m_SoundVolumes[i] = 80;
				}
				else
				{
					this.m_SoundVolumes[i] = 100;
				}
			}
			this.m_eVoiceLanguage = NKC_GAME_OPTION_VOICE_LANGUAGE.KOREAN;
			this.m_bAllowAlarms = new bool[7];
			for (int j = 0; j < 7; j++)
			{
				this.m_bAllowAlarms[j] = false;
			}
			this.m_bAllowPushes = new bool[1];
			for (int k = 0; k < 1; k++)
			{
				this.m_bAllowPushes[k] = false;
			}
			this.m_bUseClassGuide = true;
			this.m_bUseEmoticonBlock = false;
			this.m_bUseDamageAndBuffNumberFx = NKCGameOptionDataSt.GameOptionDamageNumber.Off;
			this.m_bUseVideoTexture = true;
			this.m_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO = NKC_GAME_OPTION_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO.FAST;
			this.m_eStreamingOption = NKCGameOptionDataSt.GameOptionStreamingOption.Normal;
			this.m_ePrivatePvpInviteOption = PrivatePvpInvitation.Friend;
			this.m_bNpcSubtitle = true;
			this.m_bShowNormalSubtitleAfterLifeTime = false;
			this.m_bMemoryOptimize = false;
			this.m_bHideGameHud = false;
			this.m_bUseChatContent = true;
			this.m_bUseChatNotifySound = true;
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x0011703C File Offset: 0x0011523C
		public void DeepCopyFromSource(NKCGameOptionDataSt source)
		{
			this.m_bActionCamera = source.m_bActionCamera;
			this.m_bTrackCamera = source.m_bTrackCamera;
			this.m_bViewSkillCutIn = source.m_bViewSkillCutIn;
			this.m_bUsePvPAutoRespawn = source.m_bUsePvPAutoRespawn;
			this.m_bAutoSyncFriendDeck = source.m_bAutoSyncFriendDeck;
			this.m_bDisabledGraphicQuality = source.m_bDisabledGraphicQuality;
			this.m_eGraphicQuality = source.m_eGraphicQuality;
			this.m_CameraShakeLevel = source.m_CameraShakeLevel;
			this.m_bUseGameEffect = source.m_bUseGameEffect;
			this.m_bUseCommonEffect = source.m_bUseCommonEffect;
			this.m_AnimationQuality = source.m_AnimationQuality;
			this.m_GameFrameLimit = source.m_GameFrameLimit;
			this.m_QualityLevel = source.m_QualityLevel;
			this.m_LoginCutin = source.m_LoginCutin;
			this.m_AspectRatio = source.m_AspectRatio;
			this.m_bUseHitEffect = source.m_bUseHitEffect;
			this.m_EffectOpacity = source.m_EffectOpacity;
			this.m_eNKM_NATIONAL_CODE = source.m_eNKM_NATIONAL_CODE;
			this.m_bKeyStringView = source.m_bKeyStringView;
			this.m_bUseClassGuide = source.m_bUseClassGuide;
			this.m_bUseEmoticonBlock = source.m_bUseEmoticonBlock;
			this.m_bUseDamageAndBuffNumberFx = source.m_bUseDamageAndBuffNumberFx;
			this.m_bUseVideoTexture = source.m_bUseVideoTexture;
			this.m_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO = source.m_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO;
			this.m_eStreamingOption = source.m_eStreamingOption;
			this.m_ePrivatePvpInviteOption = source.m_ePrivatePvpInviteOption;
			this.m_bNpcSubtitle = source.m_bNpcSubtitle;
			this.m_bShowNormalSubtitleAfterLifeTime = source.m_bShowNormalSubtitleAfterLifeTime;
			this.m_bMemoryOptimize = source.m_bMemoryOptimize;
			for (int i = 0; i < 4; i++)
			{
				this.m_SoundVolumes[i] = source.m_SoundVolumes[i];
			}
			this.m_eVoiceLanguage = source.m_eVoiceLanguage;
			for (int j = 0; j < 7; j++)
			{
				this.m_bAllowAlarms[j] = source.m_bAllowAlarms[j];
			}
			this.m_bUseChatContent = source.m_bUseChatContent;
			this.m_bUseChatNotifySound = source.m_bUseChatNotifySound;
		}

		// Token: 0x0400337C RID: 13180
		public const int MAX_SOUND_VOLUME = 100;

		// Token: 0x0400337D RID: 13181
		public const int DEFAULT_EFFECT_VOLUME = 60;

		// Token: 0x0400337E RID: 13182
		public const int DEFAULT_VOICE_VOLUME = 80;

		// Token: 0x0400337F RID: 13183
		public bool m_bActionCamera;

		// Token: 0x04003380 RID: 13184
		public bool m_bTrackCamera;

		// Token: 0x04003381 RID: 13185
		public bool m_bViewSkillCutIn;

		// Token: 0x04003382 RID: 13186
		public NKM_GAME_AUTO_RESPAWN_TYPE m_bUsePvPAutoRespawn;

		// Token: 0x04003383 RID: 13187
		public bool m_bAutoSyncFriendDeck;

		// Token: 0x04003384 RID: 13188
		public bool m_bDisabledGraphicQuality;

		// Token: 0x04003385 RID: 13189
		public NKC_GAME_OPTION_GRAPHIC_QUALITY m_eGraphicQuality;

		// Token: 0x04003386 RID: 13190
		public NKCGameOptionDataSt.GameOptionCameraShake m_CameraShakeLevel;

		// Token: 0x04003387 RID: 13191
		public bool m_bUseGameEffect;

		// Token: 0x04003388 RID: 13192
		public bool m_bUseCommonEffect;

		// Token: 0x04003389 RID: 13193
		public bool m_bUseHitEffect;

		// Token: 0x0400338A RID: 13194
		public int m_EffectOpacity;

		// Token: 0x0400338B RID: 13195
		public NKM_NATIONAL_CODE m_eNKM_NATIONAL_CODE;

		// Token: 0x0400338C RID: 13196
		public bool m_bKeyStringView;

		// Token: 0x0400338D RID: 13197
		public NKCGameOptionDataSt.GraphicOptionAnimationQuality m_AnimationQuality;

		// Token: 0x0400338E RID: 13198
		public NKCGameOptionDataSt.GraphicOptionGameFrameLimit m_GameFrameLimit;

		// Token: 0x0400338F RID: 13199
		public NKCGameOptionDataSt.GraphicOptionQualityLevel m_QualityLevel;

		// Token: 0x04003390 RID: 13200
		public NKCGameOptionDataSt.GraphicOptionLoginCutin m_LoginCutin;

		// Token: 0x04003391 RID: 13201
		public NKCGameOptionDataSt.GameOptionGraphicAspectRatio m_AspectRatio;

		// Token: 0x04003392 RID: 13202
		public bool m_bUseBuffEffect;

		// Token: 0x04003393 RID: 13203
		public int[] m_SoundVolumes;

		// Token: 0x04003394 RID: 13204
		public bool m_bSoundMute;

		// Token: 0x04003395 RID: 13205
		public NKC_GAME_OPTION_VOICE_LANGUAGE m_eVoiceLanguage;

		// Token: 0x04003396 RID: 13206
		public bool[] m_bAllowAlarms;

		// Token: 0x04003397 RID: 13207
		public bool[] m_bAllowPushes;

		// Token: 0x04003398 RID: 13208
		public bool m_bUseClassGuide;

		// Token: 0x04003399 RID: 13209
		public bool m_bUseEmoticonBlock;

		// Token: 0x0400339A RID: 13210
		public NKCGameOptionDataSt.GameOptionDamageNumber m_bUseDamageAndBuffNumberFx;

		// Token: 0x0400339B RID: 13211
		public bool m_bUseVideoTexture;

		// Token: 0x0400339C RID: 13212
		public NKC_GAME_OPTION_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO m_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO;

		// Token: 0x0400339D RID: 13213
		public bool m_bNpcSubtitle;

		// Token: 0x0400339E RID: 13214
		public bool m_bShowNormalSubtitleAfterLifeTime;

		// Token: 0x0400339F RID: 13215
		public bool m_bMemoryOptimize;

		// Token: 0x040033A0 RID: 13216
		public bool m_bHideGameHud;

		// Token: 0x040033A1 RID: 13217
		public NKCGameOptionDataSt.GameOptionStreamingOption m_eStreamingOption;

		// Token: 0x040033A2 RID: 13218
		public PrivatePvpInvitation m_ePrivatePvpInviteOption;

		// Token: 0x040033A3 RID: 13219
		public bool m_bUseChatContent;

		// Token: 0x040033A4 RID: 13220
		public bool m_bUseChatNotifySound;

		// Token: 0x02001333 RID: 4915
		public enum GraphicOptionAnimationQuality
		{
			// Token: 0x040098FD RID: 39165
			Normal,
			// Token: 0x040098FE RID: 39166
			High,
			// Token: 0x040098FF RID: 39167
			ENUM_COUNT
		}

		// Token: 0x02001334 RID: 4916
		public enum GraphicOptionGameFrameLimit
		{
			// Token: 0x04009901 RID: 39169
			Thirty,
			// Token: 0x04009902 RID: 39170
			Sixty,
			// Token: 0x04009903 RID: 39171
			ENUM_COUNT
		}

		// Token: 0x02001335 RID: 4917
		public enum GraphicOptionQualityLevel
		{
			// Token: 0x04009905 RID: 39173
			Low,
			// Token: 0x04009906 RID: 39174
			High,
			// Token: 0x04009907 RID: 39175
			ENUM_COUNT
		}

		// Token: 0x02001336 RID: 4918
		public enum GraphicOptionLoginCutin
		{
			// Token: 0x04009909 RID: 39177
			Always,
			// Token: 0x0400990A RID: 39178
			Random,
			// Token: 0x0400990B RID: 39179
			OncePerDay,
			// Token: 0x0400990C RID: 39180
			Off,
			// Token: 0x0400990D RID: 39181
			ENUM_COUNT
		}

		// Token: 0x02001337 RID: 4919
		public enum GameOptionCameraShake
		{
			// Token: 0x0400990F RID: 39183
			None,
			// Token: 0x04009910 RID: 39184
			Low,
			// Token: 0x04009911 RID: 39185
			Normal,
			// Token: 0x04009912 RID: 39186
			ENUM_COUNT
		}

		// Token: 0x02001338 RID: 4920
		public enum GameOptionDamageNumber
		{
			// Token: 0x04009914 RID: 39188
			Off,
			// Token: 0x04009915 RID: 39189
			Limited,
			// Token: 0x04009916 RID: 39190
			On,
			// Token: 0x04009917 RID: 39191
			ENUM_COUNT
		}

		// Token: 0x02001339 RID: 4921
		public enum GameOptionGraphicAspectRatio
		{
			// Token: 0x04009919 RID: 39193
			Ratio4_3,
			// Token: 0x0400991A RID: 39194
			Ratio16_9,
			// Token: 0x0400991B RID: 39195
			Ratio21_9
		}

		// Token: 0x0200133A RID: 4922
		public enum GameOptionStreamingOption
		{
			// Token: 0x0400991D RID: 39197
			Normal,
			// Token: 0x0400991E RID: 39198
			HideOpponent,
			// Token: 0x0400991F RID: 39199
			HideAll
		}
	}
}
