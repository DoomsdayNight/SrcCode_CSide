using System;
using NKC.Office;
using NKC.UI;
using NKC.UI.Office;
using NKM;
using NKM.Templet;
using NKM.Templet.Office;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000723 RID: 1827
	public class NKC_SCEN_OFFICE : NKC_SCEN_BASIC
	{
		// Token: 0x0600489E RID: 18590 RVA: 0x0015E87F File Offset: 0x0015CA7F
		public NKC_SCEN_OFFICE()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_OFFICE;
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x0015E88F File Offset: 0x0015CA8F
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			NKCOfficeManager.LoadTemplets();
			if (!NKCUIManager.IsValid(this.m_loadUIDataOffice))
			{
				this.m_loadUIDataOffice = NKCUIOffice.OpenNewInstanceAsync();
			}
			if (!NKCUIManager.IsValid(this.m_loadUIDataMapFront))
			{
				this.m_loadUIDataMapFront = NKCUIOfficeMapFront.OpenNewInstanceAsync();
			}
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x0015E8CC File Offset: 0x0015CACC
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (this.m_uiOffice == null)
			{
				if (this.m_loadUIDataOffice != null && this.m_loadUIDataOffice.CheckLoadAndGetInstance<NKCUIOffice>(out this.m_uiOffice))
				{
					this.m_uiOffice.Init();
					this.m_uiOffice.Preload();
					NKCUtil.SetGameobjectActive(this.m_uiOffice.gameObject, false);
				}
				else
				{
					Debug.LogError("NKC_SCEN_OFFICE.ScenLoadUIComplete - ui load AB_UI_OFFICE fail");
				}
			}
			if (this.m_uiOfficeMapFront == null)
			{
				if (this.m_loadUIDataMapFront != null && this.m_loadUIDataMapFront.CheckLoadAndGetInstance<NKCUIOfficeMapFront>(out this.m_uiOfficeMapFront))
				{
					this.m_uiOfficeMapFront.Init();
					NKCUtil.SetGameobjectActive(this.m_uiOfficeMapFront.gameObject, false);
					return;
				}
				Debug.LogError("NKC_SCEN_OFFICE.ScenLoadUIComplete - ui load AB_UI_OFFICE_MINIMAP_ROOM fail");
			}
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x0015E98C File Offset: 0x0015CB8C
		public override void ScenStart()
		{
			base.ScenStart();
			if (this.m_uiOfficeMapFront == null)
			{
				Debug.LogError("MapFront ui not found");
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_GAME_LOAD_FAILED, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			this.m_uiOfficeMapFront.Open();
			if (this.m_eReserverdShortcutType != NKM_SHORTCUT_TYPE.SHORTCUT_NONE)
			{
				this.ProcessShortcut();
			}
			if (NKCScenManager.CurrentUserData().OfficeData.BizcardCount == 0)
			{
				NKCScenManager.CurrentUserData().OfficeData.TryRefreshOfficePost(false);
			}
			if (NKCUIJukeBox.IsHasInstance && NKCUIJukeBox.Instance.AlreadyJukeBoxMode)
			{
				NKCUIManager.GetNKCUIPowerSaveMode().SetFinishJukeBox(true);
				NKCScenManager.GetScenManager().GetNKCPowerSaveMode().SetJukeBoxMode(true);
			}
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x0015EA4C File Offset: 0x0015CC4C
		private void ProcessShortcut()
		{
			if (this.m_uiOffice.IsOpen)
			{
				this.m_uiOffice.Close();
			}
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = null;
			NKM_SHORTCUT_TYPE eReserverdShortcutType = this.m_eReserverdShortcutType;
			switch (eReserverdShortcutType)
			{
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_SCOUT:
			{
				NKMOfficeRoomTemplet nkmofficeRoomTemplet2 = NKMOfficeRoomTemplet.Find(NKMOfficeRoomTemplet.RoomType.CEO);
				if (nkmofficeRoomTemplet2 != null)
				{
					this.m_uiOffice.Open(nkmofficeRoomTemplet2.ID);
					this.m_uiOffice.OfficeFacilityInterfaces.OnCEOScout();
				}
				break;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_LIFETIME:
			{
				NKMOfficeRoomTemplet nkmofficeRoomTemplet3 = NKMOfficeRoomTemplet.Find(NKMOfficeRoomTemplet.RoomType.CEO);
				if (nkmofficeRoomTemplet3 != null)
				{
					this.m_uiOffice.Open(nkmofficeRoomTemplet3.ID);
					long uid;
					if (long.TryParse(this.m_ReservedShortCutParam, out uid))
					{
						this.m_uiOffice.OfficeFacilityInterfaces.OnCEOLifetime(uid);
					}
					else
					{
						this.m_uiOffice.OfficeFacilityInterfaces.OnCEOLifetime(0L);
					}
				}
				break;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_DISMISS:
			case NKM_SHORTCUT_TYPE.SHORTCUT_EQUIP_ENCHANT:
			case NKM_SHORTCUT_TYPE.SHORTCUT_EQUIP_TUNING:
				break;
			case NKM_SHORTCUT_TYPE.SHORTCUT_EQUIP_MAKE:
			{
				NKMOfficeRoomTemplet nkmofficeRoomTemplet4 = NKMOfficeRoomTemplet.Find(NKMOfficeRoomTemplet.RoomType.Forge);
				if (nkmofficeRoomTemplet4 != null)
				{
					this.m_uiOffice.Open(nkmofficeRoomTemplet4.ID);
					this.m_uiOffice.OfficeFacilityInterfaces.OnForgeBuild();
					NKM_CRAFT_TAB_TYPE tabType;
					if (!string.IsNullOrEmpty(this.m_ReservedShortCutParam) && NKCUIForgeCraftMold.IsInstanceOpen && Enum.TryParse<NKM_CRAFT_TAB_TYPE>(this.m_ReservedShortCutParam, out tabType))
					{
						NKCUIForgeCraftMold.Instance.SelectCraftTab(tabType);
					}
				}
				break;
			}
			case NKM_SHORTCUT_TYPE.SHORTCUT_SHIP_MAKE:
			{
				NKMOfficeRoomTemplet nkmofficeRoomTemplet5 = NKMOfficeRoomTemplet.Find(NKMOfficeRoomTemplet.RoomType.Hangar);
				if (nkmofficeRoomTemplet5 != null)
				{
					this.m_uiOffice.Open(nkmofficeRoomTemplet5.ID);
					this.m_uiOffice.OfficeFacilityInterfaces.OnHangarBuild();
				}
				break;
			}
			default:
				if (eReserverdShortcutType == NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE)
				{
					int roomID;
					NKMOfficeRoomTemplet.RoomType key;
					NKCUIOfficeMapFront.SectionType sectionType;
					ContentsType contentsType;
					if (int.TryParse(this.m_ReservedShortCutParam, out roomID))
					{
						this.m_uiOffice.Open(roomID);
					}
					else if (Enum.TryParse<NKMOfficeRoomTemplet.RoomType>(this.m_ReservedShortCutParam, out key))
					{
						nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(key);
						if (nkmofficeRoomTemplet != null)
						{
							this.m_uiOffice.Open(nkmofficeRoomTemplet.ID);
						}
					}
					else if (Enum.TryParse<NKCUIOfficeMapFront.SectionType>(this.m_ReservedShortCutParam, out sectionType))
					{
						if (sectionType != NKCUIOfficeMapFront.SectionType.Room)
						{
							if (sectionType == NKCUIOfficeMapFront.SectionType.Facility)
							{
								this.m_uiOfficeMapFront.SelectFacilityTab();
							}
						}
						else
						{
							this.m_uiOfficeMapFront.SelectRoomTab();
						}
					}
					else if (Enum.TryParse<ContentsType>(this.m_ReservedShortCutParam, out contentsType) && contentsType == ContentsType.EXTRACT)
					{
						if (!NKCContentManager.IsContentsUnlocked(ContentsType.EXTRACT, 0, 0))
						{
							NKCContentManager.ShowLockedMessagePopup(ContentsType.EXTRACT, 0);
						}
						else
						{
							NKMOfficeRoomTemplet nkmofficeRoomTemplet6 = NKMOfficeRoomTemplet.Find(NKMOfficeRoomTemplet.RoomType.Lab);
							if (nkmofficeRoomTemplet6 != null)
							{
								this.m_uiOffice.Open(nkmofficeRoomTemplet6.ID);
								this.m_uiOffice.OfficeFacilityInterfaces.OnLabUnitExtract();
							}
						}
					}
				}
				break;
			}
			if (NKCScenManager.CurrentUserData().OfficeData.GetFriendProfile() != null)
			{
				this.m_uiOfficeMapFront.SetFriendOfficeData();
			}
			else
			{
				this.m_uiOfficeMapFront.SetMyOfficeData(nkmofficeRoomTemplet);
			}
			this.m_eReserverdShortcutType = NKM_SHORTCUT_TYPE.SHORTCUT_NONE;
			this.m_ReservedShortCutParam = null;
		}

		// Token: 0x060048A3 RID: 18595 RVA: 0x0015ECFA File Offset: 0x0015CEFA
		public override void ScenEnd()
		{
			base.ScenEnd();
			NKCUIOffice uiOffice = this.m_uiOffice;
			if (uiOffice != null)
			{
				uiOffice.Close();
			}
			NKCUIOfficeMapFront uiOfficeMapFront = this.m_uiOfficeMapFront;
			if (uiOfficeMapFront != null)
			{
				uiOfficeMapFront.Close();
			}
			this.UnloadUI();
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x0015ED2C File Offset: 0x0015CF2C
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_uiOffice = null;
			NKCUIManager.LoadedUIData loadUIDataOffice = this.m_loadUIDataOffice;
			if (loadUIDataOffice != null)
			{
				loadUIDataOffice.CloseInstance();
			}
			this.m_loadUIDataOffice = null;
			this.m_uiOfficeMapFront = null;
			NKCUIManager.LoadedUIData loadUIDataMapFront = this.m_loadUIDataMapFront;
			if (loadUIDataMapFront != null)
			{
				loadUIDataMapFront.CloseInstance();
			}
			this.m_loadUIDataMapFront = null;
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x0015ED7D File Offset: 0x0015CF7D
		public void ReserveShortcut(NKM_SHORTCUT_TYPE shortCut, string shortcutParam = null)
		{
			this.m_eReserverdShortcutType = shortCut;
			this.m_ReservedShortCutParam = shortcutParam;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_OFFICE)
			{
				this.ProcessShortcut();
			}
		}

		// Token: 0x0400384E RID: 14414
		private NKCUIOffice m_uiOffice;

		// Token: 0x0400384F RID: 14415
		private NKCUIManager.LoadedUIData m_loadUIDataOffice;

		// Token: 0x04003850 RID: 14416
		private NKCUIOfficeMapFront m_uiOfficeMapFront;

		// Token: 0x04003851 RID: 14417
		private NKCUIManager.LoadedUIData m_loadUIDataMapFront;

		// Token: 0x04003852 RID: 14418
		private NKM_SHORTCUT_TYPE m_eReserverdShortcutType;

		// Token: 0x04003853 RID: 14419
		private string m_ReservedShortCutParam;
	}
}
