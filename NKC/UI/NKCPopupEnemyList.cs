using System;
using System.Collections.Generic;
using System.Linq;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A43 RID: 2627
	public class NKCPopupEnemyList : NKCUIBase
	{
		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06007347 RID: 29511 RVA: 0x00265288 File Offset: 0x00263488
		public static NKCPopupEnemyList Instance
		{
			get
			{
				if (NKCPopupEnemyList.m_Instance == null)
				{
					NKCPopupEnemyList.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEnemyList>("ab_ui_nkm_ui_popup_enemy", "NKM_UI_POPUP_ENEMY_LIST", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEnemyList.CleanupInstance)).GetInstance<NKCPopupEnemyList>();
					NKCPopupEnemyList.m_Instance.Init();
				}
				return NKCPopupEnemyList.m_Instance;
			}
		}

		// Token: 0x06007348 RID: 29512 RVA: 0x002652D7 File Offset: 0x002634D7
		private static void CleanupInstance()
		{
			NKCPopupEnemyList.m_Instance = null;
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06007349 RID: 29513 RVA: 0x002652DF File Offset: 0x002634DF
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEnemyList.m_Instance != null && NKCPopupEnemyList.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600734A RID: 29514 RVA: 0x002652FA File Offset: 0x002634FA
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupEnemyList.m_Instance != null && NKCPopupEnemyList.m_Instance.IsOpen)
			{
				NKCPopupEnemyList.m_Instance.Close();
			}
		}

		// Token: 0x0600734B RID: 29515 RVA: 0x0026531F File Offset: 0x0026351F
		private void OnDestroy()
		{
			NKCPopupEnemyList.m_Instance = null;
		}

		// Token: 0x0600734C RID: 29516 RVA: 0x00265327 File Offset: 0x00263527
		public override void CloseInternal()
		{
			this.Clear();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x0600734D RID: 29517 RVA: 0x0026533B File Offset: 0x0026353B
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x0600734E RID: 29518 RVA: 0x0026533E File Offset: 0x0026353E
		public override string MenuName
		{
			get
			{
				return "ENEMY_LIST_POPUP";
			}
		}

		// Token: 0x0600734F RID: 29519 RVA: 0x00265345 File Offset: 0x00263545
		private void Init()
		{
			NKCUtil.SetBindFunction(this.m_csbtnClose, new UnityAction(base.Close));
			NKCUtil.SetEventTriggerDelegate(this.m_eventTrigger, new UnityAction(base.Close));
		}

		// Token: 0x06007350 RID: 29520 RVA: 0x00265378 File Offset: 0x00263578
		public void Open(NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet == null)
			{
				return;
			}
			Dictionary<string, NKCEnemyData> enemyUnits = NKMDungeonManager.GetEnemyUnits(stageTemplet);
			this.Open(enemyUnits.Values.ToList<NKCEnemyData>());
		}

		// Token: 0x06007351 RID: 29521 RVA: 0x002653A4 File Offset: 0x002635A4
		public void Open(NKMWarfareTemplet cNKMWarfareTemplet)
		{
			if (cNKMWarfareTemplet == null)
			{
				return;
			}
			Dictionary<string, NKCEnemyData> enemyUnits = NKMDungeonManager.GetEnemyUnits(cNKMWarfareTemplet);
			this.Open(enemyUnits.Values.ToList<NKCEnemyData>());
		}

		// Token: 0x06007352 RID: 29522 RVA: 0x002653D0 File Offset: 0x002635D0
		public void Open(NKMDungeonTempletBase cNKMDungeonTempletBase)
		{
			if (cNKMDungeonTempletBase == null)
			{
				return;
			}
			Dictionary<string, NKCEnemyData> enemyUnits = NKMDungeonManager.GetEnemyUnits(cNKMDungeonTempletBase);
			this.Open(enemyUnits.Values.ToList<NKCEnemyData>());
		}

		// Token: 0x06007353 RID: 29523 RVA: 0x002653FC File Offset: 0x002635FC
		public void Open(NKMDungeonTempletBase cNKMDungeonTempletBase, NKMDiveTemplet diveTemplet, bool isBossSector)
		{
			if (cNKMDungeonTempletBase == null)
			{
				return;
			}
			Dictionary<string, NKCEnemyData> enemyUnits = NKMDungeonManager.GetEnemyUnits(cNKMDungeonTempletBase);
			if (diveTemplet != null)
			{
				foreach (NKCEnemyData nkcenemyData in enemyUnits.Values)
				{
					if (isBossSector)
					{
						nkcenemyData.m_Level += diveTemplet.SetLevelScale + diveTemplet.StageLevelScale;
					}
					else
					{
						nkcenemyData.m_Level += diveTemplet.StageLevelScale;
					}
				}
			}
			this.Open(enemyUnits.Values.ToList<NKCEnemyData>());
		}

		// Token: 0x06007354 RID: 29524 RVA: 0x0026549C File Offset: 0x0026369C
		public void Open(List<NKCEnemyData> lstEnemyUnits)
		{
			NKCUtil.SetLabelText(this.m_lbBattleTitle, NKCUtilString.GET_STRING_ENEMY_LIST_TITLE);
			if (lstEnemyUnits.Count <= 0)
			{
				return;
			}
			this.UpdateEnemySlot(lstEnemyUnits);
			NKCUtil.SetGameobjectActive(this.m_objBottomWarfareUI, false);
			base.UIOpened(true);
		}

		// Token: 0x06007355 RID: 29525 RVA: 0x002654D4 File Offset: 0x002636D4
		public void Open(int dungeonID, string battleConditionStrID = "")
		{
			NKCUtil.SetGameobjectActive(this.m_objBottomWarfareUI, true);
			NKCUtil.SetLabelText(this.m_lbBattleTitle, NKCUtilString.GET_STRING_ENEMY_LIST_TITLE);
			string msg = NKCUtilString.GET_STRING_WARFARE_POPUP_ENEMY_INFO_KILL;
			string msg2 = "";
			NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(dungeonID);
			if (dungeonTemplet != null && dungeonTemplet.m_DungeonTempletBase != null)
			{
				Dictionary<string, NKCEnemyData> enemyUnits = NKMDungeonManager.GetEnemyUnits(dungeonTemplet.m_DungeonTempletBase);
				this.UpdateEnemySlot(enemyUnits.Values.ToList<NKCEnemyData>());
				if (dungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_WAVE)
				{
					msg = NKCUtilString.GET_STRING_WARFARE_POPUP_ENEMY_INFO_WAVE;
					msg2 = string.Format(NKCUtilString.GET_STRING_WARFARE_POPUP_ENEMY_INFO_WAVE_ONE_PARAM, dungeonTemplet.m_listDungeonWave.Count);
				}
				NKCUtil.SetLabelText(this.m_lbMedalDesc3, NKCUtilString.GetDGMissionText(DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR, 0));
				DUNGEON_GAME_MISSION_TYPE dgmissionType_ = dungeonTemplet.m_DungeonTempletBase.m_DGMissionType_1;
				DUNGEON_GAME_MISSION_TYPE dgmissionType_2 = dungeonTemplet.m_DungeonTempletBase.m_DGMissionType_2;
				int dgmissionValue_ = dungeonTemplet.m_DungeonTempletBase.m_DGMissionValue_1;
				int dgmissionValue_2 = dungeonTemplet.m_DungeonTempletBase.m_DGMissionValue_2;
				NKCUtil.SetLabelText(this.m_lbMedalDesc2, NKCUtilString.GetDGMissionText(dgmissionType_, dgmissionValue_));
				NKCUtil.SetLabelText(this.m_lbMedalDesc1, NKCUtilString.GetDGMissionText(dgmissionType_2, dgmissionValue_2));
			}
			NKCUtil.SetLabelText(this.m_lbVictoryCond1, msg);
			NKCUtil.SetLabelText(this.m_lbVictoryCond2, msg2);
			NKCUIComStateButton csbtnBattleCond = this.m_csbtnBattleCond;
			if (csbtnBattleCond != null)
			{
				csbtnBattleCond.PointerDown.RemoveAllListeners();
			}
			if (!string.IsNullOrEmpty(battleConditionStrID))
			{
				NKMBattleConditionTemplet cNKMBattleConditionTemplet = NKMBattleConditionManager.GetTempletByStrID(battleConditionStrID);
				if (cNKMBattleConditionTemplet != null)
				{
					NKCUtil.SetImageSprite(this.m_imgBattleCond, NKCUtil.GetSpriteBattleConditionICon(cNKMBattleConditionTemplet), false);
					NKCUIComStateButton csbtnBattleCond2 = this.m_csbtnBattleCond;
					if (csbtnBattleCond2 != null)
					{
						csbtnBattleCond2.PointerDown.AddListener(delegate(PointerEventData e)
						{
							NKCUITooltip.Instance.Open(NKCUISlot.eSlotMode.Etc, cNKMBattleConditionTemplet.BattleCondName_Translated, cNKMBattleConditionTemplet.BattleCondDesc_Translated, new Vector2?(e.position));
						});
					}
				}
				NKCUtil.SetGameobjectActive(this.m_objBattleCond, cNKMBattleConditionTemplet.BattleCondID != 0);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objBattleCond, false);
			}
			base.UIOpened(true);
		}

		// Token: 0x06007356 RID: 29526 RVA: 0x00265698 File Offset: 0x00263898
		private void UpdateEnemySlot(List<NKCEnemyData> lstEnemyUnits)
		{
			this.m_lstEnemyData = lstEnemyUnits;
			this.Clear();
			this.m_lstEnemyData.Sort(new NKCEnemyData.CompNED());
			for (int i = 0; i < this.m_lstEnemyData.Count; i++)
			{
				if (this.m_lstEnemyData[i] != null)
				{
					NKCDeckViewEnemySlot unitSlot = this.GetSlot(i);
					if (null != unitSlot)
					{
						if (unitSlot.m_NKCUIComButton != null)
						{
							unitSlot.m_NKCUIComButton.PointerClick.AddListener(delegate()
							{
								this.OnSelectUnitInfo(unitSlot.m_Index);
							});
						}
						unitSlot.SetEnemyData(NKMUnitManager.GetUnitTempletBase(this.m_lstEnemyData[i].m_UnitStrID), this.m_lstEnemyData[i]);
					}
				}
			}
			this.OnSelectUnitInfo(0);
		}

		// Token: 0x06007357 RID: 29527 RVA: 0x00265780 File Offset: 0x00263980
		private NKCDeckViewEnemySlot GetSlot(int iCnt)
		{
			NKCDeckViewEnemySlot newInstance = NKCDeckViewEnemySlot.GetNewInstance(this.m_rtLeftSlotParent);
			if (null != newInstance)
			{
				newInstance.Init(iCnt);
				newInstance.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
				Vector3 localPosition = newInstance.gameObject.transform.localPosition;
				newInstance.gameObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);
				this.m_lstEnemySlot.Add(newInstance);
			}
			return newInstance;
		}

		// Token: 0x06007358 RID: 29528 RVA: 0x00265814 File Offset: 0x00263A14
		private void Clear()
		{
			for (int i = 0; i < this.m_lstEnemySlot.Count; i++)
			{
				if (null != this.m_lstEnemySlot[i])
				{
					UnityEngine.Object.Destroy(this.m_lstEnemySlot[i].gameObject);
					this.m_lstEnemySlot[i] = null;
				}
			}
			this.m_lstEnemySlot.Clear();
		}

		// Token: 0x06007359 RID: 29529 RVA: 0x0026587C File Offset: 0x00263A7C
		private void OnSelectUnitInfo(int idx)
		{
			if (this.m_lstEnemyData.Count <= idx)
			{
				Debug.LogError(string.Format("Size Error {0} / max {1}", idx, this.m_lstEnemyData.Count - 1));
				return;
			}
			if (this.m_lstEnemyData[idx] == null)
			{
				return;
			}
			foreach (NKCDeckViewEnemySlot nkcdeckViewEnemySlot in this.m_lstEnemySlot)
			{
				if (nkcdeckViewEnemySlot.m_Index == idx)
				{
					nkcdeckViewEnemySlot.ButtonSelect();
				}
				else
				{
					nkcdeckViewEnemySlot.ButtonDeSelect(true, true);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objBossIcon, this.m_lstEnemyData[idx].m_NKM_BOSS_TYPE > NKM_BOSS_TYPE.NBT_NONE);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_lstEnemyData[idx].m_UnitStrID);
			this.m_imgEnemyFace.preserveAspect = (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP && unitTempletBase.IsShip());
			NKCUtil.SetImageSprite(this.m_imgEnemyFace, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase), false);
			NKCUtil.SetLabelText(this.m_lbEnemyType, NKCUtilString.GetUnitStyleMarkString(unitTempletBase));
			string msg = (!string.IsNullOrEmpty(this.m_lstEnemyData[idx].m_ChangeUnitName)) ? NKCStringTable.GetString(this.m_lstEnemyData[idx].m_ChangeUnitName, false) : unitTempletBase.GetUnitName();
			NKCUtil.SetLabelText(this.m_lbEnemyName, msg);
			NKCUtil.SetLabelText(this.m_lbEnemyLv, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, this.m_lstEnemyData[idx].m_Level));
			NKCUtil.SetImageSprite(this.m_imgEnemyClass, NKCResourceUtility.GetOrLoadUnitRoleIcon(unitTempletBase, false), false);
			if (unitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_INVALID)
			{
				NKCUtil.SetLabelText(this.m_lbEnemyClass, "");
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbEnemyClass, NKCUtilString.GetRoleText(unitTempletBase));
			}
			if (this.m_imgEnemyAttackType != null)
			{
				if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID)
				{
					this.m_imgEnemyAttackType.sprite = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase.m_NKM_FIND_TARGET_TYPE, false);
				}
				else
				{
					this.m_imgEnemyAttackType.sprite = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc, false);
				}
			}
			if (this.m_lbEnemyAttackType != null)
			{
				if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID)
				{
					this.m_lbEnemyAttackType.text = NKCUtilString.GetAtkTypeText(unitTempletBase.m_NKM_FIND_TARGET_TYPE);
				}
				else
				{
					this.m_lbEnemyAttackType.text = NKCUtilString.GetAtkTypeText(unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc);
				}
			}
			this.UpdateMonsterSkill(unitTempletBase.m_UnitID);
		}

		// Token: 0x0600735A RID: 29530 RVA: 0x00265AE0 File Offset: 0x00263CE0
		private void UpdateMonsterSkill(int unitID)
		{
			Debug.Log(string.Format("<color=green>UpdateMonsterSkill : {0}</color>", unitID));
			NKCMonsterTagTemplet nkcmonsterTagTemplet = NKMTempletContainer<NKCMonsterTagTemplet>.Find(unitID);
			if (nkcmonsterTagTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objTagNone, true);
				NKCUtil.SetGameobjectActive(this.m_objTagInfo, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objTagNone, false);
			NKCUtil.SetGameobjectActive(this.m_objTagInfo, true);
			int i = 0;
			while (i < this.m_lstSkillInfo.Count)
			{
				if (nkcmonsterTagTemplet.lstTags.Count <= i)
				{
					goto IL_DD;
				}
				NKCMonsterTagInfoTemplet nkcmonsterTagInfoTemplet = NKMTempletContainer<NKCMonsterTagInfoTemplet>.Find(nkcmonsterTagTemplet.lstTags[i]);
				if (nkcmonsterTagInfoTemplet == null)
				{
					goto IL_DD;
				}
				NKCUtil.SetLabelText(this.m_lstSkillInfo[i].Desc, NKCStringTable.GetString(nkcmonsterTagInfoTemplet.m_MonsterTagDesc, false));
				NKCUtil.SetImageSprite(this.m_lstSkillInfo[i].Icon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_ENEMY_SKILL_ICON", nkcmonsterTagInfoTemplet.m_MonsterTagIcon, false), false);
				NKCUtil.SetGameobjectActive(this.m_lstSkillInfo[i].obj, true);
				IL_F4:
				i++;
				continue;
				IL_DD:
				NKCUtil.SetGameobjectActive(this.m_lstSkillInfo[i].obj, false);
				goto IL_F4;
			}
		}

		// Token: 0x04005F38 RID: 24376
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_enemy";

		// Token: 0x04005F39 RID: 24377
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_ENEMY_LIST";

		// Token: 0x04005F3A RID: 24378
		private static NKCPopupEnemyList m_Instance;

		// Token: 0x04005F3B RID: 24379
		[Header("Common")]
		public Text m_lbBattleTitle;

		// Token: 0x04005F3C RID: 24380
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04005F3D RID: 24381
		public EventTrigger m_eventTrigger;

		// Token: 0x04005F3E RID: 24382
		[Header("Enemy UI")]
		public Image m_imgEnemyFace;

		// Token: 0x04005F3F RID: 24383
		public GameObject m_objBossIcon;

		// Token: 0x04005F40 RID: 24384
		public Text m_lbEnemyName;

		// Token: 0x04005F41 RID: 24385
		public Text m_lbEnemyType;

		// Token: 0x04005F42 RID: 24386
		public Text m_lbEnemyLv;

		// Token: 0x04005F43 RID: 24387
		public Image m_imgEnemyClass;

		// Token: 0x04005F44 RID: 24388
		public Text m_lbEnemyClass;

		// Token: 0x04005F45 RID: 24389
		public Image m_imgEnemyAttackType;

		// Token: 0x04005F46 RID: 24390
		public Text m_lbEnemyAttackType;

		// Token: 0x04005F47 RID: 24391
		public RectTransform m_rtLeftSlotParent;

		// Token: 0x04005F48 RID: 24392
		[Space]
		public List<NKCPopupEnemyList.strSkillInfo> m_lstSkillInfo;

		// Token: 0x04005F49 RID: 24393
		public GameObject m_objTagInfo;

		// Token: 0x04005F4A RID: 24394
		public GameObject m_objTagNone;

		// Token: 0x04005F4B RID: 24395
		[Header("Warfare UI")]
		public GameObject m_objBottomWarfareUI;

		// Token: 0x04005F4C RID: 24396
		public Text m_lbVictoryCond1;

		// Token: 0x04005F4D RID: 24397
		public Text m_lbVictoryCond2;

		// Token: 0x04005F4E RID: 24398
		public Text m_lbMedalDesc1;

		// Token: 0x04005F4F RID: 24399
		public Text m_lbMedalDesc2;

		// Token: 0x04005F50 RID: 24400
		public Text m_lbMedalDesc3;

		// Token: 0x04005F51 RID: 24401
		[Space]
		public GameObject m_objBattleCond;

		// Token: 0x04005F52 RID: 24402
		public Image m_imgBattleCond;

		// Token: 0x04005F53 RID: 24403
		public NKCUIComStateButton m_csbtnBattleCond;

		// Token: 0x04005F54 RID: 24404
		[Header("prefab")]
		private List<NKCDeckViewEnemySlot> m_lstEnemySlot = new List<NKCDeckViewEnemySlot>();

		// Token: 0x04005F55 RID: 24405
		private List<NKCEnemyData> m_lstEnemyData = new List<NKCEnemyData>();

		// Token: 0x0200178B RID: 6027
		[Serializable]
		public struct strSkillInfo
		{
			// Token: 0x0400A70A RID: 42762
			public GameObject obj;

			// Token: 0x0400A70B RID: 42763
			public Image Icon;

			// Token: 0x0400A70C RID: 42764
			public Text Desc;
		}
	}
}
