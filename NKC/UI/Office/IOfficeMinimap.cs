using System;
using ClientPacket.Office;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AF4 RID: 2804
	public interface IOfficeMinimap
	{
		// Token: 0x06007E62 RID: 32354
		GameObject GetGameObject();

		// Token: 0x06007E63 RID: 32355
		ScrollRect GetScrollRect();

		// Token: 0x06007E64 RID: 32356
		Transform GetScrollTargetTileTransform(int sectionId);

		// Token: 0x06007E65 RID: 32357
		Transform GetRightEndTileTransform();

		// Token: 0x06007E66 RID: 32358
		RectTransform GetTileRectTransform(int roomId);

		// Token: 0x06007E67 RID: 32359
		float GetScrollRectContentOriginalWidth();

		// Token: 0x06007E68 RID: 32360
		void SetActive(bool value);

		// Token: 0x06007E69 RID: 32361
		void UpdateRoomStateAll();

		// Token: 0x06007E6A RID: 32362
		void UpdateRoomState(NKMOfficeRoomTemplet.RoomType roomType);

		// Token: 0x06007E6B RID: 32363
		void UpdateRoomStateInSection(int sectionId);

		// Token: 0x06007E6C RID: 32364
		void UpdateRoomInfo(NKMOfficeRoom officeRoom);

		// Token: 0x06007E6D RID: 32365
		void UpdatePurchasedRoom(NKMOfficeRoom officeRoom);

		// Token: 0x06007E6E RID: 32366
		void LockRoomsInSection(int sectionId);

		// Token: 0x06007E6F RID: 32367
		void ExpandScrollRectRange();

		// Token: 0x06007E70 RID: 32368
		void RevertScrollRectRange();

		// Token: 0x06007E71 RID: 32369
		void UpdateCameraPosition();

		// Token: 0x06007E72 RID: 32370
		bool IsRedDotOn();
	}
}
