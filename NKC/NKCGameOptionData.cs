using System;
using ClientPacket.Common;
using NKC.Publisher;
using NKM;
using Spine.Unity;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000686 RID: 1670
	public class NKCGameOptionData
	{
		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060035D8 RID: 13784 RVA: 0x00117201 File Offset: 0x00115401
		public bool ChangedPrivatePvpInvite
		{
			get
			{
				return this.m_bChangedPrivatePvpInvite;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060035D9 RID: 13785 RVA: 0x00117209 File Offset: 0x00115409
		public bool ChangedServerOption
		{
			get
			{
				return this.m_bChangedServerOption;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060035DA RID: 13786 RVA: 0x00117211 File Offset: 0x00115411
		public bool ActionCamera
		{
			get
			{
				return this.m_DataSt.m_bActionCamera;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060035DB RID: 13787 RVA: 0x0011721E File Offset: 0x0011541E
		public bool TrackCamera
		{
			get
			{
				return this.m_DataSt.m_bTrackCamera;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060035DC RID: 13788 RVA: 0x0011722B File Offset: 0x0011542B
		public bool ViewSkillCutIn
		{
			get
			{
				return this.m_DataSt.m_bViewSkillCutIn;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060035DD RID: 13789 RVA: 0x00117238 File Offset: 0x00115438
		public NKM_GAME_AUTO_RESPAWN_TYPE PvPAutoRespawn
		{
			get
			{
				return this.m_DataSt.m_bUsePvPAutoRespawn;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060035DE RID: 13790 RVA: 0x00117245 File Offset: 0x00115445
		public bool AutSyncFriendDeck
		{
			get
			{
				return this.m_DataSt.m_bAutoSyncFriendDeck;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x00117252 File Offset: 0x00115452
		// (set) Token: 0x060035E0 RID: 13792 RVA: 0x0011725F File Offset: 0x0011545F
		public bool DisabledGraphicQuality
		{
			get
			{
				return this.m_DataSt.m_bDisabledGraphicQuality;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bDisabledGraphicQuality = value;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x00117274 File Offset: 0x00115474
		// (set) Token: 0x060035E2 RID: 13794 RVA: 0x00117281 File Offset: 0x00115481
		public NKC_GAME_OPTION_GRAPHIC_QUALITY GraphicQuality
		{
			get
			{
				return this.m_DataSt.m_eGraphicQuality;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_eGraphicQuality = value;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060035E3 RID: 13795 RVA: 0x00117296 File Offset: 0x00115496
		// (set) Token: 0x060035E4 RID: 13796 RVA: 0x001172A3 File Offset: 0x001154A3
		public NKCGameOptionDataSt.GameOptionCameraShake CameraShakeLevel
		{
			get
			{
				return this.m_DataSt.m_CameraShakeLevel;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_CameraShakeLevel = value;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x001172B8 File Offset: 0x001154B8
		// (set) Token: 0x060035E6 RID: 13798 RVA: 0x001172C5 File Offset: 0x001154C5
		public bool UseGameEffect
		{
			get
			{
				return this.m_DataSt.m_bUseGameEffect;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bUseGameEffect = value;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060035E7 RID: 13799 RVA: 0x001172DA File Offset: 0x001154DA
		// (set) Token: 0x060035E8 RID: 13800 RVA: 0x001172E7 File Offset: 0x001154E7
		public bool UseCommonEffect
		{
			get
			{
				return this.m_DataSt.m_bUseCommonEffect;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bUseCommonEffect = value;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060035E9 RID: 13801 RVA: 0x001172FC File Offset: 0x001154FC
		// (set) Token: 0x060035EA RID: 13802 RVA: 0x00117309 File Offset: 0x00115509
		public bool UseHitEffect
		{
			get
			{
				return this.m_DataSt.m_bUseHitEffect;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bUseHitEffect = value;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x0011731E File Offset: 0x0011551E
		// (set) Token: 0x060035EC RID: 13804 RVA: 0x0011732B File Offset: 0x0011552B
		public int EffectOpacity
		{
			get
			{
				return this.m_DataSt.m_EffectOpacity;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_EffectOpacity = value;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x00117340 File Offset: 0x00115540
		// (set) Token: 0x060035EE RID: 13806 RVA: 0x0011734D File Offset: 0x0011554D
		public NKM_NATIONAL_CODE NKM_NATIONAL_CODE
		{
			get
			{
				return this.m_DataSt.m_eNKM_NATIONAL_CODE;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_eNKM_NATIONAL_CODE = value;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x00117362 File Offset: 0x00115562
		// (set) Token: 0x060035F0 RID: 13808 RVA: 0x00117365 File Offset: 0x00115565
		public bool UseKeyStringView
		{
			get
			{
				return false;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bKeyStringView = value;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060035F1 RID: 13809 RVA: 0x0011737A File Offset: 0x0011557A
		// (set) Token: 0x060035F2 RID: 13810 RVA: 0x0011737D File Offset: 0x0011557D
		public bool HideGameHud
		{
			get
			{
				return false;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bHideGameHud = value;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060035F3 RID: 13811 RVA: 0x00117392 File Offset: 0x00115592
		// (set) Token: 0x060035F4 RID: 13812 RVA: 0x0011739F File Offset: 0x0011559F
		public NKCGameOptionDataSt.GraphicOptionAnimationQuality AnimationQuality
		{
			get
			{
				return this.m_DataSt.m_AnimationQuality;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_AnimationQuality = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060035F5 RID: 13813 RVA: 0x001173B4 File Offset: 0x001155B4
		// (set) Token: 0x060035F6 RID: 13814 RVA: 0x001173C1 File Offset: 0x001155C1
		public NKCGameOptionDataSt.GraphicOptionGameFrameLimit GameFrameLimit
		{
			get
			{
				return this.m_DataSt.m_GameFrameLimit;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_GameFrameLimit = value;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x001173D6 File Offset: 0x001155D6
		// (set) Token: 0x060035F8 RID: 13816 RVA: 0x001173E3 File Offset: 0x001155E3
		public NKCGameOptionDataSt.GraphicOptionQualityLevel QualityLevel
		{
			get
			{
				return this.m_DataSt.m_QualityLevel;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_QualityLevel = value;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x001173F8 File Offset: 0x001155F8
		// (set) Token: 0x060035FA RID: 13818 RVA: 0x00117405 File Offset: 0x00115605
		public NKCGameOptionDataSt.GraphicOptionLoginCutin LoginCutin
		{
			get
			{
				return this.m_DataSt.m_LoginCutin;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_LoginCutin = value;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060035FB RID: 13819 RVA: 0x0011741A File Offset: 0x0011561A
		// (set) Token: 0x060035FC RID: 13820 RVA: 0x00117427 File Offset: 0x00115627
		public NKCGameOptionDataSt.GameOptionGraphicAspectRatio AspectRatio
		{
			get
			{
				return this.m_DataSt.m_AspectRatio;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_AspectRatio = value;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x0011743C File Offset: 0x0011563C
		// (set) Token: 0x060035FE RID: 13822 RVA: 0x00117449 File Offset: 0x00115649
		public NKC_GAME_OPTION_VOICE_LANGUAGE VoiceLanguage
		{
			get
			{
				return this.m_DataSt.m_eVoiceLanguage;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_eVoiceLanguage = value;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x0011745E File Offset: 0x0011565E
		// (set) Token: 0x06003600 RID: 13824 RVA: 0x0011746B File Offset: 0x0011566B
		public bool UseClassGuide
		{
			get
			{
				return this.m_DataSt.m_bUseClassGuide;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bUseClassGuide = value;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06003601 RID: 13825 RVA: 0x00117480 File Offset: 0x00115680
		// (set) Token: 0x06003602 RID: 13826 RVA: 0x0011748D File Offset: 0x0011568D
		public bool UseNpcSubtitle
		{
			get
			{
				return this.m_DataSt.m_bNpcSubtitle;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bNpcSubtitle = value;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06003603 RID: 13827 RVA: 0x001174A2 File Offset: 0x001156A2
		// (set) Token: 0x06003604 RID: 13828 RVA: 0x001174AF File Offset: 0x001156AF
		public bool UseShowNormalSubtitleAfterLifeTime
		{
			get
			{
				return this.m_DataSt.m_bShowNormalSubtitleAfterLifeTime;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bShowNormalSubtitleAfterLifeTime = value;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06003605 RID: 13829 RVA: 0x001174C4 File Offset: 0x001156C4
		// (set) Token: 0x06003606 RID: 13830 RVA: 0x001174D1 File Offset: 0x001156D1
		public bool UseEmoticonBlock
		{
			get
			{
				return this.m_DataSt.m_bUseEmoticonBlock;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bUseEmoticonBlock = value;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06003607 RID: 13831 RVA: 0x001174E6 File Offset: 0x001156E6
		// (set) Token: 0x06003608 RID: 13832 RVA: 0x001174F3 File Offset: 0x001156F3
		public NKCGameOptionDataSt.GameOptionDamageNumber UseDamageAndBuffNumberFx
		{
			get
			{
				return this.m_DataSt.m_bUseDamageAndBuffNumberFx;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bUseDamageAndBuffNumberFx = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003609 RID: 13833 RVA: 0x00117508 File Offset: 0x00115708
		// (set) Token: 0x0600360A RID: 13834 RVA: 0x00117515 File Offset: 0x00115715
		public bool UseMemoryOptimize
		{
			get
			{
				return this.m_DataSt.m_bMemoryOptimize;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bMemoryOptimize = value;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x0011752A File Offset: 0x0011572A
		// (set) Token: 0x0600360C RID: 13836 RVA: 0x00117537 File Offset: 0x00115737
		public bool UseChatContent
		{
			get
			{
				return this.m_DataSt.m_bUseChatContent;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bUseChatContent = value;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x0011754C File Offset: 0x0011574C
		// (set) Token: 0x0600360E RID: 13838 RVA: 0x00117559 File Offset: 0x00115759
		public bool UseChatNotifySound
		{
			get
			{
				return this.m_DataSt.m_bUseChatNotifySound;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bUseChatNotifySound = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x0011756E File Offset: 0x0011576E
		// (set) Token: 0x06003610 RID: 13840 RVA: 0x0011758E File Offset: 0x0011578E
		public bool UseVideoTexture
		{
			get
			{
				return !NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable() && this.m_DataSt.m_bUseVideoTexture;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_bUseVideoTexture = value;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x001175A3 File Offset: 0x001157A3
		// (set) Token: 0x06003612 RID: 13842 RVA: 0x001175B0 File Offset: 0x001157B0
		public bool SoundMute
		{
			get
			{
				return this.m_DataSt.m_bSoundMute;
			}
			set
			{
				this.m_DataSt.m_bSoundMute = value;
				this.m_bChanged = true;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06003613 RID: 13843 RVA: 0x001175C5 File Offset: 0x001157C5
		// (set) Token: 0x06003614 RID: 13844 RVA: 0x001175D2 File Offset: 0x001157D2
		public bool UseBuffEffect
		{
			get
			{
				return this.m_DataSt.m_bUseBuffEffect;
			}
			set
			{
				this.m_DataSt.m_bUseBuffEffect = value;
				this.m_bChanged = true;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06003615 RID: 13845 RVA: 0x001175E7 File Offset: 0x001157E7
		// (set) Token: 0x06003616 RID: 13846 RVA: 0x001175F4 File Offset: 0x001157F4
		public NKC_GAME_OPTION_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO
		{
			get
			{
				return this.m_DataSt.m_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO;
			}
			set
			{
				this.m_bChanged = true;
				this.m_DataSt.m_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO = value;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x00117609 File Offset: 0x00115809
		// (set) Token: 0x06003618 RID: 13848 RVA: 0x00117616 File Offset: 0x00115816
		public PrivatePvpInvitation ePrivatePvpInviteOption
		{
			get
			{
				return this.m_DataSt.m_ePrivatePvpInviteOption;
			}
			set
			{
				this.m_DataSt.m_ePrivatePvpInviteOption = value;
				this.m_bChangedPrivatePvpInvite = true;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06003619 RID: 13849 RVA: 0x0011762B File Offset: 0x0011582B
		// (set) Token: 0x0600361A RID: 13850 RVA: 0x00117638 File Offset: 0x00115838
		public NKCGameOptionDataSt.GameOptionStreamingOption eStreamingOption
		{
			get
			{
				return this.m_DataSt.m_eStreamingOption;
			}
			set
			{
				this.m_DataSt.m_eStreamingOption = value;
				this.m_bChanged = true;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600361B RID: 13851 RVA: 0x0011764D File Offset: 0x0011584D
		public bool StreamingHideMyInfo
		{
			get
			{
				return this.m_DataSt.m_eStreamingOption == NKCGameOptionDataSt.GameOptionStreamingOption.HideAll;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x0011765D File Offset: 0x0011585D
		public bool StreamingHideOpponentInfo
		{
			get
			{
				return this.m_DataSt.m_eStreamingOption > NKCGameOptionDataSt.GameOptionStreamingOption.Normal;
			}
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x0011766D File Offset: 0x0011586D
		public NKCGameOptionData()
		{
			this.Init();
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x0011767B File Offset: 0x0011587B
		public void Init()
		{
			this.m_bChanged = false;
			this.m_bChangedServerOption = false;
			this.m_bChangedPrivatePvpInvite = false;
			this.m_DataSt.Init();
			this.m_DefaultDataSt.Init();
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x001176A8 File Offset: 0x001158A8
		public int GetFrameLimit()
		{
			if (this.GameFrameLimit == NKCGameOptionDataSt.GraphicOptionGameFrameLimit.Thirty)
			{
				return 30;
			}
			return 60;
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x001176B7 File Offset: 0x001158B7
		public void SetUseActionCamera(bool bUse, bool bForce = false)
		{
			if (!bForce)
			{
				this.m_bChanged = true;
				this.m_bChangedServerOption = true;
			}
			this.m_DataSt.m_bActionCamera = bUse;
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x001176D6 File Offset: 0x001158D6
		public void SetUseTrackCamera(bool bUse, bool bForce = false)
		{
			if (!bForce)
			{
				this.m_bChanged = true;
				this.m_bChangedServerOption = true;
			}
			this.m_DataSt.m_bTrackCamera = bUse;
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x001176F5 File Offset: 0x001158F5
		public void SetViewSkillCutIn(bool bView, bool bForce = false)
		{
			if (!bForce)
			{
				this.m_bChanged = true;
				this.m_bChangedServerOption = true;
			}
			this.m_DataSt.m_bViewSkillCutIn = bView;
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x00117714 File Offset: 0x00115914
		public void SetNpcSubtitle(bool bView, bool bForce = false)
		{
			if (!bForce)
			{
				this.m_bChanged = true;
			}
			this.m_DataSt.m_bViewSkillCutIn = bView;
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x0011772C File Offset: 0x0011592C
		public void SetPvPAutoRespawn(NKM_GAME_AUTO_RESPAWN_TYPE type, bool bForce = false)
		{
			if (!bForce)
			{
				this.m_bChanged = true;
				this.m_bChangedServerOption = true;
			}
			this.m_DataSt.m_bUsePvPAutoRespawn = type;
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x0011774B File Offset: 0x0011594B
		public void SetAutoSyncFriendDeck(bool bUse, bool bForce = false)
		{
			if (!bForce)
			{
				this.m_bChanged = true;
				this.m_bChangedServerOption = true;
			}
			this.m_DataSt.m_bAutoSyncFriendDeck = bUse;
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x0011776A File Offset: 0x0011596A
		public int GetSoundVolume(NKC_GAME_OPTION_SOUND_GROUP type)
		{
			return this.m_DataSt.m_SoundVolumes[(int)type];
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x00117779 File Offset: 0x00115979
		public float GetSoundVolumeAsFloat(NKC_GAME_OPTION_SOUND_GROUP type)
		{
			return (float)this.m_DataSt.m_SoundVolumes[(int)type] / 100f;
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x00117790 File Offset: 0x00115990
		public void ChangeSoundVolume(NKC_GAME_OPTION_SOUND_GROUP type, int delta)
		{
			int num = this.m_DataSt.m_SoundVolumes[(int)type] + delta;
			if (num < 0)
			{
				num = 0;
			}
			if (num > 100)
			{
				num = 100;
			}
			this.SetSoundVolume(type, num);
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x001177C3 File Offset: 0x001159C3
		public void SetSoundVolume(NKC_GAME_OPTION_SOUND_GROUP type, int volume)
		{
			this.m_bChanged = true;
			this.m_DataSt.m_SoundVolumes[(int)type] = volume;
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x001177DA File Offset: 0x001159DA
		public bool GetAllowAlarm(NKC_GAME_OPTION_ALARM_GROUP type)
		{
			return this.m_DataSt.m_bAllowAlarms[(int)type];
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x001177E9 File Offset: 0x001159E9
		public void SetAllowAlarm(NKC_GAME_OPTION_ALARM_GROUP type, bool allow)
		{
			this.m_bChanged = true;
			this.m_DataSt.m_bAllowAlarms[(int)type] = allow;
			NKCPublisherModule.Push.SetAlarm(type, allow);
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x0011780C File Offset: 0x00115A0C
		public string GetAccountLocalSaveKey(string defaultKey)
		{
			string result = "";
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				result = defaultKey + myUserData.m_UserUID.ToString();
			}
			return result;
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x00117840 File Offset: 0x00115A40
		public bool GetAllowPush(NKC_GAME_OPTION_PUSH_GROUP type)
		{
			return this.m_DataSt.m_bAllowPushes[(int)type];
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x0011784F File Offset: 0x00115A4F
		public void SetAllowPush(NKC_GAME_OPTION_PUSH_GROUP type, bool bAllow)
		{
			this.m_bChanged = true;
			this.m_DataSt.m_bAllowPushes[(int)type] = bAllow;
			this.SaveOnlyPush();
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x0011786C File Offset: 0x00115A6C
		public void SetAllPush(bool bPush, bool bPushAd, bool bPushAdNight)
		{
			this.m_DataSt.m_bAllowPushes[0] = bPush;
			this.SaveOnlyPush();
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x00117882 File Offset: 0x00115A82
		public void SaveOnlyPush()
		{
			PlayerPrefs.SetString("NKM_LOCAL_SAVE_GAME_OPTION_ALLOW_PUSHES", string.Join<bool>(":", this.m_DataSt.m_bAllowPushes));
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x001178A3 File Offset: 0x00115AA3
		public static NKM_NATIONAL_CODE LoadLanguageCode(NKM_NATIONAL_CODE defaultValue = NKM_NATIONAL_CODE.NNC_KOREA)
		{
			return (NKM_NATIONAL_CODE)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_LANG", (int)defaultValue);
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x001178B1 File Offset: 0x00115AB1
		public static void DeleteLanguageCode()
		{
			PlayerPrefs.DeleteKey("NKM_LOCAL_SAVE_GAME_OPTION_GAME_LANG");
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x001178BD File Offset: 0x00115ABD
		public static void SaveOnlyLang(NKM_NATIONAL_CODE eCode)
		{
			PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_LANG", (int)eCode);
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x001178CC File Offset: 0x00115ACC
		public void Save()
		{
			if (this.m_bChanged)
			{
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_DISABLED_GRAPHIC_QUALITY", this.m_DataSt.m_bDisabledGraphicQuality ? 1 : 0);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_QUALITY", (int)this.m_DataSt.m_eGraphicQuality);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_CAMERA_SHAKE", (int)this.m_DataSt.m_CameraShakeLevel);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_GAME_EFFECT", this.m_DataSt.m_bUseGameEffect ? 1 : 0);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_COMMON_EFFECT", this.m_DataSt.m_bUseCommonEffect ? 1 : 0);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_ANIMATION_QUALITY", (int)this.m_DataSt.m_AnimationQuality);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_GAME_FRAME_LIMIT", (int)this.m_DataSt.m_GameFrameLimit);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_QUALITY_LEVEL", (int)this.m_DataSt.m_QualityLevel);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_LOGIN_CUTIN", (int)this.m_DataSt.m_LoginCutin);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_ASPECT_RATIO", (int)this.m_DataSt.m_AspectRatio);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_HIT_EFFECT", this.m_DataSt.m_bUseHitEffect ? 1 : 0);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_EFFECT_OPACITY", this.m_DataSt.m_EffectOpacity);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_CLASS_GUIDE", this.m_DataSt.m_bUseClassGuide ? 1 : 0);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_EMOTICON_BLOCK_VER2", this.m_DataSt.m_bUseEmoticonBlock ? 1 : 0);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_DAMAGE_AND_BUFF_NUMBER_FX", (int)this.m_DataSt.m_bUseDamageAndBuffNumberFx);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_VIDEO_TEXTURE", this.m_DataSt.m_bUseVideoTexture ? 1 : 0);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO", (int)this.m_DataSt.m_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_NPC_SUBTITLE", this.m_DataSt.m_bNpcSubtitle ? 1 : 0);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_NPC_SUBTITLE_SHOW_NORMAL_AFTER_LIFE_TIME", this.m_DataSt.m_bShowNormalSubtitleAfterLifeTime ? 1 : 0);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_MEMORY_OPTIMIZE", this.m_DataSt.m_bMemoryOptimize ? 1 : 0);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_STREAMING_OPTION", (int)this.m_DataSt.m_eStreamingOption);
				NKCGameOptionData.SaveOnlyLang(this.m_DataSt.m_eNKM_NATIONAL_CODE);
				PlayerPrefs.SetString("NKM_LOCAL_SAVE_GAME_OPTION_SOUND_VOLUMES", string.Join<int>(":", this.m_DataSt.m_SoundVolumes));
				this.SaveOptionValue("NKM_LOCAL_SAVE_GAME_OPTION_SOUND_MUTE", this.m_DataSt.m_bSoundMute);
				this.SaveOptionValue("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_BUFF_EFFECT", this.m_DataSt.m_bUseBuffEffect);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_VOICE_LANGUAGE", (int)this.m_DataSt.m_eVoiceLanguage);
				PlayerPrefs.SetString(this.GetAccountLocalSaveKey("NKM_LOCAL_SAVE_GAME_OPTION_ALLOW_ALARMS_"), string.Join<bool>(":", this.m_DataSt.m_bAllowAlarms));
				PlayerPrefs.SetString("NKM_LOCAL_SAVE_GAME_OPTION_ALLOW_PUSHES", string.Join<bool>(":", this.m_DataSt.m_bAllowPushes));
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_CHAT_NOTIFY", this.m_DataSt.m_bUseChatContent ? 1 : 0);
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_GAME_OPTION_CHAT_NOTIFY_SOUND", this.m_DataSt.m_bUseChatNotifySound ? 1 : 0);
				this.ApplyToGame();
				this.m_bChanged = false;
				this.m_bChangedServerOption = false;
				this.m_bChangedPrivatePvpInvite = false;
			}
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x00117BDE File Offset: 0x00115DDE
		private void SaveOptionValue(string prefKey, bool value)
		{
			PlayerPrefs.SetInt(prefKey, value ? 1 : 0);
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x00117BED File Offset: 0x00115DED
		private bool LoadOptionValue(string prefKey, bool defaultValue)
		{
			return PlayerPrefs.GetInt(prefKey, defaultValue ? 1 : 0) == 1;
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x00117C00 File Offset: 0x00115E00
		public void LoadLocal()
		{
			this.m_DataSt.m_bDisabledGraphicQuality = (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_DISABLED_GRAPHIC_QUALITY", 0) == 1);
			this.m_DataSt.m_eGraphicQuality = (NKC_GAME_OPTION_GRAPHIC_QUALITY)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_QUALITY", 3);
			this.m_DataSt.m_CameraShakeLevel = (NKCGameOptionDataSt.GameOptionCameraShake)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_CAMERA_SHAKE", 1);
			this.m_DataSt.m_bUseGameEffect = (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_GAME_EFFECT", 0) == 1);
			this.m_DataSt.m_bUseCommonEffect = (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_COMMON_EFFECT", 0) == 1);
			this.m_DataSt.m_AnimationQuality = (NKCGameOptionDataSt.GraphicOptionAnimationQuality)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_ANIMATION_QUALITY", 0);
			this.m_DataSt.m_AspectRatio = (NKCGameOptionDataSt.GameOptionGraphicAspectRatio)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_ASPECT_RATIO", 1);
			this.m_DataSt.m_GameFrameLimit = (NKCGameOptionDataSt.GraphicOptionGameFrameLimit)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_GAME_FRAME_LIMIT", 1);
			this.m_DataSt.m_QualityLevel = (NKCGameOptionDataSt.GraphicOptionQualityLevel)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_QUALITY_LEVEL", 1);
			this.m_DataSt.m_LoginCutin = (NKCGameOptionDataSt.GraphicOptionLoginCutin)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_LOGIN_CUTIN", 0);
			this.m_DataSt.m_bUseBuffEffect = this.LoadOptionValue("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_BUFF_EFFECT", true);
			this.m_DataSt.m_bUseHitEffect = (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_HIT_EFFECT", 1) == 1);
			this.m_DataSt.m_EffectOpacity = PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_EFFECT_OPACITY", 100);
			this.m_DataSt.m_eNKM_NATIONAL_CODE = NKCGameOptionData.LoadLanguageCode(NKM_NATIONAL_CODE.NNC_KOREA);
			this.m_DataSt.m_bUseEmoticonBlock = (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_EMOTICON_BLOCK_VER2", 0) == 1);
			this.m_DataSt.m_bUseDamageAndBuffNumberFx = (NKCGameOptionDataSt.GameOptionDamageNumber)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_DAMAGE_AND_BUFF_NUMBER_FX", 0);
			this.m_DataSt.m_bUseVideoTexture = (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_VIDEO_TEXTURE", 1) == 1);
			this.m_DataSt.m_bMemoryOptimize = (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_MEMORY_OPTIMIZE", 1) == 1);
			this.m_DataSt.m_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO = (NKC_GAME_OPTION_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO", 0);
			this.m_DataSt.m_bNpcSubtitle = (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_NPC_SUBTITLE", 1) == 1);
			this.m_DataSt.m_bShowNormalSubtitleAfterLifeTime = (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_NPC_SUBTITLE_SHOW_NORMAL_AFTER_LIFE_TIME", 0) == 1);
			if (!PlayerPrefs.HasKey("NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_CLASS_GUIDE"))
			{
				this.m_DataSt.m_bUseClassGuide = true;
			}
			else
			{
				this.m_DataSt.m_bUseClassGuide = (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_CLASS_GUIDE", 0) == 1);
			}
			if (!PlayerPrefs.HasKey("NKM_LOCAL_SAVE_GAME_OPTION_CHAT_NOTIFY"))
			{
				this.m_DataSt.m_bUseChatContent = true;
			}
			else
			{
				this.m_DataSt.m_bUseChatContent = (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_CHAT_NOTIFY", 0) == 1);
			}
			if (!PlayerPrefs.HasKey("NKM_LOCAL_SAVE_GAME_OPTION_CHAT_NOTIFY_SOUND"))
			{
				this.m_DataSt.m_bUseChatNotifySound = true;
			}
			else
			{
				this.m_DataSt.m_bUseChatNotifySound = (PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_CHAT_NOTIFY_SOUND", 0) == 1);
			}
			string @string = PlayerPrefs.GetString("NKM_LOCAL_SAVE_GAME_OPTION_SOUND_VOLUMES", "");
			if (@string != "")
			{
				string[] array = @string.Split(new char[]
				{
					':'
				});
				int num = 0;
				while (num < array.Length && num < this.m_DataSt.m_SoundVolumes.Length)
				{
					this.m_DataSt.m_SoundVolumes[num] = int.Parse(array[num]);
					num++;
				}
			}
			this.m_DataSt.m_bSoundMute = this.LoadOptionValue("NKM_LOCAL_SAVE_GAME_OPTION_SOUND_MUTE", false);
			this.m_DataSt.m_eVoiceLanguage = (NKC_GAME_OPTION_VOICE_LANGUAGE)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_VOICE_LANGUAGE", 0);
			this.m_DataSt.m_eStreamingOption = (NKCGameOptionDataSt.GameOptionStreamingOption)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GAME_STREAMING_OPTION", 0);
			string string2 = PlayerPrefs.GetString("NKM_LOCAL_SAVE_GAME_OPTION_ALLOW_PUSHES", "");
			if (string2 != "")
			{
				string[] array2 = string2.Split(new char[]
				{
					':'
				});
				int num2 = 0;
				while (num2 < array2.Length && this.m_DataSt.m_bAllowPushes.Length > num2)
				{
					bool flag;
					if (bool.TryParse(array2[num2], out flag))
					{
						this.m_DataSt.m_bAllowPushes[num2] = flag;
					}
					else
					{
						this.m_DataSt.m_bAllowPushes[num2] = true;
					}
					num2++;
				}
			}
			this.ApplyToGame();
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x00117FEA File Offset: 0x001161EA
		private void SetSpineHalfUpdate()
		{
			SkeletonGraphic.UseHalfUpdate = (this.m_DataSt.m_GameFrameLimit == NKCGameOptionDataSt.GraphicOptionGameFrameLimit.Sixty && this.m_DataSt.m_AnimationQuality != NKCGameOptionDataSt.GraphicOptionAnimationQuality.High);
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x00118014 File Offset: 0x00116214
		public void LoadAccountLocal(NKMUserOption cNKMUserOption)
		{
			this.m_DataSt.m_bActionCamera = cNKMUserOption.m_bActionCamera;
			this.m_DataSt.m_bTrackCamera = cNKMUserOption.m_bTrackCamera;
			this.m_DataSt.m_bViewSkillCutIn = cNKMUserOption.m_bViewSkillCutIn;
			this.m_DataSt.m_bUsePvPAutoRespawn = cNKMUserOption.m_bDefaultPvpAutoRespawn;
			this.m_DataSt.m_bAutoSyncFriendDeck = cNKMUserOption.m_bAutoSyncFriendDeck;
			this.m_DataSt.m_ePrivatePvpInviteOption = cNKMUserOption.privatePvpInvitation;
			string @string = PlayerPrefs.GetString(this.GetAccountLocalSaveKey("NKM_LOCAL_SAVE_GAME_OPTION_ALLOW_ALARMS_"), "");
			if (@string != "")
			{
				string[] array = @string.Split(new char[]
				{
					':'
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (this.m_DataSt.m_bAllowAlarms.Length <= i)
					{
						return;
					}
					bool flag;
					if (bool.TryParse(array[i], out flag))
					{
						this.m_DataSt.m_bAllowAlarms[i] = flag;
					}
					else
					{
						this.m_DataSt.m_bAllowAlarms[i] = true;
					}
				}
				return;
			}
			if (NKCPublisherModule.IsZlongPublished())
			{
				for (int j = 0; j < 7; j++)
				{
					this.m_DataSt.m_bAllowAlarms[j] = true;
				}
			}
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x0011812B File Offset: 0x0011632B
		public void Rollback()
		{
			this.m_DataSt.DeepCopyFromSource(this.m_DefaultDataSt);
			this.m_bChanged = true;
			this.m_bChangedServerOption = true;
			this.m_bChangedPrivatePvpInvite = true;
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x00118154 File Offset: 0x00116354
		public void SetAllLocalAlarm(bool bAllow)
		{
			foreach (object obj in Enum.GetValues(typeof(NKC_GAME_OPTION_ALARM_GROUP)))
			{
				NKC_GAME_OPTION_ALARM_GROUP nkc_GAME_OPTION_ALARM_GROUP = (NKC_GAME_OPTION_ALARM_GROUP)obj;
				if (nkc_GAME_OPTION_ALARM_GROUP != NKC_GAME_OPTION_ALARM_GROUP.MAX)
				{
					this.SetAllowAlarm(nkc_GAME_OPTION_ALARM_GROUP, bAllow);
				}
			}
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x001181BC File Offset: 0x001163BC
		public void SetGameOptionDataByGrahpicQuality(NKC_GAME_OPTION_GRAPHIC_QUALITY quality)
		{
			this.GraphicQuality = quality;
			switch (quality)
			{
			case NKC_GAME_OPTION_GRAPHIC_QUALITY.VERY_LOW:
				this.UseGameEffect = false;
				this.UseCommonEffect = false;
				this.UseHitEffect = false;
				this.UseBuffEffect = false;
				this.AnimationQuality = NKCGameOptionDataSt.GraphicOptionAnimationQuality.Normal;
				this.GameFrameLimit = NKCGameOptionDataSt.GraphicOptionGameFrameLimit.Thirty;
				this.QualityLevel = NKCGameOptionDataSt.GraphicOptionQualityLevel.Low;
				this.UseVideoTexture = false;
				return;
			case NKC_GAME_OPTION_GRAPHIC_QUALITY.LOW:
				this.UseGameEffect = false;
				this.UseCommonEffect = false;
				this.UseHitEffect = false;
				this.UseBuffEffect = false;
				this.AnimationQuality = NKCGameOptionDataSt.GraphicOptionAnimationQuality.Normal;
				this.GameFrameLimit = NKCGameOptionDataSt.GraphicOptionGameFrameLimit.Sixty;
				this.QualityLevel = NKCGameOptionDataSt.GraphicOptionQualityLevel.High;
				this.UseVideoTexture = true;
				return;
			case NKC_GAME_OPTION_GRAPHIC_QUALITY.NORMAL:
				this.UseGameEffect = false;
				this.UseCommonEffect = false;
				this.UseHitEffect = true;
				this.UseBuffEffect = true;
				this.AnimationQuality = NKCGameOptionDataSt.GraphicOptionAnimationQuality.Normal;
				this.GameFrameLimit = NKCGameOptionDataSt.GraphicOptionGameFrameLimit.Sixty;
				this.QualityLevel = NKCGameOptionDataSt.GraphicOptionQualityLevel.High;
				this.UseVideoTexture = true;
				return;
			case NKC_GAME_OPTION_GRAPHIC_QUALITY.HIGH:
				this.UseGameEffect = false;
				this.UseCommonEffect = false;
				this.UseHitEffect = true;
				this.UseBuffEffect = true;
				this.AnimationQuality = NKCGameOptionDataSt.GraphicOptionAnimationQuality.Normal;
				this.GameFrameLimit = NKCGameOptionDataSt.GraphicOptionGameFrameLimit.Sixty;
				this.QualityLevel = NKCGameOptionDataSt.GraphicOptionQualityLevel.High;
				this.UseVideoTexture = true;
				return;
			case NKC_GAME_OPTION_GRAPHIC_QUALITY.VERY_HIGH:
				this.UseGameEffect = true;
				this.UseCommonEffect = true;
				this.UseHitEffect = true;
				this.UseBuffEffect = true;
				this.AnimationQuality = NKCGameOptionDataSt.GraphicOptionAnimationQuality.High;
				this.GameFrameLimit = NKCGameOptionDataSt.GraphicOptionGameFrameLimit.Sixty;
				this.QualityLevel = NKCGameOptionDataSt.GraphicOptionQualityLevel.High;
				this.UseVideoTexture = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x00118308 File Offset: 0x00116508
		public void ApplyToGame()
		{
			NKCCamera.SetBloomEnableUI(this.UseCommonEffect);
			Application.targetFrameRate = this.GetFrameLimit();
			if (QualitySettings.GetQualityLevel() != (int)this.QualityLevel)
			{
				QualitySettings.SetQualityLevel((int)this.QualityLevel, true);
			}
			SkeletonGraphic.UseHalfUpdate = (this.m_DataSt.m_GameFrameLimit == NKCGameOptionDataSt.GraphicOptionGameFrameLimit.Sixty && this.m_DataSt.m_AnimationQuality != NKCGameOptionDataSt.GraphicOptionAnimationQuality.High);
			NKCSoundManager.SetMute(this.SoundMute, false);
			NKCPacketObjectPool.SetUsePool(this.m_DataSt.m_bMemoryOptimize);
		}

		// Token: 0x040033A5 RID: 13221
		private const string NKM_LOCAL_SAVE_GAME_OPTION_DISABLED_GRAPHIC_QUALITY = "NKM_LOCAL_SAVE_GAME_OPTION_DISABLED_GRAPHIC_QUALITY";

		// Token: 0x040033A6 RID: 13222
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_QUALITY = "NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_QUALITY";

		// Token: 0x040033A7 RID: 13223
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_CAMERA_SHAKE = "NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_CAMERA_SHAKE";

		// Token: 0x040033A8 RID: 13224
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_GAME_EFFECT = "NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_GAME_EFFECT";

		// Token: 0x040033A9 RID: 13225
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_COMMON_EFFECT = "NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_COMMON_EFFECT";

		// Token: 0x040033AA RID: 13226
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_ANIMATION_QUALITY = "NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_ANIMATION_QUALITY";

		// Token: 0x040033AB RID: 13227
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_GAME_FRAME_LIMIT = "NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_GAME_FRAME_LIMIT";

		// Token: 0x040033AC RID: 13228
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_QUALITY_LEVEL = "NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_QUALITY_LEVEL";

		// Token: 0x040033AD RID: 13229
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_LOGIN_CUTIN = "NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_LOGIN_CUTIN";

		// Token: 0x040033AE RID: 13230
		public const string NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_ASPECT_RATIO = "NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_ASPECT_RATIO";

		// Token: 0x040033AF RID: 13231
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_BUFF_EFFECT = "NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_BUFF_EFFECT";

		// Token: 0x040033B0 RID: 13232
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_EFFECT_OPACITY = "NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_EFFECT_OPACITY";

		// Token: 0x040033B1 RID: 13233
		public const string NKM_LOCAL_SAVE_GAME_OPTION_SOUND_VOLUMES = "NKM_LOCAL_SAVE_GAME_OPTION_SOUND_VOLUMES";

		// Token: 0x040033B2 RID: 13234
		public const string NKM_LOCAL_SAVE_GAME_OPTION_SOUND_MUTE = "NKM_LOCAL_SAVE_GAME_OPTION_SOUND_MUTE";

		// Token: 0x040033B3 RID: 13235
		private const string NKM_LOCAL_SAVE_GAME_OPTION_VOICE_LANGUAGE = "NKM_LOCAL_SAVE_GAME_OPTION_VOICE_LANGUAGE";

		// Token: 0x040033B4 RID: 13236
		private const string NKM_LOCAL_SAVE_GAME_OPTION_ALLOW_ALARMS = "NKM_LOCAL_SAVE_GAME_OPTION_ALLOW_ALARMS_";

		// Token: 0x040033B5 RID: 13237
		private const string NKM_LOCAL_SAVE_GAME_OPTION_ALLOW_PUSHES = "NKM_LOCAL_SAVE_GAME_OPTION_ALLOW_PUSHES";

		// Token: 0x040033B6 RID: 13238
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_HIT_EFFECT = "NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_HIT_EFFECT";

		// Token: 0x040033B7 RID: 13239
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GAME_LANG = "NKM_LOCAL_SAVE_GAME_OPTION_GAME_LANG";

		// Token: 0x040033B8 RID: 13240
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GAME_LANG_KEY_STR_VIEW = "NKM_LOCAL_SAVE_GAME_OPTION_GAME_LANG_KEY_STR_VIEW";

		// Token: 0x040033B9 RID: 13241
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_CLASS_GUIDE = "NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_CLASS_GUIDE";

		// Token: 0x040033BA RID: 13242
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_EMOTICON_BLOCK = "NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_EMOTICON_BLOCK_VER2";

		// Token: 0x040033BB RID: 13243
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_DAMAGE_BUFF_NUMBER_FX = "NKM_LOCAL_SAVE_GAME_OPTION_GAME_USE_DAMAGE_AND_BUFF_NUMBER_FX";

		// Token: 0x040033BC RID: 13244
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_VIDEO_TEXTURE = "NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_USE_VIDEO_TEXTURE";

		// Token: 0x040033BD RID: 13245
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GAME_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO = "NKM_LOCAL_SAVE_GAME_OPTION_GAME_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO";

		// Token: 0x040033BE RID: 13246
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GAME_NPC_SUBTITLE = "NKM_LOCAL_SAVE_GAME_OPTION_GAME_NPC_SUBTITLE";

		// Token: 0x040033BF RID: 13247
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GAME_NPC_SUBTITLE_SHOW_NORMAL_AFTER_LIFE_TIME = "NKM_LOCAL_SAVE_GAME_OPTION_GAME_NPC_SUBTITLE_SHOW_NORMAL_AFTER_LIFE_TIME";

		// Token: 0x040033C0 RID: 13248
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GAME_MEMORY_OPTIMIZE = "NKM_LOCAL_SAVE_GAME_OPTION_GAME_MEMORY_OPTIMIZE";

		// Token: 0x040033C1 RID: 13249
		private const string NKM_LOCAL_SAVE_GAME_OPTION_GAME_STREAMING_OPTION = "NKM_LOCAL_SAVE_GAME_OPTION_GAME_STREAMING_OPTION";

		// Token: 0x040033C2 RID: 13250
		private const string NKM_LOCAL_SAVE_GAME_OPTION_CHAT_NOTIFY = "NKM_LOCAL_SAVE_GAME_OPTION_CHAT_NOTIFY";

		// Token: 0x040033C3 RID: 13251
		private const string NKM_LOCAL_SAVE_GAME_OPTION_CHAT_NOTIFY_SOUND = "NKM_LOCAL_SAVE_GAME_OPTION_CHAT_NOTIFY_SOUND";

		// Token: 0x040033C4 RID: 13252
		private bool m_bChanged;

		// Token: 0x040033C5 RID: 13253
		private bool m_bChangedServerOption;

		// Token: 0x040033C6 RID: 13254
		private bool m_bChangedPrivatePvpInvite;

		// Token: 0x040033C7 RID: 13255
		private NKCGameOptionDataSt m_DataSt;

		// Token: 0x040033C8 RID: 13256
		private NKCGameOptionDataSt m_DefaultDataSt;
	}
}
