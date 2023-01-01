using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIGenerator
{
    internal static class MidiFiles
    {
        public static bool WriteMidiFile(string filename, MidiFile midi)
        {

            if (File.Exists(filename))
                File.Delete(filename);

            try
            {
                midi.Write(filename);
            }
            catch (Exception ex) 
            {
                Console.Write(ex.Message);
                return false;
            }

            return true;
        }
    }
}
