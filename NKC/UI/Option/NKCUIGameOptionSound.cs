using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Option
{
	// Token: 0x02000B96 RID: 2966
	public class NKCUIGameOptionSound : NKCUIGameOptionContentBase
	{
		// Token: 0x060088F1 RID: 35057 RVA: 0x002E5164 File Offset: 0x002E3364
		public override void Init()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			this.m_SoundSliderWithButtons[0] = this.NKM_UI_GAME_OPTION_SOUND_SLOT1_GAUGE;
			this.m_SoundSliderWithButtons[1] = this.NKM_UI_GAME_OPTION_SOUND_SLOT2_GAUGE;
			this.m_SoundSliderWithButtons[2] = this.NKM_UI_GAME_OPTION_SOUND_SLOT3_GAUGE;
			this.m_SoundSliderWithButtons[3] = this.NKM_UI_GAME_OPTION_SOUND_SLOT0_GAUGE;
			for (int i = 0; i < 4; i++)
			{
				NKC_GAME_OPTION_SOUND_GROUP soundGroup = (NKC_GAME_OPTION_SOUND_GROUP)i;
				int soundVolume = gameOptionData.GetSoundVolume(soundGroup);
				this.m_SoundSliderWithButtons[i].Init(0, 100, soundVolume, null, delegate
				{
					this.ChangeSoundVolume(soundGroup);
				});
			}
			List<NKC_VOICE_CODE> availableVoiceCode = NKCUIVoiceManager.GetAvailableVoiceCode();
			int num = (availableVoiceCode != null) ? availableVoiceCode.Count : 0;
			NKCUtil.SetGameobjectActive(this.m_objVoiceLanguageSelectSlot, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.VOICE_SWITCH) && num >= 2);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnVoiceLanguage, new UnityAction(this.OnSelectVoiceLanguage));
			if (this.m_csbtnVoiceLanguage != null)
			{
				this.m_csbtnVoiceLanguage.SetTitleText(NKCUIVoiceManager.GetVoiceLanguageName(NKCUIVoiceManager.CurrentVoiceCode));
			}
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglMute, new UnityAction<bool>(this.OnTglMute));
			if (this.m_tglChatNotifySound != null)
			{
				this.m_tglChatNotifySound.OnValueChanged.RemoveAllListeners();
				this.m_tglChatNotifySound.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickChatNotifySound));
			}
		}

		// Token: 0x060088F2 RID: 35058 RVA: 0x002E52C8 File Offset: 0x002E34C8
		public override void SetContent()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			for (int i = 0; i < 4; i++)
			{
				NKC_GAME_OPTION_SOUND_GROUP type = (NKC_GAME_OPTION_SOUND_GROUP)i;
				int soundVolume = gameOptionData.GetSoundVolume(type);
				this.m_SoundSliderWithButtons[i].m_Slider.value = (float)soundVolume;
			}
			this.m_tglMute.Select(gameOptionData.SoundMute, true, false);
			if (!NKMOpenTagManager.IsOpened("CHAT_PRIVATE"))
			{
				NKCUtil.SetGameobjectActive(this.m_objChatNotifySound, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objChatNotifySound, true);
			NKCUIComToggle tglChatNotifySound = this.m_tglChatNotifySound;
			if (tglChatNotifySound == null)
			{
				return;
			}
			tglChatNotifySound.Select(gameOptionData.UseChatNotifySound, true, false);
		}

		// Token: 0x060088F3 RID: 35059 RVA: 0x002E5360 File Offset: 0x002E3560
		private void OnTglMute(bool value)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.SoundMute = value;
			NKCSoundManager.SetMute(value, false);
		}

		// Token: 0x060088F4 RID: 35060 RVA: 0x002E538C File Offset: 0x002E358C
		private void ChangeSoundVolume(NKC_GAME_OPTION_SOUND_GROUP soundGroup)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			int value = this.m_SoundSliderWithButtons[(int)soundGroup].GetValue();
			gameOptionData.SetSoundVolume(soundGroup, value);
			switch (soundGroup)
			{
			case NKC_GAME_OPTION_SOUND_GROUP.BGM:
				NKCSoundManager.SetMusicVolume(gameOptionData.GetSoundVolumeAsFloat(soundGroup));
				return;
			case NKC_GAME_OPTION_SOUND_GROUP.SE:
				NKCSoundManager.SetSoundVolume(gameOptionData.GetSoundVolumeAsFloat(soundGroup));
				return;
			case NKC_GAME_OPTION_SOUND_GROUP.VOICE:
				NKCSoundManager.SetVoiceVolume(gameOptionData.GetSoundVolumeAsFloat(soundGroup));
				return;
			case NKC_GAME_OPTION_SOUND_GROUP.ALL:
				NKCSoundManager.SetAllVolume(gameOptionData.GetSoundVolumeAsFloat(soundGroup));
				return;
			default:
				return;
			}
		}

		// Token: 0x060088F5 RID: 35061 RVA: 0x002E5408 File Offset: 0x002E3608
		private void OnSelectVoiceLanguage()
		{
			NKCPopupVoiceLanguageSelect.Instance.Open();
		}

		// Token: 0x060088F6 RID: 35062 RVA: 0x002E5414 File Offset: 0x002E3614
		public void OnClickChatNotifySound(bool bUse)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseChatNotifySound = bUse;
		}

		// Token: 0x060088F7 RID: 35063 RVA: 0x002E5438 File Offset: 0x002E3638
		public override bool Processhotkey(HotkeyEventType eventType)
		{
			if (eventType == HotkeyEventType.ShowHotkey)
			{
				if (this.m_SoundSliderWithButtons[3] != null)
				{
					NKCUIComStateButton plusButton = this.m_SoundSliderWithButtons[3].m_PlusButton;
					NKCUIComHotkeyDisplay.OpenInstance((plusButton != null) ? plusButton.transform : null, HotkeyEventType.MasterVolumeUp);
					NKCUIComStateButton minusButton = this.m_SoundSliderWithButtons[3].m_MinusButton;
					NKCUIComHotkeyDisplay.OpenInstance((minusButton != null) ? minusButton.transform : null, HotkeyEventType.MasterVolumeDown);
				}
				if (this.m_tglMute != null)
				{
					NKCUIComHotkeyDisplay.OpenInstance(this.m_tglMute.transform, HotkeyEventType.Mute);
				}
			}
			return false;
		}

		// Token: 0x0400756B RID: 30059
		private NKCUIGameOptionSliderWithButton[] m_SoundSliderWithButtons = new NKCUIGameOptionSliderWithButton[4];

		// Token: 0x0400756C RID: 30060
		public NKCUIGameOptionSliderWithButton NKM_UI_GAME_OPTION_SOUND_SLOT0_GAUGE;

		// Token: 0x0400756D RID: 30061
		public NKCUIGameOptionSliderWithButton NKM_UI_GAME_OPTION_SOUND_SLOT1_GAUGE;

		// Token: 0x0400756E RID: 30062
		public NKCUIGameOptionSliderWithButton NKM_UI_GAME_OPTION_SOUND_SLOT2_GAUGE;

		// Token: 0x0400756F RID: 30063
		public NKCUIGameOptionSliderWithButton NKM_UI_GAME_OPTION_SOUND_SLOT3_GAUGE;

		// Token: 0x04007570 RID: 30064
		public GameObject m_objVoiceLanguageSelectSlot;

		// Token: 0x04007571 RID: 30065
		public NKCUIComStateButton m_csbtnVoiceLanguage;

		// Token: 0x04007572 RID: 30066
		public NKCUIComToggle m_tglMute;

		// Token: 0x04007573 RID: 30067
		[Header("채팅 알림음")]
		public GameObject m_objChatNotifySound;

		// Token: 0x04007574 RID: 30068
		public NKCUIComToggle m_tglChatNotifySound;
	}
}
