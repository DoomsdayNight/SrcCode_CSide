using System;

namespace NKC.UI
{
	// Token: 0x0200099E RID: 2462
	public class NKCUIGame : NKCUIBase
	{
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06006670 RID: 26224 RVA: 0x0020BC19 File Offset: 0x00209E19
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06006671 RID: 26225 RVA: 0x0020BC1C File Offset: 0x00209E1C
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06006672 RID: 26226 RVA: 0x0020BC1F File Offset: 0x00209E1F
		public override string MenuName
		{
			get
			{
				return "인게임";
			}
		}

		// Token: 0x06006673 RID: 26227 RVA: 0x0020BC26 File Offset: 0x00209E26
		public void Open()
		{
			base.UIOpened(true);
		}

		// Token: 0x06006674 RID: 26228 RVA: 0x0020BC2F File Offset: 0x00209E2F
		public override void CloseInternal()
		{
		}

		// Token: 0x06006675 RID: 26229 RVA: 0x0020BC31 File Offset: 0x00209E31
		public override void UnHide()
		{
		}

		// Token: 0x06006676 RID: 26230 RVA: 0x0020BC33 File Offset: 0x00209E33
		public override void Hide()
		{
		}

		// Token: 0x06006677 RID: 26231 RVA: 0x0020BC35 File Offset: 0x00209E35
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnBackButton();
		}
	}
}
