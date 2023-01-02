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
        public static Pattern GetRandomPattern(bool chords, int baseNoteLength, int weightStrength, int octave, MusicalTimeSpan patternLength)
        {
            PatternBuilder pattern = new();
            pattern.SetOctave(Octave.Get(octave));

            switch (baseNoteLength)
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

                return GetPattern(pattern, chords, patternLength, weightStrength);
        }

        private static Pattern GetPattern(PatternBuilder builder, bool chords, MusicalTimeSpan patternLength, int noteLengthStrength)
        { 
            MusicalTimeSpan baseNoteLength = (MusicalTimeSpan)builder.NoteLength;
            var nextNoteLength = GetRandomNoteLength(baseNoteLength, noteLengthStrength);
            var remainingPatternLength = patternLength;
            var completed = false;

            var counter = 0;
            while (!completed)
            {
                Console.WriteLine("Remaining pattern length " + remainingPatternLength + " is " + remainingPatternLength.CompareTo(nextNoteLength) + " compared to " + "next note length " + nextNoteLength);

                if (remainingPatternLength.CompareTo(nextNoteLength) == 0)
                {
                    Console.WriteLine("Completing as equal");
                    completed = true;
                }

                else if (remainingPatternLength.CompareTo(nextNoteLength) < 0)
                {
                    Console.WriteLine("Completing as changed to equal:");
                    Console.WriteLine("NextNoteLength changing from " + nextNoteLength);

                    nextNoteLength = remainingPatternLength;

                    Console.WriteLine("...to " + nextNoteLength);
                    Console.WriteLine("Remaining pattern length " + remainingPatternLength + " is " + remainingPatternLength.CompareTo(nextNoteLength) + " compared to " + "next note length " + nextNoteLength);


                    completed = true;
                }

                else
                {
                    if (remainingPatternLength.CompareTo(nextNoteLength) > 0)
                        remainingPatternLength = (MusicalTimeSpan)remainingPatternLength.Subtract(nextNoteLength, TimeSpanMode.LengthLength);
                }

                builder.SetNoteLength(nextNoteLength);

                if (chords)
                {
                    builder.Chord(GetRandomScaleChord());
                    Console.WriteLine("Writing chord with " + builder.NoteLength + " length");
                }
                else
                {
                    builder.Note(GetRandomScaleNote());
                    Console.WriteLine("Writing note with " + builder.NoteLength + " length");
                }

                counter++;
                if (!completed)
                {
                    //Console.WriteLine("NextNoteLength changing from " + nextNoteLength);
                    nextNoteLength = GetRandomNoteLength(baseNoteLength, noteLengthStrength);
                    //Console.WriteLine("...to " + nextNoteLength);
                }

            }

            //Console.WriteLine(remainingPatternLength + " is " + remainingPatternLength.CompareTo(nextNoteLength) + " compared to " + nextNoteLength);

            Console.WriteLine("End of Loop - Wrote " + counter + " notes");

            return builder.Build();
        }

        private static MusicalTimeSpan GetRandomNoteLength(MusicalTimeSpan weightedLength, int lengthWeight)
        {
            Random rand = new();
            var segment = 20;
            MusicalTimeSpan length = weightedLength;

            var val = rand.Next(0, 100);
            bool modify = val > (segment * lengthWeight);
            //Console.WriteLine("Val is " + val + " and weighting needs over " + (segment * lengthWeight));

            if (modify)
            {
                var amount = rand.Next(0, 99);

                if (amount < 10)
                    ModifyTimeSpan(length, -1);
                else if (amount < 35)
                    ModifyTimeSpan(length, 1);
                else if (amount < 60)
                    ModifyTimeSpan(length, 2);
                else if(amount< 85)
                    ModifyTimeSpan(length, 3);
                else
                    ModifyTimeSpan(length, 4);
            }

            return length;
        }

        private static string GetRandomScaleChord()
        {
            Random rand = new();
            var note = rand.Next(1, 8);

            return note switch
            {
                1 => "C",
                2 => "Bm",
                3 => "Em",
                4 => "F",
                5 => "G",
                6 => "Am",
                7 => "Bdim",
                _ => "C",
            };
        }

        private static NoteName GetRandomScaleNote()
        {
            Random rand = new();
            var note = rand.Next(1, 8);

            return note switch
            {
                1 => NoteName.C,
                2 => NoteName.D,
                3 => NoteName.E,
                4 => NoteName.F,
                5 => NoteName.G,
                6 => NoteName.A,
                7 => NoteName.B,
                _ => NoteName.C,
            };
        }

        private static MusicalTimeSpan ModifyTimeSpan(MusicalTimeSpan baseSpan, int shift)
        {
            if (shift == 0)
                return baseSpan;
            if (shift<0)
                return (MusicalTimeSpan)baseSpan.Divide(Math.Abs(shift) * 2);
            else
                return (MusicalTimeSpan)baseSpan.Multiply(shift * 2);
        }
    }
}
