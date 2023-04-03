using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B35 RID: 2869
	public class NKCUIComGuildArtifactContent : MonoBehaviour
	{
		// Token: 0x06008290 RID: 33424 RVA: 0x002C0CB8 File Offset: 0x002BEEB8
		public void Init()
		{
			if (this.m_loop != null)
			{
				this.m_loop.dOnGetObject += this.GetObject;
				this.m_loop.dOnReturnObject += this.ReturnObject;
				this.m_loop.dOnProvideData += this.ProvideData;
				this.m_loop.PrepareCells(0);
			}
			if (this.m_tglEach != null)
			{
				this.m_tglEach.OnValueChanged.RemoveAllListeners();
				this.m_tglEach.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedViewEach));
			}
			if (this.m_tglAll != null)
			{
				this.m_tglAll.OnValueChanged.RemoveAllListeners();
				this.m_tglAll.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedViewAll));
			}
			if (this.m_tglArena != null)
			{
				this.m_tglArena.OnValueChanged.RemoveAllListeners();
				this.m_tglArena.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedViewArena));
			}
		}

		// Token: 0x06008291 RID: 33425 RVA: 0x002C0DD2 File Offset: 0x002BEFD2
		public void Close()
		{
			this.m_LastViewType = this.m_CurViewType;
			this.m_CurViewType = NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.NONE;
		}

		// Token: 0x06008292 RID: 33426 RVA: 0x002C0DE8 File Offset: 0x002BEFE8
		private RectTransform GetObject(int idx)
		{
			NKCPopupGuildCoopArtifactStorageSlot nkcpopupGuildCoopArtifactStorageSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcpopupGuildCoopArtifactStorageSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcpopupGuildCoopArtifactStorageSlot = UnityEngine.Object.Instantiate<NKCPopupGuildCoopArtifactStorageSlot>(this.m_pfbNormalSlot, this.m_trParent);
			}
			return nkcpopupGuildCoopArtifactStorageSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008293 RID: 33427 RVA: 0x002C0E2C File Offset: 0x002BF02C
		private void ReturnObject(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
			NKCPopupGuildCoopArtifactStorageSlot component = tr.GetComponent<NKCPopupGuildCoopArtifactStorageSlot>();
			if (component == null)
			{
				return;
			}
			this.m_stkSlot.Push(component);
		}

		// Token: 0x06008294 RID: 33428 RVA: 0x002C0E6C File Offset: 0x002BF06C
		private void ProvideData(Transform tr, int idx)
		{
			NKCPopupGuildCoopArtifactStorageSlot component = tr.GetComponent<NKCPopupGuildCoopArtifactStorageSlot>();
			if (component == null)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			if (this.m_CurViewType == NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ARENA)
			{
				component.SetData(this.m_lstSlotDataByArena[idx]);
				return;
			}
			component.SetData(this.m_lstSlotDataByID[idx]);
		}

		// Token: 0x06008295 RID: 33429 RVA: 0x002C0EC0 File Offset: 0x002BF0C0
		public void SetData(Dictionary<int, List<GuildDungeonArtifactTemplet>> dicArtifactTemplet)
		{
			this.m_lstSlotDataByID.Clear();
			this.m_lstSlotDataByArena.Clear();
			if (this.m_CurViewType == NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.NONE)
			{
				this.m_LastViewType = NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.NONE;
				this.m_CurViewType = NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ARENA;
				this.m_tglArena.Select(true, true, true);
			}
			List<int> list = new List<int>();
			List<int> list2 = dicArtifactTemplet.Keys.ToList<int>();
			list2.Sort();
			for (int i = 0; i < list2.Count; i++)
			{
				this.m_lstSlotDataByArena.Add(NKCUIComGuildArtifactContent.ArtifactSlotData.MakeSlotData(list2[i]));
				List<GuildDungeonArtifactTemplet> list3 = dicArtifactTemplet[list2[i]];
				for (int j = 0; j < list3.Count; j++)
				{
					list.Add(list3[j].GetArtifactId());
					this.m_lstSlotDataByArena.Add(NKCUIComGuildArtifactContent.ArtifactSlotData.MakeSlotData(list3[j]));
					this.m_lstSlotDataByID.Add(NKCUIComGuildArtifactContent.ArtifactSlotData.MakeSlotData(list3[j]));
				}
			}
			this.m_lstSlotDataByID.Sort(new Comparison<NKCUIComGuildArtifactContent.ArtifactSlotData>(this.CompByID));
			if (this.m_CurViewType == NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ARENA)
			{
				this.m_loop.TotalCount = this.m_lstSlotDataByArena.Count;
			}
			else
			{
				this.m_loop.TotalCount = this.m_lstSlotDataByID.Count;
			}
			this.m_loop.SetIndexPosition(0);
			NKCUtil.SetGameobjectActive(this.m_objNone, this.m_loop.TotalCount == 0);
			NKCUtil.SetGameobjectActive(this.m_lbAll, this.m_CurViewType == NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ALL);
			NKCUtil.SetLabelText(this.m_lbAll, NKCUtilString.GetGuildArtifactTotalViewDesc(list));
			this.PlayAnimation();
		}

		// Token: 0x06008296 RID: 33430 RVA: 0x002C1052 File Offset: 0x002BF252
		private int CompByID(NKCUIComGuildArtifactContent.ArtifactSlotData left, NKCUIComGuildArtifactContent.ArtifactSlotData right)
		{
			return left.id.CompareTo(right.id);
		}

		// Token: 0x06008297 RID: 33431 RVA: 0x002C1065 File Offset: 0x002BF265
		private void OnChangedViewEach(bool bValue)
		{
			if (bValue)
			{
				if (this.m_LastViewType == this.m_CurViewType)
				{
					return;
				}
				this.m_LastViewType = this.m_CurViewType;
				this.m_CurViewType = NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.EACH;
				this.RefreshUI(true);
			}
		}

		// Token: 0x06008298 RID: 33432 RVA: 0x002C1093 File Offset: 0x002BF293
		private void OnChangedViewAll(bool bValue)
		{
			if (bValue)
			{
				if (this.m_LastViewType == this.m_CurViewType)
				{
					return;
				}
				this.m_LastViewType = this.m_CurViewType;
				this.m_CurViewType = NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ALL;
				this.RefreshUI(true);
			}
		}

		// Token: 0x06008299 RID: 33433 RVA: 0x002C10C1 File Offset: 0x002BF2C1
		private void OnChangedViewArena(bool bValue)
		{
			if (bValue)
			{
				if (this.m_LastViewType == this.m_CurViewType)
				{
					return;
				}
				this.m_LastViewType = this.m_CurViewType;
				this.m_CurViewType = NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ARENA;
				this.RefreshUI(true);
			}
		}

		// Token: 0x0600829A RID: 33434 RVA: 0x002C10F0 File Offset: 0x002BF2F0
		private void PlayAnimation()
		{
			switch (this.m_LastViewType)
			{
			case NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.NONE:
				if (this.m_CurViewType == NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ARENA || this.m_CurViewType == NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.EACH)
				{
					this.m_Animator.Play("NKM_UI_POPUP_CONSORTIUM_COOP_ARTIFACT_STORAGE_CONTENT_OPEN_EACH");
					return;
				}
				if (this.m_CurViewType == NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ALL)
				{
					this.m_Animator.Play("NKM_UI_POPUP_CONSORTIUM_COOP_ARTIFACT_STORAGE_CONTENT_OPEN");
					return;
				}
				break;
			case NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ALL:
				if (this.m_CurViewType == NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ARENA || this.m_CurViewType == NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.EACH)
				{
					this.m_Animator.Play("NKM_UI_POPUP_CONSORTIUM_COOP_ARTIFACT_STORAGE_CONTENT_OPEN_TO_EACH");
					return;
				}
				break;
			case NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.EACH:
			case NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ARENA:
				if (this.m_CurViewType == NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ALL)
				{
					this.m_Animator.Play("NKM_UI_POPUP_CONSORTIUM_COOP_ARTIFACT_STORAGE_CONTENT_EACH_TO_OPEN");
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600829B RID: 33435 RVA: 0x002C1194 File Offset: 0x002BF394
		public void RefreshUI(bool bResetScroll = false)
		{
			NKCUtil.SetGameobjectActive(this.m_lbAll, this.m_CurViewType == NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ALL);
			if (this.m_CurViewType == NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE.ARENA)
			{
				this.m_loop.TotalCount = this.m_lstSlotDataByArena.Count;
			}
			else
			{
				this.m_loop.TotalCount = this.m_lstSlotDataByID.Count;
			}
			this.m_loop.SetIndexPosition(0);
			NKCUtil.SetGameobjectActive(this.m_objNone, this.m_loop.TotalCount == 0);
			this.PlayAnimation();
		}

		// Token: 0x04006ECB RID: 28363
		public NKCPopupGuildCoopArtifactStorageSlot m_pfbNormalSlot;

		// Token: 0x04006ECC RID: 28364
		public Animator m_Animator;

		// Token: 0x04006ECD RID: 28365
		public NKCUIComToggle m_tglEach;

		// Token: 0x04006ECE RID: 28366
		public NKCUIComToggle m_tglAll;

		// Token: 0x04006ECF RID: 28367
		public NKCUIComToggle m_tglArena;

		// Token: 0x04006ED0 RID: 28368
		public LoopScrollRect m_loop;

		// Token: 0x04006ED1 RID: 28369
		public Transform m_trParent;

		// Token: 0x04006ED2 RID: 28370
		public Text m_lbAll;

		// Token: 0x04006ED3 RID: 28371
		public GameObject m_objNone;

		// Token: 0x04006ED4 RID: 28372
		private Stack<NKCPopupGuildCoopArtifactStorageSlot> m_stkSlot = new Stack<NKCPopupGuildCoopArtifactStorageSlot>();

		// Token: 0x04006ED5 RID: 28373
		private List<NKCUIComGuildArtifactContent.ArtifactSlotData> m_lstSlotDataByArena = new List<NKCUIComGuildArtifactContent.ArtifactSlotData>();

		// Token: 0x04006ED6 RID: 28374
		private List<NKCUIComGuildArtifactContent.ArtifactSlotData> m_lstSlotDataByID = new List<NKCUIComGuildArtifactContent.ArtifactSlotData>();

		// Token: 0x04006ED7 RID: 28375
		private NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE m_CurViewType;

		// Token: 0x04006ED8 RID: 28376
		private NKCUIComGuildArtifactContent.GUILD_ARTIFACT_STORAGE_VIEW_TYPE m_LastViewType;

		// Token: 0x020018CB RID: 6347
		public enum GUILD_ARTIFACT_STORAGE_VIEW_TYPE
		{
			// Token: 0x0400A9E6 RID: 43494
			NONE,
			// Token: 0x0400A9E7 RID: 43495
			ALL,
			// Token: 0x0400A9E8 RID: 43496
			EACH,
			// Token: 0x0400A9E9 RID: 43497
			ARENA
		}

		// Token: 0x020018CC RID: 6348
		public class ArtifactSlotData
		{
			// Token: 0x0600B6C1 RID: 46785 RVA: 0x003671FD File Offset: 0x003653FD
			public static NKCUIComGuildArtifactContent.ArtifactSlotData MakeSlotData(int arenaNum)
			{
				return new NKCUIComGuildArtifactContent.ArtifactSlotData
				{
					bIsArenaNum = true,
					id = arenaNum
				};
			}

			// Token: 0x0600B6C2 RID: 46786 RVA: 0x00367212 File Offset: 0x00365412
			public static NKCUIComGuildArtifactContent.ArtifactSlotData MakeSlotData(GuildDungeonArtifactTemplet templet)
			{
				return new NKCUIComGuildArtifactContent.ArtifactSlotData
				{
					bIsArenaNum = false,
					id = templet.GetArtifactId(),
					iconName = templet.GetIconName(),
					name = templet.GetName(),
					desc = templet.GetDescFull()
				};
			}

			// Token: 0x0400A9EA RID: 43498
			public bool bIsArenaNum;

			// Token: 0x0400A9EB RID: 43499
			public int id;

			// Token: 0x0400A9EC RID: 43500
			public string iconName;

			// Token: 0x0400A9ED RID: 43501
			public string name;

			// Token: 0x0400A9EE RID: 43502
			public string desc;
		}
	}
}
