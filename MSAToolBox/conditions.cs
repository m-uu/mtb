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
    
    public partial class conditions
    {
        public int SourceTypeOrReferenceId { get; set; }
        public int SourceGroup { get; set; }
        public int SourceEntry { get; set; }
        public int SourceId { get; set; }
        public int ElseGroup { get; set; }
        public int ConditionTypeOrReference { get; set; }
        public byte ConditionTarget { get; set; }
        public int ConditionValue1 { get; set; }
        public int ConditionValue2 { get; set; }
        public int ConditionValue3 { get; set; }
        public byte NegativeCondition { get; set; }
        public int ErrorType { get; set; }
        public int ErrorTextId { get; set; }
        public string ScriptName { get; set; }
        public string Comment { get; set; }
    }
}