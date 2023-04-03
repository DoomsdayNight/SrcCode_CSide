using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000961 RID: 2401
	public class NKCDeckViewEnemySlot : MonoBehaviour
	{
		// Token: 0x06005FCD RID: 24525 RVA: 0x001DD1B0 File Offset: 0x001DB3B0
		public static NKCDeckViewEnemySlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_UNIT_SLOT_DECK", "NKM_UI_DECK_VIEW_UNIT_SLOT_ENEMY", false, null);
			NKCDeckViewEnemySlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCDeckViewEnemySlot>();
			if (component == null)
			{
				Debug.LogError("NKCDeckViewUnitSlot Prefab null!");
				return null;
			}
			component.m_instace = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			return component;
		}

		// Token: 0x06005FCE RID: 24526 RVA: 0x001DD242 File Offset: 0x001DB442
		public void Init(int index)
		{
			this.m_Index = index;
		}

		// Token: 0x06005FCF RID: 24527 RVA: 0x001DD24C File Offset: 0x001DB44C
		public void SetEnemyData(NKMUnitTempletBase cNKMUnitTempletBase, NKCEnemyData cNKMEnemyData)
		{
			if (cNKMEnemyData == null)
			{
				return;
			}
			Sprite backPanelImage = this.GetBackPanelImage(NKM_UNIT_GRADE.NUG_N);
			NKCUtil.SetImageSprite(this.m_imgBGPanel, backPanelImage, false);
			NKCUtil.SetImageSprite(this.m_imgBgAddPanel, backPanelImage, false);
			if (backPanelImage == null)
			{
				Debug.LogError("SetEnemyData m_spPanelN: null");
			}
			if (this.m_imgBGPanel.sprite == null)
			{
				Debug.LogError("SetEnemyData m_imgBGPanel.sprite: null");
			}
			if (cNKMUnitTempletBase != null)
			{
				Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, cNKMUnitTempletBase);
				this.m_imgUnitPanel.sprite = sprite;
				this.m_textLevel.SetText(cNKMEnemyData.m_Level.ToString(), false, Array.Empty<Text>());
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS, cNKMEnemyData.m_NKM_BOSS_TYPE >= NKM_BOSS_TYPE.NBT_DUNGEON_BOSS);
				if (cNKMEnemyData.m_NKM_BOSS_TYPE == NKM_BOSS_TYPE.NBT_DUNGEON_BOSS)
				{
					this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS_img.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_POPUP_ENEMY_ICON", false);
				}
				else if (cNKMEnemyData.m_NKM_BOSS_TYPE == NKM_BOSS_TYPE.NBT_WARFARE_BOSS)
				{
					this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS_img.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_POPUP_ENEMY_BOSS_ICON", false);
				}
				Sprite orLoadUnitRoleIcon = NKCResourceUtility.GetOrLoadUnitRoleIcon(cNKMUnitTempletBase, true);
				NKCUtil.SetImageSprite(this.m_imgClassType, orLoadUnitRoleIcon, true);
				Sprite orLoadUnitAttackTypeIcon = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(cNKMUnitTempletBase, true);
				NKCUtil.SetImageSprite(this.m_imgAttackType, orLoadUnitAttackTypeIcon, true);
				NKCUtil.SetGameobjectActive(this.m_objUnitMain, true);
				NKCUtil.SetGameobjectActive(this.m_imgUnitPanel, true);
				NKCUtil.SetGameobjectActive(this.m_textLevel, true);
				NKCUtil.SetGameobjectActive(this.m_imgBgAddPanel, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUnitMain, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS, false);
		}

		// Token: 0x06005FD0 RID: 24528 RVA: 0x001DD3BC File Offset: 0x001DB5BC
		private Sprite GetBackPanelImage(NKM_UNIT_GRADE unitGrade)
		{
			switch (unitGrade)
			{
			case NKM_UNIT_GRADE.NUG_N:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_unit_slot_deck_sprite", "FACE_DECK_BG_N", false);
			case NKM_UNIT_GRADE.NUG_R:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_unit_slot_deck_sprite", "FACE_DECK_BG_R", false);
			case NKM_UNIT_GRADE.NUG_SR:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_unit_slot_deck_sprite", "FACE_DECK_BG_SR", false);
			case NKM_UNIT_GRADE.NUG_SSR:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_unit_slot_deck_sprite", "FACE_DECK_BG_SSR", false);
			default:
				return null;
			}
		}

		// Token: 0x06005FD1 RID: 24529 RVA: 0x001DD426 File Offset: 0x001DB626
		public void ButtonSelect()
		{
			this.m_NKCUIComButton.Select(true, false, false);
		}

		// Token: 0x06005FD2 RID: 24530 RVA: 0x001DD436 File Offset: 0x001DB636
		public void ButtonDeSelect(bool bForce = false, bool bImmediate = false)
		{
			this.m_NKCUIComButton.Select(false, bForce, bImmediate);
		}

		// Token: 0x06005FD3 RID: 24531 RVA: 0x001DD446 File Offset: 0x001DB646
		private void OnDestroy()
		{
			this.CloseInstance();
		}

		// Token: 0x06005FD4 RID: 24532 RVA: 0x001DD44E File Offset: 0x001DB64E
		public void CloseInstance()
		{
			if (this.m_instace != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_instace);
				this.m_instace = null;
			}
		}

		// Token: 0x04004C0F RID: 19471
		[NonSerialized]
		public int m_Index;

		// Token: 0x04004C10 RID: 19472
		public NKCUIComButton m_NKCUIComButton;

		// Token: 0x04004C11 RID: 19473
		public Image m_imgBGPanel;

		// Token: 0x04004C12 RID: 19474
		public Image m_imgBgAddPanel;

		// Token: 0x04004C13 RID: 19475
		public GameObject m_objUnitMain;

		// Token: 0x04004C14 RID: 19476
		public Image m_imgUnitPanel;

		// Token: 0x04004C15 RID: 19477
		public NKCUIComTextUnitLevel m_textLevel;

		// Token: 0x04004C16 RID: 19478
		public Image m_imgClassType;

		// Token: 0x04004C17 RID: 19479
		public Image m_imgAttackType;

		// Token: 0x04004C18 RID: 19480
		[Header("Enemy")]
		public GameObject m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS;

		// Token: 0x04004C19 RID: 19481
		public Image m_NKM_UI_DECK_VIEW_UNIT_SLOT_CARD_BOSS_img;

		// Token: 0x04004C1A RID: 19482
		private NKCAssetInstanceData m_instace;

		// Token: 0x04004C1B RID: 19483
		private const string DECK_SPRITE_BUNDLE_NAME = "ab_ui_unit_slot_deck_sprite";
	}
}
