using System;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI.Option
{
	// Token: 0x02000B8A RID: 2954
	public class NKCUIGameOptionAlarm : NKCUIGameOptionContentBase
	{
		// Token: 0x0600886F RID: 34927 RVA: 0x002E290C File Offset: 0x002E0B0C
		public override void Init()
		{
			this.m_AllowAlarmToggles[0] = this.m_NKM_UI_GAME_OPTION_ALARM_SLOT1_TOGGLE;
			this.m_AllowAlarmToggles[1] = this.m_NKM_UI_GAME_OPTION_ALARM_SLOT2_TOGGLE;
			this.m_AllowAlarmToggles[2] = this.m_NKM_UI_GAME_OPTION_ALARM_SLOT3_TOGGLE;
			this.m_AllowAlarmToggles[3] = this.m_NKM_UI_GAME_OPTION_ALARM_SLOT4_TOGGLE;
			this.m_AllowAlarmToggles[4] = this.m_NKM_UI_GAME_OPTION_ALARM_SLOT6_TOGGLE;
			this.m_AllowAlarmToggles[6] = this.m_NKM_UI_GAME_OPTION_ALARM_SLOT9_TOGGLE;
			for (int i = 0; i < 7; i++)
			{
				NKC_GAME_OPTION_ALARM_GROUP alarmGroup = (NKC_GAME_OPTION_ALARM_GROUP)i;
				NKCUIComToggle nkcuicomToggle = this.m_AllowAlarmToggles[i];
				if (nkcuicomToggle != null)
				{
					nkcuicomToggle.OnValueChanged.AddListener(delegate(bool allow)
					{
						this.OnClickAllowAlarmButton(alarmGroup, allow);
					});
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ALARM_DESCRIPTION, NKMContentsVersionManager.HasCountryTag(CountryTagType.KOR));
		}

		// Token: 0x06008870 RID: 34928 RVA: 0x002E29C4 File Offset: 0x002E0BC4
		public override void SetContent()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			for (int i = 0; i < 7; i++)
			{
				NKC_GAME_OPTION_ALARM_GROUP type = (NKC_GAME_OPTION_ALARM_GROUP)i;
				NKCUIComToggle nkcuicomToggle = this.m_AllowAlarmToggles[i];
				if (nkcuicomToggle != null)
				{
					nkcuicomToggle.Select(gameOptionData.GetAllowAlarm(type), true, false);
				}
			}
			bool bSelect = true;
			for (int j = 1; j < this.m_AllowAlarmToggles.Length; j++)
			{
				NKCUIComToggle nkcuicomToggle2 = this.m_AllowAlarmToggles[j];
				if (nkcuicomToggle2 != null && !nkcuicomToggle2.m_bChecked)
				{
					bSelect = false;
				}
			}
			this.m_AllowAlarmToggles[0].Select(bSelect, true, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ALARM_SLOT9, NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_DUNGEON, 0, 0));
		}

		// Token: 0x06008871 RID: 34929 RVA: 0x002E2A68 File Offset: 0x002E0C68
		private void OnClickAllowAlarmButton(NKC_GAME_OPTION_ALARM_GROUP alarmGroup, bool allow)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			if (alarmGroup == NKC_GAME_OPTION_ALARM_GROUP.ALLOW_ALL_ALARM)
			{
				gameOptionData.SetAllLocalAlarm(allow);
			}
			else
			{
				gameOptionData.SetAllowAlarm(alarmGroup, allow);
			}
			this.SetContent();
		}

		// Token: 0x040074E8 RID: 29928
		private NKCUIComToggle[] m_AllowAlarmToggles = new NKCUIComToggle[7];

		// Token: 0x040074E9 RID: 29929
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_ALARM_SLOT1_TOGGLE;

		// Token: 0x040074EA RID: 29930
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_ALARM_SLOT2_TOGGLE;

		// Token: 0x040074EB RID: 29931
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_ALARM_SLOT3_TOGGLE;

		// Token: 0x040074EC RID: 29932
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_ALARM_SLOT4_TOGGLE;

		// Token: 0x040074ED RID: 29933
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_ALARM_SLOT5_TOGGLE;

		// Token: 0x040074EE RID: 29934
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_ALARM_SLOT6_TOGGLE;

		// Token: 0x040074EF RID: 29935
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_ALARM_SLOT7_TOGGLE;

		// Token: 0x040074F0 RID: 29936
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_ALARM_SLOT8_TOGGLE;

		// Token: 0x040074F1 RID: 29937
		public GameObject m_NKM_UI_GAME_OPTION_ALARM_SLOT9;

		// Token: 0x040074F2 RID: 29938
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_ALARM_SLOT9_TOGGLE;

		// Token: 0x040074F3 RID: 29939
		public GameObject m_NKM_UI_GAME_OPTION_ALARM_DESCRIPTION;
	}
}
