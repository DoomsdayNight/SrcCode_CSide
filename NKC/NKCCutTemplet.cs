using System;
using DG.Tweening;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000663 RID: 1635
	public class NKCCutTemplet
	{
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x0010119B File Offset: 0x000FF39B
		public CutUnitPosType CutUnitPos
		{
			get
			{
				return this.m_cutUnitPosType;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x001011A3 File Offset: 0x000FF3A3
		public CutUnitPosAction CutUnitAction
		{
			get
			{
				return this.m_cutUnitPosAction;
			}
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x001011AC File Offset: 0x000FF3AC
		public string GetActionFirstToken()
		{
			string[] actionTokens = this.GetActionTokens();
			if (actionTokens.Length != 0)
			{
				return actionTokens[0];
			}
			return "";
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x001011CD File Offset: 0x000FF3CD
		public string[] GetActionTokens()
		{
			return this.m_ActionStrKey.Split(NKCCutTemplet.Seperator, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x001013E0 File Offset: 0x000FF5E0
		public bool LoadFromLUA(NKMLua cNKMLua, string cutSceneStrID, int index)
		{
			string nationalPostfix = NKCStringTable.GetNationalPostfix(NKCStringTable.GetNationalCode());
			cNKMLua.GetData("m_bWaitClick", ref this.m_bWaitClick);
			cNKMLua.GetData("m_fWaitTime", ref this.m_fWaitTime);
			cNKMLua.GetData("m_bTitleClear", ref this.m_bTitleClear);
			cNKMLua.GetData("m_bTitleFadeOut", ref this.m_bTitleFadeOut);
			cNKMLua.GetData("m_fTitleFadeOutTime", ref this.m_fTitleFadeOutTime);
			cNKMLua.GetData("m_Title" + nationalPostfix, ref this.m_Title);
			cNKMLua.GetData("m_fTitleTalkTime", ref this.m_fTitleTalkTime);
			cNKMLua.GetData("m_SubTitle" + nationalPostfix, ref this.m_SubTitle);
			cNKMLua.GetData("m_fSubTitleTalkTime", ref this.m_fSubTitleTalkTime);
			cNKMLua.GetData("m_bTalkCenterFadeIn", ref this.m_bTalkCenterFadeIn);
			cNKMLua.GetData("m_bTalkCenterFadeOut", ref this.m_bTalkCenterFadeOut);
			cNKMLua.GetData("m_bTalkCenterFadeTime", ref this.m_bTalkCenterFadeTime);
			cNKMLua.GetData<NKC_CUTSCEN_FILTER_TYPE>("m_FilterType", ref this.m_FilterType);
			cNKMLua.GetData("m_fBGFadeInTime", ref this.m_fBGFadeInTime);
			cNKMLua.GetData<Ease>("m_easeBGFadeIn", ref this.m_easeBGFadeIn);
			if (cNKMLua.OpenTable("m_colBGFadeInStart"))
			{
				cNKMLua.GetData(1, ref this.m_colBGFadeInStart.r);
				cNKMLua.GetData(2, ref this.m_colBGFadeInStart.g);
				cNKMLua.GetData(3, ref this.m_colBGFadeInStart.b);
				cNKMLua.GetData(4, ref this.m_colBGFadeInStart.a);
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_colBGFadeIn"))
			{
				cNKMLua.GetData(1, ref this.m_colBGFadeIn.r);
				cNKMLua.GetData(2, ref this.m_colBGFadeIn.g);
				cNKMLua.GetData(3, ref this.m_colBGFadeIn.b);
				cNKMLua.GetData(4, ref this.m_colBGFadeIn.a);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_fBGFadeOutTime", ref this.m_fBGFadeOutTime);
			cNKMLua.GetData<Ease>("m_easeBGFadeOut", ref this.m_easeBGFadeOut);
			if (cNKMLua.OpenTable("m_colBGFadeOut"))
			{
				cNKMLua.GetData(1, ref this.m_colBGFadeOut.r);
				cNKMLua.GetData(2, ref this.m_colBGFadeOut.g);
				cNKMLua.GetData(3, ref this.m_colBGFadeOut.b);
				cNKMLua.GetData(4, ref this.m_colBGFadeOut.a);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bGameObjectBGType", ref this.m_bGameObjectBGType);
			cNKMLua.GetData("m_BGFileName", ref this.m_BGFileName);
			cNKMLua.GetData("m_GameObjectBGAniName", ref this.m_GameObjectBGAniName);
			cNKMLua.GetData("m_bGameObjectBGLoop", ref this.m_bGameObjectBGLoop);
			cNKMLua.GetData("m_bNoWaitBGAni", ref this.m_bNoWaitBGAni);
			cNKMLua.GetData("m_BGCrash", ref this.m_BGCrash);
			cNKMLua.GetData("m_fBGCrashTime", ref this.m_fBGCrashTime);
			cNKMLua.GetData("m_fBGAnITime", ref this.m_fBGAnITime);
			cNKMLua.GetData("m_bBGAniPos", ref this.m_bBGAniPos);
			if (cNKMLua.OpenTable("m_BGOffsetPose"))
			{
				cNKMLua.GetData(1, ref this.m_BGOffsetPose.x);
				cNKMLua.GetData(2, ref this.m_BGOffsetPose.y);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData<TRACKING_DATA_TYPE>("m_tdtBGPos", ref this.m_tdtBGPos);
			cNKMLua.GetData("m_bBGAniScale", ref this.m_bBGAniScale);
			if (cNKMLua.OpenTable("m_BGOffsetScale"))
			{
				cNKMLua.GetData(1, ref this.m_BGOffsetScale.x);
				cNKMLua.GetData(2, ref this.m_BGOffsetScale.y);
				cNKMLua.GetData(3, ref this.m_BGOffsetScale.z);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData<TRACKING_DATA_TYPE>("m_tdtBGScale", ref this.m_tdtBGScale);
			cNKMLua.GetData("m_bLooseShake", ref this.m_bLooseShake);
			cNKMLua.GetData("m_bFadeIn", ref this.m_bFadeIn);
			cNKMLua.GetData("m_fFadeTime", ref this.m_fFadeTime);
			cNKMLua.GetData("m_bFadeWhite", ref this.m_bFadeWhite);
			cNKMLua.GetData("m_fFlashBangTime", ref this.m_fFlashBangTime);
			cNKMLua.GetData("m_StartBGMFileName", ref this.m_StartBGMFileName);
			cNKMLua.GetData("m_EndBGMFileName", ref this.m_EndBGMFileName);
			if (!cNKMLua.GetDataEnum<NKC_CUTSCEN_SOUND_CONTROL>("m_StartFXSoundControl", out this.m_StartFXSoundControl))
			{
				this.m_StartFXSoundControl = NKC_CUTSCEN_SOUND_CONTROL.NCSC_ONE_TIME_PLAY;
			}
			cNKMLua.GetData("m_StartFXSoundName", ref this.m_StartFXSoundName);
			if (!cNKMLua.GetDataEnum<NKC_CUTSCEN_SOUND_CONTROL>("m_EndFXSoundControl", out this.m_EndFXSoundControl))
			{
				this.m_EndFXSoundControl = NKC_CUTSCEN_SOUND_CONTROL.NCSC_ONE_TIME_PLAY;
			}
			cNKMLua.GetData("m_EndFXSoundName", ref this.m_EndFXSoundName);
			cNKMLua.GetData("m_VoiceFileName", ref this.m_VoiceFileName);
			cNKMLua.GetData("m_CharStrID", ref this.m_CharStrID);
			cNKMLua.GetData("m_bClear", ref this.m_bClear);
			cNKMLua.GetData("m_bFlip", ref this.m_bFlip);
			cNKMLua.GetData("m_Pos", ref this.m_Pos);
			this.ParsePosCommand(cutSceneStrID, index);
			if (cNKMLua.OpenTable("m_CharOffSet"))
			{
				cNKMLua.GetData(1, ref this.m_CharOffSet.x);
				cNKMLua.GetData(2, ref this.m_CharOffSet.y);
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_Bounce"))
			{
				cNKMLua.GetData(1, ref this.m_BounceCount);
				cNKMLua.GetData(2, ref this.m_BounceTime);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_TrackingTime", ref this.m_TrackingTime);
			cNKMLua.GetData("m_bCharHologramEffect", ref this.m_bCharHologramEffect);
			cNKMLua.GetData("m_bCharPinup", ref this.m_bCharPinup);
			cNKMLua.GetData("m_bCharPinupEasingTime", ref this.m_bCharPinupEasingTime);
			cNKMLua.GetData("m_bCharFadeIn", ref this.m_bCharFadeIn);
			cNKMLua.GetData("m_bCharFadeOut", ref this.m_bCharFadeOut);
			if (cNKMLua.OpenTable("m_CharScale"))
			{
				cNKMLua.GetData(1, ref this.m_CharScale.x);
				cNKMLua.GetData(2, ref this.m_CharScale.y);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_CharScaleTime", ref this.m_CharScaleTime);
			cNKMLua.GetData<NKCASUIUnitIllust.eAnimation>("m_Face", ref this.m_Face);
			cNKMLua.GetData("m_bFaceLoop", ref this.m_bFaceLoop);
			cNKMLua.GetData("m_Crash", ref this.m_Crash);
			cNKMLua.GetData("m_Talk" + nationalPostfix, ref this.m_Talk);
			cNKMLua.GetData("m_fTalkTime", ref this.m_fTalkTime);
			cNKMLua.GetData("m_bTalkAppend", ref this.m_bTalkAppend);
			cNKMLua.GetData("m_ImageName", ref this.m_ImageName);
			cNKMLua.GetData("m_fImageScale", ref this.m_fImageScale);
			cNKMLua.GetData("m_CloseTalkBox", ref this.m_CloseTalkBox);
			if (cNKMLua.OpenTable("m_ImageOffsetPos"))
			{
				cNKMLua.GetData(1, ref this.m_ImageOffsetPos.x);
				cNKMLua.GetData(2, ref this.m_ImageOffsetPos.y);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bMovieSkipEnable", ref this.m_bMovieSkipEnable);
			cNKMLua.GetData("m_MovieName", ref this.m_MovieName);
			if (cNKMLua.GetDataEnum<NKCCutTemplet.eCutsceneAction>("m_Action", out this.m_Action))
			{
				cNKMLua.GetData("m_ActionStrKey", ref this.m_ActionStrKey);
			}
			return true;
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x00101B3C File Offset: 0x000FFD3C
		public void ParsePosCommand(string cutSceneStrID, int index)
		{
			if (string.IsNullOrEmpty(this.m_Pos))
			{
				return;
			}
			string[] array = this.m_Pos.Split(new char[]
			{
				'_'
			});
			int num = 0;
			string text = array[0];
			if (text != null)
			{
				float num2;
				if (!(text == "L"))
				{
					if (!(text == "R"))
					{
						if (!(text == "C"))
						{
							goto IL_8A;
						}
						this.m_cutUnitPosType = CutUnitPosType.CENTER;
						num2 = 1800f;
					}
					else
					{
						this.m_cutUnitPosType = CutUnitPosType.RIGHT;
						num2 = 1400f;
					}
				}
				else
				{
					this.m_cutUnitPosType = CutUnitPosType.LEFT;
					num2 = 1400f;
				}
				if (array.Length == 1)
				{
					this.m_cutUnitPosAction = CutUnitPosAction.PLACE;
					return;
				}
				text = array[1];
				if (text != null)
				{
					if (!(text == "D"))
					{
						if (!(text == "L") && !(text == "R"))
						{
							if (!(text == "M"))
							{
								if (text == "F")
								{
									if (array.Length != 3)
									{
										Debug.LogError(string.Format("Invalid m_Pos Command. 'F' Command must have only 3 token. m_Pos [{0}], CutSceneStrID [{1}], CutIndex [{2}]", this.m_Pos, cutSceneStrID, index));
										return;
									}
									this.m_cutUnitPosAction = CutUnitPosAction.MOVE;
									string text2 = array[2];
									if (text2 != null)
									{
										if (text2 == "L")
										{
											this.m_StartPosType = CutUnitPosType.LEFT;
											goto IL_37F;
										}
										if (text2 == "R")
										{
											this.m_StartPosType = CutUnitPosType.RIGHT;
											goto IL_37F;
										}
										if (text2 == "C")
										{
											this.m_StartPosType = CutUnitPosType.CENTER;
											goto IL_37F;
										}
									}
									Debug.LogError(string.Format("Invalid m_Pos Command. Token next to 'F' must be 'L' or 'R' or 'C'. m_Pos [{0}], CutSceneStrID [{1}], CutIndex [{2}]", this.m_Pos, cutSceneStrID, index));
									return;
								}
							}
							else
							{
								if (array.Length < 3)
								{
									Debug.LogError(string.Format("Invalid m_Pos Command. 'M' Command must greater than 3. m_Pos [{0}], CutSceneStrID [{1}], CutIndex [{2}]", this.m_Pos, cutSceneStrID, index));
									return;
								}
								num = 1;
								if (array[2] == "I")
								{
									if (array.Length > 3)
									{
										Debug.LogError(string.Format("Invalid m_Pos Command. 'I' Command must less than 3. m_Pos [{0}], CutSceneStrID [{1}], CutIndex [{2}]", this.m_Pos, cutSceneStrID, index));
										return;
									}
									this.m_cutUnitPosAction = CutUnitPosAction.IN;
								}
								else
								{
									if (!(array[2] == "O"))
									{
										Debug.LogError(string.Format("Invalid m_Pos Command. Token next to 'M' must be 'I' or 'O'. m_Pos [{0}], CutSceneStrID [{1}], CutIndex [{2}]", this.m_Pos, cutSceneStrID, index));
										return;
									}
									if (array.Length > 4)
									{
										Debug.LogError(string.Format("Invalid m_Pos Command. 'O' Command must less than 4. m_Pos [{0}], CutSceneStrID [{1}], CutIndex [{2}]", this.m_Pos, cutSceneStrID, index));
										return;
									}
									this.m_cutUnitPosAction = CutUnitPosAction.OUT;
									if (array.Length == 4)
									{
										if (array[3] != "DOWN")
										{
											Debug.LogError(string.Format("Invalid m_Pos Command. Token next to 'O' must be 'DOWN'. m_Pos [{0}], CutSceneStrID [{1}], CutIndex [{2}]", this.m_Pos, cutSceneStrID, index));
											return;
										}
										this.m_bMoveVertical = true;
									}
								}
							}
						}
						else
						{
							if (array.Length != 4)
							{
								Debug.LogError(string.Format("Invalid m_Pos Command. 'C_L_M', 'C_R_M' Command must have only 4 token. m_Pos [{0}], CutSceneStrID [{1}], CutIndex [{2}]", this.m_Pos, cutSceneStrID, index));
								return;
							}
							if (array[2] != "M")
							{
								Debug.LogError(string.Format("Invalid m_Pos Command. Token next to 'C_L', 'C_R' must be 'M'. m_Pos [{0}], CutSceneStrID [{1}], CutIndex [{2}]", this.m_Pos, cutSceneStrID, index));
								return;
							}
							num = 2;
							string text2 = array[3];
							if (text2 != null)
							{
								if (text2 == "I")
								{
									this.m_cutUnitPosAction = CutUnitPosAction.IN;
									goto IL_37F;
								}
								if (text2 == "O")
								{
									this.m_cutUnitPosAction = CutUnitPosAction.OUT;
									goto IL_37F;
								}
							}
							Debug.LogError(string.Format("Invalid m_Pos Command. Token next to 'M' must be 'I' or 'O'. m_Pos [{0}], CutSceneStrID [{1}], CutIndex [{2}]", this.m_Pos, cutSceneStrID, index));
							return;
						}
					}
					else
					{
						if (array.Length != 2)
						{
							Debug.LogError(string.Format("Invalid m_Pos Command. 'D' Command must have only 2 token. m_Pos [{0}], CutSceneStrID [{1}], CutIndex [{2}]", this.m_Pos, cutSceneStrID, index));
							return;
						}
						this.m_cutUnitPosAction = CutUnitPosAction.DARK;
					}
				}
				IL_37F:
				if (this.m_cutUnitPosAction != CutUnitPosAction.IN)
				{
					if (this.m_cutUnitPosAction == CutUnitPosAction.OUT)
					{
						if (this.m_bMoveVertical)
						{
							this.m_TargetPos.y = -num2;
							return;
						}
						if (array[num - 1] == "L")
						{
							this.m_TargetPos.x = -num2;
							return;
						}
						this.m_TargetPos.x = num2;
					}
					return;
				}
				if (array[num - 1] == "L")
				{
					this.m_StartPos.x = -num2;
					return;
				}
				this.m_StartPos.x = num2;
				return;
			}
			IL_8A:
			this.m_cutUnitPosType = CutUnitPosType.NONE;
			Debug.LogError(string.Format("Invalid m_Pos Command. First token must be 'L', 'R', 'C'. m_Pos [{0}], CutSceneStrID [{1}], CutIndex [{2}]", this.m_Pos, cutSceneStrID, index));
		}

		// Token: 0x040031E2 RID: 12770
		private const float UNIT_MOVE_DIST = 1400f;

		// Token: 0x040031E3 RID: 12771
		private const float UNIT_MOVE_DIST_FOR_CENTER = 1800f;

		// Token: 0x040031E4 RID: 12772
		private const float UNIT_MOVE_TIME_FOR_CENTER = 0.7f;

		// Token: 0x040031E5 RID: 12773
		private const float UNIT_MOVE_TIME_TO_DOWN = 0.9f;

		// Token: 0x040031E6 RID: 12774
		private const float UNIT_MOVE_TIME = 0.6f;

		// Token: 0x040031E7 RID: 12775
		public bool m_bWaitClick = true;

		// Token: 0x040031E8 RID: 12776
		public float m_fWaitTime;

		// Token: 0x040031E9 RID: 12777
		public bool m_bTitleClear;

		// Token: 0x040031EA RID: 12778
		public bool m_bTitleFadeOut;

		// Token: 0x040031EB RID: 12779
		public float m_fTitleFadeOutTime;

		// Token: 0x040031EC RID: 12780
		public string m_Title = "";

		// Token: 0x040031ED RID: 12781
		public float m_fTitleTalkTime = 0.15f;

		// Token: 0x040031EE RID: 12782
		public string m_SubTitle = "";

		// Token: 0x040031EF RID: 12783
		public float m_fSubTitleTalkTime = 0.15f;

		// Token: 0x040031F0 RID: 12784
		public bool m_bTalkCenterFadeIn;

		// Token: 0x040031F1 RID: 12785
		public bool m_bTalkCenterFadeOut;

		// Token: 0x040031F2 RID: 12786
		public float m_bTalkCenterFadeTime;

		// Token: 0x040031F3 RID: 12787
		public NKC_CUTSCEN_FILTER_TYPE m_FilterType;

		// Token: 0x040031F4 RID: 12788
		public float m_fBGFadeInTime;

		// Token: 0x040031F5 RID: 12789
		public Ease m_easeBGFadeIn = Ease.Linear;

		// Token: 0x040031F6 RID: 12790
		public Color m_colBGFadeInStart = new Color(1f, 1f, 1f, 1f);

		// Token: 0x040031F7 RID: 12791
		public Color m_colBGFadeIn = new Color(1f, 1f, 1f, 1f);

		// Token: 0x040031F8 RID: 12792
		public float m_fBGFadeOutTime;

		// Token: 0x040031F9 RID: 12793
		public Ease m_easeBGFadeOut = Ease.Linear;

		// Token: 0x040031FA RID: 12794
		public Color m_colBGFadeOut = new Color(1f, 1f, 1f, 1f);

		// Token: 0x040031FB RID: 12795
		public bool m_bGameObjectBGType;

		// Token: 0x040031FC RID: 12796
		public string m_BGFileName = "";

		// Token: 0x040031FD RID: 12797
		public string m_GameObjectBGAniName = "";

		// Token: 0x040031FE RID: 12798
		public bool m_bGameObjectBGLoop = true;

		// Token: 0x040031FF RID: 12799
		public bool m_bNoWaitBGAni;

		// Token: 0x04003200 RID: 12800
		public int m_BGCrash;

		// Token: 0x04003201 RID: 12801
		public float m_fBGCrashTime;

		// Token: 0x04003202 RID: 12802
		public float m_fBGAnITime;

		// Token: 0x04003203 RID: 12803
		public bool m_bBGAniPos;

		// Token: 0x04003204 RID: 12804
		public Vector2 m_BGOffsetPose = new Vector2(0f, 0f);

		// Token: 0x04003205 RID: 12805
		public TRACKING_DATA_TYPE m_tdtBGPos = TRACKING_DATA_TYPE.TDT_NORMAL;

		// Token: 0x04003206 RID: 12806
		public bool m_bBGAniScale;

		// Token: 0x04003207 RID: 12807
		public Vector3 m_BGOffsetScale = new Vector3(1f, 1f, 1f);

		// Token: 0x04003208 RID: 12808
		public TRACKING_DATA_TYPE m_tdtBGScale = TRACKING_DATA_TYPE.TDT_NORMAL;

		// Token: 0x04003209 RID: 12809
		public bool m_bLooseShake;

		// Token: 0x0400320A RID: 12810
		public bool m_bFadeIn;

		// Token: 0x0400320B RID: 12811
		public float m_fFadeTime;

		// Token: 0x0400320C RID: 12812
		public bool m_bFadeWhite;

		// Token: 0x0400320D RID: 12813
		public float m_fFlashBangTime;

		// Token: 0x0400320E RID: 12814
		public string m_StartBGMFileName = "";

		// Token: 0x0400320F RID: 12815
		public string m_EndBGMFileName = "";

		// Token: 0x04003210 RID: 12816
		public NKC_CUTSCEN_SOUND_CONTROL m_StartFXSoundControl = NKC_CUTSCEN_SOUND_CONTROL.NCSC_ONE_TIME_PLAY;

		// Token: 0x04003211 RID: 12817
		public string m_StartFXSoundName = "";

		// Token: 0x04003212 RID: 12818
		public NKC_CUTSCEN_SOUND_CONTROL m_EndFXSoundControl = NKC_CUTSCEN_SOUND_CONTROL.NCSC_ONE_TIME_PLAY;

		// Token: 0x04003213 RID: 12819
		public string m_EndFXSoundName = "";

		// Token: 0x04003214 RID: 12820
		public string m_VoiceFileName = "";

		// Token: 0x04003215 RID: 12821
		public string m_CharStrID = "";

		// Token: 0x04003216 RID: 12822
		public bool m_bClear;

		// Token: 0x04003217 RID: 12823
		public bool m_bFlip;

		// Token: 0x04003218 RID: 12824
		public string m_Pos = "";

		// Token: 0x04003219 RID: 12825
		public Vector2 m_CharOffSet = Vector2.zero;

		// Token: 0x0400321A RID: 12826
		public Vector2 m_StartPos = Vector2.zero;

		// Token: 0x0400321B RID: 12827
		public Vector2 m_TargetPos = Vector2.zero;

		// Token: 0x0400321C RID: 12828
		public bool m_bMoveVertical;

		// Token: 0x0400321D RID: 12829
		public float m_TrackingTime = 0.6f;

		// Token: 0x0400321E RID: 12830
		public CutUnitPosType m_StartPosType = CutUnitPosType.NONE;

		// Token: 0x0400321F RID: 12831
		public int m_BounceCount;

		// Token: 0x04003220 RID: 12832
		public float m_BounceTime;

		// Token: 0x04003221 RID: 12833
		public bool m_bCharHologramEffect;

		// Token: 0x04003222 RID: 12834
		public bool m_bCharPinup;

		// Token: 0x04003223 RID: 12835
		public float m_bCharPinupEasingTime;

		// Token: 0x04003224 RID: 12836
		public bool m_bCharFadeOut;

		// Token: 0x04003225 RID: 12837
		public bool m_bCharFadeIn;

		// Token: 0x04003226 RID: 12838
		public Vector2 m_CharScale;

		// Token: 0x04003227 RID: 12839
		public float m_CharScaleTime;

		// Token: 0x04003228 RID: 12840
		public NKCASUIUnitIllust.eAnimation m_Face = NKCASUIUnitIllust.eAnimation.NONE;

		// Token: 0x04003229 RID: 12841
		public bool m_bFaceLoop = true;

		// Token: 0x0400322A RID: 12842
		public int m_Crash;

		// Token: 0x0400322B RID: 12843
		public string m_Talk = "";

		// Token: 0x0400322C RID: 12844
		public float m_fTalkTime;

		// Token: 0x0400322D RID: 12845
		public bool m_bTalkAppend;

		// Token: 0x0400322E RID: 12846
		public bool m_CloseTalkBox;

		// Token: 0x0400322F RID: 12847
		public string m_ImageName = "";

		// Token: 0x04003230 RID: 12848
		public float m_fImageScale = 1f;

		// Token: 0x04003231 RID: 12849
		public Vector2 m_ImageOffsetPos = new Vector2(0f, 0f);

		// Token: 0x04003232 RID: 12850
		public bool m_bMovieSkipEnable = true;

		// Token: 0x04003233 RID: 12851
		public string m_MovieName = "";

		// Token: 0x04003234 RID: 12852
		private CutUnitPosType m_cutUnitPosType = CutUnitPosType.NONE;

		// Token: 0x04003235 RID: 12853
		private CutUnitPosAction m_cutUnitPosAction = CutUnitPosAction.NONE;

		// Token: 0x04003236 RID: 12854
		public NKCCutTemplet.eCutsceneAction m_Action;

		// Token: 0x04003237 RID: 12855
		public string m_ActionStrKey;

		// Token: 0x04003238 RID: 12856
		public static readonly char[] Seperator = new char[]
		{
			',',
			' ',
			'\t',
			'\n'
		};

		// Token: 0x0200130D RID: 4877
		public enum eCutsceneAction
		{
			// Token: 0x040097DB RID: 38875
			NONE,
			// Token: 0x040097DC RID: 38876
			MARK,
			// Token: 0x040097DD RID: 38877
			JUMP,
			// Token: 0x040097DE RID: 38878
			SELECT,
			// Token: 0x040097DF RID: 38879
			PLAY_MUSIC
		}
	}
}
