using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007C2 RID: 1986
	public class NKCUIGameHudDevUnit : MonoBehaviour
	{
		// Token: 0x06004EA2 RID: 20130 RVA: 0x0017B680 File Offset: 0x00179880
		public static NKCUIGameHudDevUnit GetNewInstance(Transform parent, NKCUIGameHudDevUnit._OnValueChanged onValueChanged)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HUD_RENEWAL", "NKM_GAME_DEV_MENU_WINDOW_UNIT", false, null);
			if (nkcassetInstanceData.m_Instant == null)
			{
				Debug.LogError("NKCUIGameHudDevUnit Prefab null!");
				return null;
			}
			NKCUIGameHudDevUnit component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIGameHudDevUnit>();
			if (component == null)
			{
				Debug.LogError("NKCUIGameHudDevUnit Prefab null!");
				return null;
			}
			component.m_OnValueChanged = onValueChanged;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.m_NKCUIComToggle.SetToggleGroup(parent.gameObject.GetComponent<NKCUIComToggleGroup>());
			component.SetChangeEventToToggleBtn();
			component.m_LayoutElement = component.gameObject.GetComponent<LayoutElement>();
			return component;
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x0017B724 File Offset: 0x00179924
		public void SetLayoutElementActive(bool bSet)
		{
			this.m_LayoutElement.enabled = bSet;
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x0017B732 File Offset: 0x00179932
		public void SetOnValueChanged(NKCUIGameHudDevUnit._OnValueChanged onValueChanged)
		{
			this.m_OnValueChanged = onValueChanged;
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x0017B73B File Offset: 0x0017993B
		public void OnValueChanged(bool bSet)
		{
			if (this.m_OnValueChanged != null)
			{
				if (this.m_NKMUnitTempletBase == null)
				{
					this.m_OnValueChanged(0, bSet);
					return;
				}
				this.m_OnValueChanged(this.m_NKMUnitTempletBase.m_UnitID, bSet);
			}
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x0017B772 File Offset: 0x00179972
		public void SetChangeEventToToggleBtn()
		{
			this.m_NKCUIComToggle.OnValueChanged.RemoveAllListeners();
			this.m_NKCUIComToggle.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x0017B7A0 File Offset: 0x001799A0
		public void SetRevToggleCallbackSeq()
		{
			this.m_NKCUIComToggle.SetbReverseSeqCallbackCall(true);
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x0017B7AE File Offset: 0x001799AE
		public void Preload(NKMUnitTempletBase cNKMUnitTempletBase)
		{
			NKCResourceUtility.PreloadUnitResource(NKCResourceUtility.eUnitResourceType.INVEN_ICON, cNKMUnitTempletBase, true);
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x0017B7B8 File Offset: 0x001799B8
		private void SetEnemy(bool bSet)
		{
			if (this.m_goEnemy.activeSelf == !bSet)
			{
				this.m_goEnemy.SetActive(bSet);
			}
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x0017B7D7 File Offset: 0x001799D7
		public void SetLayoutElement(bool bSet)
		{
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x0017B7DC File Offset: 0x001799DC
		public void SetData(NKMUnitData cNKMUnitData, bool bEnemy = false)
		{
			this.m_NKMUnitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMUnitData);
			if (cNKMUnitData != null && this.m_NKMUnitTempletBase != null)
			{
				this.SetEnemy(bEnemy);
				Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, cNKMUnitData);
				if (sprite != null)
				{
					this.m_imgUnitPanel.sprite = sprite;
					this.m_imgUnitAddPanel.sprite = sprite;
					this.m_imgUnitGrayPanel.sprite = sprite;
				}
				else
				{
					Debug.LogError(string.Format("INVEN_ICON Load Failed! unitID : {0}", cNKMUnitData.m_UnitID));
				}
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(cNKMUnitData.m_UnitID);
				if (unitStatTemplet != null)
				{
					this.m_textCardCost.text = unitStatTemplet.GetRespawnCost(false, null, null).ToString();
				}
				else
				{
					Debug.LogError(string.Format("NKMUnitStatTemplet Load Failed! unitID : {0}", cNKMUnitData.m_UnitID));
				}
				this.m_objUnitMain.SetActive(true);
				this.m_imgUnitPanel.gameObject.SetActive(true);
				this.m_imgUnitGrayPanel.gameObject.SetActive(false);
				this.m_textCardCost.gameObject.SetActive(true);
				this.m_imgBgAddPanel.gameObject.SetActive(false);
				this.m_imgUnitAddPanel.gameObject.SetActive(false);
				return;
			}
			this.m_imgBGPanel.sprite = this.m_spritePanelEmptySlot;
			this.m_imgBgAddPanel.sprite = this.m_spritePanelEmptySlot;
			this.m_objUnitMain.SetActive(false);
			this.SetEnemy(false);
		}

		// Token: 0x04003E4D RID: 15949
		[NonSerialized]
		public NKMUnitTempletBase m_NKMUnitTempletBase;

		// Token: 0x04003E4E RID: 15950
		public GameObject m_objMain;

		// Token: 0x04003E4F RID: 15951
		public NKCUIComToggle m_NKCUIComToggle;

		// Token: 0x04003E50 RID: 15952
		public Image m_imgBGPanel;

		// Token: 0x04003E51 RID: 15953
		public Image m_imgBgAddPanel;

		// Token: 0x04003E52 RID: 15954
		public GameObject m_objUnitMain;

		// Token: 0x04003E53 RID: 15955
		public Image m_imgUnitPanel;

		// Token: 0x04003E54 RID: 15956
		public Image m_imgUnitAddPanel;

		// Token: 0x04003E55 RID: 15957
		public Image m_imgUnitGrayPanel;

		// Token: 0x04003E56 RID: 15958
		public Text m_textCardCost;

		// Token: 0x04003E57 RID: 15959
		public GameObject m_goEnemy;

		// Token: 0x04003E58 RID: 15960
		[Header("Back Panel Image")]
		public Sprite m_spritePanelCounter;

		// Token: 0x04003E59 RID: 15961
		public Sprite m_spritePanelSoldier;

		// Token: 0x04003E5A RID: 15962
		public Sprite m_spritePanelMechanic;

		// Token: 0x04003E5B RID: 15963
		public Sprite m_spritePanelEmptySlot;

		// Token: 0x04003E5C RID: 15964
		private NKCUIGameHudDevUnit._OnValueChanged m_OnValueChanged;

		// Token: 0x04003E5D RID: 15965
		private LayoutElement m_LayoutElement;

		// Token: 0x0200147F RID: 5247
		// (Invoke) Token: 0x0600A8FF RID: 43263
		public delegate void _OnValueChanged(int unitID, bool bSet);
	}
}
