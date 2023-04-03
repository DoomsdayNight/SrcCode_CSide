using System;
using NKC.UI;
using NKM;

namespace NKC
{
	// Token: 0x02000720 RID: 1824
	public class NKC_SCEN_INVENTORY : NKC_SCEN_BASIC
	{
		// Token: 0x0600486D RID: 18541 RVA: 0x0015DD68 File Offset: 0x0015BF68
		public NKC_SCEN_INVENTORY()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_INVENTORY;
		}

		// Token: 0x0600486E RID: 18542 RVA: 0x0015DD7E File Offset: 0x0015BF7E
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			this.m_NKCUIInventory = NKCUIInventory.Instance;
			this.m_NKCUIInventory.Load();
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x0015DD9C File Offset: 0x0015BF9C
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x0015DDA4 File Offset: 0x0015BFA4
		public override void ScenStart()
		{
			base.ScenStart();
			this.Open();
			NKCCamera.EnableBloom(false);
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x0015DDB8 File Offset: 0x0015BFB8
		public override void ScenEnd()
		{
			NKCUIForge.CheckInstanceAndClose();
			base.ScenEnd();
			this.Close();
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x0015DDCC File Offset: 0x0015BFCC
		public void Open()
		{
			NKCUIInventory.EquipSelectListOptions options = new NKCUIInventory.EquipSelectListOptions(NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL, false, true);
			options.strEmptyMessage = NKCUtilString.GET_STRING_INVEN_MISC_NO_EXIST;
			this.m_NKCUIInventory.Open(options, null, 0L, this.m_reservedOpenType);
			this.m_reservedOpenType = NKCUIInventory.NKC_INVENTORY_TAB.NIT_NONE;
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x0015DE0B File Offset: 0x0015C00B
		public void Close()
		{
			this.m_NKCUIInventory.Close();
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x0015DE18 File Offset: 0x0015C018
		public void OnRemoveEquipItemAck()
		{
			if (this.m_NKCUIInventory != null && this.m_NKCUIInventory.IsOpen)
			{
				this.m_NKCUIInventory.OnRemoveMode(false);
			}
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x0015DE41 File Offset: 0x0015C041
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x0015DE49 File Offset: 0x0015C049
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x06004877 RID: 18551 RVA: 0x0015DE4C File Offset: 0x0015C04C
		public void SetReservedOpenTyp(NKCUIInventory.NKC_INVENTORY_TAB openType)
		{
			this.m_reservedOpenType = openType;
		}

		// Token: 0x0400383D RID: 14397
		private NKCUIInventory m_NKCUIInventory;

		// Token: 0x0400383E RID: 14398
		private NKCUIInventory.NKC_INVENTORY_TAB m_reservedOpenType = NKCUIInventory.NKC_INVENTORY_TAB.NIT_NONE;
	}
}
