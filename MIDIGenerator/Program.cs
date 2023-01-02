using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Tools;
using MIDIGenerator;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Generating MIDI files...");

        var fourBars = (MusicalTimeSpan)MusicalTimeSpan.Whole.Multiply(8);
        var eightBars = (MusicalTimeSpan)MusicalTimeSpan.Whole.Multiply(16);

        MidiFile bassPattern = Patterns.GetRandomPattern(false, 2, 2, 3, fourBars).ToFile(TempoMap.Default);
        MidiFile melodyPattern = Patterns.GetRandomPattern(false, 4, 1, 5, fourBars).ToFile(TempoMap.Default);
        MidiFile chordPattern = Patterns.GetRandomPattern(true, 2, 3, 4, fourBars).ToFile(TempoMap.Default);

        Repeater repeater= new();

        MidiFile bassMidi = repeater.Repeat(bassPattern, 2);
        MidiFile melodyMidi = repeater.Repeat(melodyPattern, 2);
        MidiFile chordMidi = repeater.Repeat(chordPattern, 2);

        MidiFiles.WriteMidiFile("bass.mid", bassMidi);
        MidiFiles.WriteMidiFile("melody.mid", melodyMidi);
        MidiFiles.WriteMidiFile("chords.mid", chordMidi);
    }
}