using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BinaryReplacer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("BinaryReplacer <instructionsFile> <inputFile> <outputFile>");
                return;
            }

            string instructionFileName = args[0];
            string inputFileName = args[1];
            string outputFileName = args[2];

            string[] instructionLines = File.ReadAllLines(instructionFileName);
            byte[] bytes = File.ReadAllBytes(inputFileName);
        
            List<SearchInstruction> instructions = CreateInstructions(instructionLines);
            if (instructions == null || instructions.Count == 0)
            {
                Console.WriteLine("No instructions found!");
                return;
            }

            ProcessInstructions(instructions, bytes);
            File.WriteAllBytes(outputFileName, bytes);
        }

        static private List<SearchInstruction> CreateInstructions(string[] lines)
        {
            InstructionFactory factory = new InstructionFactory();
            List<SearchInstruction> list = factory.Create(lines);
            return list;
        }

        static private void ProcessInstructions(List<SearchInstruction> instructions, byte[] bytes)
        {
            int instrIndex = 0;
            int byteIndex = 0;

            while (instrIndex < instructions.Count)
            {
                SearchInstruction current = instructions[instrIndex];
                Console.WriteLine(current.ToString());
                int newIndex = current.FindMatch(bytes, byteIndex);
                if (newIndex < 0)
                {
                    Console.WriteLine("Failed to find match for instruction " + instrIndex);
                    return;
                }
                if (current is ReplaceInstruction)
                {
                    ReplaceInstruction r = (current as ReplaceInstruction);
                    r.ReplaceAt(bytes, newIndex);
                }
                byteIndex = newIndex + current.MatchLength;
                instrIndex++;
            }

        }
    }
}
