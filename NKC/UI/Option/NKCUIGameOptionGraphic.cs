using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Option
{
	// Token: 0x02000B8D RID: 2957
	public class NKCUIGameOptionGraphic : NKCUIGameOptionContentBase
	{
		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x0600888E RID: 34958 RVA: 0x002E33D4 File Offset: 0x002E15D4
		private string GRAPHIC_DISABLED_QUALITY_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_CUSTOM;
			}
		}

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x0600888F RID: 34959 RVA: 0x002E33DB File Offset: 0x002E15DB
		private string HIGH_GRAPHIC_OPTION_CHANGE_TITLE_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_WARNING;
			}
		}

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06008890 RID: 34960 RVA: 0x002E33E2 File Offset: 0x002E15E2
		private string HIGH_GRAPHIC_OPTION_CHANGE_CONTENT_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_OPTION_CHANGE_WARNING;
			}
		}

		// Token: 0x06008891 RID: 34961 RVA: 0x002E33EC File Offset: 0x002E15EC
		public override void Init()
		{
			this.GRAPHIC_QUALITY_STRINGS = new string[]
			{
				NKCUtilString.GET_STRING_WORST,
				NKCUtilString.GET_STRING_LOW,
				NKCUtilString.GET_STRING_NORMAL,
				NKCUtilString.GET_STRING_GOOD,
				NKCUtilString.GET_STRING_BEST
			};
			this.GRAPHIC_ANIMATION_QUALITY_STRINGS = new string[]
			{
				NKCUtilString.GET_STRING_NORMAL,
				NKCUtilString.GET_STRING_OPTION_HIGH_QUALITY
			};
			this.GRAPHIC_GAME_FRAME_LIMIT_STRINGS = new string[]
			{
				NKCUtilString.GET_STRING_OPTION_30_FPS,
				NKCUtilString.GET_STRING_OPTION_60_FPS
			};
			this.GRAPHIC_QUALITY_LEVEL_STRINGS = new string[]
			{
				NKCUtilString.GET_STRING_LOW2,
				NKCUtilString.GET_STRING_HIGH
			};
			this.GRAPHIC_LOGIN_CUTIN_STRINGS = new string[]
			{
				NKCStringTable.GetString("SI_DP_OPTION_GRAPHIC_LOGIN_ANIM_ALWAYS", false),
				NKCStringTable.GetString("SI_DP_OPTION_GRAPHIC_LOGIN_ANIM_RANDOM", false),
				NKCStringTable.GetString("SI_DP_OPTION_GRAPHIC_LOGIN_ANIM_DAYONE", false),
				NKCStringTable.GetString("SI_DP_OPTION_GRAPHIC_LOGIN_ANIM_OFF", false)
			};
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			if (gameOptionData.DisabledGraphicQuality)
			{
				this.m_GraphicSlot1SliderWithButton.SetDisabled(true, this.GRAPHIC_DISABLED_QUALITY_STRING);
			}
			this.m_GraphicSlot1SliderWithButton.Init(0, 4, (int)gameOptionData.GraphicQuality, this.GRAPHIC_QUALITY_STRINGS, new NKCUIGameOptionSliderWithButton.OnChanged(this.ChangeGraphicQuality));
			this.m_GraphicSlot1SliderWithButton.SetWarningPopup(4, this.HIGH_GRAPHIC_OPTION_CHANGE_TITLE_STRING, this.HIGH_GRAPHIC_OPTION_CHANGE_CONTENT_STRING);
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT4_BUTTON.Init(0, 1, (int)gameOptionData.AnimationQuality, this.GRAPHIC_ANIMATION_QUALITY_STRINGS, new NKCUIGameOptionMultiStateButton.OnClicked(this.OnClickAnimationQualityButton));
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT5_BUTTON.Init(0, 1, (int)gameOptionData.GameFrameLimit, this.GRAPHIC_GAME_FRAME_LIMIT_STRINGS, new NKCUIGameOptionMultiStateButton.OnClicked(this.OnClickGameFrameLimitButton));
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT6_BUTTON.Init(0, 1, (int)gameOptionData.QualityLevel, this.GRAPHIC_QUALITY_LEVEL_STRINGS, new NKCUIGameOptionMultiStateButton.OnClicked(this.OnClickQualityLevelButton));
			if (NKCDefineManager.DEFINE_UNITY_STANDALONE_WIN())
			{
				NKCUtil.SetGameobjectActive(this.m_objAspectRatio, true);
				if (this.m_btnAspectRatioOption != null)
				{
					this.m_btnAspectRatioOption.Init(0, 2, (int)gameOptionData.AspectRatio, new string[]
					{
						"4:3",
						"16:9",
						"21:9"
					}, new NKCUIGameOptionMultiStateButton.OnClicked(this.OnClickAspectRatioButton));
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objAspectRatio, false);
			}
			if (this.m_btnLoginCutin != null)
			{
				this.m_btnLoginCutin.Init(0, 3, (int)gameOptionData.LoginCutin, this.GRAPHIC_LOGIN_CUTIN_STRINGS, new NKCUIGameOptionMultiStateButton.OnClicked(this.OnClickLoginCutinButton));
			}
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT2_TOGGLE.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickUseGameEffectButton));
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT3_TOGGLE.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickUseCommonEffectButton));
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_HIT_EFFECT_TOGGLE.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickUseHitEffectButton));
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_VIDEO_TEXTURE.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickUseVideoTexture));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglUseBuffEffect, new UnityAction<bool>(this.OnClickUseBuffEffect));
			if (this.m_sldrEffectOpacity != null)
			{
				NKCUtil.SetGameobjectActive(this.m_sldrEffectOpacity.transform.parent, true);
				this.m_sldrEffectOpacity.Init(35, 100, gameOptionData.EffectOpacity, null, new NKCUIGameOptionSliderWithButton.OnChanged(this.OnChangeGameOpacity));
			}
		}

		// Token: 0x06008892 RID: 34962 RVA: 0x002E3708 File Offset: 0x002E1908
		public override void SetContent()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			if (gameOptionData.DisabledGraphicQuality)
			{
				this.m_GraphicSlot1SliderWithButton.SetDisabled(true, this.GRAPHIC_DISABLED_QUALITY_STRING);
				this.ApplyGameOptionData(gameOptionData);
				return;
			}
			this.m_GraphicSlot1SliderWithButton.SetDisabled(false, "");
			this.m_GraphicSlot1SliderWithButton.m_Slider.value = (float)gameOptionData.GraphicQuality;
			this.m_GraphicSlot1SliderWithButton.UpdateButtonText();
			this.ApplyGraphicQualityDetail(gameOptionData.GraphicQuality);
		}

		// Token: 0x06008893 RID: 34963 RVA: 0x002E3785 File Offset: 0x002E1985
		private void ChangeGraphicQuality()
		{
			this.OnClickChangeGraphicQualityConfirmButton();
		}

		// Token: 0x06008894 RID: 34964 RVA: 0x002E3790 File Offset: 0x002E1990
		private void OnClickChangeGraphicQualityConfirmButton()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			NKC_GAME_OPTION_GRAPHIC_QUALITY value = (NKC_GAME_OPTION_GRAPHIC_QUALITY)this.m_GraphicSlot1SliderWithButton.GetValue();
			gameOptionData.DisabledGraphicQuality = this.m_GraphicSlot1SliderWithButton.isDisabled();
			gameOptionData.GraphicQuality = value;
			this.ApplyGraphicQualityDetail(value);
		}

		// Token: 0x06008895 RID: 34965 RVA: 0x002E37D8 File Offset: 0x002E19D8
		private void ApplyGraphicQualityDetail(NKC_GAME_OPTION_GRAPHIC_QUALITY graphicQuality)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			if (gameOptionData.DisabledGraphicQuality)
			{
				return;
			}
			gameOptionData.SetGameOptionDataByGrahpicQuality(graphicQuality);
			this.ApplyGameOptionData(gameOptionData);
		}

		// Token: 0x06008896 RID: 34966 RVA: 0x002E380C File Offset: 0x002E1A0C
		private void ApplyGameOptionData(NKCGameOptionData gameOptionData)
		{
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT2_TOGGLE.Select(gameOptionData.UseGameEffect, true, false);
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT3_TOGGLE.Select(gameOptionData.UseCommonEffect, true, false);
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_HIT_EFFECT_TOGGLE.Select(gameOptionData.UseHitEffect, true, false);
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_VIDEO_TEXTURE.Select(gameOptionData.UseVideoTexture, true, false);
			NKCUIComToggle tglUseBuffEffect = this.m_tglUseBuffEffect;
			if (tglUseBuffEffect != null)
			{
				tglUseBuffEffect.Select(gameOptionData.UseBuffEffect, true, false);
			}
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT4_BUTTON.ChangeValue((int)gameOptionData.AnimationQuality);
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT5_BUTTON.ChangeValue((int)gameOptionData.GameFrameLimit);
			this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT6_BUTTON.ChangeValue((int)gameOptionData.QualityLevel);
			this.m_btnLoginCutin.ChangeValue((int)gameOptionData.LoginCutin);
			this.m_sldrEffectOpacity.ChangeValue(gameOptionData.EffectOpacity);
			this.ChangeUseGameEffect(gameOptionData.UseGameEffect);
			this.ChangeUseCommonEffect(gameOptionData.UseCommonEffect);
			this.ChangeUseHitEffect(gameOptionData.UseHitEffect);
			this.ChangeAnimationQuality();
			this.ChangeGameFrameLimit();
			this.ChangeQualityLevel();
			this.ChangeAspectRatio();
			this.ChangeUseVideoTexture(gameOptionData.UseVideoTexture);
			this.OnClickLoginCutinButton();
		}

		// Token: 0x06008897 RID: 34967 RVA: 0x002E3928 File Offset: 0x002E1B28
		private void ChangeUseGameEffect(bool use)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseGameEffect = this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT2_TOGGLE.m_bChecked;
		}

		// Token: 0x06008898 RID: 34968 RVA: 0x002E3958 File Offset: 0x002E1B58
		private void ChangeUseVideoTexture(bool use)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseVideoTexture = this.m_NKM_UI_GAME_OPTION_GRAPHIC_VIDEO_TEXTURE.m_bChecked;
		}

		// Token: 0x06008899 RID: 34969 RVA: 0x002E3988 File Offset: 0x002E1B88
		private void ChangeUseCommonEffect(bool use)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseCommonEffect = this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT3_TOGGLE.m_bChecked;
			NKCCamera.SetBloomEnableUI(gameOptionData.UseCommonEffect);
		}

		// Token: 0x0600889A RID: 34970 RVA: 0x002E39C0 File Offset: 0x002E1BC0
		private void OnClickUseBuffEffect(bool value)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseBuffEffect = value;
			this.ChangeCustomGraphicQuality();
		}

		// Token: 0x0600889B RID: 34971 RVA: 0x002E39EC File Offset: 0x002E1BEC
		private void OnChangeGameOpacity()
		{
			int value = this.m_sldrEffectOpacity.GetValue();
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.EffectOpacity = value;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				float num = (float)value / 100f;
				Shader.SetGlobalFloat("_FxGlobalTransparency", 1f - num * num);
			}
		}

		// Token: 0x0600889C RID: 34972 RVA: 0x002E3A44 File Offset: 0x002E1C44
		private void ChangeAnimationQuality()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			NKCGameOptionDataSt.GraphicOptionAnimationQuality value = (NKCGameOptionDataSt.GraphicOptionAnimationQuality)this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT4_BUTTON.GetValue();
			gameOptionData.AnimationQuality = value;
		}

		// Token: 0x0600889D RID: 34973 RVA: 0x002E3A74 File Offset: 0x002E1C74
		private void ChangeGameFrameLimit()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			NKCGameOptionDataSt.GraphicOptionGameFrameLimit value = (NKCGameOptionDataSt.GraphicOptionGameFrameLimit)this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT5_BUTTON.GetValue();
			gameOptionData.GameFrameLimit = value;
			Application.targetFrameRate = gameOptionData.GetFrameLimit();
		}

		// Token: 0x0600889E RID: 34974 RVA: 0x002E3AB0 File Offset: 0x002E1CB0
		private void ChangeQualityLevel()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			int value = this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT6_BUTTON.GetValue();
			gameOptionData.QualityLevel = (NKCGameOptionDataSt.GraphicOptionQualityLevel)value;
			if (QualitySettings.GetQualityLevel() != value)
			{
				QualitySettings.SetQualityLevel(value, true);
			}
		}

		// Token: 0x0600889F RID: 34975 RVA: 0x002E3AF0 File Offset: 0x002E1CF0
		private void OnClickLoginCutinButton()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.LoginCutin = (NKCGameOptionDataSt.GraphicOptionLoginCutin)this.m_btnLoginCutin.GetValue();
		}

		// Token: 0x060088A0 RID: 34976 RVA: 0x002E3B20 File Offset: 0x002E1D20
		private void ChangeAspectRatio()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			if (this.m_btnAspectRatioOption == null)
			{
				return;
			}
			int value = this.m_btnAspectRatioOption.GetValue();
			gameOptionData.AspectRatio = (NKCGameOptionDataSt.GameOptionGraphicAspectRatio)value;
			ValueTuple<int, int> aspect = NKCGameOptionDataSt.GetAspect(gameOptionData.AspectRatio);
			NKCScenManager.GetScenManager().SetAspectRatio(aspect);
		}

		// Token: 0x060088A1 RID: 34977 RVA: 0x002E3B78 File Offset: 0x002E1D78
		private void ChangeUseHitEffect(bool use)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseHitEffect = this.m_NKM_UI_GAME_OPTION_GRAPHIC_HIT_EFFECT_TOGGLE.m_bChecked;
		}

		// Token: 0x060088A2 RID: 34978 RVA: 0x002E3BA8 File Offset: 0x002E1DA8
		private void OnClickUseGameEffectButton(bool use)
		{
			if (use)
			{
				NKCPopupOKCancel.OpenOKCancelBox(this.HIGH_GRAPHIC_OPTION_CHANGE_TITLE_STRING, this.HIGH_GRAPHIC_OPTION_CHANGE_CONTENT_STRING, delegate()
				{
					this.OnClickUseGameEffectConfirmButton(use);
				}, delegate()
				{
					this.SetContent();
				}, false);
				return;
			}
			this.OnClickUseGameEffectConfirmButton(use);
		}

		// Token: 0x060088A3 RID: 34979 RVA: 0x002E3C08 File Offset: 0x002E1E08
		private void OnClickUseVideoTexture(bool use)
		{
			this.ChangeUseVideoTexture(use);
			this.ChangeCustomGraphicQuality();
		}

		// Token: 0x060088A4 RID: 34980 RVA: 0x002E3C17 File Offset: 0x002E1E17
		private void OnClickUseGameEffectConfirmButton(bool use)
		{
			this.ChangeUseGameEffect(use);
			this.ChangeCustomGraphicQuality();
		}

		// Token: 0x060088A5 RID: 34981 RVA: 0x002E3C26 File Offset: 0x002E1E26
		private void OnClickUseCommonEffectButton(bool use)
		{
			this.ChangeUseCommonEffect(use);
			this.ChangeCustomGraphicQuality();
		}

		// Token: 0x060088A6 RID: 34982 RVA: 0x002E3C35 File Offset: 0x002E1E35
		private void OnClickUseHitEffectButton(bool use)
		{
			this.ChangeUseHitEffect(use);
			this.ChangeCustomGraphicQuality();
		}

		// Token: 0x060088A7 RID: 34983 RVA: 0x002E3C44 File Offset: 0x002E1E44
		private void OnClickAnimationQualityButton()
		{
			if (this.m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT4_BUTTON.GetValue() == 1)
			{
				NKCPopupOKCancel.OpenOKCancelBox(this.HIGH_GRAPHIC_OPTION_CHANGE_TITLE_STRING, this.HIGH_GRAPHIC_OPTION_CHANGE_CONTENT_STRING, delegate()
				{
					this.OnClickAnimationQualityConfirmButton();
				}, delegate()
				{
					this.SetContent();
				}, false);
				return;
			}
			this.OnClickAnimationQualityConfirmButton();
		}

		// Token: 0x060088A8 RID: 34984 RVA: 0x002E3C90 File Offset: 0x002E1E90
		private void OnClickAnimationQualityConfirmButton()
		{
			this.ChangeAnimationQuality();
			this.ChangeCustomGraphicQuality();
		}

		// Token: 0x060088A9 RID: 34985 RVA: 0x002E3C9E File Offset: 0x002E1E9E
		private void OnClickGameFrameLimitButton()
		{
			this.ChangeGameFrameLimit();
			this.ChangeCustomGraphicQuality();
		}

		// Token: 0x060088AA RID: 34986 RVA: 0x002E3CAC File Offset: 0x002E1EAC
		private void OnClickQualityLevelButton()
		{
			this.ChangeQualityLevel();
			this.ChangeCustomGraphicQuality();
		}

		// Token: 0x060088AB RID: 34987 RVA: 0x002E3CBA File Offset: 0x002E1EBA
		private void OnClickAspectRatioButton()
		{
			this.ChangeAspectRatio();
		}

		// Token: 0x060088AC RID: 34988 RVA: 0x002E3CC4 File Offset: 0x002E1EC4
		private void ChangeCustomGraphicQuality()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			NKCGameOptionData nkcgameOptionData = new NKCGameOptionData();
			int num = -1;
			for (int i = 0; i < 5; i++)
			{
				nkcgameOptionData.SetGameOptionDataByGrahpicQuality((NKC_GAME_OPTION_GRAPHIC_QUALITY)i);
				if (this.CheckGameOptionData(gameOptionData, nkcgameOptionData))
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				gameOptionData.DisabledGraphicQuality = true;
				this.m_GraphicSlot1SliderWithButton.SetDisabled(true, this.GRAPHIC_DISABLED_QUALITY_STRING);
				return;
			}
			gameOptionData.DisabledGraphicQuality = false;
			gameOptionData.GraphicQuality = (NKC_GAME_OPTION_GRAPHIC_QUALITY)num;
			this.m_GraphicSlot1SliderWithButton.SetDisabled(false, "");
			this.m_GraphicSlot1SliderWithButton.ChangeValue((int)gameOptionData.GraphicQuality);
		}

		// Token: 0x060088AD RID: 34989 RVA: 0x002E3D58 File Offset: 0x002E1F58
		private bool CheckGameOptionData(NKCGameOptionData currentData, NKCGameOptionData compareData)
		{
			return currentData != null && compareData != null && currentData.UseGameEffect == compareData.UseGameEffect && currentData.UseCommonEffect == compareData.UseCommonEffect && currentData.UseHitEffect == compareData.UseHitEffect && currentData.AnimationQuality == compareData.AnimationQuality && currentData.GameFrameLimit == compareData.GameFrameLimit && currentData.QualityLevel == compareData.QualityLevel && currentData.UseVideoTexture == compareData.UseVideoTexture && currentData.UseBuffEffect == compareData.UseBuffEffect;
		}

		// Token: 0x04007512 RID: 29970
		private string[] GRAPHIC_QUALITY_STRINGS;

		// Token: 0x04007513 RID: 29971
		private string[] GRAPHIC_ANIMATION_QUALITY_STRINGS;

		// Token: 0x04007514 RID: 29972
		private string[] GRAPHIC_GAME_FRAME_LIMIT_STRINGS;

		// Token: 0x04007515 RID: 29973
		private string[] GRAPHIC_QUALITY_LEVEL_STRINGS;

		// Token: 0x04007516 RID: 29974
		private string[] GRAPHIC_LOGIN_CUTIN_STRINGS;

		// Token: 0x04007517 RID: 29975
		public NKCUIGameOptionSliderWithButton m_GraphicSlot1SliderWithButton;

		// Token: 0x04007518 RID: 29976
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT2_TOGGLE;

		// Token: 0x04007519 RID: 29977
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT3_TOGGLE;

		// Token: 0x0400751A RID: 29978
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GRAPHIC_HIT_EFFECT_TOGGLE;

		// Token: 0x0400751B RID: 29979
		public NKCUIGameOptionSliderWithButton m_sldrEffectOpacity;

		// Token: 0x0400751C RID: 29980
		public NKCUIGameOptionMultiStateButton m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT4_BUTTON;

		// Token: 0x0400751D RID: 29981
		public NKCUIGameOptionMultiStateButton m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT5_BUTTON;

		// Token: 0x0400751E RID: 29982
		public NKCUIGameOptionMultiStateButton m_NKM_UI_GAME_OPTION_GRAPHIC_SLOT6_BUTTON;

		// Token: 0x0400751F RID: 29983
		public NKCUIComToggle m_tglUseBuffEffect;

		// Token: 0x04007520 RID: 29984
		public GameObject m_objAspectRatio;

		// Token: 0x04007521 RID: 29985
		public NKCUIGameOptionMultiStateButton m_btnAspectRatioOption;

		// Token: 0x04007522 RID: 29986
		public NKCUIGameOptionMultiStateButton m_btnLoginCutin;

		// Token: 0x04007523 RID: 29987
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GRAPHIC_VIDEO_TEXTURE;
	}
}
