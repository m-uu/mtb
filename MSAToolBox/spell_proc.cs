//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MSAToolBox
{
    using System;
    using System.Collections.Generic;
    
    public partial class spell_proc
    {
        public int spellId { get; set; }
        public sbyte schoolMask { get; set; }
        public int spellFamilyName { get; set; }
        public long spellFamilyMask0 { get; set; }
        public long spellFamilyMask1 { get; set; }
        public long spellFamilyMask2 { get; set; }
        public long typeMask { get; set; }
        public long spellTypeMask { get; set; }
        public int spellPhaseMask { get; set; }
        public int hitMask { get; set; }
        public long attributesMask { get; set; }
        public float ratePerMinute { get; set; }
        public float chance { get; set; }
        public float cooldown { get; set; }
        public long charges { get; set; }
    }
}
