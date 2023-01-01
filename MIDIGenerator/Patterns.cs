using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIGenerator
{
    internal static class Patterns
    {
        public static Pattern GetRandomPattern(int noteLength, int octave, bool chords)
        {
            // 4 bars
            int patternLength = noteLength * 4;
            PatternBuilder pattern = new PatternBuilder();
            pattern.SetOctave(Octave.Get(octave));

            switch (noteLength)
            {
                case 1:
                    pattern.SetNoteLength(MusicalTimeSpan.Whole);
                    break;
                case 2:
                    pattern.SetNoteLength(MusicalTimeSpan.Half);
                    break;
                case 4:
                    pattern.SetNoteLength(MusicalTimeSpan.Quarter);
                    break;
                case 8:
                    pattern.SetNoteLength(MusicalTimeSpan.Eighth);
                    break;
                default:
                    pattern.SetNoteLength(MusicalTimeSpan.Quarter);
                    break;
            }

            if (chords)
                return getChordPattern(pattern, patternLength);
            else
                return getNotePattern(pattern, patternLength);
        }

        private static Pattern getNotePattern(PatternBuilder builder, int patternLength)
        {
            Random rand = new();

            for (int i = 0; i < patternLength; i++)
            {
                var note = rand.Next(1,8);

                switch (note)
                {
                    case 1:
                        builder.Note(NoteName.C);
                        break;
                    case 2:
                        builder.Note(NoteName.D);
                        break;
                    case 3:
                        builder.Note(NoteName.DSharp);
                        break;
                    case 4:
                        builder.Note(NoteName.F);
                        break;
                    case 5:
                        builder.Note(NoteName.G);
                        break;
                    case 6:
                        builder.Note(NoteName.A);
                        break;
                    case 7:
                        builder.Note(NoteName.ASharp);
                        break;
                    default:
                        builder.Note(NoteName.C);
                        break;
                }
            }
            return builder.Build();
        }

        private static Pattern getChordPattern(PatternBuilder builder, int patternLength)
        {
            Random rand = new();

            for (int i = 0; i < patternLength; i++)
            {
                var note = rand.Next(1, 8);

                switch (note)
                {
                    case 1:
                        builder.Chord("Cm");
                        break;
                    case 2:
                        builder.Chord("D");
                        break;
                    case 3:
                        builder.Chord("E");
                        break;
                    case 4:
                        builder.Chord("F");
                        break;
                    case 5:
                        builder.Chord("Gm");
                        break;
                    case 6:
                        builder.Chord("A");
                        break;
                    case 7:
                        builder.Chord("Bm");
                        break;
                    default:
                        builder.Chord("Cm");
                        break;
                }
            }
            return builder.Build();
        }
    }
}
