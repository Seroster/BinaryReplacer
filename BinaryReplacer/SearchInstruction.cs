using System;

namespace BinaryReplacer
{
    public class SearchInstruction
    {
        public int MatchLength
        {
            get
            {
                return this.searchField.Length;
            }
        }
        protected string searchField;

        public SearchInstruction(string text)
        {
            this.searchField = text;
        }

        public override string ToString()
        {
            return "Search " + searchField;
        }

        public int FindMatch(byte[] bytes, int byteIndex)
        {
            // search starting at 'byteIndex' to find a match for 'searchField'
            int searchFieldIndex = 0;
            while (byteIndex < bytes.Length)
            {
                if (this.searchField[searchFieldIndex] == bytes[byteIndex])
                {
                    searchFieldIndex++;
                    if (searchFieldIndex == this.searchField.Length)
                    {
                        // We have a match. return value pointing to start of match
                        return (byteIndex - this.searchField.Length + 1);
                    }
                }
                else if (searchFieldIndex > 0)
                {
                    searchFieldIndex = 0;
                    // We recheck starting from the beginning of the search field
                    continue;
                }
                
                byteIndex++;
            }

            return -1;
        }
    }
}