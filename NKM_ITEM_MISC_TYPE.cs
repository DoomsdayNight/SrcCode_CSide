using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x0200037E RID: 894
	public enum NKM_ITEM_MISC_TYPE : short
	{
		// Token: 0x04000F2D RID: 3885
		[CountryDescription("일반아이템", CountryCode.KOR)]
		IMT_MISC,
		// Token: 0x04000F2E RID: 3886
		[CountryDescription("패키지아이템", CountryCode.KOR)]
		IMT_PACKAGE,
		// Token: 0x04000F2F RID: 3887
		[CountryDescription("랜덤박스아이템", CountryCode.KOR)]
		IMT_RANDOMBOX,
		// Token: 0x04000F30 RID: 3888
		[CountryDescription("자원아이템", CountryCode.KOR)]
		IMT_RESOURCE,
		// Token: 0x04000F31 RID: 3889
		[CountryDescription("엠블렘", CountryCode.KOR)]
		IMT_EMBLEM,
		// Token: 0x04000F32 RID: 3890
		[CountryDescription("랭킹엠블렘", CountryCode.KOR)]
		IMT_EMBLEM_RANK,
		// Token: 0x04000F33 RID: 3891
		[CountryDescription("뷰 전용 아이템", CountryCode.KOR)]
		IMT_VIEW,
		// Token: 0x04000F34 RID: 3892
		[CountryDescription("유닛 선택권", CountryCode.KOR)]
		IMT_CHOICE_UNIT,
		// Token: 0x04000F35 RID: 3893
		[CountryDescription("함선 선택권", CountryCode.KOR)]
		IMT_CHOICE_SHIP,
		// Token: 0x04000F36 RID: 3894
		[CountryDescription("장비 선택권", CountryCode.KOR)]
		IMT_CHOICE_EQUIP,
		// Token: 0x04000F37 RID: 3895
		[CountryDescription("비품 선택권", CountryCode.KOR)]
		IMT_CHOICE_MISC,
		// Token: 0x04000F38 RID: 3896
		[CountryDescription("장비 금형 선택권", CountryCode.KOR)]
		IMT_CHOICE_MOLD,
		// Token: 0x04000F39 RID: 3897
		[CountryDescription("오퍼레이터 선택권", CountryCode.KOR)]
		IMT_CHOICE_OPERATOR,
		// Token: 0x04000F3A RID: 3898
		[CountryDescription("유닛 조각", CountryCode.KOR)]
		IMT_PIECE,
		// Token: 0x04000F3B RID: 3899
		[CountryDescription("로비 배경화면", CountryCode.KOR)]
		IMT_BACKGROUND,
		// Token: 0x04000F3C RID: 3900
		[CountryDescription("프로필 테두리", CountryCode.KOR)]
		IMT_SELFIE_FRAME,
		// Token: 0x04000F3D RID: 3901
		[CountryDescription("커스텀 패키지", CountryCode.KOR)]
		IMT_CUSTOM_PACKAGE,
		// Token: 0x04000F3E RID: 3902
		[CountryDescription("채용 아이템", CountryCode.KOR)]
		IMT_CONTRACT,
		// Token: 0x04000F3F RID: 3903
		[CountryDescription("가구", CountryCode.KOR)]
		IMT_INTERIOR,
		// Token: 0x04000F40 RID: 3904
		[CountryDescription("가구 선택권", CountryCode.KOR)]
		IMT_CHOICE_FURNITURE,
		// Token: 0x04000F41 RID: 3905
		[CountryDescription("스킨 선택권", CountryCode.KOR)]
		IMT_CHOICE_SKIN
	}
}
