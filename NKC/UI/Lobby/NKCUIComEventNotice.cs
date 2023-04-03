using System;
using NKC.UI.Event;
using NKM;
using NKM.Event;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C06 RID: 3078
	public class NKCUIComEventNotice : MonoBehaviour
	{
		// Token: 0x06008E93 RID: 36499 RVA: 0x00307EFC File Offset: 0x003060FC
		public void SetData(NKMUserData userData)
		{
			bool flag = NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_SUBMENU, 0, 0);
			bool flag2 = NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_EVENT, 0, 0);
			bool flag3 = NKCContentManager.IsContentsUnlocked(ContentsType.ATTENDANCE, 0, 0);
			if (!flag || !flag3 || !flag2)
			{
				base.gameObject.SetActive(false);
				return;
			}
			NKCUtil.SetButtonClickDelegate(this.m_cstbnButton, new UnityAction(this.OnClick));
			NKMEventTabTemplet nkmeventTabTemplet = null;
			foreach (NKMEventTabTemplet nkmeventTabTemplet2 in NKMTempletContainer<NKMEventTabTemplet>.Values)
			{
				if (nkmeventTabTemplet2.IsAvailable && !string.IsNullOrEmpty(nkmeventTabTemplet2.m_LobbyButtonImage) && !NKMEventManager.IsEventCompleted(nkmeventTabTemplet2) && (nkmeventTabTemplet == null || nkmeventTabTemplet2.m_OrderList < nkmeventTabTemplet.m_OrderList))
				{
					nkmeventTabTemplet = nkmeventTabTemplet2;
				}
			}
			if (nkmeventTabTemplet == null)
			{
				base.gameObject.SetActive(false);
				return;
			}
			base.gameObject.SetActive(true);
			this.m_EventID = nkmeventTabTemplet.m_EventID;
			this.SetImage(nkmeventTabTemplet.m_LobbyButtonImage);
			if (!string.IsNullOrEmpty(nkmeventTabTemplet.m_LobbyButtonString))
			{
				string remainTimeString = NKCUtilString.GetRemainTimeString(nkmeventTabTemplet.TimeLimit, 1);
				string @string = NKCStringTable.GetString(nkmeventTabTemplet.m_LobbyButtonString, new object[]
				{
					remainTimeString
				});
				NKCUtil.SetLabelText(this.m_lbSpeechbubble, @string);
			}
			switch (nkmeventTabTemplet.m_EventType)
			{
			case NKM_EVENT_TYPE.SIMPLE:
				NKCUtil.SetLabelText(this.m_lbEventType, NKCStringTable.GetString("SI_DP_EVENT_NOTICE_LABEL_SIMPLE", false));
				goto IL_1C6;
			case NKM_EVENT_TYPE.BINGO:
				NKCUtil.SetLabelText(this.m_lbEventType, NKCStringTable.GetString("SI_DP_EVENT_NOTICE_LABEL_BINGO", false));
				goto IL_1C6;
			case NKM_EVENT_TYPE.MISSION:
			case NKM_EVENT_TYPE.KAKAOEMOTE:
			case NKM_EVENT_TYPE.MISSION_ROW:
				NKCUtil.SetLabelText(this.m_lbEventType, NKCStringTable.GetString("SI_DP_EVENT_NOTICE_LABEL_MISSION", false));
				goto IL_1C6;
			case NKM_EVENT_TYPE.ONTIME:
				NKCUtil.SetLabelText(this.m_lbEventType, NKCStringTable.GetString("SI_DP_EVENT_NOTICE_LABEL_COMPLETE_MISSION", false));
				goto IL_1C6;
			}
			NKCUtil.SetLabelText(this.m_lbEventType, "");
			IL_1C6:
			bool bValue = NKMEventManager.CheckRedDot(nkmeventTabTemplet);
			NKCUtil.SetGameobjectActive(this.m_objReddot, bValue);
			NKCUtil.SetGameobjectActive(this.m_objSpeechbubble, bValue);
		}

		// Token: 0x06008E94 RID: 36500 RVA: 0x00308100 File Offset: 0x00306300
		private void OnClick()
		{
			NKMEventTabTemplet reservedTabTemplet = NKMEventTabTemplet.Find(this.m_EventID);
			NKCUIEvent.Instance.Open(reservedTabTemplet);
		}

		// Token: 0x06008E95 RID: 36501 RVA: 0x00308124 File Offset: 0x00306324
		private void SetImage(string assetName)
		{
			NKMAssetName cNKMAssetName = NKMAssetName.ParseBundleName("ab_ui_nkm_ui_lobby_texture", assetName);
			NKCUtil.SetImageSprite(this.m_imgIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(cNKMAssetName), false);
		}

		// Token: 0x04007BA4 RID: 31652
		public NKCUIComStateButton m_cstbnButton;

		// Token: 0x04007BA5 RID: 31653
		public Image m_imgIcon;

		// Token: 0x04007BA6 RID: 31654
		public Text m_lbEventType;

		// Token: 0x04007BA7 RID: 31655
		public GameObject m_objReddot;

		// Token: 0x04007BA8 RID: 31656
		public GameObject m_objSpeechbubble;

		// Token: 0x04007BA9 RID: 31657
		public Text m_lbSpeechbubble;

		// Token: 0x04007BAA RID: 31658
		private int m_EventID;
	}
}
