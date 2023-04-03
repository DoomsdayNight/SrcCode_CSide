using System;
using System.Collections.Generic;
using System.Text;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B02 RID: 2818
	public class NKCUITooltip : NKCUIBase
	{
		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x0600802E RID: 32814 RVA: 0x002B399C File Offset: 0x002B1B9C
		public static NKCUITooltip Instance
		{
			get
			{
				if (NKCUITooltip.m_Instance == null)
				{
					NKCUITooltip.m_Instance = NKCUIManager.OpenNewInstance<NKCUITooltip>("AB_UI_NKM_UI_POPUP_TOOLTIP", "NKM_UI_POPUP_TOOLTIP", NKCUIManager.eUIBaseRect.UIOverlay, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUITooltip.CleanupInstance)).GetInstance<NKCUITooltip>();
					NKCUITooltip.m_Instance.Init();
				}
				return NKCUITooltip.m_Instance;
			}
		}

		// Token: 0x0600802F RID: 32815 RVA: 0x002B39EB File Offset: 0x002B1BEB
		private static void CleanupInstance()
		{
			NKCUITooltip.m_Instance = null;
		}

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x06008030 RID: 32816 RVA: 0x002B39F3 File Offset: 0x002B1BF3
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUITooltip.m_Instance != null && NKCUITooltip.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008031 RID: 32817 RVA: 0x002B3A0E File Offset: 0x002B1C0E
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.Clear();
		}

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x06008032 RID: 32818 RVA: 0x002B3A22 File Offset: 0x002B1C22
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_TOOLTIP;
			}
		}

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x06008033 RID: 32819 RVA: 0x002B3A29 File Offset: 0x002B1C29
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x06008034 RID: 32820 RVA: 0x002B3A2C File Offset: 0x002B1C2C
		private void Init()
		{
			if (this.m_RectToCalcTouchPos != null)
			{
				return;
			}
			this.m_RectToCalcTouchPos = base.GetComponent<RectTransform>();
		}

		// Token: 0x06008035 RID: 32821 RVA: 0x002B3A4C File Offset: 0x002B1C4C
		public void Open(NKCUISlot.SlotData slotData, Vector2? touchPos)
		{
			if (slotData == null)
			{
				return;
			}
			List<NKCUITooltip.Data> list = new List<NKCUITooltip.Data>();
			if (slotData.eType == NKCUISlot.eSlotMode.DiveArtifact)
			{
				NKCUITooltip.DiveArtifactData item = new NKCUITooltip.DiveArtifactData(slotData);
				list.Add(item);
			}
			else if (slotData.eType == NKCUISlot.eSlotMode.Unit || slotData.eType == NKCUISlot.eSlotMode.UnitCount)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(slotData.ID);
				if (unitTempletBase == null)
				{
					return;
				}
				NKCUITooltip.UnitData item2 = new NKCUITooltip.UnitData(slotData, unitTempletBase);
				list.Add(item2);
			}
			else
			{
				NKCUITooltip.ItemData item3 = new NKCUITooltip.ItemData(slotData);
				list.Add(item3);
				NKCUITooltip.TextData item4 = new NKCUITooltip.TextData(NKCUISlot.GetDesc(slotData.eType, slotData.ID, false));
				list.Add(item4);
			}
			this.m_vlg.spacing = 160f;
			this.Open(list, touchPos);
		}

		// Token: 0x06008036 RID: 32822 RVA: 0x002B3AF8 File Offset: 0x002B1CF8
		public void Open(NKCUISlot.eSlotMode type, string title, string desc, Vector2? touchPos)
		{
			List<NKCUITooltip.Data> list = new List<NKCUITooltip.Data>();
			if (type == NKCUISlot.eSlotMode.Etc)
			{
				NKCUITooltip.EtcData item = new NKCUITooltip.EtcData(title, desc);
				list.Add(item);
			}
			this.m_vlg.spacing = 160f;
			this.Open(list, touchPos);
		}

		// Token: 0x06008037 RID: 32823 RVA: 0x002B3B38 File Offset: 0x002B1D38
		public void Open(NKMUnitTempletBase unitTempletBase, Vector2? touchPos)
		{
			if (unitTempletBase == null)
			{
				return;
			}
			List<NKCUITooltip.Data> list = new List<NKCUITooltip.Data>();
			NKCUITooltip.UnitData item = new NKCUITooltip.UnitData(NKCUISlot.SlotData.MakeUnitData(unitTempletBase.m_UnitID, 1, 0, 0), unitTempletBase);
			list.Add(item);
			this.m_vlg.spacing = 160f;
			this.Open(list, touchPos);
		}

		// Token: 0x06008038 RID: 32824 RVA: 0x002B3B84 File Offset: 0x002B1D84
		public void Open(NKMShipSkillTemplet shipSkillTemplet, Vector2? touchPos)
		{
			if (shipSkillTemplet == null)
			{
				return;
			}
			List<NKCUITooltip.Data> list = new List<NKCUITooltip.Data>();
			NKCUITooltip.ShipSkillData item = new NKCUITooltip.ShipSkillData(shipSkillTemplet);
			list.Add(item);
			NKCUITooltip.TextData item2 = new NKCUITooltip.TextData(shipSkillTemplet.GetDesc());
			list.Add(item2);
			this.m_vlg.spacing = 160f;
			this.Open(list, touchPos);
		}

		// Token: 0x06008039 RID: 32825 RVA: 0x002B3BD4 File Offset: 0x002B1DD4
		public void Open(NKMTacticalCommandTemplet cNKMTacticalCommandTemplet, int level, Vector2? touchPos)
		{
			if (cNKMTacticalCommandTemplet == null)
			{
				return;
			}
			if (!cNKMTacticalCommandTemplet.CheckMyTeamTargetBuffExist() && !cNKMTacticalCommandTemplet.CheckEnemyTargetBuffExist())
			{
				return;
			}
			List<NKCUITooltip.Data> list = new List<NKCUITooltip.Data>();
			NKCUITooltip.TacticalCommandData item = new NKCUITooltip.TacticalCommandData(cNKMTacticalCommandTemplet);
			list.Add(item);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(NKCUtilString.ApplyBuffValueToString(cNKMTacticalCommandTemplet, level));
			stringBuilder.Append("\n\n");
			if (cNKMTacticalCommandTemplet.m_NKM_TACTICAL_COMMAND_TYPE == NKM_TACTICAL_COMMAND_TYPE.NTCT_ACTIVE)
			{
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_TC_COST", false), cNKMTacticalCommandTemplet.m_StartCost, cNKMTacticalCommandTemplet.m_CostAdd) + "\n");
			}
			else if (cNKMTacticalCommandTemplet.m_NKM_TACTICAL_COMMAND_TYPE == NKM_TACTICAL_COMMAND_TYPE.NTCT_COMBO)
			{
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_COOLTIME_ONE_PARAM", false), (int)cNKMTacticalCommandTemplet.m_fCoolTime) + "\n");
			}
			NKCUITooltip.TextData item2 = new NKCUITooltip.TextData(stringBuilder.ToString());
			list.Add(item2);
			this.m_vlg.spacing = 160f;
			this.Open(list, touchPos);
		}

		// Token: 0x0600803A RID: 32826 RVA: 0x002B3CD0 File Offset: 0x002B1ED0
		public void Open(NKMUnitSkillTemplet unitSkillTemplet, Vector2? touchPos, int unitStarGradeMax, int unitLimitBreakLevel = 0)
		{
			if (unitSkillTemplet == null)
			{
				return;
			}
			List<NKCUITooltip.Data> list = new List<NKCUITooltip.Data>();
			NKCUITooltip.UnitSkillData item = new NKCUITooltip.UnitSkillData(unitSkillTemplet, unitStarGradeMax, unitLimitBreakLevel);
			list.Add(item);
			if (unitSkillTemplet.m_Level == 1)
			{
				NKCUITooltip.TextData item2 = new NKCUITooltip.TextData(unitSkillTemplet.GetSkillDesc());
				list.Add(item2);
			}
			else
			{
				NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(unitSkillTemplet.m_ID, 1);
				if (skillTemplet != null)
				{
					NKCUITooltip.TextData item3 = new NKCUITooltip.TextData(skillTemplet.GetSkillDesc());
					list.Add(item3);
				}
			}
			NKCUITooltip.SkillLevelData item4 = new NKCUITooltip.SkillLevelData(unitSkillTemplet);
			list.Add(item4);
			this.m_vlg.spacing = 20f;
			this.Open(list, touchPos);
		}

		// Token: 0x0600803B RID: 32827 RVA: 0x002B3D64 File Offset: 0x002B1F64
		public void Open(int operatorSkillID, int operatorSkillLevel, Vector2? touchPos)
		{
			NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(operatorSkillID);
			if (skillTemplet == null)
			{
				return;
			}
			List<NKCUITooltip.Data> list = new List<NKCUITooltip.Data>();
			NKCUITooltip.OperatorSkillData item = new NKCUITooltip.OperatorSkillData(skillTemplet, operatorSkillLevel);
			list.Add(item);
			NKCUITooltip.TextData item2 = new NKCUITooltip.TextData(NKCOperatorUtil.MakeOperatorSkillDesc(skillTemplet, operatorSkillLevel));
			list.Add(item2);
			if (skillTemplet.m_OperSkillType == OperatorSkillType.m_Tactical)
			{
				NKMTacticalCommandTemplet tacticalCommandTempletByStrID = NKMTacticalCommandManager.GetTacticalCommandTempletByStrID(skillTemplet.m_OperSkillTarget);
				if (tacticalCommandTempletByStrID.m_listComboType != null && tacticalCommandTempletByStrID.m_listComboType.Count > 0)
				{
					NKCUITooltip.OperatorSkillComboData item3 = new NKCUITooltip.OperatorSkillComboData(tacticalCommandTempletByStrID.m_listComboType);
					list.Add(item3);
				}
			}
			this.m_vlg.spacing = 20f;
			this.Open(list, touchPos);
		}

		// Token: 0x0600803C RID: 32828 RVA: 0x002B3E00 File Offset: 0x002B2000
		public void Open(NKCUITooltip.TextData textData, Vector2? touchPos)
		{
			if (string.IsNullOrEmpty(textData.Text))
			{
				return;
			}
			this.Open(new List<NKCUITooltip.Data>
			{
				textData
			}, touchPos);
		}

		// Token: 0x0600803D RID: 32829 RVA: 0x002B3E30 File Offset: 0x002B2030
		private void Open(List<NKCUITooltip.Data> datas, Vector2? touchPos)
		{
			if (this.m_listAsset.Count > 0)
			{
				this.Clear();
			}
			for (int i = 0; i < datas.Count; i++)
			{
				NKCUITooltip.Data data = datas[i];
				string name = this.GetName(data.Type);
				NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_POPUP_TOOLTIP", name, false, null);
				if (nkcassetInstanceData.m_Instant == null)
				{
					Debug.LogError("툴팁 " + data.Type.ToString());
					return;
				}
				this.m_listAsset.Add(nkcassetInstanceData);
				Transform transform = nkcassetInstanceData.m_Instant.transform;
				transform.SetParent(this.m_rtParent.transform);
				transform.localScale = Vector3.one;
				Vector3 localPosition = transform.localPosition;
				localPosition.z = 0f;
				transform.localPosition = localPosition;
				NKCUITooltipBase component = transform.GetComponent<NKCUITooltipBase>();
				component.Init();
				component.SetData(data);
			}
			this.bFirstTouch = true;
			base.UIOpened(true);
			if (touchPos != null)
			{
				this.m_touchPos = touchPos.Value;
			}
			this.SetPosition(touchPos);
		}

		// Token: 0x0600803E RID: 32830 RVA: 0x002B3F4C File Offset: 0x002B214C
		private void Update()
		{
			if (!Input.anyKey)
			{
				base.Close();
			}
			if (Input.GetMouseButton(0))
			{
				if (this.bFirstTouch)
				{
					this.firstTouch = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
					this.bFirstTouch = false;
				}
				else
				{
					Vector2 b = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
					if ((this.firstTouch - b).sqrMagnitude > 10000f)
					{
						base.Close();
					}
				}
			}
			if (base.IsOpen)
			{
				this.m_rtPanel.SetHeight(this.m_rtParent.GetHeight());
				this.m_rtDeco.transform.localPosition = this.m_rtParent.transform.localPosition;
				this.m_rtDeco.sizeDelta = this.m_rtParent.sizeDelta;
				this.SetPosition(new Vector2?(this.m_touchPos));
			}
		}

		// Token: 0x0600803F RID: 32831 RVA: 0x002B4044 File Offset: 0x002B2244
		private void Clear()
		{
			for (int i = 0; i < this.m_listAsset.Count; i++)
			{
				NKCAssetResourceManager.CloseInstance(this.m_listAsset[i]);
			}
			this.m_listAsset.Clear();
		}

		// Token: 0x06008040 RID: 32832 RVA: 0x002B4083 File Offset: 0x002B2283
		private void OnDestroy()
		{
			if (this.m_RectToCalcTouchPos != null)
			{
				UnityEngine.Object.Destroy(this.m_RectToCalcTouchPos.gameObject);
				this.m_RectToCalcTouchPos = null;
			}
			NKCUITooltip.m_Instance = null;
		}

		// Token: 0x06008041 RID: 32833 RVA: 0x002B40B0 File Offset: 0x002B22B0
		private void SetPosition(Vector2? touchPos)
		{
			NKCUITooltip.PivotType pivotType = this.VectorToPivotType(touchPos);
			Vector3 zero = Vector3.zero;
			if (touchPos != null)
			{
				this.m_touchPos = touchPos.Value;
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_RectToCalcTouchPos, touchPos.Value, NKCCamera.GetSubUICamera(), out vector);
				zero.x = vector.x;
				zero.y = vector.y;
				zero.z = 0f;
			}
			Vector3 vector2;
			switch (pivotType)
			{
			default:
				this.m_rtPanel.pivot = new Vector2(0.5f, 0.5f);
				vector2 = Vector3.zero;
				break;
			case NKCUITooltip.PivotType.RightUp:
				this.m_rtPanel.pivot = new Vector2(1f, 1f);
				vector2 = zero + new Vector3(-1f, -1f, 0f) * 50f;
				break;
			case NKCUITooltip.PivotType.RightDown:
				this.m_rtPanel.pivot = new Vector2(1f, 0f);
				vector2 = zero + new Vector3(-1f, 1f, 0f) * 50f;
				break;
			case NKCUITooltip.PivotType.LeftUp:
				this.m_rtPanel.pivot = new Vector2(0f, 1f);
				vector2 = zero + new Vector3(1f, -1f, 0f) * 50f;
				break;
			case NKCUITooltip.PivotType.LeftDown:
				this.m_rtPanel.pivot = new Vector2(0f, 0f);
				vector2 = zero + new Vector3(1f, 1f, 0f) * 50f;
				break;
			}
			NKCUtil.SetGameobjectActive(this.m_deco_rightUp, pivotType == NKCUITooltip.PivotType.RightUp);
			NKCUtil.SetGameobjectActive(this.m_deco_rightDown, pivotType == NKCUITooltip.PivotType.RightDown);
			NKCUtil.SetGameobjectActive(this.m_deco_leftUp, pivotType == NKCUITooltip.PivotType.LeftUp);
			NKCUtil.SetGameobjectActive(this.m_deco_leftDown, pivotType == NKCUITooltip.PivotType.LeftDown);
			float num = this.m_rtPanel.GetHeight() + 50f;
			float height = this.m_RectToCalcTouchPos.GetHeight();
			float num2;
			float num3;
			switch (pivotType)
			{
			case NKCUITooltip.PivotType.RightUp:
			case NKCUITooltip.PivotType.LeftUp:
				num2 = height * 0.5f + zero.y;
				num3 = 1f;
				break;
			default:
				num2 = height * 0.5f - zero.y;
				num3 = -1f;
				break;
			}
			if (num > num2)
			{
				vector2 += new Vector3(0f, (num - num2) * num3, 0f);
			}
			this.m_rtPanel.localPosition = vector2;
		}

		// Token: 0x06008042 RID: 32834 RVA: 0x002B4344 File Offset: 0x002B2544
		private NKCUITooltip.PivotType VectorToPivotType(Vector2? touchPos)
		{
			if (touchPos == null)
			{
				return NKCUITooltip.PivotType.None;
			}
			Vector2 value = touchPos.Value;
			float num = (float)Screen.width * 0.5f;
			float num2 = (float)Screen.height * 0.5f;
			if (value.x > num)
			{
				if (value.y > num2)
				{
					return NKCUITooltip.PivotType.RightUp;
				}
				return NKCUITooltip.PivotType.RightDown;
			}
			else
			{
				if (value.y > num2)
				{
					return NKCUITooltip.PivotType.LeftUp;
				}
				return NKCUITooltip.PivotType.LeftDown;
			}
		}

		// Token: 0x06008043 RID: 32835 RVA: 0x002B43A0 File Offset: 0x002B25A0
		private string GetName(NKCUITooltip.DataType type)
		{
			switch (type)
			{
			case NKCUITooltip.DataType.Item:
				return "NKM_UI_POPUP_TOOLTIP_ITEM";
			case NKCUITooltip.DataType.ShipSkill:
				return "NKM_UI_POPUP_TOOLTIP_SKILL";
			case NKCUITooltip.DataType.UnitSkill:
				return "NKM_UI_POPUP_TOOLTIP_SKILL_UNIT";
			case NKCUITooltip.DataType.SkillLevel:
				return "NKM_UI_POPUP_TOOLTIP_SKILL_LEVEL";
			case NKCUITooltip.DataType.Text:
				return "NKM_UI_POPUP_TOOLTIP_TEXT";
			case NKCUITooltip.DataType.Ship:
				return "NKM_UI_POPUP_TOOLTIP_SHIP";
			case NKCUITooltip.DataType.Unit:
				return "NKM_UI_POPUP_TOOLTIP_UNIT";
			case NKCUITooltip.DataType.DiveArtifact:
				return "NKM_UI_POPUP_TOOLTIP_ARTIFACT";
			case NKCUITooltip.DataType.TacticalCommand:
				return "NKM_UI_POPUP_TOOLTIP_TACTICAL_COMMAND";
			case NKCUITooltip.DataType.OperatorSkill:
				return "NKM_UI_POPUP_TOOLTIP_SKILL_OPERATOR";
			case NKCUITooltip.DataType.OperatorSkillCombo:
				return "NKM_UI_POPUP_TOOLTIP_SKILL_OPERATOR_COMBO";
			case NKCUITooltip.DataType.Etc:
				return "NKM_UI_POPUP_TOOLTIP_ETC";
			default:
				return "";
			}
		}

		// Token: 0x04006C72 RID: 27762
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_TOOLTIP";

		// Token: 0x04006C73 RID: 27763
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_TOOLTIP";

		// Token: 0x04006C74 RID: 27764
		private static NKCUITooltip m_Instance;

		// Token: 0x04006C75 RID: 27765
		public RectTransform m_rtPanel;

		// Token: 0x04006C76 RID: 27766
		public RectTransform m_rtParent;

		// Token: 0x04006C77 RID: 27767
		public RectTransform m_rtDeco;

		// Token: 0x04006C78 RID: 27768
		public VerticalLayoutGroup m_vlg;

		// Token: 0x04006C79 RID: 27769
		public GameObject m_deco_rightUp;

		// Token: 0x04006C7A RID: 27770
		public GameObject m_deco_rightDown;

		// Token: 0x04006C7B RID: 27771
		public GameObject m_deco_leftUp;

		// Token: 0x04006C7C RID: 27772
		public GameObject m_deco_leftDown;

		// Token: 0x04006C7D RID: 27773
		private List<NKCAssetInstanceData> m_listAsset = new List<NKCAssetInstanceData>();

		// Token: 0x04006C7E RID: 27774
		private RectTransform m_RectToCalcTouchPos;

		// Token: 0x04006C7F RID: 27775
		private Vector2 firstTouch = Vector2.zero;

		// Token: 0x04006C80 RID: 27776
		private bool bFirstTouch = true;

		// Token: 0x04006C81 RID: 27777
		private const int VERTICAL_LAYOUT_GROUP_SPACING = 160;

		// Token: 0x04006C82 RID: 27778
		private const int VERTICAL_LAYOUT_GROUP_SPACING_SKILL = 20;

		// Token: 0x04006C83 RID: 27779
		private const float TOUCH_DISTANCE = 50f;

		// Token: 0x04006C84 RID: 27780
		private Vector3 m_touchPos;

		// Token: 0x0200189C RID: 6300
		public enum DataType
		{
			// Token: 0x0400A950 RID: 43344
			None,
			// Token: 0x0400A951 RID: 43345
			Item,
			// Token: 0x0400A952 RID: 43346
			ShipSkill,
			// Token: 0x0400A953 RID: 43347
			UnitSkill,
			// Token: 0x0400A954 RID: 43348
			SkillLevel,
			// Token: 0x0400A955 RID: 43349
			Text,
			// Token: 0x0400A956 RID: 43350
			Ship,
			// Token: 0x0400A957 RID: 43351
			Unit,
			// Token: 0x0400A958 RID: 43352
			DiveArtifact,
			// Token: 0x0400A959 RID: 43353
			TacticalCommand,
			// Token: 0x0400A95A RID: 43354
			OperatorSkill,
			// Token: 0x0400A95B RID: 43355
			OperatorSkillCombo,
			// Token: 0x0400A95C RID: 43356
			Etc
		}

		// Token: 0x0200189D RID: 6301
		public class Data
		{
			// Token: 0x0400A95D RID: 43357
			public NKCUITooltip.DataType Type;
		}

		// Token: 0x0200189E RID: 6302
		public class ItemData : NKCUITooltip.Data
		{
			// Token: 0x0600B65A RID: 46682 RVA: 0x00366607 File Offset: 0x00364807
			public ItemData(NKCUISlot.SlotData slot)
			{
				this.Type = NKCUITooltip.DataType.Item;
				this.Slot = slot;
			}

			// Token: 0x0400A95E RID: 43358
			public NKCUISlot.SlotData Slot;
		}

		// Token: 0x0200189F RID: 6303
		public class ShipSkillData : NKCUITooltip.Data
		{
			// Token: 0x0600B65B RID: 46683 RVA: 0x0036661D File Offset: 0x0036481D
			public ShipSkillData(NKMShipSkillTemplet shipSkillTemplet)
			{
				this.Type = NKCUITooltip.DataType.ShipSkill;
				this.ShipSkillTemplet = shipSkillTemplet;
			}

			// Token: 0x0400A95F RID: 43359
			public NKMShipSkillTemplet ShipSkillTemplet;
		}

		// Token: 0x020018A0 RID: 6304
		public class TacticalCommandData : NKCUITooltip.Data
		{
			// Token: 0x0600B65C RID: 46684 RVA: 0x00366633 File Offset: 0x00364833
			public TacticalCommandData(NKMTacticalCommandTemplet tacticalCommandTemplet)
			{
				this.Type = NKCUITooltip.DataType.TacticalCommand;
				this.TacticalCommandTemplet = tacticalCommandTemplet;
			}

			// Token: 0x0400A960 RID: 43360
			public NKMTacticalCommandTemplet TacticalCommandTemplet;
		}

		// Token: 0x020018A1 RID: 6305
		public class UnitSkillData : NKCUITooltip.Data
		{
			// Token: 0x0600B65D RID: 46685 RVA: 0x0036664A File Offset: 0x0036484A
			public UnitSkillData(NKMUnitSkillTemplet unitSkillTemplet, int unitStarMax, int unitLimitBreakLv)
			{
				this.Type = NKCUITooltip.DataType.UnitSkill;
				this.UnitSkillTemplet = unitSkillTemplet;
				this.UnitStarGradeMax = unitStarMax;
				this.UnitLimitBreakLevel = unitLimitBreakLv;
			}

			// Token: 0x0400A961 RID: 43361
			public NKMUnitSkillTemplet UnitSkillTemplet;

			// Token: 0x0400A962 RID: 43362
			public int UnitStarGradeMax;

			// Token: 0x0400A963 RID: 43363
			public int UnitLimitBreakLevel;
		}

		// Token: 0x020018A2 RID: 6306
		public class SkillLevelData : NKCUITooltip.Data
		{
			// Token: 0x0600B65E RID: 46686 RVA: 0x0036666E File Offset: 0x0036486E
			public SkillLevelData(NKMUnitSkillTemplet skillTemplet)
			{
				this.Type = NKCUITooltip.DataType.SkillLevel;
				this.SkillTemplet = skillTemplet;
			}

			// Token: 0x0400A964 RID: 43364
			public NKMUnitSkillTemplet SkillTemplet;
		}

		// Token: 0x020018A3 RID: 6307
		public class TextData : NKCUITooltip.Data
		{
			// Token: 0x0600B65F RID: 46687 RVA: 0x00366684 File Offset: 0x00364884
			public TextData(string text)
			{
				this.Type = NKCUITooltip.DataType.Text;
				this.Text = text;
			}

			// Token: 0x0400A965 RID: 43365
			public string Text;
		}

		// Token: 0x020018A4 RID: 6308
		public class UnitData : NKCUITooltip.Data
		{
			// Token: 0x0600B660 RID: 46688 RVA: 0x0036669A File Offset: 0x0036489A
			public UnitData(NKCUISlot.SlotData slot, NKMUnitTempletBase unitTempletBase)
			{
				if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
				{
					this.Type = NKCUITooltip.DataType.Unit;
				}
				else
				{
					this.Type = NKCUITooltip.DataType.Ship;
				}
				this.Slot = slot;
				this.UnitTempletBase = unitTempletBase;
			}

			// Token: 0x0400A966 RID: 43366
			public NKCUISlot.SlotData Slot;

			// Token: 0x0400A967 RID: 43367
			public NKMUnitTempletBase UnitTempletBase;
		}

		// Token: 0x020018A5 RID: 6309
		public class DiveArtifactData : NKCUITooltip.Data
		{
			// Token: 0x0600B661 RID: 46689 RVA: 0x003666C9 File Offset: 0x003648C9
			public DiveArtifactData(NKCUISlot.SlotData slot)
			{
				this.Type = NKCUITooltip.DataType.DiveArtifact;
				this.Slot = slot;
			}

			// Token: 0x0400A968 RID: 43368
			public NKCUISlot.SlotData Slot;
		}

		// Token: 0x020018A6 RID: 6310
		public class EtcData : NKCUITooltip.Data
		{
			// Token: 0x0600B662 RID: 46690 RVA: 0x003666DF File Offset: 0x003648DF
			public EtcData(string title, string desc)
			{
				this.Type = NKCUITooltip.DataType.Etc;
				this.m_Title = title;
				this.m_Desc = desc;
			}

			// Token: 0x0400A969 RID: 43369
			public NKCUISlot.SlotData Slot;

			// Token: 0x0400A96A RID: 43370
			public string m_Title;

			// Token: 0x0400A96B RID: 43371
			public string m_Desc;
		}

		// Token: 0x020018A7 RID: 6311
		public class OperatorSkillData : NKCUITooltip.Data
		{
			// Token: 0x0600B663 RID: 46691 RVA: 0x003666FD File Offset: 0x003648FD
			public OperatorSkillData(NKMOperatorSkillTemplet templet, int skillLv)
			{
				this.Type = NKCUITooltip.DataType.OperatorSkill;
				this.skillTemplet = templet;
				this.skillLevel = skillLv;
			}

			// Token: 0x0400A96C RID: 43372
			public NKMOperatorSkillTemplet skillTemplet;

			// Token: 0x0400A96D RID: 43373
			public int skillLevel;
		}

		// Token: 0x020018A8 RID: 6312
		public class OperatorSkillComboData : NKCUITooltip.Data
		{
			// Token: 0x0600B664 RID: 46692 RVA: 0x0036671B File Offset: 0x0036491B
			public OperatorSkillComboData(List<NKMTacticalCombo> lstCombo)
			{
				this.Type = NKCUITooltip.DataType.OperatorSkillCombo;
				this.skillCombo = lstCombo;
			}

			// Token: 0x0400A96E RID: 43374
			public List<NKMTacticalCombo> skillCombo = new List<NKMTacticalCombo>();
		}

		// Token: 0x020018A9 RID: 6313
		private enum PivotType
		{
			// Token: 0x0400A970 RID: 43376
			None,
			// Token: 0x0400A971 RID: 43377
			RightUp,
			// Token: 0x0400A972 RID: 43378
			RightDown,
			// Token: 0x0400A973 RID: 43379
			LeftUp,
			// Token: 0x0400A974 RID: 43380
			LeftDown
		}
	}
}
