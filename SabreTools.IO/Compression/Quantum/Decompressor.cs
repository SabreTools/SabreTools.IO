using System;
using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Streams;
using static SabreTools.IO.Compression.Quantum.Constants;

namespace SabreTools.IO.Compression.Quantum
{
    /// <see href="www.russotto.net/quantumcomp.html"/>
    public class Decompressor
    {
        /// <summary>
        /// Internal bitstream to use for decompression
        /// </summary>
        private readonly ReadOnlyBitStream _bitStream;

        #region Models

        /// <summary>
        /// Selector 0: literal, 64 entries, starting symbol 0
        /// </summary>
        private readonly Model _model0;

        /// <summary>
        /// Selector 1: literal, 64 entries, starting symbol 64
        /// </summary>
        private readonly Model _model1;

        /// <summary>
        /// Selector 2: literal, 64 entries, starting symbol 128
        /// </summary>
        private readonly Model _model2;

        /// <summary>
        /// Selector 3: literal, 64 entries, starting symbol 192
        /// </summary>
        private readonly Model _model3;

        /// <summary>
        /// Selector 4: LZ, 3 character matches
        /// </summary>
        private readonly Model _model4;

        /// <summary>
        /// Selector 5: LZ, 4 character matches
        /// </summary>
        private readonly Model _model5;

        /// <summary>
        /// Selector 6: LZ, 5+ character matches
        /// </summary>
        private readonly Model _model6;

        /// <summary>
        /// Selector 6 length model
        /// </summary>
        private readonly Model _model6len;

        /// <summary>
        /// Selector selector model
        /// </summary>
        private readonly Model _selector;

        #endregion

        #region Coding State

        /// <summary>
        /// Artihmetic coding state: high
        /// </summary>
        private ushort CS_H;

        /// <summary>
        /// Artihmetic coding state: low
        /// </summary>
        private ushort CS_L;

        /// <summary>
        /// Artihmetic coding state: current
        /// </summary>
        private ushort CS_C;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new Decompressor from a Stream
        /// </summary>
        /// <param name="source">Stream to decompress</param>
        /// <param name="windowBits">Number of bits in the sliding window</param>
        private Decompressor(Stream source, uint windowBits)
        {
            // Validate the inputs
            if (source.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(source));
            if (!source.CanRead)
                throw new InvalidOperationException(nameof(source));
            if (windowBits < 10 || windowBits > 21)
                throw new ArgumentOutOfRangeException(nameof(windowBits));

            // Wrap the stream in a ReadOnlyBitStream
            _bitStream = new ReadOnlyBitStream(source);

            // Initialize literal models
            _model0 = CreateModel(0, 64);
            _model1 = CreateModel(64, 64);
            _model2 = CreateModel(128, 64);
            _model3 = CreateModel(192, 64);

            // Initialize LZ models
            int maxBitLength = (int)(windowBits * 2);
            _model4 = CreateModel(0, maxBitLength > 24 ? 24 : maxBitLength);
            _model5 = CreateModel(0, maxBitLength > 36 ? 36 : maxBitLength);
            _model6 = CreateModel(0, maxBitLength);
            _model6len = CreateModel(0, 27);

            // Initialze the selector model
            _selector = CreateModel(0, 7);

            // Initialize coding state
            CS_H = 0;
            CS_L = 0;
            CS_C = 0;
        }

        /// <summary>
        /// Create a Quantum decompressor
        /// </summary>
        public static Decompressor Create(byte[] source, uint windowBits)
            => Create(new MemoryStream(source), windowBits);

        /// <summary>
        /// Create a Quantum decompressor
        /// </summary>
        public static Decompressor Create(Stream source, uint windowBits)
            => new(source, windowBits);

        #endregion

        /// <summary>
        /// Process the stream and return the decompressed output
        /// </summary>
        /// <returns>Byte array representing the decompressed data, null on error</returns>
        public byte[] Process()
        {
            // Initialize the coding state
            CS_H = 0xffff;
            CS_L = 0x0000;
            CS_C = (ushort)(_bitStream.ReadBitsBE(16) ?? 0);

            // Loop until the end of the stream
            var bytes = new List<byte>();
            while (_bitStream.Position < _bitStream.Length)
            {
                // Determine the selector to use
                int selector = GetSymbol(_selector);

                // Handle literal selectors
                if (selector < 4)
                {
                    switch (selector)
                    {
                        case 0:
                            bytes.Add((byte)GetSymbol(_model0));
                            break;
                        case 1:
                            bytes.Add((byte)GetSymbol(_model1));
                            break;
                        case 2:
                            bytes.Add((byte)GetSymbol(_model2));
                            break;
                        case 3:
                            bytes.Add((byte)GetSymbol(_model3));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                // Handle LZ selectors
                else
                {
                    int offset, length;
                    switch (selector)
                    {
                        case 4:
                            int model4sym = GetSymbol(_model4);
                            int model4extra = (int)(_bitStream.ReadBitsBE(PositionExtraBits[model4sym]) ?? 0);
                            offset = PositionSlot[model4sym] + model4extra + 1;
                            length = 3;
                            break;

                        case 5:
                            int model5sym = GetSymbol(_model5);
                            int model5extra = (int)(_bitStream.ReadBitsBE(PositionExtraBits[model5sym]) ?? 0);
                            offset = PositionSlot[model5sym] + model5extra + 1;
                            length = 4;
                            break;

                        case 6:
                            int lengthSym = GetSymbol(_model6len);
                            int lengthExtra = (int)(_bitStream.ReadBitsBE(LengthExtraBits[lengthSym]) ?? 0);
                            length = LengthSlot[lengthSym] + lengthExtra + 5;

                            int model6sym = GetSymbol(_model6);
                            int model6extra = (int)(_bitStream.ReadBitsBE(PositionExtraBits[model6sym]) ?? 0);
                            offset = PositionSlot[model6sym] + model6extra + 1;
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    // Copy the previous data
                    int copyIndex = bytes.Count - offset;
                    while (length-- > 0)
                    {
                        bytes.Add(bytes[copyIndex++]);
                    }

                    // TODO: Add MS-CAB specific padding
                    // TODO: Add Cinematronics specific checksum
                }
            }

            return [.. bytes];
        }

        /// <summary>
        /// Create and initialize a model base on the start symbol and length
        /// </summary>
        private Model CreateModel(ushort start, int length)
        {
            // Create the model
            var model = new Model
            {
                Entries = length,
                Symbols = new ModelSymbol[length + 1],
                TimeToReorder = 4,
            };

            // Populate the symbol array
            for (int i = 0; i <= length; i++)
            {
                model.Symbols[i] = new ModelSymbol
                {
                    Symbol = (ushort)(start + i),
                    CumulativeFrequency = (ushort)(length - 1),
                };
            }

            return model;
        }

        /// <summary>
        /// Get the next symbol from a model
        /// </summary>
        private int GetSymbol(Model model)
        {
            int freq = GetFrequency(model.Symbols![0]!.CumulativeFrequency);

            int i;
            for (i = 1; i < model.Entries; i++)
            {
                if (model.Symbols[i]!.CumulativeFrequency <= freq)
                    break;
            }

            int sym = model.Symbols![i - 1]!.Symbol;

            GetCode(model.Symbols![i - 1]!.CumulativeFrequency,
                    model.Symbols![i]!.CumulativeFrequency,
                    model.Symbols![0]!.CumulativeFrequency);

            UpdateModel(model, i);

            return sym;
        }

        /// <summary>
        /// Get the next code based on the frequencies
        /// </summary>
        private void GetCode(int prevFrequency, int currentFrequency, int totalFrequency)
        {
            uint range = (ushort)((CS_H - CS_L) + 1);
            CS_H = (ushort)(CS_L + (prevFrequency * range) / totalFrequency - 1);
            CS_L = (ushort)(CS_L + (currentFrequency * range) / totalFrequency);

            while (true)
            {
                if ((CS_L & 0x8000) != (CS_H & 0x8000))
                {
                    if ((CS_L & 0x4000) != 0 && (CS_H & 0x4000) == 0)
                    {
                        // Underflow case
                        CS_C ^= 0x4000;
                        CS_L &= 0x3FFF;
                        CS_H |= 0x4000;
                    }
                    else
                    {
                        break;
                    }
                }

                CS_L <<= 1;
                CS_H = (ushort)((CS_H << 1) | 1);
                CS_C = (ushort)((CS_C << 1) | _bitStream.ReadBit() ?? 0);
            }
        }

        /// <summary>
        /// Update the model after an encode or decode step
        /// </summary>
        private void UpdateModel(Model model, int lastUpdated)
        {
            // Update cumulative frequencies
            for (int i = 0; i < lastUpdated; i++)
            {
                var sym = model.Symbols![i]!;
                sym.CumulativeFrequency += 8;
            }

            // Decrement reordering time, if needed
            if (model.Symbols![0]!.CumulativeFrequency > 3800)
                model.TimeToReorder--;

            // If we haven't hit the reordering time
            if (model.TimeToReorder > 0)
            {
                // Update the cumulative frequencies
                for (int i = model.Entries - 1; i >= 0; i--)
                {
                    // Divide with truncation by 2
                    var sym = model.Symbols![i]!;
                    sym.CumulativeFrequency >>= 1;

                    // If we are lower the next frequency
                    if (i != 0 && sym.CumulativeFrequency <= model.Symbols![i + 1]!.CumulativeFrequency)
                        sym.CumulativeFrequency = (ushort)(model.Symbols![i + 1]!.CumulativeFrequency + 1);
                }
            }

            // If we hit the reordering time
            else
            {
                // Calculate frequencies from cumulative frequencies
                for (int i = 0; i < model.Entries; i++)
                {
                    if (i != model.Entries - 1)
                        model.Symbols![i]!.CumulativeFrequency -= model.Symbols![i + 1]!.CumulativeFrequency;

                    model.Symbols![i]!.CumulativeFrequency++;
                    model.Symbols![i]!.CumulativeFrequency >>= 1;
                }

                // Sort frequencies in decreasing order
                for (int i = 0; i < model.Entries; i++)
                {
                    for (int j = i + 1; j < model.Entries; j++)
                    {
                        if (model.Symbols![i]!.CumulativeFrequency < model.Symbols![j]!.CumulativeFrequency)
                        {
#if NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                            (model.Symbols[j], model.Symbols[i]) = (model.Symbols[i], model.Symbols[j]);
#else
                            var temp = model.Symbols[i];
                            model.Symbols[i] = model.Symbols[j];
                            model.Symbols[j] = temp;
#endif
                        }
                    }
                }

                // Calculate cumulative frequencies from frequencies
                for (int i = model.Entries - 1; i >= 0; i--)
                {
                    if (i != model.Entries - 1)
                        model.Symbols![i]!.CumulativeFrequency += model.Symbols![i + 1]!.CumulativeFrequency;
                }

                // Reset the time to reorder
                model.TimeToReorder = 50;
            }
        }

        /// <summary>
        /// Get the frequency of a symbol based on its total frequency
        /// </summary>
        private ushort GetFrequency(ushort totalFrequency)
        {
            ulong range = (ulong)(((CS_H - CS_L) & 0xFFFF) + 1);
            ulong frequency = (ulong)(((CS_C - CS_L + 1) * totalFrequency) - 1) / range;
            return (ushort)(frequency & 0xFFFF);
        }
    }
}
