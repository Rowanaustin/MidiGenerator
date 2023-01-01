using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using MIDIGenerator;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Generating MIDI files...");

        Pattern bassPattern = Patterns.GetRandomPattern(2,3,false);
        Pattern melodyPattern = Patterns.GetRandomPattern(4,5,false);
        Pattern chordPattern = Patterns.GetRandomPattern(1,4,true);

        MidiFile bassMidi = bassPattern.ToFile(TempoMap.Default);
        MidiFile melodyMidi = melodyPattern.ToFile(TempoMap.Default);
        MidiFile chordMidi = chordPattern.ToFile(TempoMap.Default);

        MidiFiles.WriteMidiFile("bass.mid", bassMidi);
        MidiFiles.WriteMidiFile("melody.mid", melodyMidi);
        MidiFiles.WriteMidiFile("chords.mid", chordMidi);
    }
}