using System;
using NKC.Templet;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C08 RID: 3080
	public class NKCUILobbyEventIndexSlot : MonoBehaviour
	{
		// Token: 0x06008EA6 RID: 36518 RVA: 0x00308707 File Offset: 0x00306907
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnButton, new UnityAction(this.OnClick));
		}

		// Token: 0x06008EA7 RID: 36519 RVA: 0x00308720 File Offset: 0x00306920
		public void SetData(NKCLobbyEventIndexTemplet templet)
		{
			this.m_Templet = templet;
			if (templet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objEmpty, true);
				NKCUtil.SetGameobjectActive(this.m_imgMain, false);
				NKCUtil.SetGameobjectActive(this.m_objTimeWarning, false);
				NKCUtil.SetGameobjectActive(this.m_objReddot, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
			NKCUtil.SetGameobjectActive(this.m_imgMain, true);
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("ab_ui_nkm_ui_lobby_texture", templet.BannerID));
			NKCUtil.SetImageSprite(this.m_imgMain, orLoadAssetResource, false);
			if (templet.IntervalTemplet != null && templet.IntervalTemplet.IsValid)
			{
				bool bValue = NKCSynchronizedTime.IsFinished(templet.IntervalTemplet.GetEndDateUtc().AddDays(-7.0));
				NKCUtil.SetGameobjectActive(this.m_objTimeWarning, bValue);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objTimeWarning, false);
			}
			this.UpdateReddot();
		}

		// Token: 0x06008EA8 RID: 36520 RVA: 0x003087FA File Offset: 0x003069FA
		private void UpdateReddot()
		{
			NKCUtil.SetGameobjectActive(this.m_objReddot, this.CheckReddot());
		}

		// Token: 0x06008EA9 RID: 36521 RVA: 0x0030880D File Offset: 0x00306A0D
		private bool CheckReddot()
		{
			return this.m_Templet != null && (NKCAlarmManager.HasCompletableMission(this.m_Templet.m_lstAlarmMissionTab) || NKCAlarmManager.CheckAlarmByShortcut(this.m_Templet.ShortCutType, this.m_Templet.ShortCutParam));
		}

		// Token: 0x06008EAA RID: 36522 RVA: 0x00308848 File Offset: 0x00306A48
		private void OnClick()
		{
			if (this.m_Templet == null)
			{
				return;
			}
			NKCContentManager.MoveToShortCut(this.m_Templet.ShortCutType, this.m_Templet.ShortCutParam, false);
		}

		// Token: 0x04007BB9 RID: 31673
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x04007BBA RID: 31674
		public GameObject m_objEmpty;

		// Token: 0x04007BBB RID: 31675
		public Image m_imgMain;

		// Token: 0x04007BBC RID: 31676
		public GameObject m_objTimeWarning;

		// Token: 0x04007BBD RID: 31677
		public GameObject m_objReddot;

		// Token: 0x04007BBE RID: 31678
		private NKCLobbyEventIndexTemplet m_Templet;
	}
}
