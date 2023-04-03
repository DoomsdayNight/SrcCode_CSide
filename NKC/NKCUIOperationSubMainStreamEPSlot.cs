using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A09 RID: 2569
	public class NKCUIOperationSubMainStreamEPSlot : MonoBehaviour
	{
		// Token: 0x0600702A RID: 28714 RVA: 0x00252C58 File Offset: 0x00250E58
		public void InitUI(NKCUIOperationSubMainStreamEPSlot.OnEPSlotSelect onSelected, NKCUIComToggleGroup toggleGroup)
		{
			this.m_tgl.OnValueChanged.RemoveAllListeners();
			this.m_tgl.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTgl));
			this.m_tgl.m_bGetCallbackWhileLocked = true;
			this.m_tgl.SetToggleGroup(toggleGroup);
			this.m_dOnEPSlotSelect = onSelected;
		}

		// Token: 0x0600702B RID: 28715 RVA: 0x00252CB0 File Offset: 0x00250EB0
		public int GetEpisodeID()
		{
			return this.m_EpisodeID;
		}

		// Token: 0x0600702C RID: 28716 RVA: 0x00252CB8 File Offset: 0x00250EB8
		public int GetUIIndex()
		{
			return this.m_UIIndex;
		}

		// Token: 0x0600702D RID: 28717 RVA: 0x00252CC0 File Offset: 0x00250EC0
		public void SetData(int episodeID, string epName, int uIIndex)
		{
			this.m_EpisodeID = episodeID;
			this.m_UIIndex = uIIndex;
			NKCUtil.SetLabelText(this.m_lbOn, epName);
			NKCUtil.SetLabelText(this.m_lbOff, epName);
			NKCUtil.SetGameobjectActive(this.m_objEventDrop, NKMEpisodeMgr.CheckEpisodeHasEventDrop(NKMEpisodeTempletV2.Find(episodeID, EPISODE_DIFFICULTY.NORMAL)));
			this.RefreshRedDot();
		}

		// Token: 0x0600702E RID: 28718 RVA: 0x00252D10 File Offset: 0x00250F10
		public void SetSelected(bool bValue)
		{
			this.m_tgl.Select(bValue, true, true);
			this.m_Ani.SetBool("ON", bValue);
			if (bValue)
			{
				this.m_Ani.Play("ON_IDLE");
				return;
			}
			this.m_Ani.Play("OFF_IDLE");
		}

		// Token: 0x0600702F RID: 28719 RVA: 0x00252D61 File Offset: 0x00250F61
		public void ChangeSelected(bool bValue)
		{
			this.m_tgl.Select(bValue, true, true);
			this.m_Ani.SetBool("ON", bValue);
		}

		// Token: 0x06007030 RID: 28720 RVA: 0x00252D83 File Offset: 0x00250F83
		private void OnTgl(bool bValue)
		{
			if (bValue)
			{
				NKCUIOperationSubMainStreamEPSlot.OnEPSlotSelect dOnEPSlotSelect = this.m_dOnEPSlotSelect;
				if (dOnEPSlotSelect == null)
				{
					return;
				}
				dOnEPSlotSelect(this.m_EpisodeID);
			}
		}

		// Token: 0x06007031 RID: 28721 RVA: 0x00252D9E File Offset: 0x00250F9E
		public void RefreshRedDot()
		{
			NKCUtil.SetGameobjectActive(this.m_objRedDot, NKMEpisodeMgr.HasReddot(this.m_EpisodeID));
		}

		// Token: 0x04005BDD RID: 23517
		public NKCUIComToggle m_tgl;

		// Token: 0x04005BDE RID: 23518
		public Animator m_Ani;

		// Token: 0x04005BDF RID: 23519
		public Text m_lbOn;

		// Token: 0x04005BE0 RID: 23520
		public Text m_lbOff;

		// Token: 0x04005BE1 RID: 23521
		public GameObject m_objRedDot;

		// Token: 0x04005BE2 RID: 23522
		public GameObject m_objEventDrop;

		// Token: 0x04005BE3 RID: 23523
		private NKCUIOperationSubMainStreamEPSlot.OnEPSlotSelect m_dOnEPSlotSelect;

		// Token: 0x04005BE4 RID: 23524
		private int m_EpisodeID;

		// Token: 0x04005BE5 RID: 23525
		private int m_UIIndex = -1;

		// Token: 0x02001749 RID: 5961
		// (Invoke) Token: 0x0600B2D9 RID: 45785
		public delegate void OnEPSlotSelect(int episodeID);
	}
}
