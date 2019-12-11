using System;

namespace BinaryReplacer
{
    public class ReplaceInstruction : SearchInstruction
    {
        protected string replaceField;

        public ReplaceInstruction(string search, string replace) : base(search)
        {
            this.replaceField = replace;
        }

        public override string ToString()
        {
            return "Replace " + this.searchField + "/" + this.replaceField;
        }

        public void ReplaceAt(byte[] bytes, int byteIndex)
        {
            for (int i = 0; i < this.replaceField.Length; ++i)
            {
                bytes[byteIndex + i] = (byte)this.replaceField[i];
            }
        }
    }
}