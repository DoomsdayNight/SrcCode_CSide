using System;
using System.Text;
using NKC.Templet;
using NKM;
using NKM.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BE3 RID: 3043
	public class NKCUIEventSubUIPayReward : NKCUIEventSubUIBase
	{
		// Token: 0x06008D2E RID: 36142 RVA: 0x00300618 File Offset: 0x002FE818
		public override void Init()
		{
			base.Init();
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOpen, new UnityAction(this.OpenSpecialCutscene));
		}

		// Token: 0x06008D2F RID: 36143 RVA: 0x00300637 File Offset: 0x002FE837
		public override void Open(NKMEventTabTemplet tabTemplet)
		{
			this.m_eventId = tabTemplet.m_EventID;
			this.m_tabTemplet = tabTemplet;
			base.SetDateLimit();
		}

		// Token: 0x06008D30 RID: 36144 RVA: 0x00300652 File Offset: 0x002FE852
		public override void Refresh()
		{
		}

		// Token: 0x06008D31 RID: 36145 RVA: 0x00300654 File Offset: 0x002FE854
		private void OpenSpecialCutscene()
		{
			NKCEventPaybackTemplet nkceventPaybackTemplet = NKCEventPaybackTemplet.Find(this.m_eventId);
			if (nkceventPaybackTemplet == null)
			{
				return;
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(nkceventPaybackTemplet.SkinId);
			if (skinTemplet != null && !string.IsNullOrEmpty(skinTemplet.m_LoginCutin))
			{
				string key = "PAYBACK_INTRO";
				string introString = this.GetIntroString(nkceventPaybackTemplet.Key, nkceventPaybackTemplet.SkinId);
				string text = PlayerPrefs.GetString(key);
				bool flag = !text.Contains(introString);
				bool flag2 = true;
				if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && (NKCDefineManager.DEFINE_UNITY_EDITOR() || NKCDefineManager.DEFINE_UNITY_STANDALONE()))
				{
					flag = true;
					flag2 = false;
				}
				if (flag)
				{
					NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(skinTemplet.m_LoginCutin, skinTemplet.m_LoginCutin);
					nkmassetName.m_BundleName = nkmassetName.m_BundleName.ToLower();
					NKCUIEventSequence nkcuieventSequence = NKCUIEventSequence.OpenInstance(nkmassetName);
					if (nkcuieventSequence != null)
					{
						if (flag2)
						{
							if (!string.IsNullOrEmpty(text))
							{
								text = text.Insert(0, ",");
							}
							text = text.Insert(0, introString.ToString());
							PlayerPrefs.SetString(key, text.ToString());
						}
						nkcuieventSequence.Open(new NKCUIEventSequence.OnClose(this.OpenPayRewardPopup));
						return;
					}
				}
			}
			this.OpenPayRewardPopup();
		}

		// Token: 0x06008D32 RID: 36146 RVA: 0x0030077D File Offset: 0x002FE97D
		private string GetIntroString(int eventId, int skinId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(eventId);
			stringBuilder.Append("_");
			stringBuilder.Append(skinId);
			return stringBuilder.ToString();
		}

		// Token: 0x06008D33 RID: 36147 RVA: 0x003007A8 File Offset: 0x002FE9A8
		private void OpenPayRewardPopup()
		{
			NKCEventPaybackTemplet nkceventPaybackTemplet = NKCEventPaybackTemplet.Find(this.m_eventId);
			if (nkceventPaybackTemplet == null)
			{
				return;
			}
			NKCPopupEventPayReward.SetAssetName(nkceventPaybackTemplet.BannerPrefabId, nkceventPaybackTemplet.BannerPrefabId);
			NKCPopupEventPayReward.Instance.Open(this.m_eventId, nkceventPaybackTemplet.MissionTabId);
		}

		// Token: 0x04007A04 RID: 31236
		public Text m_lbEventDesc;

		// Token: 0x04007A05 RID: 31237
		public NKCUIComStateButton m_csbtnOpen;

		// Token: 0x04007A06 RID: 31238
		private int m_eventId;
	}
}
