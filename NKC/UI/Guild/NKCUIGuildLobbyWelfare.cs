using System;
using System.Collections.Generic;
using System.Linq;
using NKM;
using NKM.Guild;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B58 RID: 2904
	public class NKCUIGuildLobbyWelfare : MonoBehaviour
	{
		// Token: 0x06008475 RID: 33909 RVA: 0x002CAA34 File Offset: 0x002C8C34
		public void InitUI()
		{
			this.m_tglUser.OnValueChanged.RemoveAllListeners();
			this.m_tglUser.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedUserTgl));
			this.m_tglGuild.OnValueChanged.RemoveAllListeners();
			this.m_tglGuild.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedGuildTgl));
			this.m_btnAddPoint.PointerClick.RemoveAllListeners();
			this.m_btnAddPoint.PointerClick.AddListener(new UnityAction(this.OnClickAddPoint));
			this.m_loop.dOnGetObject += this.GetObject;
			this.m_loop.dOnReturnObject += this.ReturnObject;
			this.m_loop.dOnProvideData += this.ProvideData;
			this.m_loop.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loop, null);
		}

		// Token: 0x06008476 RID: 33910 RVA: 0x002CAB24 File Offset: 0x002C8D24
		private RectTransform GetObject(int idx)
		{
			NKCUIGuildLobbyWelfareSlot nkcuiguildLobbyWelfareSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuiguildLobbyWelfareSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcuiguildLobbyWelfareSlot = UnityEngine.Object.Instantiate<NKCUIGuildLobbyWelfareSlot>(this.m_pfbSlot);
			}
			nkcuiguildLobbyWelfareSlot.InitUI();
			return nkcuiguildLobbyWelfareSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008477 RID: 33911 RVA: 0x002CAB68 File Offset: 0x002C8D68
		private void ReturnObject(Transform tr)
		{
			NKCUIGuildLobbyWelfareSlot component = tr.GetComponent<NKCUIGuildLobbyWelfareSlot>();
			NKCUtil.SetGameobjectActive(component, false);
			tr.SetParent(base.transform);
			this.m_stkSlot.Push(component);
		}

		// Token: 0x06008478 RID: 33912 RVA: 0x002CAB9C File Offset: 0x002C8D9C
		private void ProvideData(Transform tr, int idx)
		{
			NKCUIGuildLobbyWelfareSlot component = tr.GetComponent<NKCUIGuildLobbyWelfareSlot>();
			if (this.m_currentType == NKCUIGuildLobbyWelfare.UITabType.User)
			{
				if (idx < this.m_lstUserTemplet.Count)
				{
					component.SetData(this.m_lstUserTemplet[idx]);
					return;
				}
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			else
			{
				if (idx < this.m_lstGuildTemplet.Count)
				{
					component.SetData(this.m_lstGuildTemplet[idx]);
					return;
				}
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
		}

		// Token: 0x06008479 RID: 33913 RVA: 0x002CAC0C File Offset: 0x002C8E0C
		public void SetData()
		{
			this.m_lstUserTemplet = NKMTempletContainer<GuildWelfareTemplet>.Values.ToList<GuildWelfareTemplet>().FindAll((GuildWelfareTemplet x) => x.WelfareCategory == WELFARE_BUFF_TYPE.PERSONAL);
			this.m_lstUserTemplet.Sort(new Comparison<GuildWelfareTemplet>(this.Comparer));
			this.m_lstGuildTemplet = NKMTempletContainer<GuildWelfareTemplet>.Values.ToList<GuildWelfareTemplet>().FindAll((GuildWelfareTemplet x) => x.WelfareCategory == WELFARE_BUFF_TYPE.GUILD);
			this.m_lstGuildTemplet.Sort(new Comparison<GuildWelfareTemplet>(this.Comparer));
			if (this.m_currentType == NKCUIGuildLobbyWelfare.UITabType.User)
			{
				NKCUtil.SetGameobjectActive(this.m_btnAddPoint, true);
				this.m_btnAddPoint.UnLock(false);
				this.m_tglUser.Select(true, true, true);
				this.OnChangedUserTgl(true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_btnAddPoint, false);
			this.m_btnAddPoint.Lock(false);
			this.m_tglGuild.Select(true, true, true);
			this.OnChangedGuildTgl(true);
		}

		// Token: 0x0600847A RID: 33914 RVA: 0x002CAD14 File Offset: 0x002C8F14
		private int Comparer(GuildWelfareTemplet left, GuildWelfareTemplet right)
		{
			if (left.Order == right.Order)
			{
				return left.ID.CompareTo(right.ID);
			}
			return left.Order.CompareTo(right.Order);
		}

		// Token: 0x0600847B RID: 33915 RVA: 0x002CAD48 File Offset: 0x002C8F48
		private void SetPoint()
		{
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			if (this.m_currentType == NKCUIGuildLobbyWelfare.UITabType.User)
			{
				NKCUtil.SetImageSprite(this.m_imgPoint, NKCResourceUtility.GetOrLoadMiscItemSmallIcon(23), false);
				NKCUtil.SetLabelText(this.m_lbPointCount, inventoryData.GetCountMiscItem(23).ToString());
				NKCUtil.SetGameobjectActive(this.m_btnAddPoint, true);
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgPoint, NKCResourceUtility.GetOrLoadMiscItemSmallIcon(24), false);
			NKCUtil.SetLabelText(this.m_lbPointCount, NKCGuildManager.MyGuildData.unionPoint.ToString());
			NKCUtil.SetGameobjectActive(this.m_btnAddPoint, false);
		}

		// Token: 0x0600847C RID: 33916 RVA: 0x002CADDC File Offset: 0x002C8FDC
		private void OnChangedUserTgl(bool bValue)
		{
			if (bValue)
			{
				this.m_currentType = NKCUIGuildLobbyWelfare.UITabType.User;
				this.m_loop.TotalCount = this.m_lstUserTemplet.Count;
				this.m_loop.RefreshCells(true);
				NKCUtil.SetGameobjectActive(this.m_objNone, this.m_loop.TotalCount == 0);
				this.SetPoint();
			}
		}

		// Token: 0x0600847D RID: 33917 RVA: 0x002CAE34 File Offset: 0x002C9034
		private void OnChangedGuildTgl(bool bValue)
		{
			if (bValue)
			{
				this.m_currentType = NKCUIGuildLobbyWelfare.UITabType.Guild;
				this.m_loop.TotalCount = this.m_lstGuildTemplet.Count;
				this.m_loop.RefreshCells(true);
				NKCUtil.SetGameobjectActive(this.m_objNone, this.m_loop.TotalCount == 0);
				this.SetPoint();
			}
		}

		// Token: 0x0600847E RID: 33918 RVA: 0x002CAE8C File Offset: 0x002C908C
		private void OnClickAddPoint()
		{
			if (this.m_currentType != NKCUIGuildLobbyWelfare.UITabType.User)
			{
				return;
			}
			if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(23, true) + (long)NKMCommonConst.Guild.WelfarePointBuyAmount > NKMCommonConst.Guild.WelfarePointBuyLimit)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_GUILD_WELFARE_POINT_LIMIT, null, "");
				return;
			}
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_WELFARE_BUY_POINT_TITLE, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_WELFARE_BUY_POINT_DESC, NKMCommonConst.Guild.WelfarePointBuyAmount), 101, (int)NKMCommonConst.Guild.WelfarePointPrice, new NKCPopupResourceConfirmBox.OnButton(this.OnConfirmAddPoint), null, false);
		}

		// Token: 0x0600847F RID: 33919 RVA: 0x002CAF20 File Offset: 0x002C9120
		private void OnConfirmAddPoint()
		{
			if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(101) >= NKMCommonConst.Guild.WelfarePointPrice)
			{
				NKCPacketSender.Send_NKMPacket_GUILD_BUY_WELFARE_POINT_REQ(NKMCommonConst.Guild.WelfarePointBuyAmount);
				return;
			}
			NKCPopupItemLack.Instance.OpenItemMiscLackPopup(101, (int)NKMCommonConst.Guild.WelfarePointPrice);
		}

		// Token: 0x040070A0 RID: 28832
		public NKCUIComToggle m_tglUser;

		// Token: 0x040070A1 RID: 28833
		public NKCUIComToggle m_tglGuild;

		// Token: 0x040070A2 RID: 28834
		public Image m_imgPoint;

		// Token: 0x040070A3 RID: 28835
		public Text m_lbPointCount;

		// Token: 0x040070A4 RID: 28836
		public NKCUIComStateButton m_btnAddPoint;

		// Token: 0x040070A5 RID: 28837
		public NKCUIGuildLobbyWelfareSlot m_pfbSlot;

		// Token: 0x040070A6 RID: 28838
		public LoopScrollRect m_loop;

		// Token: 0x040070A7 RID: 28839
		public Transform m_trSlotParent;

		// Token: 0x040070A8 RID: 28840
		public GameObject m_objNone;

		// Token: 0x040070A9 RID: 28841
		private Stack<NKCUIGuildLobbyWelfareSlot> m_stkSlot = new Stack<NKCUIGuildLobbyWelfareSlot>();

		// Token: 0x040070AA RID: 28842
		private List<GuildWelfareTemplet> m_lstUserTemplet = new List<GuildWelfareTemplet>();

		// Token: 0x040070AB RID: 28843
		private List<GuildWelfareTemplet> m_lstGuildTemplet = new List<GuildWelfareTemplet>();

		// Token: 0x040070AC RID: 28844
		private NKCUIGuildLobbyWelfare.UITabType m_currentType;

		// Token: 0x020018F2 RID: 6386
		public enum UITabType
		{
			// Token: 0x0400AA3A RID: 43578
			User,
			// Token: 0x0400AA3B RID: 43579
			Guild
		}
	}
}
