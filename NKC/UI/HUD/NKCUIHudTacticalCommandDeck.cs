using System;
using System.Text;
using NKC.UI.Tooltip;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.HUD
{
	// Token: 0x02000C40 RID: 3136
	public class NKCUIHudTacticalCommandDeck
	{
		// Token: 0x0600925C RID: 37468 RVA: 0x0031F3CC File Offset: 0x0031D5CC
		public void InitUI(NKCGameHud cNKCGameHud, GameObject deckMainObject, int index)
		{
			this.m_NUF_GAME_HUD_TACTICAL_COMMAND_MAIN = deckMainObject;
			this.m_NKCGameHud = cNKCGameHud;
			this.m_Index = index;
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			this.m_StringBuilder.AppendFormat("NKM_UI_TACTICAL_COMMAND_DECK{0}", index);
			this.m_NKM_UI_TACTICAL_COMMAND_DECK = this.m_NUF_GAME_HUD_TACTICAL_COMMAND_MAIN.transform.Find(this.m_StringBuilder.ToString()).gameObject;
			this.m_NKM_UI_TACTICAL_COMMAND_DECK_NKCUIComStateButton = this.m_NKM_UI_TACTICAL_COMMAND_DECK.GetComponentInChildren<NKCUIComStateButton>();
			this.m_NKM_UI_TACTICAL_COMMAND_DECK_NKCUIComStateButton.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_TACTICAL_COMMAND_DECK_NKCUIComStateButton.PointerClick.AddListener(new UnityAction(this.Click));
			this.m_NKM_UI_TACTICAL_COMMAND_DECK_NKCUIComStateButton.PointerDown.RemoveAllListeners();
			this.m_NKM_UI_TACTICAL_COMMAND_DECK_NKCUIComStateButton.PointerDown.AddListener(new UnityAction<PointerEventData>(this.PointerDown));
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD = this.m_NKM_UI_TACTICAL_COMMAND_DECK.transform.Find("NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD").gameObject;
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_Animator = this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD.GetComponentInChildren<Animator>();
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel = this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD.transform.Find("NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel").gameObject;
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel_Image = this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel.GetComponentInChildren<Image>();
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel = this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD.transform.Find("NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel").gameObject;
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel_Image = this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel.GetComponentInChildren<Image>();
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_ADD_Panel = this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD.transform.Find("NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_ADD_Panel").gameObject;
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_ADD_Panel_Image = this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_ADD_Panel.GetComponentInChildren<Image>();
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_UNIT_BORDER = this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD.transform.Find("NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_BORDER").gameObject;
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME = this.m_NKM_UI_TACTICAL_COMMAND_DECK.transform.Find("NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME").gameObject;
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_RectTransform = this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME.GetComponentInChildren<RectTransform>();
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_BG_Panel = this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME.transform.Find("NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_BG_Panel").gameObject;
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel = this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME.transform.Find("NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel").gameObject;
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel_Image = this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel.GetComponentInChildren<Image>();
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_COST_Text = this.m_NKM_UI_TACTICAL_COMMAND_DECK.transform.Find("NKM_UI_GAME_TACTICAL_COMMAND_COST_BG_Panel/NKM_UI_GAME_TACTICAL_COMMAND_COST_Text").gameObject;
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_COST_Text_Text = this.m_NKM_UI_GAME_TACTICAL_COMMAND_COST_Text.GetComponentInChildren<Text>();
		}

		// Token: 0x0600925D RID: 37469 RVA: 0x0031F643 File Offset: 0x0031D843
		public void PointerDown(PointerEventData e)
		{
			if (this.m_NKMTacticalCommandTemplet == null || this.m_NKMTacticalCommandData == null)
			{
				return;
			}
			NKCUITooltip.Instance.Open(this.m_NKMTacticalCommandTemplet, (int)this.m_NKMTacticalCommandData.m_Level, new Vector2?(e.position));
		}

		// Token: 0x0600925E RID: 37470 RVA: 0x0031F67C File Offset: 0x0031D87C
		public void Click()
		{
			if (this.m_NKMTacticalCommandTemplet == null || this.m_NKMTacticalCommandData == null)
			{
				return;
			}
			if (!this.CanUse())
			{
				return;
			}
			this.m_NKCGameHud.GetGameClient().Send_Packet_GAME_TACTICAL_COMMAND_REQ(this.m_NKMTacticalCommandTemplet.m_TCID);
			this.m_NKCGameHud.GetGameClient().AddCostHolderTC(this.m_NKMTacticalCommandTemplet.m_TCID, this.m_NKMTacticalCommandTemplet.GetNeedCost(this.m_NKMTacticalCommandData));
			this.m_NKCGameHud.SetRespawnCost();
			this.SetActive(false, false);
		}

		// Token: 0x0600925F RID: 37471 RVA: 0x0031F700 File Offset: 0x0031D900
		public void Init()
		{
			this.m_NKMTacticalCommandTemplet = null;
			NKCAssetResourceData assetResource = NKCResourceUtility.GetAssetResource("AB_UI_TACTICAL_COMMAND_ICON", "ICON_TC_NO_SKILL_ICON");
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel.SetActive(true);
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel.SetActive(true);
			if (assetResource != null)
			{
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel_Image.sprite = assetResource.GetAsset<Sprite>();
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel_Image.sprite = assetResource.GetAsset<Sprite>();
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_ADD_Panel_Image.sprite = assetResource.GetAsset<Sprite>();
			}
			else
			{
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel_Image.sprite = null;
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel_Image.sprite = null;
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_ADD_Panel_Image.sprite = null;
			}
			if (this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_BG_Panel.activeSelf)
			{
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_BG_Panel.SetActive(false);
			}
			if (this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel.activeSelf)
			{
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel.SetActive(false);
			}
			this.m_bEventControl = false;
			this.m_NeedCostBefore = -1;
			this.SetActive(false, false);
		}

		// Token: 0x06009260 RID: 37472 RVA: 0x0031F7E4 File Offset: 0x0031D9E4
		public void SetDeckSprite(NKMTacticalCommandTemplet cNKMTacticalCommandTemplet)
		{
			if (cNKMTacticalCommandTemplet == null)
			{
				this.Init();
				return;
			}
			this.m_NKMTacticalCommandTemplet = cNKMTacticalCommandTemplet;
			this.SetActive(true, false);
			if (this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_ADD_Panel.activeSelf)
			{
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_ADD_Panel.SetActive(false);
			}
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			this.m_StringBuilder.Append(this.m_NKMTacticalCommandTemplet.m_TCIconName);
			NKCAssetResourceData assetResource = NKCResourceUtility.GetAssetResource("AB_UI_TACTICAL_COMMAND_ICON", this.m_StringBuilder.ToString());
			if (assetResource != null)
			{
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel_Image.sprite = assetResource.GetAsset<Sprite>();
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_ADD_Panel_Image.sprite = assetResource.GetAsset<Sprite>();
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel_Image.sprite = assetResource.GetAsset<Sprite>();
			}
			else
			{
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel_Image.sprite = null;
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_ADD_Panel_Image.sprite = null;
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel_Image.sprite = null;
			}
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel.SetActive(true);
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel.SetActive(true);
		}

		// Token: 0x06009261 RID: 37473 RVA: 0x0031F8E0 File Offset: 0x0031DAE0
		public void SetDeckData(float fRespawnCostNow, NKMTacticalCommandData cNKMTacticalCommandData)
		{
			float num = 1f;
			if (this.m_NKMTacticalCommandTemplet != null)
			{
				this.m_NKMTacticalCommandData = cNKMTacticalCommandData;
				float needCost = this.m_NKMTacticalCommandTemplet.GetNeedCost(this.m_NKMTacticalCommandData);
				float num2 = fRespawnCostNow / needCost;
				float num3 = 1f - cNKMTacticalCommandData.m_fCoolTimeNow / this.m_NKMTacticalCommandTemplet.m_fCoolTime;
				if (num2 < num3)
				{
					num = num2;
				}
				else
				{
					num = num3;
				}
				if (this.m_NeedCostBefore != (int)needCost)
				{
					this.m_NeedCostBefore = (int)needCost;
					this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
					this.m_StringBuilder.Append(this.m_NeedCostBefore);
					this.m_NKM_UI_GAME_TACTICAL_COMMAND_COST_Text_Text.text = this.m_StringBuilder.ToString();
				}
			}
			this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel_Image.fillAmount = num;
			if (num < 1f)
			{
				if (this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel.activeSelf)
				{
					this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel.SetActive(false);
				}
				if (!this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel.activeSelf)
				{
					this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel.SetActive(true);
				}
			}
			else
			{
				if (!this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel.activeSelf)
				{
					this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel.SetActive(true);
					if (this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_Animator.gameObject.activeInHierarchy)
					{
						this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_Animator.Play("NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_READY", -1, 0f);
					}
				}
				if (this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel.activeSelf)
				{
					this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel.SetActive(false);
				}
			}
			if (num >= 1f)
			{
				if (this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_BG_Panel.activeSelf)
				{
					this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_BG_Panel.SetActive(false);
				}
				if (this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel.activeSelf)
				{
					this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel.SetActive(false);
				}
				if (!this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_UNIT_BORDER.activeSelf)
				{
					this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_UNIT_BORDER.SetActive(true);
					return;
				}
			}
			else
			{
				if (!this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_BG_Panel.activeSelf)
				{
					this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_BG_Panel.SetActive(true);
				}
				if (!this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel.activeSelf)
				{
					this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel.SetActive(true);
				}
				if (this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_UNIT_BORDER.activeSelf)
				{
					this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_UNIT_BORDER.SetActive(false);
				}
			}
		}

		// Token: 0x06009262 RID: 37474 RVA: 0x0031FAD1 File Offset: 0x0031DCD1
		public bool CanUse()
		{
			return this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel_Image.fillAmount >= 1f;
		}

		// Token: 0x06009263 RID: 37475 RVA: 0x0031FAE8 File Offset: 0x0031DCE8
		public void ReturnTacticalCommandDeck()
		{
			this.SetActive(true, false);
		}

		// Token: 0x06009264 RID: 37476 RVA: 0x0031FAF4 File Offset: 0x0031DCF4
		public void SetActive(bool bActive, bool bEventControl = false)
		{
			if (bActive && this.m_bEventControl && !bEventControl)
			{
				return;
			}
			this.m_bEventControl = bEventControl;
			if (this.m_NKM_UI_TACTICAL_COMMAND_DECK.activeSelf != bActive)
			{
				this.m_NKM_UI_TACTICAL_COMMAND_DECK.SetActive(bActive);
			}
			if (this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME.activeSelf != bActive)
			{
				this.m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME.SetActive(bActive);
			}
		}

		// Token: 0x04007F44 RID: 32580
		public int m_Index;

		// Token: 0x04007F45 RID: 32581
		public NKCGameHud m_NKCGameHud;

		// Token: 0x04007F46 RID: 32582
		public GameObject m_NUF_GAME_HUD_TACTICAL_COMMAND_MAIN;

		// Token: 0x04007F47 RID: 32583
		public NKMTacticalCommandTemplet m_NKMTacticalCommandTemplet;

		// Token: 0x04007F48 RID: 32584
		public NKMTacticalCommandData m_NKMTacticalCommandData;

		// Token: 0x04007F49 RID: 32585
		public GameObject m_NKM_UI_TACTICAL_COMMAND_DECK;

		// Token: 0x04007F4A RID: 32586
		public NKCUIComStateButton m_NKM_UI_TACTICAL_COMMAND_DECK_NKCUIComStateButton;

		// Token: 0x04007F4B RID: 32587
		public GameObject m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD;

		// Token: 0x04007F4C RID: 32588
		public Animator m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_Animator;

		// Token: 0x04007F4D RID: 32589
		public GameObject m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel;

		// Token: 0x04007F4E RID: 32590
		public Image m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_Panel_Image;

		// Token: 0x04007F4F RID: 32591
		public GameObject m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel;

		// Token: 0x04007F50 RID: 32592
		public Image m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_GRAY_Panel_Image;

		// Token: 0x04007F51 RID: 32593
		public GameObject m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_ADD_Panel;

		// Token: 0x04007F52 RID: 32594
		public Image m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_ICON_ADD_Panel_Image;

		// Token: 0x04007F53 RID: 32595
		public GameObject m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_BG_Panel;

		// Token: 0x04007F54 RID: 32596
		public GameObject m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel;

		// Token: 0x04007F55 RID: 32597
		public Image m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_Panel_Image;

		// Token: 0x04007F56 RID: 32598
		public GameObject m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_UNIT_BORDER;

		// Token: 0x04007F57 RID: 32599
		public GameObject m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME;

		// Token: 0x04007F58 RID: 32600
		public RectTransform m_NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_COOL_TIME_RectTransform;

		// Token: 0x04007F59 RID: 32601
		public GameObject m_NKM_UI_GAME_TACTICAL_COMMAND_COST_Text;

		// Token: 0x04007F5A RID: 32602
		public Text m_NKM_UI_GAME_TACTICAL_COMMAND_COST_Text_Text;

		// Token: 0x04007F5B RID: 32603
		private bool m_bEventControl;

		// Token: 0x04007F5C RID: 32604
		private StringBuilder m_StringBuilder = new StringBuilder();

		// Token: 0x04007F5D RID: 32605
		private int m_NeedCostBefore;
	}
}
