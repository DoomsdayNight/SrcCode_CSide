using System;
using System.Collections.Generic;
using Cs.Core.Util;
using Cs.Logging;
using NKM.Guild;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.UI.Guild
{
	// Token: 0x02000B38 RID: 2872
	public class NKCUIGuildCoopBack : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x060082C6 RID: 33478 RVA: 0x002C1E18 File Offset: 0x002C0018
		public void SetEnableDrag(bool bSet)
		{
			this.m_bEnableDrag = bSet;
		}

		// Token: 0x060082C7 RID: 33479 RVA: 0x002C1E24 File Offset: 0x002C0024
		public void Init(int seasonID, NKCUIGuildCoopBack.OnClickArena onClickArena, NKCUIGuildCoopBack.OnClickBoss onClickBoss)
		{
			this.m_GuildSeasonTemplet = GuildDungeonTempletManager.GetGuildSeasonTemplet(NKCGuildCoopManager.m_SeasonId);
			if (this.m_GuildSeasonTemplet == null)
			{
				Log.Error(string.Format("GuildSeasonTepmlet is null - id : {0}", seasonID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/GuildCoop/NKCUIGuildCoopBack.cs", 37);
				return;
			}
			this.m_CurSessionData = this.m_GuildSeasonTemplet.GetCurrentSession(ServiceTime.Recent);
			if (this.m_CurSessionData.SessionId == 0)
			{
				Log.Error(string.Format("SessionData is null - SeasonId : {0}, curTime : {1}", seasonID, ServiceTime.Recent), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/GuildCoop/NKCUIGuildCoopBack.cs", 44);
				return;
			}
			base.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			List<GuildDungeonInfoTemplet> dungeonInfoList = GuildDungeonTempletManager.GetDungeonInfoList(this.m_GuildSeasonTemplet.GetSeasonDungeonGroup());
			List<int> lstDungeonId = this.m_CurSessionData.templet.GetDungeonList();
			int i;
			Predicate<GuildDungeonInfoTemplet> <>9__1;
			int j;
			for (i = 0; i < this.m_lstArenaPin.Count; i = j + 1)
			{
				this.m_lstArenaPin[i].InitUI(onClickArena);
				List<GuildDungeonInfoTemplet> list = dungeonInfoList.FindAll((GuildDungeonInfoTemplet x) => x.GetArenaIndex() == i + 1);
				Predicate<GuildDungeonInfoTemplet> match;
				if ((match = <>9__1) == null)
				{
					match = (<>9__1 = ((GuildDungeonInfoTemplet x) => lstDungeonId.Contains(x.GetSeasonDungeonId())));
				}
				GuildDungeonInfoTemplet guildDungeonInfoTemplet = list.Find(match);
				this.m_lstArenaPin[i].SetData(guildDungeonInfoTemplet);
				if (guildDungeonInfoTemplet != null)
				{
					this.m_dicArena.Add(guildDungeonInfoTemplet.GetArenaIndex(), this.m_lstArenaPin[i]);
				}
				j = i;
			}
			if (this.m_RaidPin != null)
			{
				this.m_RaidPin.InitUI(onClickBoss);
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x060082C8 RID: 33480 RVA: 0x002C1FDC File Offset: 0x002C01DC
		public void SetData()
		{
			List<GuildDungeonInfoTemplet> dungeonInfoList = GuildDungeonTempletManager.GetDungeonInfoList(this.m_GuildSeasonTemplet.GetSeasonDungeonGroup());
			List<int> lstDungeonId = this.m_CurSessionData.templet.GetDungeonList();
			int i;
			Predicate<GuildDungeonInfoTemplet> <>9__2;
			int j;
			for (i = 0; i < this.m_lstArenaPin.Count; i = j + 1)
			{
				List<GuildDungeonInfoTemplet> list = dungeonInfoList.FindAll((GuildDungeonInfoTemplet x) => x.GetArenaIndex() == i + 1);
				Predicate<GuildDungeonInfoTemplet> match;
				if ((match = <>9__2) == null)
				{
					match = (<>9__2 = ((GuildDungeonInfoTemplet x) => lstDungeonId.Contains(x.GetSeasonDungeonId())));
				}
				GuildDungeonInfoTemplet data = list.Find(match);
				this.m_lstArenaPin[i].SetData(data);
				j = i;
			}
			List<GuildRaidTemplet> raidTempletList = GuildDungeonTempletManager.GetRaidTempletList(this.m_GuildSeasonTemplet.GetSeasonRaidGroup());
			NKCUIGuildCoopBoss raidPin = this.m_RaidPin;
			GuildRaidTemplet data2;
			if (raidTempletList == null)
			{
				data2 = null;
			}
			else
			{
				data2 = raidTempletList.Find((GuildRaidTemplet x) => x.GetStageId() == NKCGuildCoopManager.m_cGuildRaidTemplet.GetStageId());
			}
			raidPin.SetData(data2);
			NKCCamera.StopTrackingCamera();
			NKCCamera.GetTrackingPos().SetNowValue(0f, 0f, this.m_fCameraZPosZoomOut);
			base.gameObject.SetActive(true);
		}

		// Token: 0x060082C9 RID: 33481 RVA: 0x002C2110 File Offset: 0x002C0310
		public Vector3 GetTargetPosition(int arenaIdx, bool bIsArena = true)
		{
			if (!bIsArena)
			{
				return new Vector3(this.m_RaidPin.transform.position.x, this.m_RaidPin.transform.position.y, this.m_fCameraZPosZoomOut);
			}
			if (this.m_dicArena.ContainsKey(arenaIdx))
			{
				return new Vector3(this.m_dicArena[arenaIdx].transform.position.x, this.m_dicArena[arenaIdx].transform.position.y, this.m_fCameraZPosZoomOut);
			}
			return new Vector3(0f, 0f, this.m_fCameraZPosZoomOut);
		}

		// Token: 0x060082CA RID: 33482 RVA: 0x002C21BB File Offset: 0x002C03BB
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x060082CB RID: 33483 RVA: 0x002C21C0 File Offset: 0x002C03C0
		public void OnDrag(PointerEventData pointData)
		{
			if (!this.m_bEnableDrag)
			{
				return;
			}
			float num = NKCCamera.GetPosNowX(false) - pointData.delta.x * 10f;
			float num2 = NKCCamera.GetPosNowY(false) - pointData.delta.y * 10f;
			num = Mathf.Clamp(num, -this.m_vCameraMoveRange.x, this.m_vCameraMoveRange.x);
			num2 = Mathf.Clamp(num2, -this.m_vCameraMoveRange.y, this.m_vCameraMoveRange.y);
			NKCCamera.TrackingPos(1f, num, num2, -1f);
		}

		// Token: 0x060082CC RID: 33484 RVA: 0x002C2255 File Offset: 0x002C0455
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x060082CD RID: 33485 RVA: 0x002C2257 File Offset: 0x002C0457
		public void RefreshArenaSlot(int idx)
		{
			if (this.m_dicArena.ContainsKey(idx))
			{
				this.m_dicArena[idx].Refresh();
			}
		}

		// Token: 0x060082CE RID: 33486 RVA: 0x002C2278 File Offset: 0x002C0478
		public void RefreshBossSlot()
		{
			this.m_RaidPin.Refresh();
		}

		// Token: 0x04006EFE RID: 28414
		public List<NKCUIGuildCoopArena> m_lstArenaPin = new List<NKCUIGuildCoopArena>();

		// Token: 0x04006EFF RID: 28415
		public NKCUIGuildCoopBoss m_RaidPin = new NKCUIGuildCoopBoss();

		// Token: 0x04006F00 RID: 28416
		public float m_fCameraXPosAddValue = 150f;

		// Token: 0x04006F01 RID: 28417
		public float m_fCameraZPosZoomIn = -300f;

		// Token: 0x04006F02 RID: 28418
		public float m_fCameraZPosZoomOut = -676f;

		// Token: 0x04006F03 RID: 28419
		public Vector2 m_vCameraMoveRange;

		// Token: 0x04006F04 RID: 28420
		private bool m_bEnableDrag = true;

		// Token: 0x04006F05 RID: 28421
		private Vector2 currentCameraPos;

		// Token: 0x04006F06 RID: 28422
		private GuildSeasonTemplet m_GuildSeasonTemplet;

		// Token: 0x04006F07 RID: 28423
		private GuildSeasonTemplet.SessionData m_CurSessionData;

		// Token: 0x04006F08 RID: 28424
		private Dictionary<int, NKCUIGuildCoopArena> m_dicArena = new Dictionary<int, NKCUIGuildCoopArena>();

		// Token: 0x020018D0 RID: 6352
		// (Invoke) Token: 0x0600B6D1 RID: 46801
		public delegate void OnClickArena(GuildDungeonInfoTemplet templet);

		// Token: 0x020018D1 RID: 6353
		// (Invoke) Token: 0x0600B6D5 RID: 46805
		public delegate void OnClickBoss(int bossID);
	}
}
