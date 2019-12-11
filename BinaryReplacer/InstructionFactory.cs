using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryReplacer
{
    class InstructionFactory
    {
        public List<SearchInstruction> Create(string[] lines)
        {
            List<SearchInstruction> list = new List<SearchInstruction>();

            for (int i = 0; i < lines.Length; ++i)
            {
                SearchInstruction inst = CreateInstruction(lines[i], i);
                if (inst == null)
                {
                    continue;
                }
                list.Add(inst);
            }

            return list;
        }

        private SearchInstruction CreateInstruction(string line, int lineNumber)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }

            string[] tokens = line.Split('/');
            // we need at least two tokens
            if (tokens.Length < 2)
            {
                Console.WriteLine("Instruction " + lineNumber + " is missing anything to search with");
                return null;
            }
            
            if (tokens[0] == "find")
            {
                return CreateSearchInstruction(tokens, lineNumber);
            }
            else if (tokens[0] == "replace")
            {
                return CreateReplaceInstruction(tokens, lineNumber);
            }
            
            return null;
        }

        private SearchInstruction CreateSearchInstruction(string[] tokens, int lineNumber)
        {
            SearchInstruction s = new SearchInstruction(tokens[1]);
            return s;
        }

        private ReplaceInstruction CreateReplaceInstruction(string[] tokens, int lineNumber)
        {
            // we need THREE tokens
            if (tokens.Length < 3)
            {
                Console.WriteLine("Instruction " + lineNumber + " is 'replace' but is missing anything to replace with");
                return null;
            }
            if (tokens[1].Length != tokens[2].Length)
            {
                Console.WriteLine("Instruction " + lineNumber + " has key and replacement of different lengths: " + tokens[1] + " vs " + tokens[2]);
                return null;
            }
            ReplaceInstruction r = new ReplaceInstruction(tokens[1], tokens[2]);
            return r;
        }
    }
}
