using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI
{
	// Token: 0x02000A64 RID: 2660
	public class NKCPopupInGameEmoticon : NKCUIBase
	{
		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06007504 RID: 29956 RVA: 0x0026E95E File Offset: 0x0026CB5E
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06007505 RID: 29957 RVA: 0x0026E961 File Offset: 0x0026CB61
		public override string MenuName
		{
			get
			{
				return "NKCPopupInGameEmoticon";
			}
		}

		// Token: 0x06007506 RID: 29958 RVA: 0x0026E968 File Offset: 0x0026CB68
		public void Init()
		{
			this.m_etBG.triggers.Clear();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				base.Close();
			});
			this.m_etBG.triggers.Add(entry);
			this.m_ctglBlock.OnValueChanged.RemoveAllListeners();
			this.m_ctglBlock.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedValueBlockState));
			for (int i = 0; i < this.m_lstNKCPopupEmoticonSlotComment.Count; i++)
			{
				this.m_lstNKCPopupEmoticonSlotComment[i].SetClickEvent(new NKCPopupEmoticonSlotComment.dOnClick(this.OnClickEmoticon));
			}
			for (int j = 0; j < this.m_lstNKCPopupEmoticonSlotSD.Count; j++)
			{
				this.m_lstNKCPopupEmoticonSlotSD[j].SetClickEvent(new NKCUISlot.OnClick(this.OnClickEmoticon));
			}
		}

		// Token: 0x06007507 RID: 29959 RVA: 0x0026EA4C File Offset: 0x0026CC4C
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007508 RID: 29960 RVA: 0x0026EA5A File Offset: 0x0026CC5A
		private void OnClickEmoticon(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.OnClickEmoticon(slotData.ID);
		}

		// Token: 0x06007509 RID: 29961 RVA: 0x0026EA68 File Offset: 0x0026CC68
		private void OnClickEmoticon(int emoticonID)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			if (gameOptionData.UseEmoticonBlock)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_GAME_EMOTICON_REQ(emoticonID);
		}

		// Token: 0x0600750A RID: 29962 RVA: 0x0026EA94 File Offset: 0x0026CC94
		private void OnChangedValueBlockState(bool bSet)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseEmoticonBlock = bSet;
			gameOptionData.Save();
			NKCUtil.SetGameobjectActive(this.m_objBlocking, gameOptionData.UseEmoticonBlock);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
				if (gameClient != null && gameClient.GetGameHud() != null && gameClient.GetGameHud().GetNKCGameHudEmoticon() != null)
				{
					gameClient.GetGameHud().GetNKCGameHudEmoticon().SetBlockUI();
				}
			}
			if (bSet)
			{
				base.Close();
			}
		}

		// Token: 0x0600750B RID: 29963 RVA: 0x0026EB24 File Offset: 0x0026CD24
		public void Open(List<int> lstEmoticonID_SD, List<int> lstEmoticonIDComment)
		{
			bool flag = false;
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null)
			{
				flag = gameOptionData.UseEmoticonBlock;
			}
			this.m_ctglBlock.Select(flag, true, false);
			NKCUtil.SetGameobjectActive(this.m_objBlocking, flag);
			base.UIOpened(true);
			if (lstEmoticonID_SD != null)
			{
				for (int i = 0; i < this.m_lstNKCPopupEmoticonSlotSD.Count; i++)
				{
					if (lstEmoticonID_SD.Count > i)
					{
						this.m_lstNKCPopupEmoticonSlotSD[i].SetUI(lstEmoticonID_SD[i]);
					}
				}
			}
			if (lstEmoticonIDComment != null)
			{
				for (int j = 0; j < this.m_lstNKCPopupEmoticonSlotComment.Count; j++)
				{
					if (lstEmoticonIDComment.Count > j)
					{
						this.m_lstNKCPopupEmoticonSlotComment[j].SetUI(lstEmoticonIDComment[j]);
					}
				}
			}
		}

		// Token: 0x04006154 RID: 24916
		public EventTrigger m_etBG;

		// Token: 0x04006155 RID: 24917
		public NKCUIComToggle m_ctglBlock;

		// Token: 0x04006156 RID: 24918
		public GameObject m_objBlocking;

		// Token: 0x04006157 RID: 24919
		public List<NKCPopupEmoticonSlotComment> m_lstNKCPopupEmoticonSlotComment = new List<NKCPopupEmoticonSlotComment>();

		// Token: 0x04006158 RID: 24920
		public List<NKCPopupEmoticonSlotSD> m_lstNKCPopupEmoticonSlotSD = new List<NKCPopupEmoticonSlotSD>();

		// Token: 0x020017C2 RID: 6082
		// (Invoke) Token: 0x0600B422 RID: 46114
		public delegate void dOnClose();
	}
}
