using System;
using NKC.Publisher;

namespace NKC.UI.Option
{
	// Token: 0x02000B93 RID: 2963
	public class NKCUIGameOptionPush : NKCUIGameOptionContentBase
	{
		// Token: 0x060088D6 RID: 35030 RVA: 0x002E4AC8 File Offset: 0x002E2CC8
		public override void Init()
		{
			this.m_AllowPushToggles[0] = this.m_NKM_UI_GAME_OPTION_PUSH_SLOT1_BTN;
			for (int i = 0; i < 1; i++)
			{
				NKC_GAME_OPTION_PUSH_GROUP pushGroup = (NKC_GAME_OPTION_PUSH_GROUP)i;
				NKCUIComToggle nkcuicomToggle = this.m_AllowPushToggles[i];
				if (nkcuicomToggle != null)
				{
					nkcuicomToggle.OnValueChanged.AddListener(delegate(bool allow)
					{
						this.OnClickAllowPushButton(pushGroup, allow);
					});
				}
			}
		}

		// Token: 0x060088D7 RID: 35031 RVA: 0x002E4B28 File Offset: 0x002E2D28
		public override void SetContent()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			for (int i = 0; i < 1; i++)
			{
				NKC_GAME_OPTION_PUSH_GROUP type = (NKC_GAME_OPTION_PUSH_GROUP)i;
				NKCUIComToggle nkcuicomToggle = this.m_AllowPushToggles[i];
				if (nkcuicomToggle != null)
				{
					nkcuicomToggle.Select(gameOptionData.GetAllowPush(type), true, false);
				}
			}
		}

		// Token: 0x060088D8 RID: 35032 RVA: 0x002E4B70 File Offset: 0x002E2D70
		private void OnClickAllowPushButton(NKC_GAME_OPTION_PUSH_GROUP pushGroup, bool allow)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.SetAllowPush(pushGroup, allow);
			NKCPublisherModule.Push.ReRegisterPush();
			this.SetContent();
		}

		// Token: 0x04007553 RID: 30035
		private NKCUIComToggle[] m_AllowPushToggles = new NKCUIComToggle[1];

		// Token: 0x04007554 RID: 30036
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_PUSH_SLOT1_BTN;
	}
}
